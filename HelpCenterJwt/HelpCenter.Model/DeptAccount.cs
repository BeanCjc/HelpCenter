using System;
namespace HelpCenter.Model
{
	/// <summary>
	/// deptaccount:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class DeptAccount
	{
		public DeptAccount()
		{}
		#region Model
		private string _deptaccountid;
		private string _deptaccount;
		private string _deptpsw;
		private string _deptid;
		private string _crusrid;
		private DateTime? _crdt;
		private DateTime? _updt;
		private string _upusrid;
		/// <summary>
		/// 
		/// </summary>
		public string deptAccountId
		{
			set{ _deptaccountid=value;}
			get{return _deptaccountid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string deptAccount
		{
			set{ _deptaccount=value;}
			get{return _deptaccount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string deptPsw
		{
			set{ _deptpsw=value;}
			get{return _deptpsw;}
		}
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
		#endregion Model

	}
}

