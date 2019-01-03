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
    public class UserRoleController : ControllerBase
    {
        /// <summary>
        /// 获取所有用户角色关系清单（分页），提供指定页的数据、总数据量及总页数
        /// </summary>
        /// <param name="pageIndex">页码，从1开始</param>
        /// <param name="rowCount">每页数据量</param>
        /// <returns></returns>
        // GET: api/UserRole
        [HttpGet("pagelist")]
        public IActionResult GetUserRoleListByPage(int pageIndex, int rowCount)
        {
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {
                    if (pageIndex > 0 && rowCount > 0)
                    {
                        int _totalCount, _pageCount = 0;
                        var userRoleList = HelpCenter.BLL.UserRole.GetList(pageIndex, rowCount, out _totalCount, out _pageCount);

                        return null == userRoleList
                            ? Ok(new { result = false, tips = ResponseMessageTips.MSG_USRROLE_UPDATE_FAIL })
                            : Ok(new
                            {
                                result = true,
                                tips = ResponseMessageTips.MSG_PROCESS_SUCCESS,
                                data = new { totalCount = _totalCount, pageCount = _pageCount, info = userRoleList }
                            });
                    }
                }
                catch (Exception e)
                {
                    return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_EXCEPTION + e.Message.ToString() });
                }
            }

            return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_DATA_FORMAT_ERROR });
        }

        /// <summary>
        /// 根据用户ID获取用户角色关系详细信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        // GET: api/UserRole/5
        [HttpGet]
        public IActionResult Get(string userId)
        {
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {
                    var userRoleInfo = HelpCenter.BLL.UserRole.GetModelByUserId(userId);
                    return null == userRoleInfo
                        ? Ok(new { result = true, tips = ResponseMessageTips.MSG_USRROLE_NO_FOUND })
                        : Ok(new
                        {
                            result = true,
                            tips = ResponseMessageTips.MSG_PROCESS_SUCCESS,
                            data = userRoleInfo
                        });
                }
                catch (Exception e)
                {
                    return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_EXCEPTION + e.Message.ToString() });
                }
            }

            return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_DATA_FORMAT_ERROR });
        }

        /// <summary>
        /// 更改用户角色关系信息
        /// </summary>
        /// <param name="userRoleViewModel"></param>
        /// <returns></returns>
        // POST: api/UserRole
        [HttpPost]
        public IActionResult Post([FromBody] ViewModel.UserRoleViewModel userRoleViewModel)
        {
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {
                    bool isUserRoleAdd = HelpCenter.BLL.UserRole.Set(Guid.NewGuid().ToString(), userRoleViewModel.UserId, userRoleViewModel.RoleIdList, 1,
                        User.Identities.First(u => u.IsAuthenticated).FindFirst("UsrId").Value);
                    /*判断是否新增成功*/
                    return !isUserRoleAdd
                        ? Ok(new { result = false, tips = ResponseMessageTips.MSG_USRROLE_ADD_FAIL })
                        : Ok(new { result = true, tips = ResponseMessageTips.MSG_USRROLE_ADD_SUCCESS });
                }
                catch (Exception e)
                {
                    return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_EXCEPTION + e.Message.ToString() });
                }
            }

            return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_DATA_FORMAT_ERROR });
        }

        ///// <summary>
        ///// 更新指定用户角色关系信息
        ///// </summary>
        ///// <param name="userRoleId">指定用户角色关系ID</param>
        ///// <param name="userRoleViewModel"></param>
        //// PUT: api/UserRole/5
        //[HttpPut("{userRoleId}")]
        //public IActionResult Put(string userRoleId, [FromBody] ViewModel.UserRoleViewModel userRoleViewModel)
        //{
        //    /*判断是否合法*/
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            string strUserRoleInfo = string.Empty;
        //            bool isDeptUpdate = HelpCenter.BLL.UserRole.Update(userRoleId, userRoleViewModel.UserId, userRoleViewModel.RoleIdList, 2, string.Empty);
        //            /*判断是否新增成功*/
        //            return !isDeptUpdate
        //                ? Ok(new { result = false, tips = ResponseMessageTips.MSG_USRROLE_UPDATE_FAIL })
        //                : Ok(new { result = true, tips = ResponseMessageTips.MSG_USRROLE_UPDATE_SUCCESS });
        //        }
        //        catch (Exception e)
        //        {
        //            return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_EXCEPTION + e.Message.ToString() });
        //        }
        //    }

        //    return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_DATA_FORMAT_ERROR });
        //}

        /// <summary>
        /// 删除指定的用户角色关系信息
        /// </summary>
        /// <param name="userId">用户角色关系ID</param>
        // DELETE: api/UserRole/5
        [HttpDelete]
        public IActionResult Delete(string userId)
        {
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {
                    bool isUsrRoleDel = HelpCenter.BLL.UserRole.Delete(userId);
                    return !isUsrRoleDel
                        ? Ok(new { result = false, tips = ResponseMessageTips.MSG_USRROLE_DEL_FAIL })
                        : Ok(new { result = true, tips = ResponseMessageTips.MSG_USRROLE_DEL_SUCCESS });
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