using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;
using Model;
using DataAccess;
using System.Data;

namespace BusinessLayer.S01
{
    public class UCRoleUnitPositionManagerBL : BaseBL
    {
        DataAccess.S01.UCRoleUnitManagerPositionData _da = new DataAccess.S01.UCRoleUnitManagerPositionData();

        #region 取得角色、單位資料
        /// <summary>
        /// 取得角色、單位資料
        /// </summary>
        /// <param name="sys_rid">角色代碼</param>
        /// <param name="sys_uid">單位代碼</param>
        /// <returns></returns>
        public Model.S01.UCRoleUnitManagerPositionInfo.RoleUnit GetRoleUnitInfo(string sys_rid, string sys_uid)
        {
            return _da.GetRoleUnit(sys_rid, sys_uid);
        }
        #endregion

        #region 取得職位資料
        /// <summary>
        /// 取得職位資料
        /// </summary>
        /// <param name="sys_rpid"></param>
        /// <returns></returns>
        public List<Model.S01.UCRoleUnitManagerPositionInfo.Main> GetRoleUnitPositionList(string sys_rid, string sys_uid)
        {
            return _da.GetList(sys_rid, sys_uid);
        }
        #endregion

        #region 取得某角色單位的可新增的職位代碼
        /// <summary>
        /// 取得某角色單位的可新增的職位代碼
        /// </summary>
        /// <param name="sys_rid">角色代碼</param>
        /// <param name="sys_uid">單位代碼</param>
        /// <returns></returns>
        public string GetNextRpid(string sys_rid, string sys_uid)
        {
            var curRpid_dt = _da.GetRpidList(sys_rid, sys_uid);
            int minId, maxId;
            if (sys_uid == AuthData.GlobalSymbol)
            {
                // 通用職位
                minId = Model.S01.UCRoleUnitManagerPositionInfo.PositionSetting.MinCommonPositionId;
                maxId = Model.S01.UCRoleUnitManagerPositionInfo.PositionSetting.MaxCommonPositionId;
            }
            else
            {
                // 單位特定職位
                minId = Model.S01.UCRoleUnitManagerPositionInfo.PositionSetting.MinUnitPositionId;
                maxId = Model.S01.UCRoleUnitManagerPositionInfo.PositionSetting.MaxUnitPositionId;
            }

            int nextRpid = minId;

            #region 取得下一個可用的職位代碼
            foreach (DataRow dr in curRpid_dt.Rows)
            {
                int cur = CommonConvert.GetIntOrZero(dr[0].ToString());
                if (cur <= nextRpid)
                    nextRpid++;
                else
                    break;
            }
            #endregion

            return nextRpid.ToString().PadLeft(2, '0');
        }
        #endregion

        #region 新增
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="dict">資料</param>
        /// <returns></returns>
        public CommonResult InsertData(Dictionary<string, object> dict)
        {
            // 取得職位代碼
            dict["sys_rpid"] = GetNextRpid(dict["sys_rid"].ToString(), dict["sys_uid"].ToString());

            var res = CommonHelper.ValidateModel<Model.S01.UCRoleUnitManagerPositionInfo.Main>(dict);

            if (res.IsSuccess)
            {
                res = new Sys_role_positionData().InsertData(dict);
            }
            return res;
        }
        #endregion

        #region 刪除
        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="data_dict">資料</param>
        /// <returns></returns>
        public CommonResult DeleteData(Dictionary<string, object> dict)
        {
            var res = CommonHelper.ValidateModel<Model.S01.UCRoleUnitManagerPositionInfo.Main>(dict);
            string sys_rid = dict["sys_rid"].ToString();
            string sys_uid = dict["sys_uid"].ToString();
            string sys_rpid = dict["sys_rpid"].ToString();

            #region 檢查是否有帳號使用該身分
            int actCount = 0;
            if (sys_uid == AuthData.GlobalSymbol)
            {
                // 單位是「不分」時，檢查是否有「帳號」具有該角色的通用職位
                actCount = _da.GetActRoleCommonPositionCount(sys_rid, sys_rpid);
            }
            else
            {
                // 特定單位時，檢查該單位的職位，是否有帳號使用
                actCount = _da.GetActRoleUnitPositionCount(sys_rid, sys_uid, sys_rpid);
            }
            if (actCount > 0)
            {
                res.IsSuccess = false;
                res.Message = "刪除失敗，因為還有 " + actCount + " 個帳號具有此身分權限。";
            }
            #endregion

            if (res.IsSuccess)
            {
                try
                {
                    var trans = BeginTransaction();

                    // 刪除代碼檔資料
                    res = new Sys_role_positionData().DeleteData(trans, dict);

                    if (res.IsSuccess)
                    {
                        // 刪除相關table資料
                        new Sys_process_roleData().DeleteByRoleUnitPosition(trans, sys_rid, sys_uid, sys_rpid);
                        new Sys_processcontrol_roleData().DeleteByRoleUnitPosition(trans, sys_rid, sys_uid, sys_rpid);
                    }

                    if (res.IsSuccess)
                        Commit();
                    else
                        Rollback();
                }
                catch
                {
                    res.IsSuccess = false;
                    Rollback();
                }
            }
            return res;
        }
        #endregion

        #region 更新
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="oldData_dict">原資料</param>
        /// <param name="newData_dict">新資料</param>
        /// <returns></returns>
        public CommonResult UpdateData(Dictionary<string, object> oldData_dict, Dictionary<string, object> newData_dict)
        {
            var res = CommonHelper.ValidateModel<Model.S01.UCRoleUnitManagerPositionInfo.Main>(newData_dict);

            if (res.IsSuccess)
            {
                res = new Sys_role_positionData().UpdateData(oldData_dict, newData_dict);
            }

            return res;
        }
        #endregion

        #region 設定順序
        public void SetOrder(string sys_rid, string sys_uid, List<string> items)
        {
            var da = new Sys_role_positionData();
            for (int i = 0; i < items.Count; i++)
            {
                var pk_dict = new Dictionary<string, object>();
                pk_dict["sys_rid"] = sys_rid;
                pk_dict["sys_uid"] = sys_uid;
                pk_dict["sys_rpid"] = items[i];

                var new_dict = new Dictionary<string, object>();
                new_dict["sys_seq"] = i + 1;

                da.UpdateData(pk_dict, new_dict);
            }
        }
        #endregion
    }
}
