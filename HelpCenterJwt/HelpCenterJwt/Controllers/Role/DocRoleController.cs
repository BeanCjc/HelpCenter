using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelpCenter.ActionService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HelpCenterJwt.Controllers.Role
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DocRoleController : ControllerBase
    {
        /// <summary>
        /// 更新指定的文档角色关系信息，每次提交最新的所有权限列表，留空为清空所有权限
        /// </summary>
        /// <param name="docRoleViewModel">文档角色对应关系视图</param>
        // DELETE: api/UserRole/5
        [HttpPost]
        public IActionResult Post(ViewModel.DocRoleViewModel docRoleViewModel)
        {
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {
                    bool isUsrRoleDel = HelpCenter.BLL.DocUsrRole.Set(Guid.NewGuid().ToString(), 1, User.Identities.First(u => u.IsAuthenticated).FindFirst("UsrId").Value, User.Identities.First(u => u.IsAuthenticated).FindFirst("UsrId").Value, docRoleViewModel.DocIdList, docRoleViewModel.RoleId);
                    return !isUsrRoleDel
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
