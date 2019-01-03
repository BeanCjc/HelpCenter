using System;
using System.Collections;

namespace HelpCenter.Model
{
	/// <summary>
	/// dept:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Dept
	{
		public Dept()
		{}
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
	    private string _deptAccount;
        private string _deptPsw;
        /// <summary>
        /// 
        /// </summary>
        public string deptId
		{
			set{ _deptid=value;}
			get{return _deptid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string deptNO
		{
			set{ _deptno=value;}
			get{return _deptno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string deptName
		{
			set{ _deptname=value;}
			get{return _deptname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? deptState
		{
			set{ _deptstate=value;}
			get{return _deptstate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string preDeptId
		{
			set{ _predeptid=value;}
			get{return _predeptid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string crUsrId
		{
			set{ _crusrid=value;}
			get{return _crusrid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? crdt
		{
			set{ _crdt=value;}
			get{return _crdt;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? updt
		{
			set{ _updt=value;}
			get{return _updt;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string upUsrId
		{
			set{ _upusrid=value;}
			get{return _upusrid;}
		}

        /// <summary>
        /// 包含子节点的数量
        /// </summary>
	    public int childCount { get; set; }
        /// <summary>
        /// 子节点列表
        /// </summary>
	    public string[] child { get; set; }
        /// <summary>
        /// 部门账号
        /// </summary>
        public string deptAccount { get => _deptAccount; set => _deptAccount = value; }

        /// <summary>
        /// 部门密码
        /// </summary>
        public string deptPsw { get => _deptPsw; set => _deptPsw = value; }

        #endregion Model

    }
}

