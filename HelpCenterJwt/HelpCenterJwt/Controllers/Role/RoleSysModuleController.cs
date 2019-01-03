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
    public class RoleSysModuleController : ControllerBase
    {
        /// <summary>
        /// 更新指定的系统模块角色关系信息，每次提交最新的所有权限列表，留空为清空所有权限
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <param name="roleSysModuleViewModel">系统模块角色对应关系视图</param>
        // DELETE: api/UserRole/5
        [HttpPost]
        public IActionResult Post(string roleId, ViewModel.RoleSysModuleViewModel roleSysModuleViewModel)
        {
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {
                    bool isUsrRoleUpdate = HelpCenter.BLL.RoleSysModule.Set(roleId, roleSysModuleViewModel.RoleSysModuleState, this.User.Identities.First(u => u.IsAuthenticated).FindFirst("UsrId").Value, roleSysModuleViewModel.SysModuleIdList);
                    return !isUsrRoleUpdate
                        ? Ok(new { result = false, tips = ResponseMessageTips.MSG_ROLE_UPDATE_FAIL })
                        : Ok(new { result = true, tips = ResponseMessageTips.MSG_ROLE_UPDATE_SUCCESS });
                }
                catch (Exception e)
                {
                    return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_EXCEPTION + e.Message.ToString() });
                }
            }
            return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_DATA_FORMAT_ERROR });
        }

        /// <summary>
        /// 获取指定的角色ID与系统模块关系
        /// </summary>
        /// <param name="roleId">角色ID</param>
        [HttpGet("roleid")]
        public IActionResult GetModelListByRoleId(string roleId)
        {
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {
                    var result = HelpCenter.BLL.RoleSysModule.GetModelListByRoleId(roleId);
                    return Ok(new { result = true, data = result });
                }
                catch (Exception e)
                {
                    return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_EXCEPTION + e.Message.ToString() });
                }
            }
            return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_DATA_FORMAT_ERROR });
        }


        /// <summary>
        /// 获取指定用户ID与系统模块关系，留空为获取当前用户ID
        /// </summary>
        /// <param name="userId">用户ID</param>
        [HttpGet("userid")]
        public IActionResult GetModelListByUserId(string userId)
        {
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {
                    if (string.IsNullOrEmpty(userId))
                    {
                        userId = User.Identities.First(u => u.IsAuthenticated).FindFirst("UsrId").Value;
                    }
                    var result = HelpCenter.BLL.RoleSysModule.GetModelListByUserId(userId);
                    return Ok(new { result = true, data = result });
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