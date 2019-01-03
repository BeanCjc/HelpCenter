using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HelpCenter.ActionService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;

namespace HelpCenterJwt.Controllers.File
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public FileController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;//依赖注入
        }

        /// <summary>
        /// 文件上传，如果上传失败，可以尝试将文件上传标签的name属性值更改为“fileinput”
        /// </summary>
        /// <param name="fileinput">选择文件</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult File(IFormFile fileinput)
        {
            try
            {
                foreach (IFormFile file in Request.Form.Files)
                {
                    var fileName = DateTime.Now.Ticks + "_" + file.FileName;
                    var webroot = _hostingEnvironment.WebRootPath + @"\upload\" + DateTime.Now.ToString("yyyy-MM-dd") + @"\";
                    var filePath = @"\upload\" + DateTime.Now.ToString("yyyy-MM-dd") + @"\" +  fileName;
                    fileName = webroot + $@"{fileName}";
                    if (!Directory.Exists(webroot))
                    {
                        Directory.CreateDirectory(webroot);
                    }

                    using (FileStream fs = System.IO.File.Create(fileName))
                    {
                        // 复制文件
                        file.CopyTo(fs);
                        // 清空缓冲区数据
                        fs.Flush();
                        fs.Dispose();
                    }
                    return Ok(new { result = true, tips = ResponseMessageTips.MSG_FILE_UPLOAD_SUCCESS, data = filePath });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { result = false, tips = ResponseMessageTips.MSG_FILE_UPLOAD_FAIL+ex.Message });
            }
            return Ok(new { result = false, tips = ResponseMessageTips.MSG_FILE_UPLOAD_FAIL });
        }

    }
}