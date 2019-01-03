using System;
using System.Collections.Generic;
using System.Text;

namespace HelpCenter.Model.DbModel
{
    public class DbDeptModel
    {
        #region Model
        private string _deptid;
        private string _deptno;
        private string _deptname;
        private int? _deptstate;
        private string _predeptid;
        private string _crusrid;
        private DateTime? _crdt;
        private DateTime? _updt;
        private string _upusrid;
        private string _preDeptName;
        private string _deptAccount;
        private int? _childCount;
        private int? _usrType;
        private int? _usrstate;
        /// <summary>
        /// 
        /// </summary>
        public string deptId
        {
            set { _deptid = value; }
            get { return _deptid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string deptNO
        {
            set { _deptno = value; }
            get { return _deptno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string deptName
        {
            set { _deptname = value; }
            get { return _deptname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? deptState
        {
            set { _deptstate = value; }
            get { return _deptstate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string preDeptId
        {
            set { _predeptid = value; }
            get { return _predeptid; }
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
        public DateTime? crdt
        {
            set { _crdt = value; }
            get { return _crdt; }
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

        public int? childCount { get => _childCount; set => _childCount = value; }

        /// <summary>
        /// 子节点列表
        /// </summary>
        public string[] child { get; set; }
        /// <summary>
        /// 上级部门名称
        /// </summary>
        public string preDeptName { get => _preDeptName; set => _preDeptName = value; }
        /// <summary>
        /// 部门账号
        /// </summary>
        public string deptAccount { get => _deptAccount; set => _deptAccount = value; }
        /// <summary>
        /// 用户类型
        /// </summary>
        public int? usrType { get => _usrType; set => _usrType = value; }
        /// <summary>
        /// 用户状态
        /// </summary>
        public int? usrState { get => _usrstate; set => _usrstate = value; }

        #endregion Model
    }
}
