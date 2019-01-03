using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelpCenter.ActionService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HelpCenterJwt.Controllers.Role
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocRoleCtrlController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post(HelpCenter.Model.DocRoleCtrl docRoleCtrl)
        {
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {
                    bool isSuccess = HelpCenter.BLL.DocRoleCtrl.Save();
                    return !isSuccess
                        ? Ok(new { result = false, tips = ResponseMessageTips.MSG_DOCUSRROLE_UPDATE_FAIL })
                        : Ok(new { result = true, tips = ResponseMessageTips.MSG_DOCUSRROLE_UPDATE_SUCCESS });
                }
                catch (Exception e)
                {
                    return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_EXCEPTION + e.Message.ToString() });
                }
            }
            return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_DATA_FORMAT_ERROR });
        }
    }
}