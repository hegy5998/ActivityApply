using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using DataAccess;
using Util;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Data;

namespace BusinessLayer.S02
{
    public class S020101BL : BaseBL
    {
        //活動
        ActivityData _activitydata = new ActivityData();
        //場次
        Activity_sessionData _sessiondata = new Activity_sessionData();
        //報名資訊
        Activity_applyData _applydata = new Activity_applyData();
        //協作者
        Account_copperateData _cooperate = new Account_copperateData();
        S020101Data _data = new S020101Data();

        #region 更新
        /// <summary>
        /// 更新資料
        /// </summary>
        /// <param name="oldData_dict">原資料PK</param>
        /// <param name="newData_dict">新資料</param>
        /// <returns></returns>
        /// 更新活動資料
        public CommonResult UpdateData(Dictionary<string, object> oldData_dict, Dictionary<string, object> newData_dict)
        {
            var res = CommonHelper.ValidateModel<Model.ActivityInfo>(newData_dict);

            if (res.IsSuccess)
            {
                return _activitydata.UpdateData(oldData_dict, newData_dict);
            }

            return res;
        }

        //更新場次資料
        public CommonResult UpdateSessionData(Dictionary<string, object> oldData_dict, Dictionary<string, object> newData_dict)
        {
            var res = CommonHelper.ValidateModel<Model.Activity_sessionInfo>(newData_dict);

            if (res.IsSuccess)
            {
                return _sessiondata.UpdateData(oldData_dict, newData_dict);
            }

            return res;
        }

        //public CommonResult UpdateSessionCheckData(Dictionary<string, object> oldData_dict, Dictionary<string, object> newData_dict)
        //{
        //    //確定此活動是否還有其他場次
        //    DataTable check = _data.CheckisopenData(Convert.ToInt32(oldData_dict["act_idn"]));
        //    int a = Convert.ToInt32(check.Rows[0]["as_number"]);

        //    //此活動沒有場次發佈
        //    if (a == 0)
        //    {
        //        _activitydata.DeleteData(dict_act);

        //        return _sessiondata.DeleteData(dict);
        //    }
        //    //此活動還有剩餘場次，因此只刪除場次
        //    else
        //    {
        //        return _sessiondata.DeleteData(dict);
        //    }
        //}

        //更新協作者資料
        public CommonResult UpdateCopData(Dictionary<string, object> oldData_dict, Dictionary<string, object> newData_dict)
        {
            var res = CommonHelper.ValidateModel<Model.Account_copperateInfo>(newData_dict);

            if (res.IsSuccess)
            {
                return _cooperate.UpdateData(oldData_dict, newData_dict);
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
        /// 新增活動資料
        public CommonResult InsertData(Dictionary<string, object> dict)
        {
            var res = CommonHelper.ValidateModel<Model.ActivityInfo>(dict);

            if (res.IsSuccess)
            {
                res = _activitydata.InsertData(dict);
            }
            return res;
        }

        //新增場次資料
        public CommonResult InsertData_session(Dictionary<string, object> dict)
        {
            var res = CommonHelper.ValidateModel<Model.Activity_sessionInfo>(dict);

            if (res.IsSuccess)
            {
                res = _sessiondata.InsertData(dict);
            }

            return res;
        }

        //新增報名資料
        public CommonResult InsertData_apply(Dictionary<string, object> dict)
        {
            var res = CommonHelper.ValidateModel<Model.Activity_applyInfo>(dict);

            if (res.IsSuccess)
            {
                res = _applydata.InsertData(dict);
            }

            return res;
        }

        //新增協作者資料
        public CommonResult InsertData_cop(Dictionary<string, object> dict)
        {
            var res = CommonHelper.ValidateModel<Model.Account_copperateInfo>(dict);

            if (res.IsSuccess)
            {
                res = _cooperate.InsertData(dict);
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
        /// 刪除場次資料
        public CommonResult DeleteData(Dictionary<string, object> dict, Dictionary<string, object> dict_act)
        {
            //確定此活動是否還有其他場次
            DataTable check = _data.CheckDelData(Convert.ToInt32(dict_act["act_idn"]));
            int a = Convert.ToInt32(check.Rows[0]["as_number"]);

            //此活動沒有剩餘場次，因此刪除場次and活動
            if ((a - 1) == 0)
            {
                _activitydata.DeleteData(dict_act);

                return _sessiondata.DeleteData(dict);
            }
            //此活動還有剩餘場次，因此只刪除場次
            else
            {
                return _sessiondata.DeleteData(dict);
            }
        }

        // 刪除協作者
        public CommonResult DeleteCopData(Dictionary<string, object> dict)
        {
            return _cooperate.DeleteData(dict);
        }
        #endregion

        #region 查詢活動
        /// <summary>
        /// 取得所有資料
        /// </summary>
        /// <returns>所有資料</returns>
        /// 取得已發佈活動資料
        public DataTable GetAlreadyData(Dictionary<string, object> Cond)
        {
            return _data.GetAlreadyList();
        }

        //取得未發佈活動資料
        public DataTable GetReadyData(Dictionary<string, object> Cond)
        {
            return _data.GetReadyList();
        }

        //取得已結束活動資料
        public DataTable GetEndData(Dictionary<string, object> Cond)
        {
            return _data.GetEndList();
        }

        //取得修改資料
        public DataTable GetEditData(int i)
        {
            return _data.GetEditData(i);
        }

        //取得協作者資料
        public DataTable GetCopData(int i)
        {
            return _cooperate.GetList(i);
        }

        //取得協作者Info資料
        public Account_copperateInfo GetCopperateData(string act_idn)
        {
            return new Account_copperateData().GetInfo(act_idn);
        }

        //取得協作者List資料
        public List<Account_copperateInfo> GetControlList(string act_idn)
        {
            return new Account_copperateData().GetDataList(act_idn);
        }

        public DataTable GetApplyData(int i)
        {
            return  _data.GetApplyData(i);
        }
        #endregion
    }
}
