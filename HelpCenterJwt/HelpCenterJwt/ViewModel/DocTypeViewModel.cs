using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HelpCenterJwt.ViewModel
{
    public class DocTypeViewModel
    {
        /// <summary>
        /// 文档类型排序号
        /// </summary>
        public int? DocTypeNum;
        /// <summary>
        /// 上级分类ID
        /// </summary>
        [Required]
        public string DocPreTypeId;
        /// <summary>
        /// 文档类型名称
        /// </summary>
        [Required]
        public string DocTypeName;
        /// <summary>
        /// 上级部门ID
        /// </summary>
        [Required]
        public string DocTypeDeptId ;
    }
}
