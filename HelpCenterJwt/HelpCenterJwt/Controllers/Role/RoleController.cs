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
    /// <summary>
    /// 角色资料
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        /// <summary>
        /// 获取所有角色清单（分页），提供指定页的数据、总数据量及总页数
        /// </summary>
        /// <param name="pageIndex">页码，从1开始</param>
        /// <param name="rowCount">每页数据量</param>
        /// <returns></returns>
        // GET: api/Role
        [HttpGet("pagelist")]
        public IActionResult Get(int pageIndex, int rowCount)
        {
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {
                    if (pageIndex > 0 && rowCount > 0)
                    {
                        int _totalCount, _pageCount = 0;
                        var roleList = HelpCenter.BLL.Role.GetList(pageIndex, rowCount, out _totalCount, out _pageCount);

                        return null == roleList
                            ? Ok(new { result = false, tips = ResponseMessageTips.MSG_ROLE_INFO_NO_FOUND })
                            : Ok(new
                            {
                                result = true,
                                tips = ResponseMessageTips.MSG_PROCESS_SUCCESS,
                                data = new { totalCount = _totalCount, pageCount = _pageCount, info = roleList }
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
        /// 获取所有角色清单
        /// </summary>
        /// <returns></returns>
        // GET: api/Role
        [HttpGet("getAllList")]
        public IActionResult GetAllList(bool isEnable = false)
        {
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {
                    var roleList = HelpCenter.BLL.Role.GetAllList(isEnable);

                    return null == roleList
                        ? Ok(new { result = false, tips = ResponseMessageTips.MSG_ROLE_INFO_NO_FOUND })
                        : Ok(new
                        {
                            result = true,
                            tips = ResponseMessageTips.MSG_PROCESS_SUCCESS,
                            data = roleList
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
        /// 根据ID获取角色详细信息
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        // GET: api/Role/5
        [HttpGet]
        public IActionResult Get(string roleId)
        {
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {
                    var roleInfo = HelpCenter.BLL.Role.GetModel(roleId);
                    return null == roleInfo
                        ? Ok(new { result = false, tips = ResponseMessageTips.MSG_ROLE_INFO_NO_FOUND })
                        : Ok(new
                        {
                            result = true,
                            tips = ResponseMessageTips.MSG_PROCESS_SUCCESS,
                            data = roleInfo
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
        /// 新增角色信息
        /// </summary>
        /// <param name="roleViewModel"></param>
        /// <returns></returns>
        // POST: api/Role
        [HttpPost]
        public IActionResult Post([FromBody] ViewModel.RoleViewModel roleViewModel)
        {
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {
                    bool isRoleAdd = HelpCenter.BLL.Role.Add(roleViewModel.RoleName, roleViewModel.RoleState, roleViewModel.RoleRemark,
                        User.Identities.First(u => u.IsAuthenticated).FindFirst("UsrId").Value);
                    /*判断是否新增成功*/
                    return !isRoleAdd
                        ? Ok(new { result = false, tips = ResponseMessageTips.MSG_ROLE_ADD_FAIL })
                        : Ok(new { result = true, tips = ResponseMessageTips.MSG_ROLE_ADD_SUCCESS });
                }
                catch (Exception e)
                {
                    return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_EXCEPTION + e.Message.ToString() });
                }
            }

            return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_DATA_FORMAT_ERROR });
        }

        /// <summary>
        /// 更新指定角色信息
        /// </summary>
        /// <param name="roleId">指定角色ID</param>
        /// <param name="roleViewModel"></param>
        // PUT: api/Role/5
        [HttpPut]
        public IActionResult Put(string roleId, [FromBody] ViewModel.RoleViewModel roleViewModel)
        {
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {
                    bool isRoleUpdate = HelpCenter.BLL.Role.Update(roleId, roleViewModel.RoleName, roleViewModel.RoleRemark,
                        roleViewModel.RoleState,
                        User.Identities.First(u => u.IsAuthenticated).FindFirst("UsrId").Value);
                    /*判断是否修改成功*/
                    return !isRoleUpdate
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
        /// 删除指定的角色信息
        /// </summary>
        /// <param name="roleId">角色ID</param>
        // DELETE: api/Role/5
        [HttpDelete]
        public IActionResult Delete(string roleId)
        {
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {
                    bool isRoleDel = HelpCenter.BLL.Role.Delete(roleId);
                    return !isRoleDel
                        ? Ok(new { result = false, tips = ResponseMessageTips.MSG_ROLE_DELETE_FAIL })
                        : Ok(new { result = true, tips = ResponseMessageTips.MSG_ROLE_DELETE_SUCCESS });
                }
                catch (Exception e)
                {
                    return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_EXCEPTION + e.Message.ToString() });
                }
            }
            return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_DATA_FORMAT_ERROR });
        }

        /// <summary>
        /// 禁用/启用指定的角色信息
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <param name="roleState">启用状态1为启用，0为禁用</param>
        // DELETE: api/Role/5
        [HttpPut("enable/roleid")]
        public IActionResult Enable(string roleId, int roleState)
        {
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {
                    bool isRoleDel = HelpCenter.BLL.Role.Enable(roleId, roleState);
                    return !isRoleDel
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
    }
}
