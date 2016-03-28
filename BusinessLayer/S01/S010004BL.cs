using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Util;
using DataAccess;

namespace BusinessLayer.S01
{
    public class S010004BL : BaseBL
    {
        DataAccess.S01.S010004Data _data = new DataAccess.S01.S010004Data();

        #region 取得某系統下的模組
        /// <summary>
        /// 取得某系統下的模組
        /// </summary>
        /// <param name="sys_id">系統代碼</param>
        /// <returns></returns>
        public List<Sys_moduleInfo> GetData(string sys_id)
        {
            return new Sys_moduleData().GetListBySystem(sys_id).OrderBy(x=>x.Sys_mseq).ToList();
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
            var res = CommonHelper.ValidateModel<Model.S01.S010004Info.Main>(dict);
            if (res.IsSuccess)
                res = new Sys_moduleData().InsertData(dict);
            return res;
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
            var res = CommonHelper.ValidateModel<Model.S01.S010004Info.Main>(newData_dict);

            if (res.IsSuccess)
            {
                try
                {
                    var trans = BeginTransaction();

                    // 更新模組代碼檔資料
                    res = new Sys_moduleData().UpdateData(trans, oldData_dict, newData_dict);
                    if (res.IsSuccess)
                    {
                        #region 若有變動模組代碼，則更新作業的模組代碼
                        var oldSysMid = oldData_dict["sys_mid"].ToString();
                        var newSysMid = newData_dict["sys_mid"].ToString();
                        if (oldSysMid != newSysMid)
                        {
                            new Sys_processData().UpdateSysMid(trans, oldSysMid, newSysMid);
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

        #region 刪除
        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="dict">資料</param>
        /// <returns></returns>
        public CommonResult DeleteData(Dictionary<string, object> dict)
        {
            var res = new CommonResult(true);
            string sys_mid = dict["sys_mid"].ToString();

            int count = _data.GetProcessCountBySysMid(sys_mid);
            if (count > 0)
            {
                res.IsSuccess = false;
                res.Message = "刪除失敗，因為還有 " + count + " 個作業使用 " + sys_mid + " 做為模組代碼。";
            }
            else
            {
                res = new Sys_moduleData().DeleteData(dict);
            }

            return res;
        }
        #endregion

        #region 設定順序
        public void SetOrder(List<string> items)
        {
            var da = new Sys_moduleData();
            for (int i = 0; i < items.Count; i++)
            {
                var pk_dict = new Dictionary<string, object>();
                pk_dict["sys_mid"] = items[i];

                var new_dict = new Dictionary<string, object>();
                new_dict["sys_mseq"] = i + 1;

                da.UpdateData(pk_dict, new_dict);
            }
        }
        #endregion
    }
}
