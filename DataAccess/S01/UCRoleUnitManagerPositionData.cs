using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Util;
using System.Data;

namespace DataAccess.S01
{
    public class UCRoleUnitManagerPositionData : BaseData
    {
        #region 取得角色單位資料
        /// <summary>
        /// 取得角色單位資料
        /// </summary>
        /// <param name="sys_rid">角色代碼</param>
        /// <param name="sys_uid">單位代碼</param>
        /// <returns></returns>
        public Model.S01.UCRoleUnitManagerPositionInfo.RoleUnit GetRoleUnit(string sys_rid, string sys_uid)
        {
            var db = DAH.Db;
            string sql = @"
                select r.sys_rid, r.sys_rname, u.sys_uid, u.sys_uname
                from sys_role r, sys_unit u
                where r.sys_rid = @sys_rid
	                and u.sys_uid = @sys_uid";

            var param_lst = new List<IDataParameter>() { 
                db.GetParam("@sys_rid", sys_rid),
                db.GetParam("@sys_uid", sys_uid)
            };

            var lst = db.GetEnumerable<Model.S01.UCRoleUnitManagerPositionInfo.RoleUnit>(sql, param_lst.ToArray()).ToList();

            Model.S01.UCRoleUnitManagerPositionInfo.RoleUnit info = null;
            if (lst.Count() > 0)
                info = lst[0];

            return info;            
        }
        #endregion

        #region 取得資料
        /// <summary>
        /// 取得資料
        /// </summary>
        /// <param name="sys_rid">角色代碼</param>
        /// <param name="sys_uid">單位代碼</param>
        /// <returns></returns>
        public List<Model.S01.UCRoleUnitManagerPositionInfo.Main> GetList(string sys_rid, string sys_uid)
        {
            var db = DAH.Db;

            StringBuilder sb = new StringBuilder();
            sb.Append(@"
                select sys_rid, sys_uid, sys_rpid, sys_rpname, sys_seq,");

            if (sys_uid == AuthData.GlobalSymbol)
            {
                #region 單位為不分時，因職位是通用職位，所以查詢時需忽略單位
                sb.Append(@"
                    (select count(*) 
                    from sys_account_role ar 
                    where ar.sys_rid = rp.sys_rid 
                        and ar.sys_uid = rp.sys_uid 
                        and ar.sys_rpid = rp.sys_rpid) act_count");
                #endregion
            }
            else
            {
                #region 單位為特定單位時，因職位非通用職位，所以查詢時需包含單位
                sb.Append(@"
                    (select count(*) 
                    from sys_account_role ar 
                    where ar.sys_rid = rp.sys_rid 
                        and ar.sys_uid = rp.sys_uid 
                        and ar.sys_rpid = rp.sys_rpid) act_count");
                #endregion
            }

            sb.Append(@" 
                from sys_role_position rp
                where sys_rid = @sys_rid
	                and sys_uid = @sys_uid
                order by sys_seq");

            var param_lst = new List<IDataParameter>() { 
                db.GetParam("@sys_rid", sys_rid),
                db.GetParam("@sys_uid", sys_uid)
            };

            var lst = db.GetEnumerable<Model.S01.UCRoleUnitManagerPositionInfo.Main>(sb.ToString(), param_lst.ToArray()).ToList();
            return lst;
        }
        #endregion

        #region 取得職位代碼列表
        /// <summary>
        /// 取得職位代碼列表
        /// </summary>
        /// <param name="sys_rid">角色代碼</param>
        /// <param name="sys_uid">單位代碼</param>
        /// <returns></returns>
        public DataTable GetRpidList(string sys_rid, string sys_uid)
        {
            var db = DAH.Db;
            string sql = @"
                select sys_rpid 
                from sys_role_position
                where sys_rid = @sys_rid
                    and sys_uid = @sys_uid
                order by sys_rpid";

            var param_lst = new List<IDataParameter>() { 
                db.GetParam("@sys_rid", sys_rid),
                db.GetParam("@sys_uid", sys_uid)
            };

            var dt = db.GetDataTable(sql, param_lst.ToArray());
            return dt;
        }
        #endregion

        #region 取得某角色的特定通用職位的帳號數
        public int GetActRoleCommonPositionCount(string sys_rid, string sys_rpid)
        {
            var db = DAH.Db;
            string sql = @"
                select count(*) 
                from sys_account_role ar
                where ar.sys_rid = @sys_rid
                    and ar.sys_rpid = @sys_rpid";

            var param_lst = new List<IDataParameter>(){
                db.GetParam("@sys_rid", sys_rid),
                db.GetParam("@sys_rpid", sys_rpid)
            };

            var obj = db.ExecuteScalar(sql, param_lst.ToArray());
            return CommonConvert.GetIntOrZero(obj);
        }
        #endregion

        #region 取得某角色的特地單位職位的帳號數
        /// <summary>
        /// 取得某角色的特地單位職位的帳號數
        /// </summary>
        /// <param name="sys_rid">角色代碼</param>
        /// <param name="sys_uid">單位代碼</param>
        /// <param name="sys_rpid">職位代碼</param>
        /// <returns></returns>
        public int GetActRoleUnitPositionCount(string sys_rid, string sys_uid, string sys_rpid)
        {
            var db = DAH.Db;
            string sql = @"
                select count(*) 
                from sys_account_role ar
                where ar.sys_rid = @sys_rid
                    and ar.sys_uid = @sys_uid
                    and ar.sys_rpid = @sys_rpid";

            var param_lst = new List<IDataParameter>(){
                db.GetParam("@sys_rid", sys_rid),
                db.GetParam("@sys_uid", sys_uid),
                db.GetParam("@sys_rpid", sys_rpid)
            };

            var obj = db.ExecuteScalar(sql, param_lst.ToArray());
            return CommonConvert.GetIntOrZero(obj);
        }
        #endregion
    }
}
