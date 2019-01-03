using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelpCenter.Model
{
    /// <summary>
    /// 文档角色权限控制
    /// </summary>
    [Table("docrolectrl")]
    public class DocRoleCtrl
    {
        private int id;
        private string _docId;
        private string _roleId;
        private int _ctrlType;
        private string _crUsrId;
        private DateTime _crdt;
        private DateTime _updt;
        private string _upUsrId;

        /// <summary>
        /// 控制ID
        /// </summary>
        [Key]
        public int Id { get => id; set => id = value; }
        /// <summary>
        /// 文档编号
        /// </summary>
        public string DocId { get => _docId; set => _docId = value; }
        /// <summary>
        /// 角色编号
        /// </summary>
        public string RoleId { get => _roleId; set => _roleId = value; }
        /// <summary>
        /// 控制类型
        /// </summary>
        public int CtrlType { get => _ctrlType; set => _ctrlType = value; }
        /// <summary>
        /// 创建人ID
        /// </summary>
        public string CrUsrId { get => _crUsrId; set => _crUsrId = value; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Crdt { get => _crdt; set => _crdt = value; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime Updt { get => _updt; set => _updt = value; }
        /// <summary>
        /// 修改人ID
        /// </summary>
        public string UpUsrId { get => _upUsrId; set => _upUsrId = value; }
    }
}
