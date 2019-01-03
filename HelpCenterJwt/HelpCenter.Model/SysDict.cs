using System;
namespace HelpCenter.Model
{
	/// <summary>
	/// sys_dict:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class SysDict
	{
		public SysDict()
		{}
		#region Model
		private string _dictid;
		private string _dicttypecode;
		private string _dicttypename;
		private string _dictcode;
		private string _dictname;
		private int? _dictstate;
		private int? _dictnum;
		private string _dictfbh;
		private string _dictremark;
		private DateTime? _crdt;
		private string _crusrid;
		private DateTime? _updt;
		private string _upusrid;
		/// <summary>
		/// 
		/// </summary>
		public string dictId
		{
			set{ _dictid=value;}
			get{return _dictid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string dictTypeCode
		{
			set{ _dicttypecode=value;}
			get{return _dicttypecode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string dictTypeName
		{
			set{ _dicttypename=value;}
			get{return _dicttypename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string dictCode
		{
			set{ _dictcode=value;}
			get{return _dictcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string dictName
		{
			set{ _dictname=value;}
			get{return _dictname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? dictState
		{
			set{ _dictstate=value;}
			get{return _dictstate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? dictNum
		{
			set{ _dictnum=value;}
			get{return _dictnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string dictFBH
		{
			set{ _dictfbh=value;}
			get{return _dictfbh;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string dictRemark
		{
			set{ _dictremark=value;}
			get{return _dictremark;}
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
		public string crUsrId
		{
			set{ _crusrid=value;}
			get{return _crusrid;}
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

