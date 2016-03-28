using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using DataAccess;
using DataAccess.S01;
using Util;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace BusinessLayer.S01
{
    public class S010006BL : BaseBL
    {
        DataAccess.S01.S010006Data _data = new S010006Data();

        #region 查詢
        /// <summary>
        /// 取得所有資料
        /// </summary>
        /// <returns>所有資料</returns>
        public List<Model.S01.S010006Info.Main> GetData()
        {
            return _data.GetRoleList();
        }
        #endregion

        #region 更新
        /// <summary>
        /// 更新資料
        /// </summary>
        /// <param name="oldData_dict">原資料PK</param>
        /// <param name="newData_dict">新資料</param>
        /// <returns></returns>
        public CommonResult UpdateData(Dictionary<string, object> oldData_dict, Dictionary<string, object> newData_dict)
        {
            var res = CommonHelper.ValidateModel<Model.S01.S010006Info.Main>(newData_dict);

            if (res.IsSuccess)
            {
                try
                {
                    var trans = BeginTransaction();

                    // 更新代碼檔資料
                    res = new Sys_roleData().UpdateData(trans, oldData_dict, newData_dict);
                    if (res.IsSuccess)
                    {
                        #region 若有變動key，則更新相關table代碼
                        var old_sys_rid = oldData_dict["sys_rid"].ToString();
                        var new_sys_rid = newData_dict["sys_rid"].ToString();
                        if (old_sys_rid != new_sys_rid)
                        {
                            new Sys_role_unitData().UpdateSysRid(trans, old_sys_rid, new_sys_rid);
                            new Sys_role_positionData().UpdateSysRid(trans, old_sys_rid, new_sys_rid);
                            new Sys_process_roleData().UpdateSysRid(trans, old_sys_rid, new_sys_rid);
                            new Sys_processcontrol_roleData().UpdateSysRid(trans, old_sys_rid, new_sys_rid);
                            new Sys_account_roleData().UpdateSysRid(trans, old_sys_rid, new_sys_rid);
                        }
                        #endregion
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

        #region 新增
        /// <summary>
        /// 新增資料
        /// </summary>
        /// <param name="dict">資料</param>
        /// <returns></returns>
        public CommonResult InsertData(Dictionary<string, object> dict)
        {
            var res = CommonHelper.ValidateModel<Model.S01.S010006Info.Main>(dict);
            if (res.IsSuccess)
                res = new Sys_roleData().InsertData(dict);
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
            var res = new CommonResult(true);
            string sys_rid = dict["sys_rid"].ToString();

            #region 檢查是否已未具有任何單位資料
            if (res.IsSuccess)
            {
                int count = _data.GetRoleUnitCount(sys_rid);
                if (count > 0)
                {
                    res.IsSuccess = false;
                    res.Message = "刪除失敗，因為該角色下仍具有 " + count + " 個單位資料，請先刪除角色具有的單位資料。";
                }
            }
            #endregion

            #region 檢查是否已未具有通用職位資料
            if (res.IsSuccess)
            {
                int count = _data.GetRoleCommonPositionCount(sys_rid);
                if (count > 0)
                {
                    res.IsSuccess = false;
                    res.Message = "刪除失敗，因為該角色下仍具有 " + count + " 個通用職位資料，請先刪除角色具有的通用職位資料。";
                }
            }
            #endregion

            if (res.IsSuccess)
            {
                try
                {
                    var trans = BeginTransaction();

                    //刪除新代碼檔資料
                    res = new Sys_roleData().DeleteData(trans, dict);
                    if (res.IsSuccess)
                    {
                        // 刪除相關table資料
                        new Sys_process_roleData().DeleteByRole(trans, sys_rid);
                        new Sys_processcontrol_roleData().DeleteByRole(trans, sys_rid);
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
