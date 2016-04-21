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
        //報名資料(詳細)
        Activity_apply_detailData _applyDetaildata = new Activity_apply_detailData();
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

        //取得活動的場次
        public DataTable CheckCloseData(int i)
        {
            return _data.CheckCloseData(i);
        }

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

        //更新報名資料
        public CommonResult UpdateApplyData(Dictionary<string, object> oldData_dict, Dictionary<string, object> newData_dict)
        {
            var res = CommonHelper.ValidateModel<Model.Activity_apply_detailInfo>(newData_dict);

            if (res.IsSuccess)
            {
                return _applyDetaildata.UpdateData(oldData_dict, newData_dict);
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
        public int InsertData_apply(Dictionary<string, object> dict)
        {
            var res = CommonHelper.ValidateModel<Model.Activity_applyInfo>(dict);
            DataTable data;
            int i = 0;

            if (res.IsSuccess)
            {
                //res = _applydata.InsertData(dict);
                data = _data.GetApplyidn(dict);

                i = Convert.ToInt32(data.Rows[0][0]);
            }

            return i;
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

        //新增報名資料(欄位資料)
        public CommonResult InsertData_column(Dictionary<string, object> dict)
        {
            var res = CommonHelper.ValidateModel<Model.Activity_apply_detailInfo>(dict);

            if (res.IsSuccess)
            {
                res = _applyDetaildata.InsertData(dict);
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

        //刪除報名資料
        public CommonResult DeleteApply(Dictionary<string, object> dict, Dictionary<string, object> dict_aa)
        {
            DataTable data = GetApplyDeleteData(Convert.ToInt32(dict["as_idn"]), Convert.ToInt32(dict_aa["aa_idn"]));
            Dictionary<string, object> data_dict = new Dictionary<string,object>();
            Dictionary<string, object> data_dict_apply = new Dictionary<string, object>();

            foreach (DataRow r in data.Rows)
            {
                data_dict["aad_apply_id"] = dict_aa["aa_idn"].ToString();
                data_dict["aad_col_id"] = r.ItemArray.GetValue(0);
                _applyDetaildata.DeleteData(data_dict);
            }

            data_dict_apply["aa_idn"] = dict_aa["aa_idn"].ToString();
            return _applydata.DeleteData(data_dict_apply);
        }
        #endregion

        #region 查詢活動
        //取得活動資料
        public List<ActivityInfo> getActivity(int act_idn)
        {
            return _data.getActivity(act_idn);
        }
        //取得活動資料
        public List<Activity_sessionInfo> getSession(int as_act)
        {
            return _data.getSession(as_act);
        }
        //取得活動資料
        public List<Activity_sectionInfo> getSection(int acs_act)
        {
            return _data.getSection(acs_act);
        }
        //取得活動資料
        public List<Activity_columnInfo> getColumn(int acc_act)
        {
            return _data.getColumn(acc_act);
        }



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

        //取得修改活動資料
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

        //取得報名詳細資料(欄位資料)
        public DataTable GetApplyDataDetail(int i)
        {
            return _data.GetApplyDataDetail(i);
        }

        //取得報名欄位實際資料
        public DataTable GetApplyDataColumn(int col, int act)
        {
            return _data.GetApplyDataColumn(col, act);
        }

        //取得報名資料
        public DataTable GetApplyData(int i)
        {
            //報名資料
            DataTable test = new DataTable();
            //欄位資料
            DataTable column;
            //欄位實際值
            DataTable columnDetail;

            column = _data.GetApplyDataDetail(i);

            if (column.Rows.Count != 0)
            {
                //新增key的值&Row
                test.Columns.Add("aa_idn");
                columnDetail = _data.GetApplyDataColumn((Convert.ToInt32(column.Rows[0][2])), i);
                for (int j = 0; j < columnDetail.Rows.Count; j++)
                {
                    test.Rows.Add();
                    test.Rows[j][0] = columnDetail.Rows[j][0].ToString();
                }

                //存入欄位的值
                foreach (DataRow r in column.Rows)
                {
                    //增加欄位
                    test.Columns.Add(r.ItemArray.GetValue(3).ToString());
                    columnDetail = _data.GetApplyDataColumn(Convert.ToInt32(r.ItemArray.GetValue(2)), i);

                    if (columnDetail.Rows.Count != 0)
                    {
                        //存入列的值
                        for (int j = 0; j < columnDetail.Rows.Count; j++)
                        {
                            test.Rows[j][r.ItemArray.GetValue(3).ToString()] = columnDetail.Rows[j][1].ToString();
                        }
                    }
                }
            }

            return test;
        }

        //取得欲刪除的報名資料
        public DataTable GetApplyDeleteData(int asidn, int aa)
        {
            return _data.GetApplyDeleteData(asidn, aa);
        }

        //取得活動idn
        public int Getactidn(int asidn)
        {
            DataTable data = _data.Getactidn(asidn);

            return Convert.ToInt32(data.Rows[0][0]);
        }

        //取得活動標題 & 場次標題
        public DataTable Getactas(int i)
        {
            return _data.Getactas(i);
        }
        #endregion
    }
}
