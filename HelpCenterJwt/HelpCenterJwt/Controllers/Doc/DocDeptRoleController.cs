using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelpCenter.ActionService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HelpCenterJwt.Controllers.Doc
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocDeptRoleController : ControllerBase
    {
        /// <summary>
        /// 更新指定的文档部门关系信息，每次提交最新的所有权限列表，留空为清空所有权限
        /// </summary>
        /// <param name="strDocId">文档ID</param>
        /// <param name="deptIdList">共享部门ID列表</param>
        // DELETE: api/UserRole/5
        [Authorize]
        [HttpPost]
        public IActionResult Post(string strDocId, List<string> deptIdList)
        {
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {
                    bool isUsrRoleUpdate = HelpCenter.BLL.DocDeptRole.Set(Guid.NewGuid().ToString(), strDocId, deptIdList, 1, User.Identities.First(u => u.IsAuthenticated).FindFirst("UsrId").Value);
                    return !isUsrRoleUpdate
                        ? Ok(new { result = false, tips = ResponseMessageTips.MSG_DOC_UPDATE_FAIL })
                        : Ok(new { result = true, tips = ResponseMessageTips.MSG_DOC_UPDATE_SUCCESS });
                }
                catch (Exception e)
                {
                    return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_EXCEPTION + e.Message.ToString() });
                }
            }
            return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_DATA_FORMAT_ERROR });
        }

        /// <summary>
        /// 查询文档对应的部门共享关系
        /// </summary>
        /// <param name="docId">文档ID</param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public IActionResult GetDocDeptRoleModelList(string docId)
        {
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {
                    var shareDeptList = HelpCenter.BLL.DocDeptRole.GetDocDeptRoleModelList(docId);
                    return null == shareDeptList
                        ? Ok(new { result = false, tips = ResponseMessageTips.MSG_DOC_INFO_NO_FOUND })
                        : Ok(new { result = true, tips = ResponseMessageTips.MSG_PROCESS_SUCCESS, data = shareDeptList });
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