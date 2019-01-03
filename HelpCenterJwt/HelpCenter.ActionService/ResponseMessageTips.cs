using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace HelpCenter.ActionService
{
    public class ResponseMessageTips
    {
        #region 请求相关

        /*请求相关*/

        /// <summary>
        /// 请求处理异常.
        /// </summary>
        public static string MSG_PROCESS_EXCEPTION = "请求处理异常.";

        /// <summary>
        /// 请求处理成功.
        /// </summary>
        public static string MSG_PROCESS_SUCCESS = "请求处理成功.";

        /// <summary>
        /// 请求数据格式不正确.
        /// </summary>
        public static string MSG_PROCESS_DATA_FORMAT_ERROR = "请求数据格式不正确.";

        /// <summary>
        /// 登录超时.
        /// </summary>
        public static string MSG_LOGIN_TIMEOUT = "登录超时,请重新登录.";

        /// <summary>
        /// 请求处理异常.
        /// </summary>
        public static string MSG_AUTHORIZED_SUCCESS = "验证成功.";
        /// <summary>
        /// 验证失败.
        /// </summary>
        public static string MSG_AUTHORIZED_FAILED = "验证失败.";

        /// <summary>
        /// 权限不足.
        /// </summary>
        public static string MSG_INSUFFICIENT_PRIVILEGES = "权限不足.";

        /// <summary>
        /// 该账号不可用.
        /// </summary>
        public static string MSG_ACCOUNT_DISABLE = "该账号不可用.";

        /// <summary>
        /// 当前暂无数据.
        /// </summary>
        public static string MSG_NO_DATA = "当前暂无数据.";

        #endregion 请求相关

        #region 字典资料相关

        /*字典资料相关*/

        /// <summary>
        /// 新增字典资料成功.
        /// </summary>
        public static string MSG_SYSDICT_ADD_SUCCESS = "新增字典资料成功.";

        /// <summary>
        /// 字典资料已经存在.
        /// </summary>
        public static string MSG_SYSDICT_IS_EXIST = "字典资料已经存在.";

        /// <summary>
        /// 新增字典资料失败.
        /// </summary>
        public static string MSG_SYSDICT_ADD_FAIL = "新增字典资料失败.";

        /// <summary>
        /// 更新字典资料成功.
        /// </summary>
        public static string MSG_SYSDICT_UPDATE_SUCCESS = "更新字典资料成功.";

        /// <summary>
        /// 更新字典资料失败.
        /// </summary>
        public static string MSG_SYSDICT_UPDATE_FAIL = "更新字典资料失败.";

        /// <summary>
        /// 转字典资料成功.
        /// </summary>
        public static string MSG_SYSDICT_TRANSFER_SUCCESS = "转字典资料成功.";

        /// <summary>
        /// 转字典资料失败.
        /// </summary>
        public static string MSG_SYSDICT_TRANSFER_FAIL = "转字典资料失败.";

        /// <summary>
        /// 没有更新当前字典资料的权限.
        /// </summary>
        public static string MSG_SYSDICT_UPDATE_INSUFFICIENT_PRIVILEGES = "没有更新当前字典资料的权限.";

        /// <summary>
        /// 未找到该字典资料
        /// </summary>
        public static string MSG_SYSDICT_INFO_NO_FOUND = "未找到该字典资料.";

        /// <summary>
        /// 删除字典资料成功.
        /// </summary>
        public static string MSG_SYSDICT_DELETE_SUCCESS = "删除字典资料成功.";

        /// <summary>
        /// 删除字典资料失败
        /// </summary>
        public static string MSG_SYSDICT_DELETE_FAIL = "删除字典资料失败.";

        /// <summary>
        /// 无字典备注信息
        /// </summary>
        public static string MSG_SYSDICT_HISTORY_REMARK_INFO_NO_FOUND = "无历史字典备注信息.";

        /// <summary>
        /// 导入的EXCEL格式不正确
        /// </summary>
        public static string MSG_SYSDICT_IMPORT_EXCEL_DATA_FORMAT_FAIL = "导入的EXCEL格式不正确.";
        /// <summary>
        /// 未读取到记录
        /// </summary>
        public static string MSG_SYSDICT_IMPORT_EXCEL_DATA_READ_FAIL = "未读取到记录.";

        #endregion 字典资料相关

        #region 用户资料相关

        /*用户资料相关*/

        /// <summary>
        /// 新增用户资料成功.
        /// </summary>
        public static string MSG_USR_ADD_SUCCESS = "注册成功，正在审核...";

        /// <summary>
        /// 帐号或密码错误.
        /// </summary>
        public static string MSG_USR_PWD_ERR = "帐号或密码错误.";

        /// <summary>
        /// 用户资料已经存在.
        /// </summary>
        public static string MSG_USR_IS_EXIST = "该帐号已注册，如果忘记密码请前往重置密码.";

        /// <summary>
        /// 新增用户资料失败.
        /// </summary>
        public static string MSG_USR_ADD_FAIL = "新增用户资料失败.";

        /// <summary>
        /// 更新用户资料成功.
        /// </summary>
        public static string MSG_USR_UPDATE_SUCCESS = "更新用户资料成功.";

        /// <summary>
        /// 更新用户资料失败.
        /// </summary>
        public static string MSG_USR_UPDATE_FAIL = "更新用户资料失败.";

        /// <summary>
        /// 转用户资料成功.
        /// </summary>
        public static string MSG_USR_TRANSFER_SUCCESS = "转用户资料成功.";

        /// <summary>
        /// 转用户资料失败.
        /// </summary>
        public static string MSG_USR_TRANSFER_FAIL = "转用户资料失败.";

        /// <summary>
        /// 没有更新当前用户资料的权限.
        /// </summary>
        public static string MSG_USR_UPDATE_INSUFFICIENT_PRIVILEGES = "没有更新当前用户资料的权限.";

        /// <summary>
        /// 未找到该用户资料
        /// </summary>
        public static string MSG_USR_INFO_NO_FOUND = "帐号不存在，请前往注册.";

        /// <summary>
        /// 删除用户资料成功.
        /// </summary>
        public static string MSG_USR_DELETE_SUCCESS = "删除用户资料成功.";

        /// <summary>
        /// 删除用户资料失败
        /// </summary>
        public static string MSG_USR_DELETE_FAIL = "删除用户资料失败.";

        /// <summary>
        /// 无用户备注信息
        /// </summary>
        public static string MSG_USR_HISTORY_REMARK_INFO_NO_FOUND = "无历史用户备注信息.";

        /// <summary>
        /// 导入的EXCEL格式不正确
        /// </summary>
        public static string MSG_USR_IMPORT_EXCEL_DATA_FORMAT_FAIL = "导入的EXCEL格式不正确.";
        /// <summary>
        /// 未读取到记录
        /// </summary>
        public static string MSG_USR_IMPORT_EXCEL_DATA_READ_FAIL = "未读取到记录.";

        #endregion 用户资料相关

        #region 部门架构相关

        /*部门架构相关*/

        /// <summary>
        /// 新增部门资料成功.
        /// </summary>
        public static string MSG_DEPT_ADD_SUCCESS = "新增部门资料成功.";

        /// <summary>
        /// 部门账号已经存在.
        /// </summary>
        public static string MSG_DEPT_ACCOUNT_IS_EXIST = "部门账号已经存在.";

        /// <summary>
        /// 新增部门资料失败.
        /// </summary>
        public static string MSG_DEPT_ADD_FAIL = "新增部门资料失败.";

        /// <summary>
        /// 更新部门资料成功.
        /// </summary>
        public static string MSG_DEPT_UPDATE_SUCCESS = "更新部门资料成功.";

        /// <summary>
        /// 更新部门资料失败.
        /// </summary>
        public static string MSG_DEPT_UPDATE_FAIL = "更新部门资料失败.";

        /// <summary>
        /// 没有更新当前部门资料的权限.
        /// </summary>
        public static string MSG_DEPT_UPDATE_INSUFFICIENT_PRIVILEGES = "没有更新当前部门资料的权限.";

        /// <summary>
        /// 未找到该部门资料
        /// </summary>
        public static string MSG_DEPT_INFO_NO_FOUND = "未找到该部门资料.";

        /// <summary>
        /// 删除部门资料成功.
        /// </summary>
        public static string MSG_DEPT_DELETE_SUCCESS = "删除部门资料成功.";

        /// <summary>
        /// 删除部门资料失败
        /// </summary>
        public static string MSG_DEPT_DELETE_FAIL = "删除部门资料失败，请检查是否有下级部门及绑定的文档.";

        /// <summary>
        /// 有子部门不允许删除
        /// </summary>
        public static string MSG_DEPT_HAS_CHILD_DEL_FAIL = "删除部门资料失败，有子部门不允许删除.";

        /// <summary>
        /// 有绑定文档关系不允许删除
        /// </summary>
        public static string MSG_DEPT_HAS_BIND_DOC_DEL_FAIL = "删除部门资料失败，有绑定文档关系不允许删除.";

        /// <summary>
        /// 部门底下有员工不允许删除
        /// </summary>
        public static string MSG_DEPT_HAS_BIND_USER_DEL_FAIL = "删除部门资料失败，部门底下有员工不允许删除.";

        /// <summary>
        /// 无部门备注信息
        /// </summary>
        public static string MSG_DEPT_HISTORY_REMARK_INFO_NO_FOUND = "无部门备注信息.";

        /// <summary>
        /// 导入的EXCEL格式不正确
        /// </summary>
        public static string MSG_DEPT_IMPORT_EXCEL_DATA_FORMAT_FAIL = "导入的EXCEL格式不正确.";
        /// <summary>
        /// 未读取到记录
        /// </summary>
        public static string MSG_DEPT_IMPORT_EXCEL_DATA_READ_FAIL = "未读取到记录.";

        #endregion 用户资料相关

        #region 帮助文档相关

        /*帮助文档相关*/

        /// <summary>
        /// 新增文档资料成功.
        /// </summary>
        public static string MSG_DOC_ADD_SUCCESS = "新增文档资料成功.";

        /// <summary>
        /// 文档资料已经存在.
        /// </summary>
        public static string MSG_DOC_IS_EXIST = "文档资料已经存在.";

        /// <summary>
        /// 新增文档资料失败.
        /// </summary>
        public static string MSG_DOC_ADD_FAIL = "新增文档资料失败.";

        /// <summary>
        /// 更新文档资料成功.
        /// </summary>
        public static string MSG_DOC_UPDATE_SUCCESS = "更新文档资料成功.";

        /// <summary>
        /// 更新文档资料失败.
        /// </summary>
        public static string MSG_DOC_UPDATE_FAIL = "更新文档资料失败.";

        /// <summary>
        /// 没有更新当前文档资料的权限.
        /// </summary>
        public static string MSG_DOC_UPDATE_INSUFFICIENT_PRIVILEGES = "没有更新当前文档资料的权限.";

        /// <summary>
        /// 未找到该文档资料
        /// </summary>
        public static string MSG_DOC_INFO_NO_FOUND = "未找到该文档资料.";

        /// <summary>
        /// 删除文档资料成功.
        /// </summary>
        public static string MSG_DOC_DELETE_SUCCESS = "删除文档资料成功.";

        /// <summary>
        /// 删除文档资料失败
        /// </summary>
        public static string MSG_DOC_DELETE_FAIL = "删除文档资料失败.";

        /// <summary>
        /// 无文档备注信息
        /// </summary>
        public static string MSG_DOC_HISTORY_REMARK_INFO_NO_FOUND = "无文档备注信息.";

        /// <summary>
        /// 导入的EXCEL格式不正确
        /// </summary>
        public static string MSG_DOC_IMPORT_EXCEL_DATA_FORMAT_FAIL = "导入的EXCEL格式不正确.";
        /// <summary>
        /// 未读取到记录
        /// </summary>
        public static string MSG_DOC_IMPORT_EXCEL_DATA_READ_FAIL = "未读取到记录.";

        #endregion 帮助文档相关

        #region 帮助文档类型相关

        /*帮助文档类型相关*/
        /// <summary>
        /// 新增文档类型资料成功.
        /// </summary>
        public static string MSG_DOCTYPE_ADD_SUCCESS = "新增文档类型资料成功.";

        /// <summary>
        /// 文档类型资料已经存在.
        /// </summary>
        public static string MSG_DOCTYPE_IS_EXIST = "文档类型资料已经存在.";

        /// <summary>
        /// 新增文档类型资料失败.
        /// </summary>
        public static string MSG_DOCTYPE_ADD_FAIL = "新增文档类型资料失败.";

        /// <summary>
        /// 更新文档类型资料成功.
        /// </summary>
        public static string MSG_DOCTYPE_UPDATE_SUCCESS = "更新文档类型资料成功.";

        /// <summary>
        /// 更新文档类型资料失败.
        /// </summary>
        public static string MSG_DOCTYPE_UPDATE_FAIL = "更新文档类型资料失败.";

        /// <summary>
        /// 没有更新当前文档类型资料的权限.
        /// </summary>
        public static string MSG_DOCTYPE_UPDATE_INSUFFICIENT_PRIVILEGES = "没有更新当前文档类型资料的权限.";

        /// <summary>
        /// 未找到该文档类型资料
        /// </summary>
        public static string MSG_DOCTYPE_INFO_NO_FOUND = "未找到该文档类型资料.";

        /// <summary>
        /// 删除文档类型资料成功.
        /// </summary>
        public static string MSG_DOCTYPE_DELETE_SUCCESS = "删除文档类型资料成功.";

        /// <summary>
        /// 删除文档类型资料失败
        /// </summary>
        public static string MSG_DOCTYPE_DELETE_FAIL = "删除文档类型资料失败.";

        /// <summary>
        /// 文档类型下有绑定的文档
        /// </summary>
        public static string MSG_DOCTYPE_HAS_BIND_DOC = "该文档类型下有绑定的文档，不能删除.";

        /// <summary>
        /// 文档类型下有子类型
        /// </summary>
        public static string MSG_DOCTYPE_HAS_CHILD = "该文档类型下有子类型，不能删除.";

        /// <summary>
        /// 无文档类型备注信息
        /// </summary>
        public static string MSG_DOCTYPE_HISTORY_REMARK_INFO_NO_FOUND = "无文档类型备注信息.";

        /// <summary>
        /// 导入的EXCEL格式不正确
        /// </summary>
        public static string MSG_DOCTYPE_IMPORT_EXCEL_DATA_FORMAT_FAIL = "导入的EXCEL格式不正确.";
        /// <summary>
        /// 未读取到记录
        /// </summary>
        public static string MSG_DOCTYPE_IMPORT_EXCEL_DATA_READ_FAIL = "未读取到记录.";

        #endregion 帮助文档类型相关

        #region 角色相关

        /*角色相关*/

        /// <summary>
        /// 新增角色成功.
        /// </summary>
        public static string MSG_ROLE_ADD_SUCCESS = "新增角色成功.";

        /// <summary>
        /// 角色已经存在.
        /// </summary>
        public static string MSG_ROLE_IS_EXIST = "角色已经存在.";

        /// <summary>
        /// 新增角色失败.
        /// </summary>
        public static string MSG_ROLE_ADD_FAIL = "新增角色失败.";

        /// <summary>
        /// 更新角色成功.
        /// </summary>
        public static string MSG_ROLE_UPDATE_SUCCESS = "更新角色成功.";

        /// <summary>
        /// 更新角色失败.
        /// </summary>
        public static string MSG_ROLE_UPDATE_FAIL = "更新角色失败.";

        /// <summary>
        /// 转角色成功.
        /// </summary>
        public static string MSG_ROLE_TRANSFER_SUCCESS = "转角色成功.";

        /// <summary>
        /// 转角色失败.
        /// </summary>
        public static string MSG_ROLE_TRANSFER_FAIL = "转角色失败.";

        /// <summary>
        /// 没有更新当前角色的权限.
        /// </summary>
        public static string MSG_ROLE_UPDATE_INSUFFICIENT_PRIVILEGES = "没有更新当前角色的权限.";

        /// <summary>
        /// 未找到该角色
        /// </summary>
        public static string MSG_ROLE_INFO_NO_FOUND = "未找到该角色.";

        /// <summary>
        /// 删除角色成功.
        /// </summary>
        public static string MSG_ROLE_DELETE_SUCCESS = "删除角色成功.";

        /// <summary>
        /// 删除角色失败
        /// </summary>
        public static string MSG_ROLE_DELETE_FAIL = "删除角色失败.";

        /// <summary>
        /// 无用户备注信息
        /// </summary>
        public static string MSG_ROLE_HISTORY_REMARK_INFO_NO_FOUND = "无历史用户备注信息.";

        /// <summary>
        /// 导入的EXCEL格式不正确
        /// </summary>
        public static string MSG_ROLE_IMPORT_EXCEL_DATA_FORMAT_FAIL = "导入的EXCEL格式不正确.";
        /// <summary>
        /// 未读取到记录
        /// </summary>
        public static string MSG_ROLE_IMPORT_EXCEL_DATA_READ_FAIL = "未读取到记录.";

        #endregion 角色相关

        #region 文档角色权限相关

        /*文档角色权限相关*/
        /// <summary>
        /// 关联文档角色成功.
        /// </summary>
        public static string MSG_DOCUSRROLE_ADD_SUCCESS = "关联文档角色成功.";
        /// <summary>
        /// 关联文档角色失败.
        /// </summary>
        public static string MSG_DOCUSRROLE_ADD_FAIL = "关联文档角色失败.";
        /// <summary>
        /// 更新文档角色成功.
        /// </summary>
        public static string MSG_DOCUSRROLE_UPDATE_SUCCESS = "更新文档角色成功.";
        /// <summary>
        /// 更新文档角色成功.
        /// </summary>
        public static string MSG_DOCUSRROLE_UPDATE_FAIL = "更新文档角色失败.";
        /// <summary>
        /// 删除文档角色成功.
        /// </summary>
        public static string MSG_DOCUSRROLE_DEL_SUCCESS = "删除文档角色成功.";
        /// <summary>
        /// 删除文档角色成功.
        /// </summary>
        public static string MSG_DOCUSRROLE_DEL_FAIL = "删除文档角色失败.";
        /// <summary>
        /// 文档角色关系不存在.
        /// </summary>
        public static string MSG_DOCUSRROLE_NO_FOUND = "文档角色关系不存在.";

        #endregion 文档角色权限相关

        #region 用户角色权限相关

        /*用户角色权限相关*/
        /// <summary>
        /// 关联用户角色成功.
        /// </summary>
        public static string MSG_USRROLE_ADD_SUCCESS = "关联用户角色成功.";
        /// <summary>
        /// 关联用户角色失败.
        /// </summary>
        public static string MSG_USRROLE_ADD_FAIL = "关联用户角色失败.";
        /// <summary>
        /// 更新用户角色成功.
        /// </summary>
        public static string MSG_USRROLE_UPDATE_SUCCESS = "更新用户角色成功.";
        /// <summary>
        /// 更新用户角色成功.
        /// </summary>
        public static string MSG_USRROLE_UPDATE_FAIL = "更新用户角色失败.";
        /// <summary>
        /// 删除用户角色成功.
        /// </summary>
        public static string MSG_USRROLE_DEL_SUCCESS = "删除用户角色成功.";
        /// <summary>
        /// 删除用户角色成功.
        /// </summary>
        public static string MSG_USRROLE_DEL_FAIL = "删除用户角色失败.";
        /// <summary>
        /// 用户角色关系不存在.
        /// </summary>
        public static string MSG_USRROLE_NO_FOUND = "用户角色关系不存在.";

        #endregion 用户角色权限相关

        #region 用户、部门架构相关

        /// <summary>
        /// 未找到部门架构.
        /// </summary>
        public static string MSG_USER_DEPT_NO_FOUND = "未找到部门架构.";
        /// <summary>
        /// 未找到用户信息
        /// </summary>
        public static string MSG_USER_NO_FOUND = "未通过验证,请检查用户名密码或联系管理员.";
        /// <summary>
        /// 旧密码不正确
        /// </summary>
        public static string MSG_USER_PWD_MATCHOLD_FAIL = "旧密码不正确.";
        /// <summary>
        /// 新密码两次输入不一致
        /// </summary>
        public static string MSG_USER_PWD_MATCHNEW_FAIL = "新密码两次输入不一致.";
        /// <summary>
        /// 密码不能为空
        /// </summary>
        public static string MSG_USER_PWD_MATCH_NULL = "密码不能为空.";
        /// <summary>
        /// 密码修改成功
        /// </summary>
        public static string MSG_USER_PWD_SUCCESS = "密码修改成功.";
        /// <summary>
        /// 密码修改失败
        /// </summary>
        public static string MSG_USER_PWD_FAIL = "密码修改失败.";


        #endregion 用户、部门架构相关

        #region 文件操作相关

        /// <summary>
        /// 文件上传成功.
        /// </summary>
        public static string MSG_FILE_UPLOAD_SUCCESS = "文件上传成功.";

        /// <summary>
        /// 文件上传失败.
        /// </summary>
        public static string MSG_FILE_UPLOAD_FAIL = "文件上传失败.";

        #endregion 文件操作相关

        #region 系统模块相关

        /*系统模块相关*/

        /// <summary>
        /// 新增系统模块成功.
        /// </summary>
        public static string MSG_SYSMODULE_ADD_SUCCESS = "新增系统模块成功.";

        /// <summary>
        /// 系统模块已经存在.
        /// </summary>
        public static string MSG_SYSMODULE_IS_EXIST = "系统模块已经存在.";

        /// <summary>
        /// 新增系统模块失败.
        /// </summary>
        public static string MSG_SYSMODULE_ADD_FAIL = "新增系统模块失败.";

        /// <summary>
        /// 更新系统模块成功.
        /// </summary>
        public static string MSG_SYSMODULE_UPDATE_SUCCESS = "更新系统模块成功.";

        /// <summary>
        /// 更新系统模块失败.
        /// </summary>
        public static string MSG_SYSMODULE_UPDATE_FAIL = "更新系统模块失败.";

        /// <summary>
        /// 没有更新当前系统模块的权限.
        /// </summary>
        public static string MSG_SYSMODULE_UPDATE_INSUFFICIENT_PRIVILEGES = "没有更新当前系统模块的权限.";

        /// <summary>
        /// 未找到该系统模块
        /// </summary>
        public static string MSG_SYSMODULE_INFO_NO_FOUND = "未找到该系统模块.";

        /// <summary>
        /// 删除系统模块成功.
        /// </summary>
        public static string MSG_SYSMODULE_DELETE_SUCCESS = "删除系统模块成功.";

        /// <summary>
        /// 删除系统模块失败
        /// </summary>
        public static string MSG_SYSMODULE_DELETE_FAIL = "删除系统模块失败.";

        /// <summary>
        /// 无系统模块备注信息
        /// </summary>
        public static string MSG_SYSMODULE_HISTORY_REMARK_INFO_NO_FOUND = "无历史系统模块备注信息.";

        /// <summary>
        /// 未读取到记录
        /// </summary>
        public static string MSG_SYSMODULE_IMPORT_EXCEL_DATA_READ_FAIL = "未读取到记录.";

        #endregion 系统模块相关
    }
}
