using System;
using System.Collections.Generic;
using System.Text;

namespace HelpCenter.BLL
{
    public class DocUsrRole
    {
        public static bool Set(string strDocRoleId, int docRoleState,string strCrUsrId,string strUpUsrId, string[] docIdList,string strRoleId)
        {
            Model.DocUsrRole docUsrRole = new Model.DocUsrRole()
            {
                crdt = DateTime.Now,
                crUsrId = strCrUsrId,
                docRoleState = docRoleState,
                docRoleId = strDocRoleId,
                roleId = strRoleId,
                updt = DateTime.Now,
                upUsrId = strUpUsrId
            };

            return Repository.DocUsrRole.Set(docUsrRole, docIdList);
        }

        public static bool QueryExist(string strDocRoleId,string strRoleId)
        {
           var rst = Repository.DocUsrRole.QueryExist(strDocRoleId, strRoleId);
            return rst!=null;
        }

        public static List<string> GetListByDocId(string docId)
        {
            var rst = Repository.DocUsrRole.GetListByDocId(docId);
            return rst;
        }
    }
}
