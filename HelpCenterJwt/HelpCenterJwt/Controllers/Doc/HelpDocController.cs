using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelpCenter.ActionService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HelpCenterJwt.Controllers.Doc
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelpDocController : ControllerBase
    {
        /// <summary>
        /// 获取所有文档清单（分页），提供指定页的数据、总数据量及总页数
        /// </summary>
        /// <param name="pageIndex">页码，从1开始</param>
        /// <param name="rowCount">每页数据量</param>
        /// <param name="keyword">搜索关键字</param>
        /// <returns></returns>
        // GET: api/HelpDoc
        [HttpGet("pagelist")]
        public IActionResult GetPageList(int pageIndex, int rowCount, string keyword)
        {
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {
                    if (pageIndex > 0 && rowCount > 0)
                    {
                        var helpDocList = HelpCenter.BLL.HelpDoc.GetList(pageIndex, rowCount, out int _totalCount, out int _pageCount, User, keyword);

                        return null == helpDocList
                            ? Ok(new { result = false, tips = ResponseMessageTips.MSG_DOC_INFO_NO_FOUND })
                            : Ok(new
                            {
                                result = true,
                                tips = ResponseMessageTips.MSG_PROCESS_SUCCESS,
                                data = new { totalCount = _totalCount, pageCount = _pageCount, info = helpDocList }
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
        /// 根据文档ID获取文档详细信息
        /// </summary>
        /// <param name="helpDocId">文档ID</param>
        /// <returns></returns>
        // GET: api/HelpDoc/5
        [HttpGet]
        public IActionResult Get(string helpDocId)
        {
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {
                    var helpDocList = HelpCenter.BLL.HelpDoc.GetModel(helpDocId);
                    return null == helpDocList
                        ? Ok(new { result = false, tips = ResponseMessageTips.MSG_DOC_INFO_NO_FOUND })
                        : Ok(new
                        {
                            result = true,
                            tips = ResponseMessageTips.MSG_PROCESS_SUCCESS,
                            data = helpDocList
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
        /// 根据部门ID获取文档列表
        /// </summary>
        /// <param name="pageIndex">页码，从1开始</param>
        /// <param name="rowCount">每页数据量</param>
        /// <param name="deptId">部门ID</param>
        /// <returns></returns>
        // GET: api/HelpDoc/5
        [HttpGet("deptid", Name = "GetHelpDocByDeptId")]
        public IActionResult GetListByDeptId(int pageIndex, int rowCount, string deptId)
        {
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {
                    if (pageIndex > 0 && rowCount > 0)
                    {
                        int _totalCount, _pageCount = 0;
                        var helpDocList = HelpCenter.BLL.HelpDoc.GetListByDeptId(pageIndex, rowCount, out _totalCount, out _pageCount, deptId);

                        return null == helpDocList
                            ? Ok(new { result = false, tips = ResponseMessageTips.MSG_DOC_INFO_NO_FOUND })
                            : Ok(new
                            {
                                result = true,
                                tips = ResponseMessageTips.MSG_PROCESS_SUCCESS,
                                data = new { totalCount = _totalCount, pageCount = _pageCount, info = helpDocList }
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
        /// 根据文档类型ID获取文档列表
        /// </summary>
        /// <param name="pageIndex">页码，从1开始</param>
        /// <param name="rowCount">每页数据量</param>
        /// <param name="docTypeId">文档类型ID</param>
        /// <param name="isRecursion">是否递归获取文档</param>
        /// <returns></returns>
        // GET: api/HelpDoc/5
        [HttpGet("doctypeid", Name = "GetListByDocTypeId")]
        public IActionResult GetListByDocTypeId(int pageIndex, int rowCount, string docTypeId, bool isRecursion)
        {
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {
                    if (pageIndex > 0 && rowCount > 0)
                    {
                        int _totalCount, _pageCount = 0;
                        var helpDocList = HelpCenter.BLL.HelpDoc.GetListByDocTypeId(pageIndex, rowCount, docTypeId, isRecursion, out _totalCount, out _pageCount, User);

                        return null == helpDocList
                            ? Ok(new { result = false, tips = ResponseMessageTips.MSG_DOC_INFO_NO_FOUND })
                            : Ok(new
                            {
                                result = true,
                                tips = ResponseMessageTips.MSG_PROCESS_SUCCESS,
                                data = new { totalCount = _totalCount, pageCount = _pageCount, info = helpDocList }
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
        /// 新增文档信息
        /// </summary>
        /// <param name="helpDocViewModel"></param>
        /// <returns></returns>
        // POST: api/HelpDoc
        [HttpPost]
        public IActionResult Post([FromBody] ViewModel.HelpDocViewModel helpDocViewModel)
        {
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {
                    string strDocInfo = string.Empty;
                    bool isDocAdd = HelpCenter.BLL.HelpDoc.Add(helpDocViewModel.DocTypeId, helpDocViewModel.DocTitle, helpDocViewModel.DocContent,
                        helpDocViewModel.DocFullText, helpDocViewModel.DocNum, helpDocViewModel.DocDeptId,
                        User.Identities.First(u => u.IsAuthenticated).FindFirst("UsrId").Value, 1, helpDocViewModel.DocShareDeptList, helpDocViewModel.DocAttachment, helpDocViewModel.IsDefaultRole,
                        helpDocViewModel.viewRoleList, helpDocViewModel.editRoleList);
                    /*判断是否新增成功*/
                    return !isDocAdd
                        ? Ok(new { result = false, tips = ResponseMessageTips.MSG_DOC_ADD_FAIL })
                        : Ok(new { result = true, tips = ResponseMessageTips.MSG_DOC_ADD_SUCCESS });
                }
                catch (Exception e)
                {
                    return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_EXCEPTION + e.Message.ToString() });
                }
            }

            return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_DATA_FORMAT_ERROR });
        }

        /// <summary>
        /// 更新指定文档信息
        /// </summary>
        /// <param name="helpDocId">指定文档ID</param>
        /// <param name="helpDocViewModel"></param>
        // PUT: api/HelpDoc/5
        [HttpPut]
        public IActionResult Put(string helpDocId, [FromBody] ViewModel.HelpDocViewModel helpDocViewModel)
        {
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {

                    bool isDocUpdate = HelpCenter.BLL.HelpDoc.Update(helpDocId, helpDocViewModel.DocTypeId, helpDocViewModel.DocTitle, helpDocViewModel.DocContent
                    , helpDocViewModel.DocFullText, helpDocViewModel.DocNum, helpDocViewModel.DocDeptId,
                    User.Identities.First(u => u.IsAuthenticated).FindFirst("UsrId").Value, 1, helpDocViewModel.DocShareDeptList, helpDocViewModel.DocAttachment, helpDocViewModel.IsDefaultRole,
                    helpDocViewModel.viewRoleList, helpDocViewModel.editRoleList);
                    /*判断是否更新成功*/
                    return !isDocUpdate
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
        /// 删除指定的文档信息
        /// </summary>
        /// <param name="helpDocId">文档ID</param>
        // DELETE: api/HelpDoc/5
        [HttpDelete]
        public IActionResult Delete(string helpDocId)
        {
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {
                    bool isDocDel = HelpCenter.BLL.HelpDoc.Delete(helpDocId);
                    return !isDocDel
                        ? Ok(new { result = false, tips = ResponseMessageTips.MSG_DOC_DELETE_FAIL })
                        : Ok(new { result = true, tips = ResponseMessageTips.MSG_DOC_DELETE_SUCCESS });
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
