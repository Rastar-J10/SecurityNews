using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using log4net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Practice.ResponseModels;
using PracticeApi.Extensions.Base;

namespace PracticeApi.Controllers
{
    /// <summary>
    /// 图片上传接口
    /// </summary>
    [Route("api/v{version:apiVersion}/FileUpload")]
    [ApiVersion("1.0")]
    public class FileUploadController : BaseApiController
    {
        /// <summary>
        /// 路径获取
        /// </summary>
        private readonly IWebHostEnvironment _env;
        /// <summary>
        /// 日志记录
        /// </summary>
        private readonly static ILog _log = LogManager.GetLogger(typeof(FileUploadController));
        /// <summary>
        /// 构造注入
        /// </summary>
        /// <param name="webHostEnvironment"></param>
        public FileUploadController(IWebHostEnvironment webHostEnvironment)
        {
            _env = webHostEnvironment;
        }

        /// <summary>
        /// 图片上传
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("ImageUpload")]
        public async Task<ResModel<string>> ImageUpload(IFormFile file)
        {
            try
            {
                //为空
                if (file.Length == 0)
                {
                    return ResModel.Failure<string>("上传的文件不能为空，请重新上传");
                }
                string thePath;
                //图片大小限制,不超过5M
                if (file.Length >= 5242880)
                {
                    return ResModel.Failure<string>("操作失败，上传的图片不能大于5M，请重新上传");
                }
                //提取上传的文件文件后缀
                var suffix = Path.GetExtension(file.FileName);
                string FileTypes = ".jpg,.png,.JPG,.PNG";
                //是否为图片
                if (FileTypes.IndexOf(suffix) < 0)
                {
                    return ResModel.Failure<string>("操作失败，只能上传图片，请重新上传");
                }
                var path = _env.ContentRootPath + @"/Uploads/Images/";//获取项目的根目录的绝对路径
                string dirPath = Path.Combine(path + "/");//绝对路径，储存文件路径的文件夹
                if (!Directory.Exists(dirPath))//查看文件夹是否存在
                {
                    Directory.CreateDirectory(dirPath);
                }
                var fileNam = $"{Guid.NewGuid():N}_{file.FileName}";//新文件名
                string imgPath = $"{dirPath + fileNam}";//储存文件路径
                using var stream = new FileStream(imgPath, FileMode.Create);//文件流
                await file.CopyToAsync(stream);//将上传的文件文件流，复制到fs中
                stream.Flush();//清空文件流
                return ResModel.Success(imgPath, "上传图片成功，新的文件路径已返回");
            }
            catch (Exception ex)
            {
                _log.Error($"操作失败，请查看错误信息", ex);
                return ResModel.Failure<string>("上传失败，请稍后重试");
            }
        }

        /// <summary>
        /// 媒体文件上传
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("MediaUpload")]
        public async Task<ResModel<string>> MediaUpload(IFormFile file)
        {
            try
            {
                //为空
                if (file.Length == 0)
                {
                    return ResModel.Failure<string>("上传的文件不能为空，请重新上传");
                }
                string thePath;
                //图片大小限制,不超过5M
                if (file.Length >= 10485760)
                {
                    return ResModel.Failure<string>("操作失败，上传的媒体文件不能大于10M，请重新上传");
                }
                //提取上传的文件文件后缀
                var suffix = Path.GetExtension(file.FileName);
                string FileTypes = ".avi,.mp4,.mpg,.mpeg,.mp3,.wav";
                //是否为视频或音频
                if (FileTypes.IndexOf(suffix) < 0)
                {
                    return ResModel.Failure<string>("操作失败，只能上传视频或音频，请重新上传");
                }
                var path = _env.ContentRootPath + @"/Uploads/Medias/";//获取项目的根目录的绝对路径
                string dirPath = Path.Combine(path + "/");//绝对路径，储存文件路径的文件夹
                if (!Directory.Exists(dirPath))//查看文件夹是否存在
                {
                    Directory.CreateDirectory(dirPath);
                }
                var fileNam = $"{Guid.NewGuid():N}_{file.FileName}";//新文件名
                string filePath = $"{dirPath + fileNam}";//储存文件路径
                using var stream = new FileStream(filePath, FileMode.Create);//文件流
                await file.CopyToAsync(stream);//将上传的文件文件流，复制到fs中
                stream.Flush();//清空文件流
                return ResModel.Success(filePath, "上传文件成功，新的文件路径已返回");
            }
            catch (Exception ex)
            {
                _log.Error($"操作失败，请查看错误信息", ex);
                return ResModel.Failure<string>("上传失败，请稍后重试");
            }
        }
    }
}
