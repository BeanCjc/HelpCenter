using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using HelpCenter.ActionService;

namespace HelpCenter.BLL
{
    public class Dept
    {
        /// <summary>
        /// 新增部门
        /// </summary>
        /// <param name="usrId">当前登录用户ID</param>
        /// <param name="strPreDeptId">上级部门ID</param>
        /// <param name="strDeptName">当前部门名称</param>
        /// <param name="strDeptAccount">部门账号</param>
        /// <param name="strDeptPsw">部门账号的密码</param>
        /// <param name="tips">输出提示</param>
        /// <returns></returns>
        public static bool Add(string usrId, string strPreDeptId, string strDeptName, string strDeptAccount, string strDeptPsw, out string tips)
        {
            if (!Usr.CheckAccount(strDeptAccount))
            {
                tips = ResponseMessageTips.MSG_DEPT_ACCOUNT_IS_EXIST;
                return false;
            }

            var preModel = Repository.Dept.GetModel(strPreDeptId);

            string strdeptNO = "1"+"0".PadLeft(3, '0');
            if (preModel != null)
            {
                //1000
                //1000 000
                strdeptNO = preModel.deptNO+"0".PadLeft(3, '0');
            }
            var deptNO = Repository.Dept.GetMaxDeptNumByPreDeptId(strPreDeptId);
            if (null != deptNO)
            {
                long.TryParse(deptNO, out long maxNO);
                strdeptNO = (maxNO + 1).ToString();
            }

            string strDeptId = Guid.NewGuid().ToString();
            Model.Dept deptModel = new Model.Dept()
            {
                crdt = DateTime.Now,
                crUsrId = usrId,
                deptId = strDeptId,
                deptName = strDeptName,
                deptNO = strdeptNO,
                deptState = 1,
                preDeptId = strPreDeptId,
                updt = DateTime.Now,
                upUsrId = usrId
            };

            Model.DeptAccount deptAccountModel = new Model.DeptAccount()
            {
                crdt = DateTime.Now,
                crUsrId = usrId,
                deptId = strDeptId,
                deptAccount = strDeptAccount,
                deptAccountId = Guid.NewGuid().ToString(),
                deptPsw = strDeptPsw,
                updt = DateTime.Now,
                upUsrId = usrId
            };

            Model.Usr usrModel = new Model.Usr()
            {
                usrId = Guid.NewGuid().ToString(),
                crdt = DateTime.Now,
                crUsrId = usrId,
                updt = DateTime.Now,
                upUsrId = usrId,
                usrAccount = strDeptAccount,
                usrName = strDeptName,
                usrDeptId = strDeptId,
                usrPhoneNum = strDeptAccount,
                usrPsw = strDeptPsw,
                usrState = 1,
                usrType = 1,
                usrVerifyState = 1
            };
            UserRole.Set(Guid.NewGuid().ToString(), usrModel.usrId, new List<string>() { }, 1, usrId);
            bool result = Repository.Dept.Add(deptModel, deptAccountModel);
            tips = result ? ResponseMessageTips.MSG_DEPT_ADD_SUCCESS : ResponseMessageTips.MSG_DEPT_ADD_FAIL;
            return result;
        }

        /// <summary>
        /// 获取所有顶级部门清单
        /// </summary>
        /// <returns></returns>
        public static IList<Model.DbModel.DbDeptModel> GetTopList()
        {
            var deptList = Repository.Dept.GetTopList();
            foreach (var item in deptList)
            {
                item.child = item.childCount == 0 ? null : (new string[0]);
            }
            return deptList;
        }

        /// <summary>
        /// 获取所有部门清单（分页）
        /// </summary>
        /// <returns></returns>
        public static IList<Model.DbModel.DbDeptModel> GetList(int pageIndex,int rowCount, out int totalCount, out int pageCount,string deptNO)
        {
            IList<Model.DbModel.DbDeptModel> deptList = Repository.Dept.GetList(pageIndex,rowCount,out totalCount,out pageCount, deptNO);
            foreach (var item in deptList)
            {
                item.child = item.childCount==0 ? null : (new string[0]);
            }
            return deptList;
        }
        /// <summary>
        /// 根据部门名称获取所有部门清单（分页）
        /// </summary>
        /// <returns></returns>
        public static IList<Model.DbModel.DbDeptModel> GetListByDeptName(int pageIndex,int rowCount,string strDeptName, out int totalCount, out int pageCount, string deptNO)
        {
            IList<Model.DbModel.DbDeptModel> deptList = Repository.Dept.GetListByDeptName(pageIndex,rowCount, strDeptName,out totalCount,out pageCount, deptNO);
            foreach (var item in deptList)
            {
                item.child = item.childCount == 0 ? null : (new string[0]);
            }
            return deptList;
        }

        public static bool Update(string usrId,string strDeptId, string strPreDeptId, string strDeptName, string strDeptNO,string strDeptAccount,string strDeptPsw)
        {
            Model.Dept deptModel = new Model.Dept()
            {
                deptAccount = strDeptAccount,
                deptId = strDeptId,
                deptName = strDeptName,
                deptNO = strDeptNO,
                preDeptId = strPreDeptId,
                updt = DateTime.Now,
                upUsrId = usrId
            };

            Model.DeptAccount deptAccountModel = new Model.DeptAccount()
            {
                deptId = strDeptId,
                deptAccount = strDeptAccount,
                deptAccountId = Guid.NewGuid().ToString(),
                deptPsw = strDeptPsw,
                updt = DateTime.Now,
                upUsrId = usrId
            };

            Model.Usr usrModel = new Model.Usr()
            {
                updt = DateTime.Now,
                upUsrId = strDeptId,
                usrAccount = strDeptAccount,
                usrName = strDeptName,
                usrDeptId = strDeptId,
                usrPsw = strDeptPsw
            };
            return Repository.Dept.Update(deptModel, deptAccountModel, usrModel);
        }

        public static bool UpdatePassWord(string strDeptId, string strUsrPsw,string strUpUsrId)
        {
            return Repository.Dept.UpdatePassWord(strDeptId, strUsrPsw,strUpUsrId);
        }

        public static bool Delete(string strDeptId,out string tips)
        {
            return Repository.Dept.Delete(strDeptId,out tips);
        }

        /// <summary>
        /// 获取指定部门信息详情
        /// </summary>
        /// <param name="deptId">部门编号</param>
        /// <returns></returns>
        public static Model.Dept GetModel(string deptId)
        {
            var dept = Repository.Dept.GetModel(deptId);
            if (dept != null)
            {
                dept.child = dept.childCount == 0 ? null : (new string[0]);
            }

            return dept;
        }

        /// <summary>
        /// 获取指定部门信息详情
        /// </summary>
        /// <param name="deptId">部门编号</param>
        /// <returns></returns>
        public static IList<Model.Dept> GetModelListByDeptId(string deptId)
        {
            var dept = Repository.Dept.GetModelListByDeptId(deptId);
            if (dept != null)
            {
                foreach (var item in dept)
                {
                    item.child = item.childCount == 0 ? null : (new string[0]);
                }
            }

            return dept;
        }

        /// <summary>
        /// 获取指定部门信息详情
        /// </summary>
        /// <param name="deptId">部门编号</param>
        /// <returns></returns>
        public static IList<Model.DbModel.DbDeptModel> GetChildNodes(string deptId)
        {
            var dept = Repository.Dept.GetChildNodes(deptId);
            foreach (var item in dept)
            {
                item.child = item.childCount == 0 ? null : (new string[0]);
            }
            return dept;
        }
    }
}
