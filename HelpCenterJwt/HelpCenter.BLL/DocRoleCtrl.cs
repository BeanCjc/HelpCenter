using System;
using System.Collections.Generic;
using System.Text;

namespace HelpCenter.BLL
{
    public class DocRoleCtrl
    {
        public static bool Save()
        {
            List<Model.DocRoleCtrl> docRoleCtrls = new List<Model.DocRoleCtrl>();
            return Repository.DocRoleCtrl.Add(docRoleCtrls);
        }
        public static List<string> GetEditRoleList(string docId)
        {
            var rst = Repository.DocRoleCtrl.GetEditRoleList(docId);
            return rst;
        }
    }
}
