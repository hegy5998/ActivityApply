using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;
using DataAccess;
using Model;

namespace BusinessLayer.S01
{
    public class S010010BL : BaseBL
    {
        DataAccess.S01.S010010Data _da = new DataAccess.S01.S010010Data();

        #region 取得資料
        /// <summary>
        /// 取得資料
        /// </summary>
        /// <param name="cond_dict">查詢條件</param>
        /// <returns></returns>
        public List<Model.S01.S010010Info.Main> GetList(Dictionary<string, string> cond_dict)
        {
            return _da.GetList(cond_dict);
        }
        #endregion

        #region 刪除
        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="data_dict">資料</param>
        /// <returns></returns>
        public CommonResult DeleteData(Dictionary<string, object> data_dict)
        {
            var res = new CommonResult(true);
            try
            {
                var trans = BeginTransaction();

                // 刪除帳號資料
                res = new Sys_accountData().DeleteData(trans, data_dict);
                if (res.IsSuccess)
                {
                    /// 刪除身分資料
                    new Sys_account_roleData().DeleteDataByAct(trans, data_dict["act_id"].ToString());
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
            return res;
        }
        #endregion
    }
}
