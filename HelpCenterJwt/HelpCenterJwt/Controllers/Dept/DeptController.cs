using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelpCenter.ActionService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HelpCenter;
using HelpCenter.BLL;

namespace HelpCenterJwt.Controllers.Dept
{
    /// <summary>
    /// 部门信息
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DeptController : ControllerBase
    {
        /// <summary>
        /// 获取顶级部门清单
        /// </summary>
        /// <returns></returns>
        // GET: api/Dept
        [HttpGet("toplist")]
        public IActionResult GetTopList()
        {
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {
                    var deptList = HelpCenter.BLL.Dept.GetTopList();
                    return null == deptList
                        ? Ok(new { result = false, tips = ResponseMessageTips.MSG_USER_DEPT_NO_FOUND })
                        : Ok(new { result = true, tips = ResponseMessageTips.MSG_PROCESS_SUCCESS, data = deptList });
                }
                catch (Exception e)
                {
                    return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_EXCEPTION + e.Message.ToString() });
                }
            }

            return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_DATA_FORMAT_ERROR });
        }
        /// <summary>
        /// 获取所有部门清单（分页），提供指定页的数据、总数据量及总页数
        /// </summary>
        /// <param name="pageIndex">页码，从1开始</param>
        /// <param name="rowCount">每页数据量</param>
        /// <returns></returns>
        // GET: api/Dept
        [Authorize]
        [HttpGet("pagelist")]
        public IActionResult GetDeptListByPage(int pageIndex, int rowCount)
        {
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {
                    int _totalCount, _pageCount = 0;
                    var deptList = HelpCenter.BLL.Dept.GetList(pageIndex, rowCount, out _totalCount, out _pageCount,
                        this.User.Identities.First(u => u.IsAuthenticated).FindFirst("DeptNO").Value);

                    return null == deptList
                        ? Ok(new { result = false, tips = ResponseMessageTips.MSG_USER_DEPT_NO_FOUND })
                        : Ok(new
                        {
                            result = true,
                            tips = ResponseMessageTips.MSG_PROCESS_SUCCESS,
                            data = new { totalCount = _totalCount, pageCount = _pageCount, info = deptList }
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
        /// 获取按部门名称搜索的清单（分页），提供指定页的数据、总数据量及总页数
        /// </summary>
        /// <param name="pageIndex">页码，从1开始</param>
        /// <param name="rowCount">每页数据量</param>
        /// <param name="deptName">部门名称，支持模糊搜索</param>
        /// <returns></returns>
        // GET: api/Dept
        [Authorize]
        [HttpGet("keyword/deptname")]
        public IActionResult GetDeptNameListByPage(int pageIndex, int rowCount,string deptName)
        {
            
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {
                    int _totalCount, _pageCount = 0;
                    var deptList = HelpCenter.BLL.Dept.GetListByDeptName(pageIndex, rowCount, deptName, out _totalCount, out _pageCount,
                        this.User.Identities.First(u => u.IsAuthenticated).FindFirst("DeptNO").Value);

                    return null == deptList
                        ? Ok(new { result = false, tips = ResponseMessageTips.MSG_USER_DEPT_NO_FOUND })
                        : Ok(new
                        {
                            result = true,
                            tips = ResponseMessageTips.MSG_PROCESS_SUCCESS,
                            data = new { totalCount = _totalCount, pageCount = _pageCount, info = deptList }
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
        /// 获取当前登录用户的所在部门，并提供当前部门下的子节点列表
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("me",Name = "GetDeptListByUsrId")]
        public IActionResult GetDeptListByUsrId()
        {
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {
                    string deptId = User.Identities.First(u => u.IsAuthenticated).FindFirst("UsrDeptId").Value;
                    var deptInfo = HelpCenter.BLL.Dept.GetModelListByDeptId(deptId);
                    var _childNodes = HelpCenter.BLL.Dept.GetChildNodes(deptId);
                    return null == deptInfo
                        ? Ok(new { result = false, tips = ResponseMessageTips.MSG_USER_DEPT_NO_FOUND })
                        : Ok(new
                        {
                            result = true,
                            tips = ResponseMessageTips.MSG_PROCESS_SUCCESS,
                            data = new { detail = deptInfo, childNodes = _childNodes }
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
        /// 根据部门ID获取部门详细信息，并提供当前部门下的子节点列表
        /// </summary>
        /// <param name="deptId">部门ID</param>
        /// <returns></returns>
        [HttpGet()]
        public IActionResult GetDeptByDeptId(string deptId)
        {
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {
                    var deptInfo = HelpCenter.BLL.Dept.GetModel(deptId);
                    var childNodes = HelpCenter.BLL.Dept.GetChildNodes(deptId);
                    return null == deptInfo
                        ? Ok(new { result = false, tips = ResponseMessageTips.MSG_USER_DEPT_NO_FOUND })
                        : Ok(new
                        {
                            result = true,
                            tips = ResponseMessageTips.MSG_PROCESS_SUCCESS,
                            data = new { detail = deptInfo, childNodes = childNodes }
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
        /// 列出指定部门下的用户清单（分页）
        /// </summary>
        /// <param name="pageIndex">指定页码</param>
        /// <param name="rowCount">每页数据量</param>
        /// <param name="deptId">部门ID</param>
        /// <returns></returns>
        // GET: api/User
        [Authorize]
        [HttpGet("userlist")]
        public IActionResult GetUserListByDeptId(int pageIndex, int rowCount, string deptId)
        {
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {
                    if (string.IsNullOrEmpty(deptId))
                    {
                        deptId = User.Identities.First(u => u.IsAuthenticated).FindFirst("UsrDeptId").Value;
                    }
                    IList<HelpCenter.Model.DbModel.DbDeptUserModel> usrList = Usr.GetListByDeptId(pageIndex, rowCount, out int _totalCount, out int _pageCount, deptId);
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
        /// 根据部门ID获取所有文档类型清单（分页），提供指定页的数据、总数据量及总页数
        /// </summary>
        /// <param name="pageIndex">页码，从1开始</param>
        /// <param name="rowCount">每页数据量</param>
        /// <param name="deptId">部门ID</param>
        /// <returns></returns>
        // GET: api/DocTypeConfig
        [HttpGet("doctypelist")]
        public IActionResult GetDocTypeListByDeptId(int pageIndex, int rowCount, string deptId)
        {
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {
                    if (pageIndex > 0 && rowCount > 0)
                    {
                        if (string.IsNullOrEmpty(deptId))
                        {
                            deptId = User.Identities.First(u => u.IsAuthenticated).FindFirst("UsrDeptId").Value;
                        }

                        var docTypeList = DocTypeConfig.GetListByDeptId(pageIndex, rowCount, out int _totalCount, out int _pageCount, deptId);
                        return null == docTypeList
                            ? Ok(new { result = false, tips = ResponseMessageTips.MSG_DOCTYPE_INFO_NO_FOUND })
                            : Ok(new
                            {
                                result = true,
                                tips = ResponseMessageTips.MSG_PROCESS_SUCCESS,
                                data = new { totalCount = _totalCount, pageCount = _pageCount, info = docTypeList }
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
        /// 新增部门信息
        /// </summary>
        /// <param name="createDeptViewModel"></param>
        /// <returns></returns>
        // POST: api/Dept
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] ViewModel.DeptViewModel createDeptViewModel)
        {
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {
                    string strTips = string.Empty;
                    bool isDeptAdd = HelpCenter.BLL.Dept.Add(User.Identities.First(u => u.IsAuthenticated).FindFirst("UsrId").Value, createDeptViewModel.PreDeptId, createDeptViewModel.DeptName,
                        createDeptViewModel.DeptAccount, createDeptViewModel.DeptPsw,out strTips);
                    /*判断是否新增成功*/
                    return Ok(new { result = isDeptAdd, tips = strTips });
                }
                catch (Exception e)
                {
                    return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_EXCEPTION + e.Message.ToString() });
                }
            }

            return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_DATA_FORMAT_ERROR });
        }

        /// <summary>
        /// 重置指定部门账号的密码
        /// </summary>
        /// <param name="deptId">指定部门ID</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("psw")]
        public IActionResult Put(string deptId, string password)
        {
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {
                    bool isDeptPswUpdate = HelpCenter.BLL.Dept.UpdatePassWord(deptId, password,this.User.Identities.First(u => u.IsAuthenticated).FindFirst("UsrId").Value);
                    return !isDeptPswUpdate
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
        /// 更新指定部门信息
        /// </summary>
        /// <param name="deptId">指定部门ID</param>
        /// <param name="updateDeptViewModel"></param>
        // PUT: api/Dept/5
        [Authorize]
        [HttpPut]
        public IActionResult Put(string deptId, [FromBody] ViewModel.DeptViewModel updateDeptViewModel)
        {
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {
                    bool isDeptUpdate = HelpCenter.BLL.Dept.Update(User.Identities.First(u => u.IsAuthenticated).FindFirst("UsrId").Value, deptId, updateDeptViewModel.PreDeptId, updateDeptViewModel.DeptName,
                        updateDeptViewModel.DeptNO, updateDeptViewModel.DeptAccount, updateDeptViewModel.DeptPsw);
                    /*判断是否新增成功*/
                    return !isDeptUpdate
                        ? Ok(new { result = false, tips = ResponseMessageTips.MSG_DEPT_UPDATE_FAIL })
                        : Ok(new { result = true, tips = ResponseMessageTips.MSG_DEPT_UPDATE_SUCCESS });
                }
                catch (Exception e)
                {
                    return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_EXCEPTION + e.Message.ToString() });
                }
            }

            return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_DATA_FORMAT_ERROR });
        }

        /// <summary>
        /// 删除指定的部门信息
        /// </summary>
        /// <param name="deptId">部门ID</param>
        // DELETE: api/Dept/5
        [Authorize]
        [HttpDelete]
        public IActionResult Delete(string deptId)
        {
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {
                    string strTips = string.Empty;
                    bool isUsrDel = HelpCenter.BLL.Dept.Delete(deptId,out strTips);
                    return Ok(new { result = isUsrDel, tips = strTips });
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
