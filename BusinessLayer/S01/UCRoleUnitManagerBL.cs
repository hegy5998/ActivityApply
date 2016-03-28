using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DataAccess;
using Util;
using System.Data;

namespace BusinessLayer.S01
{
    public class UCRoleUnitManagerBL : BaseBL
    {
        DataAccess.S01.UCRoleUnitManagerData _da = new DataAccess.S01.UCRoleUnitManagerData();

        #region 取得角色資料
        /// <summary>
        /// 取得角色資料
        /// </summary>
        /// <param name="sys_rid">角色代碼</param>
        /// <returns></returns>
        public Sys_roleInfo GetRoleInfo(string sys_rid)
        {
            return new Sys_roleData().GetInfo(sys_rid);
        }
        #endregion

        #region 取得角色對應單位資料
        /// <summary>
        /// 取得角色對應單位資料
        /// </summary>
        /// <param name="sys_rid">角色代碼</param>
        /// <returns></returns>
        public List<Model.S01.UCRoleUnitManagerInfo.Main> GetRoleUnitList(string sys_rid)
        {
            return _da.GetRoleUnitList(sys_rid);
        }
        #endregion

        #region 新增
        /// <summary>
        /// 新增資料
        /// </summary>
        /// <param name="dict">資料</param>
        /// <returns></returns>
        public CommonResult InsertData(Dictionary<string, object> dict)
        {
            var res = CommonHelper.ValidateModel<Model.S01.UCRoleUnitManagerInfo.Main>(dict);
            if (res.IsSuccess)
            {
                res = new Sys_role_unitData().InsertData(dict);
            }
            return res;
        }
        #endregion

        #region 刪除
        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="dict">資料</param>
        /// <returns></returns>
        public CommonResult DeleteData(Dictionary<string, object> dict)
        {
            var res = CommonHelper.ValidateModel<Model.S01.UCRoleUnitManagerInfo.Main>(dict);
            string sys_rid = dict["sys_rid"].ToString();
            string sys_uid = dict["sys_uid"].ToString();


            if (sys_uid == AuthData.GlobalSymbol)
            {
                #region 單位是「不分」時，檢查是否有「帳號」具有該角色的單位身分，保留sys_role_position中的通用職位資料(由[設定通用職位]來維護刪除)
                int count = _da.GetActRoleUnitCount(sys_rid, sys_uid);
                if (count > 0)
                {
                    res.IsSuccess = false;
                    res.Message = "刪除失敗，因為還有 " + count + " 個帳號具有此角色單位的身分。";
                }
                #endregion
            }
            else
            {
                #region 檢查是否尚有設定職位資料
                int count = _da.GetRoleUnitPositionCount(sys_rid, sys_uid);
                if (count > 0)
                {
                    res.IsSuccess = false;
                    res.Message = "刪除失敗，因為該角色單位下仍具有 " + count + " 個職位設定資料，請先刪除職位資料。";
                }
                #endregion
            }

            if (res.IsSuccess)
            {
                try
                {
                    var trans = BeginTransaction();

                    //刪除新代碼檔資料
                    res = new Sys_role_unitData().DeleteData(trans, dict);

                    // 如果刪除的是一般單位，非*不分，則刪除相關table資料
                    // *不分的職位權限，由維護通用職位時刪除
                    if (res.IsSuccess && sys_uid != AuthData.GlobalSymbol)
                    {
                        // 刪除相關table資料
                        new Sys_process_roleData().DeleteByRoleUnit(trans, sys_rid, sys_uid);
                        new Sys_processcontrol_roleData().DeleteByRoleUnit(trans, sys_rid, sys_uid);
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
    }
}
