using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HelpCenterJwt.ViewModel
{
    public class DocRoleViewModel
    {
        private string roleId;
        private string[] docIdList;

        /// <summary>
        /// 文档ID
        /// </summary>
        [Required]
        public string RoleId { get => roleId; set => roleId = value; }
        /// <summary>
        /// 允许该角色查看的文档列表
        /// </summary>
        public string[] DocIdList { get => docIdList; set => docIdList = value; }
    }
}
