using System;
using System.Collections.Generic;
using System.Text;

namespace HelpCenter.Model
{
    /// <summary>
    /// 部门文档权限（共享部门）
    /// </summary>
    public class DocDeptRole
    {
        private string docDeptRoleId;
        private string docId;
        private string deptId;
        private int? docDeptState;
        private DateTime? crdt;
        private string crUsrId;
        private DateTime? _updt;
        private string _upUsrId;

        /// <summary>
        /// 文档部门权限ID
        /// </summary>
        public string DocDeptRoleId { get => docDeptRoleId; set => docDeptRoleId = value; }
        /// <summary>
        /// 文档ID
        /// </summary>
        public string DocId { get => docId; set => docId = value; }
        /// <summary>
        /// 部门ID
        /// </summary>
        public string DeptId { get => deptId; set => deptId = value; }
        /// <summary>
        /// 部门权限状态标识
        /// </summary>
        public int? DocDeptState { get => docDeptState; set => docDeptState = value; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? Crdt { get => crdt; set => crdt = value; }
        /// <summary>
        /// 创建人ID
        /// </summary>
        public string CrUsrId { get => crUsrId; set => crUsrId = value; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? Updt { get => _updt; set => _updt = value; }
        /// <summary>
        /// 修改人ID
        /// </summary>
        public string UpUsrId { get => _upUsrId; set => _upUsrId = value; }
    }
}
