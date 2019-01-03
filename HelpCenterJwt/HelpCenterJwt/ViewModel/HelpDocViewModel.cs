using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HelpCenterJwt.ViewModel
{
    public class HelpDocViewModel
    {
        /// <summary>
        /// 文档排序号码
        /// </summary>
        public int? DocNum;
        /// <summary>
        /// 文档分类ID
        /// </summary>
        [Required]
        public string DocTypeId;
        /// <summary>
        /// 文档标题
        /// </summary>
        [Required]
        public string DocTitle;
        /// <summary>
        /// 文档内容（包含html标签）
        /// </summary>
        [Required]
        public string DocContent;
        /// <summary>
        /// 文档纯文字内容（去除html标签）
        /// </summary>
        [Required]
        public string DocFullText;
        /// <summary>
        /// 文档所属部门
        /// </summary>
        [Required]
        public string DocDeptId;
        /// <summary>
        /// 共享部门ID列表
        /// </summary>
        public List<string> DocShareDeptList;
        /// <summary>
        /// 附件路径
        /// </summary>
        public string DocAttachment;
        /// <summary>
        /// 是否默认可见（游客权限）
        /// </summary>
        public bool IsDefaultRole;
        /// <summary>
        /// 允许查看的角色列表
        /// </summary>
        public List<string> viewRoleList;
        /// <summary>
        /// 允许编辑的角色列表
        /// </summary>
        public List<string> editRoleList;
    }
}
