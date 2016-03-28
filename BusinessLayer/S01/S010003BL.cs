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
    public class S010003BL : BaseBL
    {
        S010003Data _data = new S010003Data();

        #region 查詢
        /// <summary>
        /// 取得所有資料
        /// </summary>
        /// <returns>所有資料</returns>
        public List<Sys_systemInfo> GetData()
        {
            return new Sys_systemData().GetList().OrderBy(x=>x.Sys_seq).ToList();
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
            var res = CommonHelper.ValidateModel<Model.S01.S010003Info.Main>(newData_dict);

            if (res.IsSuccess)
            {
                try
                {
                    var trans = BeginTransaction();

                    // 更新系統代碼檔資料
                    res = new Sys_systemData().UpdateData(trans, oldData_dict, newData_dict);

                    if (res.IsSuccess)
                    {
                        #region 若有變動系統代碼，則更新模組的系統代碼
                        var oldSysId = oldData_dict["sys_id"].ToString();
                        var newSysId = newData_dict["sys_id"].ToString();
                        if (newSysId != oldSysId)
                        {
                            new Sys_moduleData().UpdateSysId(trans, oldSysId, newSysId);
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
            var res = CommonHelper.ValidateModel<Model.S01.S010003Info.Main>(dict);
            if (res.IsSuccess)
                res = new Sys_systemData().InsertData(dict);
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
            string sys_id = dict["sys_id"].ToString();

            int count = _data.GetModuleCountBySysId(sys_id);
            if (count > 0)
            {
                res.IsSuccess = false;
                res.Message = "刪除失敗，因為還有 " + count + " 個模組使用 " + sys_id + " 做為系統代碼。";
            }
            else
            {
                res = new Sys_systemData().DeleteData(dict);
            }

            return res;
        }
        #endregion

        #region 設定順序
        public void SetOrder(List<string> items)
        {
            var da = new Sys_systemData();
            for (int i = 0; i < items.Count; i++)
            {
                var pk_dict = new Dictionary<string, object>();
                pk_dict["sys_id"] = items[i];

                var new_dict = new Dictionary<string, object>();
                new_dict["sys_seq"] = i+1;

                da.UpdateData(pk_dict, new_dict);
            }
        }
        #endregion
    }
}
