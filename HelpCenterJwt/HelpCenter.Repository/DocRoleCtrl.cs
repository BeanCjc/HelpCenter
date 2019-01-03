using Dapper;
using Dapper.Contrib;
using HelpCenter.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;

namespace HelpCenter.Repository
{
    public class DocRoleCtrl
    {
        private static DapperMySQLHelper mysql = new DapperMySQLHelper();
        public static bool Add(List<Model.DocRoleCtrl> docRoleCtrls)
        {
            if (docRoleCtrls != null && docRoleCtrls.Count>0)
            {
                Delete(docRoleCtrls);
                using (IDbConnection conn = DbSwitcher.GetOpenConnection())
                {
                    foreach (var item in docRoleCtrls)
                    {
                        if (Get(item.DocId,item.RoleId) == null|| Get(item.DocId, item.RoleId).Count==0)
                        {
                           conn.Insert(item);
                        }
                    }
                    return true;
                }
            }

            return false;
        }

        public static List<Model.DocRoleCtrl> Get(string docId, string roleId)
        {
            using (IDbConnection conn = DbSwitcher.GetOpenConnection())
            {
                var lst = conn.GetList<Model.DocRoleCtrl>(new { DocId = docId, RoleId = roleId })?.ToList();
                return lst;
            }
        }

        public static List<Model.DocRoleCtrl> Get(string docId)
        {
            using (IDbConnection conn = DbSwitcher.GetOpenConnection())
            {
                var lst = conn.GetList<Model.DocRoleCtrl>(new { DocId = docId})?.ToList();
                return lst;
            }
        }

        public static bool Delete(Model.DocRoleCtrl docRoleCtrl)
        {
            using (IDbConnection conn = DbSwitcher.GetOpenConnection())
            {
                conn.Delete(docRoleCtrl);
            }
            return false;
        }

        public static bool Delete(List<Model.DocRoleCtrl> docRoleCtrlList)
        {
            try
            {
                using (IDbConnection conn = DbSwitcher.GetOpenConnection())
                {
                    foreach (var item in docRoleCtrlList)
                    {
                        var lst = Get(item.DocId);
                        foreach (var _item in lst)
                        {
                            conn.Delete(_item);
                        }
                    }
                    return true;
                }
            }
            catch (Exception)
            {
            }
            return false;
        }

        public static List<string> GetEditRoleList(string docId)
        {
            try
            {
                using (IDbConnection conn = DbSwitcher.GetOpenConnection())
                {
                    List<string> vs = new List<string>();
                    var lst = conn.GetList<Model.DocRoleCtrl>(new { DocId = docId }).ToList();
                    if (lst != null && lst.Count > 0)
                    {
                        foreach (var item in lst)
                        {
                            if (!vs.Contains(item.RoleId))
                            {
                                vs.Add(item.RoleId);
                            }
                        }
                    }
                    return vs;
                }
            }
            catch (Exception)
            {
            }
            return new List<string>();
        }

    }
}
