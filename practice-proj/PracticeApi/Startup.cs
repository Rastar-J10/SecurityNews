using Autofac;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Practice;
using Practice.Common.Const;
using Practice.MapperProfiles;
using PracticeApi.Extensions.Autofac;
using PracticeApi.Extensions.Cors;
using PracticeApi.Extensions.Newtonsoft;
using PracticeApi.Extensions.Swagger;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace PracticeApi
{
    /// <summary>
    /// 启动配置类
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// constructor
        /// </summary>
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        /// <summary>
        /// 应用程序配置
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// environment
        /// </summary>
        public IWebHostEnvironment Environment { get; set; }


        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            _ = services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;
                    options.SerializerSettings.Culture = new CultureInfo("zh-CN");
                    options.SerializerSettings.Formatting = Formatting.Indented;
                    // 忽略null值
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    // 驼峰命名
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    // 日期格式化
                    options.SerializerSettings.Converters.Add(new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss.fff" });
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                    options.SerializerSettings.Converters.Add(new LongStringConverter());
                });

            _ = services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });
            _ = services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VVV");

            services.AddAuthentication(options =>
            {
                //认证middleware配置
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                //主要是jwt  token参数设置
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    //Token颁发机构
                    ValidIssuer = TokenConst.Issuer,
                    //颁发给谁
                    ValidAudience = TokenConst.Audience,
                    //这里的key要进行加密，需要引用Microsoft.IdentityModel.Tokens
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenConst.SecurityKey)),

                    ValidateIssuerSigningKey = true,
                    //是否验证Token有效期，使用当前时间与Token的Claims中的NotBefore和Expires对比
                    ValidateLifetime = true
                    ////允许的服务器时间偏移量
                    //ClockSkew=TimeSpan.Zero
                };
            });

            // Swagger支持
            _ = services.AddSingleton<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            _ = services.AddSwaggerGen(options =>
            {
                foreach (var file in Directory.GetFiles(AppContext.BaseDirectory, "*.xml"))
                {
                    options.IncludeXmlComments(file);
                }

                options.OperationFilter<AssignOperationVendorExtensions>();
                var openApiSecurityScheme = new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };
                options.AddSecurityDefinition("Bearer", openApiSecurityScheme);
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        { openApiSecurityScheme, new List<string>() }
                    });
            });
            //支持swagger自定义的
            _ = services.Replace(ServiceDescriptor.Transient<ISwaggerProvider, CustomSwaggerGenerator>());

            //var corsOrigins = new string[] { "http://practice.com" };
            //_ = services.AddCors(options =>
            //    options.AddPolicy("Practice.CORS",
            //        p =>
            //        {
            //            if (corsOrigins?.Any() ?? false)
            //            {
            //                _ = p.WithOrigins(corsOrigins)
            //                    .AllowAnyMethod()
            //                    .AllowAnyHeader()
            //                    .AllowCredentials()
            //                    .Build();
            //            }

            //            var policy = p.Build();
            //            _ = policy.AllowedToAllowWildcardSubdomains(Environment.IsDevelopment());
            //        }
            //    )
            //);
            _ = services.AddCors(options =>
            {
                options.AddPolicy("Practice.CORS",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }

        /// <summary>
        /// 依赖注入
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            _ = builder.RegisterModule<ServicesModule>();
            _ = builder.RegisterModule<RepositoriesModule>();
            _ = builder.RegisterModule(new DtoEntityMapperModule(typeof(CommonProfile).Assembly));
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                _ = app.UseDeveloperExceptionPage();
            }

            _ = app.UseHttpsRedirection();
            _ = app.UseRouting();
            _ = app.UseAuthentication();
            _ = app.UseAuthorization();
            _ = app.UseCors("Practice.CORS");
            _ = app.UseEndpoints(endpoints => endpoints.MapControllers());

            _ = app.UseSwagger()
                .UseSwaggerUI(options =>
                {
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint(
                            $"/swagger/{description.GroupName}/swagger.json",
                            description.GroupName.ToUpperInvariant());
                    }
                });
        }
    }
}
