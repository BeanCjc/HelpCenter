using System;
using System.Collections.Generic;
using System.Text;

namespace HelpCenter.Model.DbModel
{
    public class DbDocTypeConfigModel
    {
        private string crdt;
        private string docTypeId;
        private string docTypeNum;
        private string docPreTypeId;
        private string docPreTypeName;
        private string docTypeName;
        private string docTypeDeptId;
        private string deptName;

        /// <summary>
        /// 文档类型创建时间
        /// </summary>
        public string Crdt { get => crdt; set => crdt = value; }
        /// <summary>
        /// 文档类型ID
        /// </summary>
        public string DocTypeId { get => docTypeId; set => docTypeId = value; }

        public string DocTypeNum { get => docTypeNum; set => docTypeNum = value; }
        public string DocPreTypeId { get => docPreTypeId; set => docPreTypeId = value; }
        public string DocPreTypeName { get => docPreTypeName; set => docPreTypeName = value; }
        public string DocTypeName { get => docTypeName; set => docTypeName = value; }
        public string DocTypeDeptId { get => docTypeDeptId; set => docTypeDeptId = value; }
        public string DeptName { get => deptName; set => deptName = value; }
    }
}
