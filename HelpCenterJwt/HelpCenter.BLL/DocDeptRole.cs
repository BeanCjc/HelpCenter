using Dapper;
using HelpCenter.Common.DBUtility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelpCenter.BLL
{
    public class DocDeptRole
    {
        /// <summary>
        /// 新增用户角色关系
        /// </summary>
        /// <param name="strDocDeptRoleId"></param>
        /// <param name="strUsrId"></param>
        /// <param name="strRoleIdList"></param>
        /// <param name="docDeptRoleState"></param>
        /// <param name="strCrUsrId"></param>
        /// <returns></returns>
        public static bool Set(string strDocDeptRoleId,string strDocId, List<string> DeptIdList, int? docDeptRoleState,string strCrUsrId)
        {
            
            Model.DocDeptRole docDeptRole = new Model.DocDeptRole()
            {
                Crdt = DateTime.Now,
                CrUsrId = strCrUsrId,
                DocDeptRoleId = Guid.NewGuid().ToString(),
                DocDeptState = docDeptRoleState,
                Updt = DateTime.Now,
                UpUsrId = strCrUsrId,
                DocId = strDocId
            };
            return Repository.DocDeptRole.Set(docDeptRole,DeptIdList);
        }

        public static IList<Model.DocDeptRole> GetDocDeptRoleModelList(string strDocId)
        {
            return Repository.DocDeptRole.GetDocDeptRoleModelList(strDocId);
        }



    }
}
