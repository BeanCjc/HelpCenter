using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HelpCenter.ActionService;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace HelpCenterJwt.Controllers.User
{
    /// <summary>
    /// 用户信息
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        /// <summary>
        /// 获取所有用户信息
        /// </summary>
        /// <param name="pageIndex">指定页码</param>
        /// <param name="rowCount">每页数据量</param>
        /// <param name="usrType">用户类型，默认1为部门账号，2为正式人员，3为试用人员，4为普通用户，留空默认为4，查询多种用户以英文逗号隔开,如“2,3”</param>
        /// <returns></returns>
        // GET: api/User
        [Authorize]
        [HttpGet("pagelist")]
        public IActionResult Get(int pageIndex, int rowCount, string usrType)
        {
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {
                    if (string.IsNullOrEmpty(usrType))
                    {
                        usrType = "2,3";
                    }
                    var usrList = HelpCenter.BLL.Usr.GetList(pageIndex, rowCount, out int _totalCount, out int _pageCount, usrType);
                    return null == usrList
                        ? base.Ok(new { result = false, tips = ResponseMessageTips.MSG_USR_INFO_NO_FOUND })
                        : base.Ok(new
                        {
                            result = true,
                            tips = ResponseMessageTips.MSG_PROCESS_SUCCESS,
                            data = new { totalCount = _totalCount, pageCount = (int)0, info = usrList }
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
        /// 根据用户名搜索，获取用户清单（分页）
        /// </summary>
        /// <param name="pageIndex">指定页码</param>
        /// <param name="rowCount">每页数据量</param>
        /// <param name="userName">用户名，支持模糊搜索</param>
        /// <param name="usrType">用户类型，默认1为部门账号，2为正式人员，3为试用人员，4为普通用户，查询多种用户以英文逗号隔开,如“2,3”</param>
        /// <returns></returns>
        // GET: api/User
        [Authorize]
        [HttpGet("keyword/username")]
        public IActionResult GetListByUsrName(int pageIndex, int rowCount, string userName, string usrType)
        {
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {
                    if (string.IsNullOrEmpty(usrType))
                    {
                        usrType = "2,3";
                    }
                    IList<HelpCenter.Model.Usr> usrList =
                        HelpCenter.BLL.Usr.GetListByUsrName(pageIndex, rowCount, out int _totalCount, out int _pageCount, userName, usrType, 
                        User.Identities.First(u => u.IsAuthenticated).FindFirst("UsrDeptId").Value);
                    return null == usrList
                        ? Ok(new { result = false, tips = ResponseMessageTips.MSG_USR_INFO_NO_FOUND })
                        : Ok(new
                        {
                            result = true,
                            tips = ResponseMessageTips.MSG_PROCESS_SUCCESS,
                            data = new { totalCount = _totalCount, pageCount = _pageCount, info = usrList }
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
        /// 获取指定ID用户信息，若UsrId为空，返回当前登录用户信息
        /// </summary>
        /// <param name="UsrId"></param>
        /// <returns></returns>
            // GET: api/User/5
        [Authorize]
        [HttpGet]
        public IActionResult Get(string UsrId)
        {
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {
                    var usrInfo = string.IsNullOrEmpty(UsrId) ? HelpCenter.BLL.Usr.usrDtoModel : HelpCenter.BLL.Usr.GetUserDtoDetail(UsrId);
                    var userRoleList = HelpCenter.BLL.UserRole.GetModelByUserId(UsrId);
                    var userRoleIdList = new List<string>();
                    foreach (var item in userRoleList)
                    {
                        userRoleIdList.Add(item.roleId);
                    }
                    usrInfo.UsrRoleIdLst = userRoleIdList;
                    return null == usrInfo
                        ? Ok(new { result = false, tips = ResponseMessageTips.MSG_USR_INFO_NO_FOUND })
                        : Ok(new { result = true, tips = ResponseMessageTips.MSG_PROCESS_SUCCESS, data = usrInfo });
                }
                catch (Exception e)
                {
                    return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_EXCEPTION + e.Message.ToString() });
                }
            }
            return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_DATA_FORMAT_ERROR });
        }

        /// <summary>
        /// 检查账号信息是否已经注册
        /// </summary>
        /// <param name="UsrAccount">注册账号</param>
        /// <returns></returns>
        // GET: api/check/5
        [HttpGet("check")]
        public IActionResult GetUsrAccount(string UsrAccount)
        {
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {
                    return HelpCenter.BLL.Usr.CheckAccount(UsrAccount)
                        ? Ok(new { result = true, tips = ResponseMessageTips.MSG_USR_INFO_NO_FOUND })
                        : Ok(new { result = false, tips = ResponseMessageTips.MSG_USR_IS_EXIST });
                }
                catch (Exception e)
                {
                    return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_EXCEPTION + e.Message.ToString() });
                }
            }
            return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_DATA_FORMAT_ERROR });
        }

        /// <summary>
        /// 新增用户信息
        /// </summary>
        /// <param name="userViewModel"></param>
        /// <returns></returns>
        // POST: api/User
        [HttpPost]
        public IActionResult Post([FromBody] ViewModel.UserViewModel userViewModel)
        {
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {
                    if (!HelpCenter.BLL.Usr.CheckAccount(userViewModel.UsrAccount))
                    {
                        return Ok(new { result = false, tips = ResponseMessageTips.MSG_USR_IS_EXIST });
                    }
                    
                    string strUsrInfo = string.Empty;
                    bool isUsrAdd = HelpCenter.BLL.Usr.Add(userViewModel.UsrPhoneNum, userViewModel.UsrAccount,
                        userViewModel.UsrPsw, userViewModel.UsrName, userViewModel.UsrDeptId, userViewModel.UsrState,
                       User.Identity.IsAuthenticated? 
                       User.Identities.First(u => u.IsAuthenticated).FindFirst("UsrId").Value:string.Empty, 
                       userViewModel.UsrType, userViewModel.UsrVerifyState,userViewModel.RoleIdList);
                    return !isUsrAdd
                        ? Ok(new { result = false, tips = ResponseMessageTips.MSG_USR_ADD_FAIL })
                        : Ok(new { result = true, tips = ResponseMessageTips.MSG_USR_ADD_SUCCESS });
                }
                catch (Exception e)
                {
                    return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_EXCEPTION + e.Message.ToString() });
                }
            }
            return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_DATA_FORMAT_ERROR });
        }

        /// <summary>
        /// 修改指定ID用户信息
        /// </summary>
        /// <param name="UsrId"></param>
        /// <param name="userViewModel"></param>
        /// <returns></returns>
        // PUT: api/User/5
        [Authorize]
        [HttpPut]
        public IActionResult Put(string UsrId, [FromBody] ViewModel.UserViewModel userViewModel)
        {
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {
                    string strUsrInfo = string.Empty;
                    bool isUsrUpdate = HelpCenter.BLL.Usr.Update(UsrId, userViewModel.UsrPhoneNum, userViewModel.UsrAccount,
                        userViewModel.UsrPsw, userViewModel.UsrName, userViewModel.UsrDeptId, userViewModel.UsrState,
                        User.Identities.First(u => u.IsAuthenticated).FindFirst("UsrId").Value, userViewModel.UsrType, userViewModel.UsrVerifyState, userViewModel.RoleIdList);
                    return !isUsrUpdate
                        ? Ok(new { result = false, tips = ResponseMessageTips.MSG_USR_UPDATE_FAIL })
                        : Ok(new { result = true, tips = ResponseMessageTips.MSG_USR_UPDATE_SUCCESS });
                }
                catch (Exception e)
                {
                    return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_EXCEPTION + e.Message.ToString() });
                }
            }
            return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_DATA_FORMAT_ERROR });
        }

        /// <summary>
        /// 重置指定用户密码
        /// </summary>
        /// <param name="UsrAccount">指定用户账号</param>
        /// <param name="Password">密码</param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("resetpsw/account")]
        public IActionResult Put(string UsrAccount, string Password)
        {
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {
                    bool isUsrPswUpdate = HelpCenter.BLL.Usr.UpdatePassWord(UsrAccount, Password);
                    return !isUsrPswUpdate
                        ? Ok(new { result = false, tips = ResponseMessageTips.MSG_USER_PWD_FAIL })
                        : Ok(new { result = true, tips = ResponseMessageTips.MSG_USER_PWD_SUCCESS });
                }
                catch (Exception e)
                {
                    return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_EXCEPTION + e.Message.ToString() });
                }
            }
            return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_DATA_FORMAT_ERROR });
        }

        /// <summary>
        /// 重置当前登录用户密码
        /// </summary>
        /// <param name="Password">密码</param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("resetpsw")]
        public IActionResult PutPassword(string Password)
        {
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {
                    bool isUsrPswUpdate = HelpCenter.BLL.Usr.UpdatePassWord(User.Identities.First(u => u.IsAuthenticated).FindFirst("UsrAccount").Value, Password);
                    return !isUsrPswUpdate
                        ? Ok(new { result = false, tips = ResponseMessageTips.MSG_USER_PWD_FAIL })
                        : Ok(new { result = true, tips = ResponseMessageTips.MSG_USER_PWD_SUCCESS });
                }
                catch (Exception e)
                {
                    return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_EXCEPTION + e.Message.ToString() });
                }
            }
            return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_DATA_FORMAT_ERROR });
        }

        /// <summary>
        /// 审核用户状态
        /// </summary>
        /// <param name="UsrId">指定用户ID</param>
        /// <param name="UsrVerifyState">用户审核状态1为通过，0为未通过</param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("verify/userid")]
        public IActionResult UpdateUsrVerifyState(string UsrId, int UsrVerifyState)
        {
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {
                    bool isUsrStateUpdate = HelpCenter.BLL.Usr.UpdateUsrVerifyState(UsrId, UsrVerifyState);
                    return !isUsrStateUpdate
                        ? Ok(new { result = false, tips = ResponseMessageTips.MSG_USR_UPDATE_FAIL })
                        : Ok(new { result = true, tips = ResponseMessageTips.MSG_USR_UPDATE_SUCCESS });
                }
                catch (Exception e)
                {
                    return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_EXCEPTION + e.Message.ToString() });
                }
            }
            return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_DATA_FORMAT_ERROR });
        }
        /// <summary>
        /// 启用/禁用用户状态
        /// </summary>
        /// <param name="UsrId">指定用户ID</param>
        /// <param name="UsrState">用户启用状态1为通过，0为未通过</param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("enable/userid")]
        public IActionResult UpdateUserState(string UsrId, int UsrState)
        {
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {
                    bool isUsrStateUpdate = HelpCenter.BLL.Usr.UpdateUserState(UsrId, UsrState);
                    return !isUsrStateUpdate
                        ? Ok(new { result = false, tips = ResponseMessageTips.MSG_USR_UPDATE_FAIL })
                        : Ok(new { result = true, tips = ResponseMessageTips.MSG_USR_UPDATE_SUCCESS });
                }
                catch (Exception e)
                {
                    return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_EXCEPTION + e.Message.ToString() });
                }
            }
            return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_DATA_FORMAT_ERROR });
        }

        /// <summary>
        ///删除指定的用户记录
        /// </summary>
        /// <param name="UsrId"></param>
        /// <returns></returns>
        // DELETE: api/User/5
        [Authorize]
        [HttpDelete]
        public IActionResult Delete(string UsrId)
        {
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {
                    string strUsrInfo = string.Empty;
                    bool isUsrDel = HelpCenter.BLL.Usr.Delete(UsrId);
                    return !isUsrDel
                        ? Ok(new { result = false, tips = ResponseMessageTips.MSG_USR_DELETE_FAIL })
                        : Ok(new { result = true, tips = ResponseMessageTips.MSG_USR_DELETE_SUCCESS });
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
