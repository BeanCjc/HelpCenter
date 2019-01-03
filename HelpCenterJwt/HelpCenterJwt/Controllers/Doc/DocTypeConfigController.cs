using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelpCenter.ActionService;
using HelpCenter.BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HelpCenterJwt.Controllers.Doc
{
    /// <summary>
    /// 文档类型资料
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DocTypeConfigController : ControllerBase
    {
        /// <summary>
        /// 获取顶级文档类型清单
        /// </summary>
        /// <returns></returns>
        // GET: api/DocTypeConfig
        [HttpGet("toplist")]
        public IActionResult GetTopList()
        {
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {
                    var docTypeConfigList = DocTypeConfig.GetTopList();
                    return null == docTypeConfigList
                        ? Ok(new { result = false, tips = ResponseMessageTips.MSG_DOCTYPE_INFO_NO_FOUND })
                        : Ok(new { result = true, tips = ResponseMessageTips.MSG_PROCESS_SUCCESS, data = docTypeConfigList });
                }
                catch (Exception e)
                {
                    return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_EXCEPTION + e.Message.ToString() });
                }
            }

            return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_DATA_FORMAT_ERROR });
        }

        /// <summary>
        /// 获取首页文档清单
        /// </summary>
        /// <returns></returns>
        // GET: api/DocTypeConfig
        [HttpGet("homepage")]
        public IActionResult GetHomePageList()
        {
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {
                    var docTypeConfigList = DocTypeConfig.GetHomePageList(User.Identity.IsAuthenticated);
                    return null == docTypeConfigList
                        ? Ok(new { result = false, tips = ResponseMessageTips.MSG_DOCTYPE_INFO_NO_FOUND })
                        : Ok(new { result = true, tips = ResponseMessageTips.MSG_PROCESS_SUCCESS, data = docTypeConfigList });
                }
                catch (Exception e)
                {
                    return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_EXCEPTION + e.Message.ToString() });
                }
            }

            return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_DATA_FORMAT_ERROR });
        }

        /// <summary>
        /// 获取所有文档类型清单，以列表方式返回数据
        /// </summary>
        /// <returns></returns>
        // GET: api/DocTypeConfig
        [HttpGet("alllist")]
        public IActionResult GetAllList()
        {
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {
                    var docTypeConfigList = DocTypeConfig.GetAllList();
                    return null == docTypeConfigList
                        ? Ok(new { result = false, tips = ResponseMessageTips.MSG_DOCTYPE_INFO_NO_FOUND })
                        : Ok(new { result = true, tips = ResponseMessageTips.MSG_PROCESS_SUCCESS, data = docTypeConfigList });
                }
                catch (Exception e)
                {
                    return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_EXCEPTION + e.Message.ToString() });
                }
            }

            return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_DATA_FORMAT_ERROR });
        }

        /// <summary>
        /// 获取所有文档类型清单,以树节点方式返回数据
        /// </summary>
        /// <returns></returns>
        // GET: api/DocTypeConfig
        [HttpGet("alllisttree")]
        public IActionResult GetAllListTree()
        {
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {
                    var docTypeConfigList = DocTypeConfig.GetAllListTree();
                    return null == docTypeConfigList
                        ? Ok(new { result = false, tips = ResponseMessageTips.MSG_DOCTYPE_INFO_NO_FOUND })
                        : Ok(new { result = true, tips = ResponseMessageTips.MSG_PROCESS_SUCCESS, data = docTypeConfigList });
                }
                catch (Exception e)
                {
                    return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_EXCEPTION + e.Message.ToString() });
                }
            }

            return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_DATA_FORMAT_ERROR });
        }
        /// <summary>
        /// 获取所有文档类型清单（分页），提供指定页的数据、总数据量及总页数
        /// </summary>
        /// <param name="pageIndex">页码，从1开始</param>
        /// <param name="rowCount">每页数据量</param>
        /// <returns></returns>
        // GET: api/DocTypeConfig
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
                        var docTypeList = DocTypeConfig.GetList(pageIndex, rowCount, out int _totalCount, out int _pageCount,
                            this.User.Identities.First(u => u.IsAuthenticated).FindFirst("UsrDeptId").Value);
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
        /// 根据ID获取文档类型详细信息，并提供当前文档类型下的子类型列表
        /// </summary>
        /// <param name="docTypeId">文档类型ID</param>
        /// <returns></returns>
        // GET: api/DocTypeConfig/5
        [HttpGet]
        public IActionResult Get(string docTypeId)
        {
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {
                    var docTypeInfo = DocTypeConfig.GetModel(docTypeId);
                    var childNodes = DocTypeConfig.GetChildNodes(docTypeId);
                    return null == docTypeInfo
                        ? Ok(new { result = false, tips = ResponseMessageTips.MSG_DOCTYPE_INFO_NO_FOUND })
                        : Ok(new
                        {
                            result = true,
                            tips = ResponseMessageTips.MSG_PROCESS_SUCCESS,
                            data = new { detail = docTypeInfo, childNodes = childNodes }
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
        /// 获取所有文档类型清单（分页），提供指定页的数据、总数据量及总页数
        /// </summary>
        /// <param name="pageIndex">页码，从1开始</param>
        /// <param name="rowCount">每页数据量</param>
        /// <param name="strDocTypeName">文档类型名称</param>
        /// <returns></returns>
        // GET: api/DocTypeConfig
        [HttpGet("keyword/doctypename")]
        public IActionResult GetListByDocTypeName(int pageIndex, int rowCount,string strDocTypeName)
        {
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {
                    if (pageIndex > 0 && rowCount > 0)
                    {
                        int _totalCount, _pageCount = 0;
                        var docTypeList = DocTypeConfig.GetListByDocTypeName(pageIndex, rowCount, out _totalCount, out _pageCount, strDocTypeName);
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
        /// 根据文档分类ID获取所有文档类型清单（分页），提供指定页的数据、总数据量及总页数
        /// </summary>
        /// <param name="pageIndex">页码，从1开始</param>
        /// <param name="rowCount">每页数据量</param>
        /// <param name="docTypeID">文档分类ID</param>
        /// <returns></returns>
        // GET: api/DocTypeConfig
        [HttpGet("doctypelist/doctypeid")]
        public IActionResult GetListByDocTypeId(int pageIndex, int rowCount, string docTypeID)
        {
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {
                    if (pageIndex > 0 && rowCount > 0)
                    {

                        var docTypeList = DocTypeConfig.GetListByDocTypeId(pageIndex, rowCount, out int _totalCount, out int _pageCount, docTypeID);
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
        /// 新增文档类型信息
        /// </summary>
        /// <param name="docTypeViewModel"></param>
        /// <returns></returns>
        // POST: api/DocTypeConfig
        [HttpPost]
        public IActionResult Post([FromBody] ViewModel.DocTypeViewModel docTypeViewModel)
        {
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {
                    string strDocTypeInfo = string.Empty;
                    bool isDocTypeAdd = DocTypeConfig.Add(docTypeViewModel.DocTypeName, 1, docTypeViewModel.DocPreTypeId, docTypeViewModel.DocTypeDeptId, docTypeViewModel.DocTypeNum,
                        this.User.Identities.First(u => u.IsAuthenticated).FindFirst("UsrId").Value);
                    /*判断是否新增成功*/
                    return !isDocTypeAdd
                        ? Ok(new { result = false, tips = ResponseMessageTips.MSG_DOCTYPE_ADD_FAIL })
                        : Ok(new { result = true, tips = ResponseMessageTips.MSG_DOCTYPE_ADD_SUCCESS });
                }
                catch (Exception e)
                {
                    return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_EXCEPTION + e.Message.ToString() });
                }
            }

            return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_DATA_FORMAT_ERROR });
        }

        /// <summary>
        /// 更新指定文档类型信息
        /// </summary>
        /// <param name="docTypeId">指定文档类型ID</param>
        /// <param name="docTypeViewModel"></param>
        // PUT: api/DocTypeConfig/5
        [Authorize]
        [HttpPut]
        public IActionResult Put(string docTypeId, [FromBody] ViewModel.DocTypeViewModel docTypeViewModel)
        {
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {
                    bool isDocTypeUpdate = DocTypeConfig.Update(docTypeId, docTypeViewModel.DocTypeName, 1,
                        docTypeViewModel.DocPreTypeId, docTypeViewModel.DocTypeDeptId, docTypeViewModel.DocTypeNum, 
                        this.User.Identities.First(u => u.IsAuthenticated).FindFirst("UsrId").Value);
                    /*判断是否修改成功*/
                    return !isDocTypeUpdate
                        ? Ok(new { result = false, tips = ResponseMessageTips.MSG_DOCTYPE_UPDATE_FAIL })
                        : Ok(new { result = true, tips = ResponseMessageTips.MSG_DOCTYPE_UPDATE_SUCCESS });
                }
                catch (Exception e)
                {
                    return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_EXCEPTION + e.Message.ToString() });
                }
            }

            return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_DATA_FORMAT_ERROR });
        }

        /// <summary>
        /// 删除指定的文档类型信息
        /// </summary>
        /// <param name="docTypeId">文档类型ID</param>
        // DELETE: api/DocTypeConfig/5
        [Authorize]
        [HttpDelete]
        public IActionResult Delete(string docTypeId)
        {
            /*判断是否合法*/
            if (ModelState.IsValid)
            {
                try
                {
                    bool isDocTypeDel = DocTypeConfig.Delete(docTypeId,out string strTips);
                    return Ok(new { result = isDocTypeDel, tips = strTips });
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
