using Autofac;
using Autofac.AttributeExtensions;
using MySql.Data.MySqlClient;
using Practice.Common.Const;
using System.Data;

namespace Practice
{
    /// <summary>
    /// 仓储模块注入
    /// </summary>
    public class RepositoriesModule : Module
    {
        /// <inheritdoc/>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAttributedClasses(ThisAssembly);

            builder.Register(ctx => new MySqlConnection("Server=192.168.50.182;Port=3306;Database=practice;Uid=practice_test;Pwd=X3K$!SPK;charset=utf8mb4;pooling=true;AllowUserVariables=True;"))
                .AsImplementedInterfaces()
                .AsSelf()
                .InstancePerLifetimeScope()
                .Keyed<IDbConnection>(DbConst.TestMySqlDb);
        }
    }
}
