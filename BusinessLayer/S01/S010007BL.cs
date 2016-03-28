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
    public class S010007BL : BaseBL
    {
        DataAccess.S01.S010007Data _data = new S010007Data();

        #region 查詢
        /// <summary>
        /// 取得所有資料
        /// </summary>
        /// <returns>所有資料</returns>
        public List<Sys_unitInfo> GetData()
        {
            return new Sys_unitData().GetList();
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
            var res = CommonHelper.ValidateModel<Model.S01.S010007Info.Main>(newData_dict);

            if (res.IsSuccess)
            {
                try
                {
                    var trans = BeginTransaction();

                    // 更新代碼檔資料
                    res = new Sys_unitData().UpdateData(trans, oldData_dict, newData_dict);
                    if (res.IsSuccess)
                    {
                        #region 若有變動key，則更新相關table代碼
                        var old_sys_uid = oldData_dict["sys_uid"].ToString();
                        var new_sys_uid = newData_dict["sys_uid"].ToString();
                        if (old_sys_uid != new_sys_uid)
                        {
                            new Sys_account_roleData().UpdateSysRid(trans, old_sys_uid, new_sys_uid);
                            new Sys_process_roleData().UpdateSysRid(trans, old_sys_uid, new_sys_uid);
                            new Sys_processcontrol_roleData().UpdateSysRid(trans, old_sys_uid, new_sys_uid);
                            new Sys_role_positionData().UpdateSysRid(trans, old_sys_uid, new_sys_uid);
                            new Sys_role_unitData().UpdateSysRid(trans, old_sys_uid, new_sys_uid);
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
            var res = CommonHelper.ValidateModel<Model.S01.S010007Info.Main>(dict);
            if (res.IsSuccess)
                res = new Sys_unitData().InsertData(dict);
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
            string sys_uid = dict["sys_uid"].ToString();

            int count = _data.GetRoleUnitCount(sys_uid);
            if (count > 0)
            {
                res.IsSuccess = false;
                res.Message = "刪除失敗，因為還有 " + count + " 個角色使用 " + sys_uid + " 做為單位代碼。";
            }
            else
            {
                res = new Sys_unitData().DeleteData(dict);
            }
            return res;
        }
        #endregion
    }
}
