using System;
namespace HelpCenter.Model
{
    /// <summary>
    /// role:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Role
    {
        public Role()
        { }
        #region Model
        private string _roleid;
        private string _rolename;
        private string _crUsrName;
        private string _upUsrName;
        private int? _rolestate;
        private string _roleRemark;
        private DateTime? _crdt;
        private string _crusrid;
        private DateTime? _updt;
        private string _upusrid;
        /// <summary>
        /// 
        /// </summary>
        public string roleId
        {
            set { _roleid = value; }
            get { return _roleid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string roleName
        {
            set { _rolename = value; }
            get { return _rolename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? roleState
        {
            set { _rolestate = value; }
            get { return _rolestate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? crdt
        {
            set { _crdt = value; }
            get { return _crdt; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string crUsrId
        {
            set { _crusrid = value; }
            get { return _crusrid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? updt
        {
            set { _updt = value; }
            get { return _updt; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string upUsrId
        {
            set { _upusrid = value; }
            get { return _upusrid; }
        }

        public string roleRemark { get => _roleRemark; set => _roleRemark = value; }
        /// <summary>
        /// 创建人名称
        /// </summary>
        public string CrUsrName { get => _crUsrName; set => _crUsrName = value; }
        /// <summary>
        /// 修改人名称
        /// </summary>
        public string UpUsrName { get => _upUsrName; set => _upUsrName = value; }
        #endregion Model

    }
}

