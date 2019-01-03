using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelpCenter.ActionService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HelpCenterJwt.Controllers.SysModule
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SysModuleController : ControllerBase
    {

        /// <summary>
        /// 获取顶级系统模块清单
        /// </summary>
        /// <returns></returns>
        // GET: api/SysModule
        [HttpGet("toplist")]
        public IActionResult Get()
        {
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {
                    var sysModuleList = HelpCenter.BLL.SysModule.GetTopList();
                    return null == sysModuleList
                        ? Ok(new { result = false, tips = ResponseMessageTips.MSG_SYSMODULE_INFO_NO_FOUND })
                        : Ok(new { result = true, tips = ResponseMessageTips.MSG_PROCESS_SUCCESS, data = sysModuleList });
                }
                catch (Exception e)
                {
                    return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_EXCEPTION + e.Message.ToString() });
                }
            }

            return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_DATA_FORMAT_ERROR });
        }

        /// <summary>
        /// 获取树形系统模块清单
        /// </summary>
        /// <returns></returns>
        // GET: api/SysModule
        [HttpGet("sysmoduletree")]
        public IActionResult GetTreeMode()
        {
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {
                    var sysModuleList = HelpCenter.BLL.SysModule.GetList();
                    return null == sysModuleList
                        ? Ok(new { result = false, tips = ResponseMessageTips.MSG_SYSMODULE_INFO_NO_FOUND })
                        : Ok(new { result = true, tips = ResponseMessageTips.MSG_PROCESS_SUCCESS, data = sysModuleList });
                }
                catch (Exception e)
                {
                    return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_EXCEPTION + e.Message.ToString() });
                }
            }

            return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_DATA_FORMAT_ERROR });
        }



        /// <summary>
        /// 获取所有系统模块信息
        /// </summary>
        /// <param name="pageIndex">指定页码</param>
        /// <param name="rowCount">每页数据量</param>
        /// <returns></returns>
        // GET: api/SysModule
        [HttpGet("pagelist")]
        public IActionResult Get(int pageIndex, int rowCount)
        {
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {
                    var sysModuleList = HelpCenter.BLL.SysModule.GetList(pageIndex, rowCount, out int _totalCount, out int _pageCount);
                    return null == sysModuleList
                        ? Ok(new { result = false, tips = ResponseMessageTips.MSG_SYSMODULE_INFO_NO_FOUND })
                        : Ok(new
                        {
                            result = true,
                            tips = ResponseMessageTips.MSG_PROCESS_SUCCESS,
                            data = new { totalCount = _totalCount, pageCount = _pageCount, info = sysModuleList }
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
        /// 获取指定ID系统模块信息，并返回该模块下的子模块列表
        /// </summary>
        /// <param name="ModuleId"></param>
        /// <returns></returns>
            // GET: api/SysModule/5
        [HttpGet]
        public IActionResult Get(string ModuleId)
        {
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {
                    var sysModuleInfo = HelpCenter.BLL.SysModule.GetDetail(ModuleId);
                    var ChildNodes = HelpCenter.BLL.SysModule.GetChildNodes(ModuleId);
                    return null == sysModuleInfo
                        ? Ok(new { result = false, tips = ResponseMessageTips.MSG_SYSMODULE_INFO_NO_FOUND })
                        : Ok(new
                        {
                            result = true,
                            tips = ResponseMessageTips.MSG_PROCESS_SUCCESS,
                            data = new { detail = sysModuleInfo, childNodes = ChildNodes }
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
        /// 新增系统模块信息
        /// </summary>
        /// <param name="sysModuleViewModel"></param>
        /// <returns></returns>
        // POST: api/SysModule
        [HttpPost]
        public IActionResult Post([FromBody] ViewModel.SysModuleViewModel sysModuleViewModel)
        {
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {
                    bool isSysModuleAdd = HelpCenter.BLL.SysModule.Add(sysModuleViewModel.ModuleName, sysModuleViewModel.ModuleCode, sysModuleViewModel.ModuleImgPath,
                        sysModuleViewModel.ModuleUrlPath, sysModuleViewModel.PreModuleId, sysModuleViewModel.ModuleRemark, 1, 
                        User.Identities.First(u => u.IsAuthenticated).FindFirst("UsrId").Value);
                    return !isSysModuleAdd
                        ? Ok(new { result = false, tips = ResponseMessageTips.MSG_SYSMODULE_ADD_FAIL })
                        : Ok(new { result = true, tips = ResponseMessageTips.MSG_SYSMODULE_ADD_SUCCESS });
                }
                catch (Exception e)
                {
                    return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_EXCEPTION + e.Message.ToString() });
                }
            }
            return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_DATA_FORMAT_ERROR });
        }

        /// <summary>
        /// 修改指定ID系统模块信息
        /// </summary>
        /// <param name="ModuleId"></param>
        /// <param name="sysModuleViewModel"></param>
        /// <returns></returns>
        // PUT: api/SysModule/5
        [HttpPut]
        public IActionResult Put(string ModuleId, [FromBody] ViewModel.SysModuleViewModel sysModuleViewModel)
        {
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {
                    bool isSysModuleUpdate = HelpCenter.BLL.SysModule.Update(ModuleId, sysModuleViewModel.ModuleName, sysModuleViewModel.ModuleImgPath,
                        sysModuleViewModel.ModuleUrlPath, sysModuleViewModel.PreModuleId, sysModuleViewModel.ModuleRemark, 1, this.User.Identities.First(u => u.IsAuthenticated).FindFirst("UsrId").Value);
                    return !isSysModuleUpdate
                        ? Ok(new { result = false, tips = ResponseMessageTips.MSG_SYSMODULE_UPDATE_FAIL })
                        : Ok(new { result = true, tips = ResponseMessageTips.MSG_SYSMODULE_UPDATE_SUCCESS });
                }
                catch (Exception e)
                {
                    return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_EXCEPTION + e.Message.ToString() });
                }
            }
            return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_DATA_FORMAT_ERROR });
        }

        /// <summary>
        /// 删除指定的系统模块记录 
        /// </summary>
        /// <param name="ModuleId"></param>
        /// <returns></returns>
        // DELETE: api/SysModule/5
        [HttpDelete]
        public IActionResult Delete(string ModuleId)
        {
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {
                    string strSysModuleInfo = string.Empty;
                    bool isSysModuleDel = HelpCenter.BLL.SysModule.Delete(ModuleId);
                    return !isSysModuleDel
                        ? Ok(new { result = false, tips = ResponseMessageTips.MSG_SYSMODULE_DELETE_FAIL })
                        : Ok(new { result = true, tips = ResponseMessageTips.MSG_SYSMODULE_DELETE_SUCCESS });
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