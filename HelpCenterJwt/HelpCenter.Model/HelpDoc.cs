using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace HelpCenter.Model
{
	/// <summary>
	/// helpdoc:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class HelpDoc
    {
        public HelpDoc()
        {
        }
		#region Model
		private string _docid;
		private DateTime? _crdt;
		private DateTime? _updt;
		private string _doctypeid;
        private string _docTypeName;
        private string _doctitle;
		private string _doccontent;
		private string _docfulltext;
		private int? _docnum;
		private string _docdeptid;
        private string _docDeptName;
        private int? _doccount;
		private string _crusrid;
		private string _upusrid;
		private int? _docstate;
        private string _docAttachment;
        private string _crUsrName;
        private bool _isDefaultRole;
        private List<string> _viewRoleList;
        private List<string> _editRoleList;
        private bool _editRole = false;
        /// <summary>
        /// 
        /// </summary>
        public string docId
		{
			set{ _docid=value;}
			get{return _docid;}
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
		public string docTypeId
		{
			set{ _doctypeid=value;}
			get{return _doctypeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string docTitle
		{
			set{ _doctitle=value;}
			get{return _doctitle;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string docContent
		{
			set{ _doccontent= ProcessContent(value); }
			get{return _doccontent;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string docFullText
		{
			set{ _docfulltext=value;}
			get{return _docfulltext;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? docNum
		{
			set{ _docnum=value;}
			get{return _docnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string docDeptId
		{
			set{ _docdeptid=value;}
			get{return _docdeptid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? docCount
		{
			set{ _doccount=value;}
			get{return _doccount;}
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
		public string upUsrId
		{
			set{ _upusrid=value;}
			get{return _upusrid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? docState
		{
			set{ _docstate=value;}
			get{return _docstate;}
		}
        /// <summary>
        /// 文档分类名称
        /// </summary>
        public string docTypeName { get => _docTypeName; set => _docTypeName = value; }
        /// <summary>
        /// 文档所属部门
        /// </summary>
        public string docDeptName { get => _docDeptName; set => _docDeptName = value; }
        /// <summary>
        /// 附件路径
        /// </summary>
        public string docAttachment { get => _docAttachment; set => _docAttachment = value; }
        /// <summary>
        /// 文档创建人名称
        /// </summary>
        public string crUsrName { get => _crUsrName; set => _crUsrName = value; }
        /// <summary>
        /// 游客是否能查阅
        /// </summary>
        public bool IsDefaultRole { get => _isDefaultRole; set => _isDefaultRole = value; }
        public List<string> viewRoleList { get => _viewRoleList; set => _viewRoleList = value; }
        public List<string> editRoleList { get => _editRoleList; set => _editRoleList = value; }
        public bool editRole { get => _editRole; set => _editRole = value; }
        #endregion Model

        /// <summary>
        /// 处理文档中的base64图片，保存到服务器目录并转化成路径
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        private string ProcessContent(string strValue)
        {
            if (!string.IsNullOrEmpty(strValue))
            {
                Regex defaultRegex = new Regex("\"(data:image/(jpeg|jpg|png|gif);base64,.+?)\"");
                MatchCollection matches;

                matches = defaultRegex.Matches(strValue);
                // Iterate matches
                for (int ctr = 0; ctr < matches.Count; ctr++)
                {
                    string path = SaveImage(matches[ctr].Value.Split(',')[1], Guid.NewGuid().ToString());
                    strValue = strValue.Replace(matches[ctr].Value, "\"" + path + "\"");
                }
            }
            return strValue;
        }

        /// <summary>
        ///  将echarts返回的base64 转成图片
        /// </summary>
        /// <param name="image">图片的base64形式</param>
        /// <param name="proname">项目区分</param>
        public string SaveImage(string image, string proname)
        {
            string path = @"\upload\" + DateTime.Now.ToString("yyyy-MM-dd") + @"\"+ $"{proname}.png";
            string filepath = Path.GetDirectoryName(Directory.GetCurrentDirectory() + @"\wwwroot"+path);
            // 如果不存在就创建file文件夹
            if (!Directory.Exists(filepath))
            {
                if (filepath != null) Directory.CreateDirectory(filepath);
            }
            image = image.Remove(image.Length - 1, 1);
            var photoBytes = Convert.FromBase64String(image);
            File.WriteAllBytes(Directory.GetCurrentDirectory() + @"\wwwroot" + path, photoBytes);
            return path;
        }
    }
}

