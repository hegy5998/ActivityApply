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
using System.Collections.Specialized;

namespace BusinessLayer.S02
{
    public class S020101BL : BaseBL
    {
        private static string MESSAGE_TITLE = "S020101BL";
        //活動
        ActivityData _activitydata = new ActivityData();
        //場次
        Activity_sessionData _sessiondata = new Activity_sessionData();
        //報名資訊
        Activity_applyData _applydata = new Activity_applyData();
        //協作者
        Account_copperateData _copperate = new Account_copperateData();
        //報名資料(詳細)
        Activity_apply_detailData _applyDetaildata = new Activity_apply_detailData();
        //Email資料
        Activity_apply_emailData _emaildata = new Activity_apply_emailData();
        //欄位資料
        Activity_columnData _columndata = new Activity_columnData();

        S020101Data _data = new S020101Data();

        #region 修改報名表
        #region 新增區塊資料
        public CommonResult InsertData_Activity_Section(Dictionary<string, object> dict)
        {
            var res = CommonHelper.ValidateModel<Model.Activity_sectionInfo>(dict);

            if (res.IsSuccess)
            {
                res = _data.InsertData_Activity_Section(dict);
            }

            return res;
        }
        #endregion

        #region 新增題目資料
        public CommonResult InsertData_Activity_Column(Dictionary<string, object> dict)
        {
            var res = CommonHelper.ValidateModel<Model.Activity_columnInfo>(dict);

            if (res.IsSuccess)
            {
                res = _data.InsertData_Activity_Column(dict);
            }

            return res;
        }
        #endregion

        #region 新增場次資料
        public CommonResult InsertData_session(Dictionary<string, object> dict)
        {
            var res = CommonHelper.ValidateModel<Model.Activity_sessionInfo>(dict);

            if (res.IsSuccess)
            {
                res = _data.InsertData_session(dict);
            }

            return res;
        }
        #endregion

        #region 刪除問題資料
        public CommonResult Delete_Column_Data(Dictionary<string, object> dict)
        {
            var res = CommonHelper.ValidateModel<Model.Activity_columnInfo>(dict);

            if (res.IsSuccess)
            {
                res = _data.Delete_Column_Data(dict);
            }
            return res;
        }
        #endregion

        #region 刪除場次報名資料
        public int Delete_Session_apply_Data(string aa_as)
        {
            int res = _data.Delete_Session_apply_Data(aa_as);
            return res;
        }
        #endregion

        #region 刪除區塊資料
        public CommonResult Delete_Section_Data(Dictionary<string, object> dict)
        {
            var res = CommonHelper.ValidateModel<Model.Activity_sectionInfo>(dict);

            if (res.IsSuccess)
            {
                res = _data.Delete_Section_Data(dict);
            }
            return res;
        }
        #endregion

        #region 刪除報名細節資料
        public int Delete_apply_detail(string aad_col_id)
        {
            int res = _data.Delete_apply_detail(aad_col_id);
            return res;
        }
        #endregion

        #region 刪除區塊資料
        public CommonResult Delete_session(Dictionary<string, object> dict)
        {
            var res = CommonHelper.ValidateModel<Model.Activity_sessionInfo>(dict);

            if (res.IsSuccess)
            {
                res = _data.Delete_session(dict);
            }
            return res;
        }
        #endregion

        #region 更新活動資料
        public CommonResult Update_Activity_Data(Dictionary<string, object> old_dict, Dictionary<string, object> new_dict)
        {
            var res = CommonHelper.ValidateModel<Model.ActivityInfo>(new_dict);

            if (res.IsSuccess)
            {
                res = _data.Update_Activity_Data(old_dict, new_dict);
            }
            return res;
        }
        #endregion

        #region 更新場次資料
        public CommonResult Update_Session_Data(Dictionary<string, object> old_dict, Dictionary<string, object> new_dict)
        {
            var res = CommonHelper.ValidateModel<Model.Activity_sessionInfo>(new_dict);

            if (res.IsSuccess)
            {
                res = _data.Update_Session_Data(old_dict, new_dict);
            }
            return res;
        }
        #endregion

        #region 更新區塊資料
        public CommonResult Update_Section_Data(Dictionary<string, object> old_dict, Dictionary<string, object> new_dict)
        {
            var res = CommonHelper.ValidateModel<Model.Activity_sectionInfo>(new_dict);

            if (res.IsSuccess)
            {
                res = _data.Update_Section_Data(old_dict, new_dict);
            }
            return res;
        }
        #endregion

        #region 更新題目資料
        public CommonResult Update_Column_Data(Dictionary<string, object> old_dict, Dictionary<string, object> new_dict)
        {
            var res = CommonHelper.ValidateModel<Model.Activity_columnInfo>(new_dict);

            if (res.IsSuccess)
            {
                res = _data.Update_Column_Data(old_dict, new_dict);
            }
            return res;
        }
        #endregion
        #endregion

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
                return _copperate.UpdateData(oldData_dict, newData_dict);
            }

            return res;
        }

        //更新報名資料
        public CommonResult UpdateApplyDetailData(Dictionary<string, object> oldData_dict, Dictionary<string, object> newData_dict)
        {
            var res = CommonHelper.ValidateModel<Model.Activity_apply_detailInfo>(newData_dict);

            if (res.IsSuccess)
            {
                return _applyDetaildata.UpdateData(oldData_dict, newData_dict);
            }

            return res;
        }

        //更新報名者資料
        public CommonResult UpdateApplyData(Dictionary<string, object> oldData_dict, Dictionary<string, object> newData_dict)
        {
            var res = CommonHelper.ValidateModel<Model.Activity_applyInfo>(newData_dict);

            if (res.IsSuccess)
            {
                return _applydata.UpdateData(oldData_dict, newData_dict);
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
                res = _copperate.InsertData(dict);
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

        //新增Email資料
        public CommonResult InsertEmailData(Dictionary<string, object> dict)
        {
            var res = CommonHelper.ValidateModel<Model.Activity_apply_emailInfo>(dict);

            if (res.IsSuccess)
            {
                res = _emaildata.InsertData(dict);
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
            //刪除報名者的實際報名資料(activity_apply_detail)
            DataTable applyDetaildata = _data.GetApplyDetailData(CommonConvert.GetIntOrZero(dict["as_idn"]));
            var dict_applyDetail = new Dictionary<string, object>();
            for (int i = 0; i < applyDetaildata.Rows.Count; i++)
            {
                dict_applyDetail["aad_apply_id"] = applyDetaildata.Rows[i][1].ToString();
                dict_applyDetail["aad_col_id"] = applyDetaildata.Rows[i][2].ToString();

                _applyDetaildata.DeleteData(dict_applyDetail);
            }

            //刪除報名者的資料(activity_apply)
            DataTable applydata = _data.GetApplyData(CommonConvert.GetIntOrZero(dict["as_idn"]));
            var dict_apply = new Dictionary<string, object>();
            for (int i = 0; i < applydata.Rows.Count; i++)
            {
                dict_apply["aa_idn"] = applydata.Rows[i][0].ToString();

                _applydata.DeleteData(dict_apply);
            }

            //刪除此場次的協作者
            DataTable copperate = _copperate.GetList(CommonConvert.GetIntOrZero(dict_act["act_idn"]));
            var dict_copperate = new Dictionary<string, object>();
            dict_copperate["cop_act"] = dict_act["act_idn"].ToString();
            for (int i = 0; i < copperate.Rows.Count; i++)
            {
                dict_copperate["cop_id"] = copperate.Rows[i][1].ToString();

                _copperate.DeleteData(dict_copperate);
            }

            //確定此活動是否還有其他場次
            DataTable check = _data.CheckDelData(CommonConvert.GetIntOrZero(dict_act["act_idn"]));
            int a = Convert.ToInt32(check.Rows[0]["as_number"]);

            //此活動沒有剩餘場次，因此刪除場次and活動
            if ((a - 1) == 0)
            {
                DataTable columndata = _data.GetApplyDataDetail(CommonConvert.GetIntOrZero(dict["as_idn"]));
                var dict_column = new Dictionary<string, object>();
                for (int i = 0; i < columndata.Rows.Count; i++)
                {
                    dict_column["acc_idn"] = columndata.Rows[i][2].ToString();

                    _columndata.DeleteData(dict_column);
                }
                
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
            return _copperate.DeleteData(dict);
        }

        //刪除報名資料
        public CommonResult DeleteApply(Dictionary<string, object> dict, Dictionary<string, object> dict_aa)
        {
            DataTable data = GetApplyDeleteData(CommonConvert.GetIntOrZero(dict["as_idn"]), Convert.ToInt32(dict_aa["aa_idn"]));
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

        //取得場次資料
        public List<Activity_sessionInfo> getSession(int as_act)
        {
            return _data.getSession(as_act);
        }

        //取得報名表區塊資料
        public List<Activity_sectionInfo> getSection(int acs_act)
        {
            return _data.getSection(acs_act);
        }

        //取得報明表欄位資料
        public List<Activity_columnInfo> getColumn(int acc_act)
        {
            return _data.getColumn(acc_act);
        }

        /// 取得已發佈活動資料
        public DataTable GetAlreadyData()
        {
            return _data.GetAlreadyList();
        }

        //取得未發佈活動資料
        public DataTable GetReadyData()
        {
            return _data.GetReadyList();
        }

        //取得已結束活動資料
        public DataTable GetEndData()
        {
            return _data.GetEndList();
        }

        //取得協作者資料
        public DataTable GetCopData(int i)
        {
            return _copperate.GetList(i);
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
            DataTable column = _data.GetApplyDataDetail(i);
            //欄位實際值
            DataTable columnDetail = new DataTable();
            DataTable time = new DataTable();

            if (column.Rows.Count != 0)
            {
                //新增key的值&Row
                test.Columns.Add("aa_idn");
                //找到姓名欄位
                foreach (DataRow r in column.Rows)
                {
                    if (r.ItemArray.GetValue(3).ToString() == "姓名")
                    {
                        time = _data.GetApplyDataColumn(CommonConvert.GetIntOrZero(r.ItemArray.GetValue(2)), i);
                    }
                }
                //加入列的資料
                for (int j = 0; j < time.Rows.Count; j++)
                {
                    test.Rows.Add();
                    test.Rows[j][0] = time.Rows[j][0].ToString();
                }

                int count = 0;
                //存入欄位的值
                foreach (DataRow r in column.Rows)
                {
                    //判斷欄位是否重複
                    int checkcolumn = 1;
                    for (int j = 0; j < count; j++)
                    {
                        if (column.Rows[j].ItemArray.GetValue(3).ToString() == r.ItemArray.GetValue(3).ToString())
                        {
                            r.ItemArray.SetValue(r.ItemArray.GetValue(3) + "(" + checkcolumn + ")", 3);
                            checkcolumn++;
                        }
                    }

                    //增加欄位
                    if (r.ItemArray.GetValue(8).ToString() == "1")
                    {
                        test.Columns.Add(r.ItemArray.GetValue(3).ToString() + "*");
                    }
                    else
                    {
                        test.Columns.Add(r.ItemArray.GetValue(3).ToString());
                    }
                    //test.Columns.Add(r.ItemArray.GetValue(3).ToString());
                    columnDetail = _data.GetApplyDataColumn(CommonConvert.GetIntOrZero(r.ItemArray.GetValue(2)), i);

                    if (columnDetail.Rows.Count != 0)
                    {
                        int k = 0;

                        //存入列的值
                        for (int j = 0; j < test.Rows.Count; j++)
                        {
                            //判斷是否有跳資料
                            if (test.Rows[j][0].ToString() == columnDetail.Rows[k][0].ToString())
                            {
                                if (r.ItemArray.GetValue(8).ToString() == "1")
                                {
                                    test.Rows[j][r.ItemArray.GetValue(3).ToString() + "*"] = columnDetail.Rows[k][1].ToString();
                                }
                                else
                                {
                                    test.Rows[j][r.ItemArray.GetValue(3).ToString()] = columnDetail.Rows[k][1].ToString();
                                }
                                //test.Rows[j][r.ItemArray.GetValue(3).ToString()] = columnDetail.Rows[k][1].ToString();
                                k++;
                            }
                            //若此欄位是空的則補null
                            else
                            {
                                test.Rows[j][r.ItemArray.GetValue(3).ToString()] = null;
                            }
                        }
                    }

                    count++;
                }

                //加入報名時間
                test.Columns.Add("報名時間");
                for (int j = 0; j < time.Rows.Count; j++)
                {
                    test.Rows[j]["報名時間"] = time.Rows[j][2].ToString();
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

        //取得查詢資料(依分類)
        public DataTable GetQueryClassData(string keyword, int tab, string cl)
        {
            DataTable data = new DataTable();

            //已結束活動
            if (tab == 2)
            {
                data = _data.GetQueryEndClassData(keyword, cl);
            }
            //已發佈活動
            else if (tab == 0)
            {
                data = _data.GetQueryClassData(keyword, 1, cl);
            }
            //未發佈活動
            else if (tab == 1)
            {
                data = _data.GetQueryClassData(keyword, 0, cl);
            }

            return data;
        }

        //取得多選的選項資料
        public string GetMultiOption (string multi)
        {
            string option = _data.GetMultiOption(CommonConvert.GetIntOrZero(multi)).Rows[0][0].ToString();

            return option;
        }

        //取得場次的限制人數
        public int GetApplyLimit(int i)
        {
            DataTable data = _data.GetApplyLimit(i);

            int limit = CommonConvert.GetIntOrZero(data.Rows[0][0]);

            return limit;
        }

        //取得email資料
        public DataTable GetEmailData ()
        {
            return _data.GetEmailData();
        }

        //取得帳號資料
        public DataTable GetAccountData()
        {
            return _data.GetAccountData();
        }

        //取得分類資料
        public DataTable GetClassData()
        {
            return _data.GetClassData();
        }
        #endregion

        #region 取得區塊列表
        public List<Activity_sectionInfo> GetSectionList(int acs_act)
        {
            return _data.GetSectionList(acs_act);
        }
        #endregion

        #region 取得問題列表
        public List<Activity_columnInfo> GetQuestionList(int acc_act)
        {
            return _data.GetQuestionList(acc_act);
        }
        #endregion

        #region 匯出Excel

        #region 取得活動資訊
        public BLResult<Dictionary<string, object>> GetActivityData(int act_idn)
        {
            var result = new BLResult<Dictionary<string, object>>("", MESSAGE_TITLE, ITCEnum.PopupMessageType.Success);

            #region -- 執行查詢 --
            if (result.PopupMessageType != ITCEnum.PopupMessageType.Error)
            {
                var dt = _data.getActivityData(act_idn);
                var param = new Dictionary<string, object>();
                param["title"] = dt.Rows[0]["act_title"].ToString();

                result.Result = new Dictionary<string, object>();
                result.Result["dt"] = dt;
                result.Result["param"] = param;

                if (dt.Rows.Count == 0)
                {
                    result.Message = "查無資料。";
                    result.PopupMessageType = ITCEnum.PopupMessageType.Warning;
                }
            }
            #endregion

            return result;
        }

        public BLResult<Dictionary<string, object>> GetActivityExcelData(Dictionary<string, object> Data)
        {
            var result = new BLResult<Dictionary<string, object>>("", MESSAGE_TITLE, ITCEnum.PopupMessageType.Success);

            #region -- 資料格式驗證 --
            if (Data == null)
            {
                result.Message = "查無資料，無法匯出。";
                result.PopupMessageType = ITCEnum.PopupMessageType.Error;
            }
            else if (Data["dt"] == null)
            {
                result.Message = "查無資料，無法匯出。";
                result.PopupMessageType = ITCEnum.PopupMessageType.Error;
            }
            else if ((Data["dt"] as DataTable).Rows.Count == 0)
            {
                result.Message = "查無資料，無法匯出。";
                result.PopupMessageType = ITCEnum.PopupMessageType.Error;
            }
            #endregion

            #region -- 執行製檔 --
            if (result.PopupMessageType != ITCEnum.PopupMessageType.Error)
            {
                var data = Data["dt"] as DataTable;
                var param = Data["param"] as Dictionary<string, object>;

                DataTable dt = new DataTable();
                OrderedDictionary colname = new OrderedDictionary();
                string filename = param["title"].ToString();
                string sheetname = "活動";
                Dictionary<string, ITCEnum.NPOIExcelFormat> format = new Dictionary<string, ITCEnum.NPOIExcelFormat>();

                #region - 建立欄位 -
                colname.Add("act_idn", "活動序號");
                colname.Add("act_title", "活動標題");
                colname.Add("act_desc", "活動描述");
                colname.Add("act_unit", "主辦單位");
                colname.Add("act_contact_name", "聯絡人");
                colname.Add("act_contact_phone", "聯絡電話");
                colname.Add("act_relate_file", "附加檔案");
                colname.Add("act_relate_link", "相關連結");
                colname.Add("act_short_link", "短網址");
                colname.Add("act_num_limit", "場次報名次數限制");
                colname.Add("act_as", "個資聲明");
                colname.Add("act_isopen", "是否發佈");
                colname.Add("act_class", "活動分類");
                colname.Add("act_image", "活動圖片");
                colname.Add("createtime", "建立時間");
                colname.Add("createid", "建立者");
                colname.Add("updid", "異動人");
                colname.Add("updtime", "異動時間");

                dt.Columns.Add("act_idn", typeof(int));
                dt.Columns.Add("act_title", typeof(string));
                dt.Columns.Add("act_desc", typeof(string));
                dt.Columns.Add("act_unit", typeof(string));
                dt.Columns.Add("act_contact_name", typeof(string));
                dt.Columns.Add("act_relate_file", typeof(string));
                dt.Columns.Add("act_relate_link", typeof(string));
                dt.Columns.Add("act_short_link", typeof(string));
                dt.Columns.Add("act_num_limit", typeof(int));
                dt.Columns.Add("act_as", typeof(int));
                dt.Columns.Add("act_isopen", typeof(int));
                dt.Columns.Add("act_class", typeof(int));
                dt.Columns.Add("act_image", typeof(string));
                dt.Columns.Add("createtime", typeof(DateTime));
                dt.Columns.Add("createid", typeof(string));
                dt.Columns.Add("updid", typeof(string));
                dt.Columns.Add("updtime", typeof(DateTime));

                format.Add("act_idn", ITCEnum.NPOIExcelFormat.Integer);
                format.Add("act_title", ITCEnum.NPOIExcelFormat.Other);
                format.Add("act_desc", ITCEnum.NPOIExcelFormat.Other);
                format.Add("act_unit", ITCEnum.NPOIExcelFormat.Other);
                format.Add("act_contact_name", ITCEnum.NPOIExcelFormat.Other);
                format.Add("act_relate_file", ITCEnum.NPOIExcelFormat.Other);
                format.Add("act_relate_link", ITCEnum.NPOIExcelFormat.Other);
                format.Add("act_short_link", ITCEnum.NPOIExcelFormat.Other);
                format.Add("act_num_limit", ITCEnum.NPOIExcelFormat.Integer);
                format.Add("act_as", ITCEnum.NPOIExcelFormat.Integer);
                format.Add("act_isopen", ITCEnum.NPOIExcelFormat.Integer);
                format.Add("act_class", ITCEnum.NPOIExcelFormat.Integer);
                format.Add("act_image", ITCEnum.NPOIExcelFormat.Other);
                format.Add("createtime", ITCEnum.NPOIExcelFormat.DateTime);
                format.Add("createid", ITCEnum.NPOIExcelFormat.Other);
                format.Add("updid", ITCEnum.NPOIExcelFormat.Other);
                format.Add("updtime", ITCEnum.NPOIExcelFormat.DateTime);
                #endregion

                #region - 塞資料 -
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    var dr = dt.NewRow();

                    dr["act_idn"] = data.Rows[i]["act_idn"];
                    dr["act_title"] = data.Rows[i]["act_title"];
                    dr["act_desc"] = data.Rows[i]["act_desc"];
                    dr["act_unit"] = data.Rows[i]["act_unit"];
                    dr["act_contact_name"] = data.Rows[i]["act_contact_name"];
                    dr["act_relate_file"] = data.Rows[i]["act_relate_file"];
                    dr["act_relate_link"] = data.Rows[i]["act_relate_link"];
                    dr["act_short_link"] = data.Rows[i]["act_short_link"];
                    dr["act_num_limit"] = data.Rows[i]["act_num_limit"];
                    dr["act_as"] = data.Rows[i]["act_as"];
                    dr["act_isopen"] = data.Rows[i]["act_isopen"];
                    dr["act_class"] = data.Rows[i]["act_class"];
                    dr["act_image"] = data.Rows[i]["act_image"];
                    dr["createtime"] = data.Rows[i]["createtime"];
                    dr["createid"] = data.Rows[i]["createid"];
                    dr["updid"] = data.Rows[i]["updid"];
                    dr["updtime"] = data.Rows[i]["updtime"];

                    dt.Rows.Add(dr);
                }
                #endregion

                result.Result = new Dictionary<string, object>();
                result.Result.Add("dt", dt);
                result.Result.Add("colname", colname);
                result.Result.Add("filename", filename);
                result.Result.Add("format", format);
                result.Result.Add("sheetname", sheetname);
            }
            #endregion

            return result;
        }
        #endregion

        #region 取得場次資訊
        public BLResult<Dictionary<string, object>> GetSessionData(int as_act)
        {
            var result = new BLResult<Dictionary<string, object>>("", MESSAGE_TITLE, ITCEnum.PopupMessageType.Success);

            #region -- 執行查詢 --
            if (result.PopupMessageType != ITCEnum.PopupMessageType.Error)
            {
                var dt = _data.getSessionData(as_act);
                var param = new Dictionary<string, object>();
                param["title"] = dt.Rows[0]["as_title"].ToString();

                result.Result = new Dictionary<string, object>();
                result.Result["dt"] = dt;
                result.Result["param"] = param;

                if (dt.Rows.Count == 0)
                {
                    result.Message = "查無資料。";
                    result.PopupMessageType = ITCEnum.PopupMessageType.Warning;
                }
            }
            #endregion

            return result;
        }

        public BLResult<Dictionary<string, object>> GetSessionExcelData(Dictionary<string, object> Data)
        {
            var result = new BLResult<Dictionary<string, object>>("", MESSAGE_TITLE, ITCEnum.PopupMessageType.Success);

            #region -- 資料格式驗證 --
            if (Data == null)
            {
                result.Message = "查無資料，無法匯出。";
                result.PopupMessageType = ITCEnum.PopupMessageType.Error;
            }
            else if (Data["dt"] == null)
            {
                result.Message = "查無資料，無法匯出。";
                result.PopupMessageType = ITCEnum.PopupMessageType.Error;
            }
            else if ((Data["dt"] as DataTable).Rows.Count == 0)
            {
                result.Message = "查無資料，無法匯出。";
                result.PopupMessageType = ITCEnum.PopupMessageType.Error;
            }
            #endregion

            #region -- 執行製檔 --
            if (result.PopupMessageType != ITCEnum.PopupMessageType.Error)
            {
                var data = Data["dt"] as DataTable;
                var param = Data["param"] as Dictionary<string, object>;

                DataTable dt = new DataTable();
                OrderedDictionary colname = new OrderedDictionary();
                string filename = param["title"].ToString();
                string sheetname = "場次";
                Dictionary<string, ITCEnum.NPOIExcelFormat> format = new Dictionary<string, ITCEnum.NPOIExcelFormat>();

                #region - 建立欄位 -
                colname.Add("as_idn", "場次序號");
                colname.Add("as_act", "活動序號");
                colname.Add("as_title", "場次名稱");
                colname.Add("as_remark", "場次備註");
                colname.Add("as_relate_link", "場次相關連結");
                colname.Add("as_short_link", "場次短網址");
                colname.Add("as_date_start", "活動開始日期");
                colname.Add("as_date_end", "活動結束日期");
                colname.Add("as_apply_start", "開始報名時間");
                colname.Add("as_apply_end", "結束報名時間");
                colname.Add("as_position", "活動地點");
                colname.Add("as_gmap", "Google Map");
                colname.Add("as_num_limit", "報名人數限制");
                colname.Add("as_isopen", "場次是否發佈");
                colname.Add("createid", "建立者");
                colname.Add("createtime", "建立時間");
                colname.Add("updid", "異動人");
                colname.Add("updtime", "異動時間");

                dt.Columns.Add("as_idn", typeof(int));
                dt.Columns.Add("as_act", typeof(int));
                dt.Columns.Add("as_title", typeof(string));
                dt.Columns.Add("as_remark", typeof(string));
                dt.Columns.Add("as_relate_link", typeof(string));
                dt.Columns.Add("as_short_link", typeof(string));
                dt.Columns.Add("as_date_start", typeof(DateTime));
                dt.Columns.Add("as_date_end", typeof(DateTime));
                dt.Columns.Add("as_apply_start", typeof(DateTime));
                dt.Columns.Add("as_apply_end", typeof(DateTime));
                dt.Columns.Add("as_position", typeof(string));
                dt.Columns.Add("as_gmap", typeof(string));
                dt.Columns.Add("as_num_limit", typeof(int));
                dt.Columns.Add("as_isopen", typeof(int));
                dt.Columns.Add("createid", typeof(string));
                dt.Columns.Add("createtime", typeof(DateTime));
                dt.Columns.Add("updid", typeof(string));
                dt.Columns.Add("updtime", typeof(DateTime));

                format.Add("as_idn", ITCEnum.NPOIExcelFormat.Integer);
                format.Add("as_act", ITCEnum.NPOIExcelFormat.Integer);
                format.Add("as_title", ITCEnum.NPOIExcelFormat.Other);
                format.Add("as_remark", ITCEnum.NPOIExcelFormat.Other);
                format.Add("as_relate_link", ITCEnum.NPOIExcelFormat.Other);
                format.Add("as_short_link", ITCEnum.NPOIExcelFormat.Other);
                format.Add("as_date_start", ITCEnum.NPOIExcelFormat.DateTime);
                format.Add("as_date_end", ITCEnum.NPOIExcelFormat.DateTime);
                format.Add("as_apply_start", ITCEnum.NPOIExcelFormat.DateTime);
                format.Add("as_apply_end", ITCEnum.NPOIExcelFormat.DateTime);
                format.Add("as_position", ITCEnum.NPOIExcelFormat.Other);
                format.Add("as_gmap", ITCEnum.NPOIExcelFormat.Other);
                format.Add("as_num_limit", ITCEnum.NPOIExcelFormat.Integer);
                format.Add("as_isopen", ITCEnum.NPOIExcelFormat.Integer);
                format.Add("createid", ITCEnum.NPOIExcelFormat.Other);
                format.Add("createtime", ITCEnum.NPOIExcelFormat.DateTime);
                format.Add("updid", ITCEnum.NPOIExcelFormat.Other);
                format.Add("updtime", ITCEnum.NPOIExcelFormat.DateTime);
                #endregion

                #region - 塞資料 -
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    var dr = dt.NewRow();

                    dr["as_idn"] = data.Rows[i]["as_idn"];
                    dr["as_act"] = data.Rows[i]["as_act"];
                    dr["as_title"] = data.Rows[i]["as_title"];
                    dr["as_remark"] = data.Rows[i]["as_remark"];
                    dr["as_relate_link"] = data.Rows[i]["as_relate_link"];
                    dr["as_short_link"] = data.Rows[i]["as_short_link"];
                    dr["as_date_start"] = data.Rows[i]["as_date_start"];
                    dr["as_date_end"] = data.Rows[i]["as_date_end"];
                    dr["as_apply_start"] = data.Rows[i]["as_apply_start"];
                    dr["as_apply_end"] = data.Rows[i]["as_apply_end"];
                    dr["as_position"] = data.Rows[i]["as_position"];
                    dr["as_gmap"] = data.Rows[i]["as_gmap"];
                    dr["as_num_limit"] = data.Rows[i]["as_num_limit"];
                    dr["as_isopen"] = data.Rows[i]["as_isopen"];
                    dr["createid"] = data.Rows[i]["createid"];
                    dr["createtime"] = data.Rows[i]["createtime"];
                    dr["updid"] = data.Rows[i]["updid"];
                    dr["updtime"] = data.Rows[i]["updtime"];

                    dt.Rows.Add(dr);
                }
                #endregion

                result.Result = new Dictionary<string, object>();
                result.Result.Add("dt", dt);
                result.Result.Add("colname", colname);
                result.Result.Add("filename", filename);
                result.Result.Add("format", format);
                result.Result.Add("sheetname", sheetname);
            }
            #endregion

            return result;
        }
        #endregion

        #region 取得活動報名區塊資訊
        public BLResult<Dictionary<string, object>> GetSectionData(int acs_act)
        {
            var result = new BLResult<Dictionary<string, object>>("", MESSAGE_TITLE, ITCEnum.PopupMessageType.Success);

            #region -- 執行查詢 --
            if (result.PopupMessageType != ITCEnum.PopupMessageType.Error)
            {
                var dt = _data.getSectionData(acs_act);
                var param = new Dictionary<string, object>();
                param["title"] = dt.Rows[0]["acs_title"].ToString();

                result.Result = new Dictionary<string, object>();
                result.Result["dt"] = dt;
                result.Result["param"] = param;

                if (dt.Rows.Count == 0)
                {
                    result.Message = "查無資料。";
                    result.PopupMessageType = ITCEnum.PopupMessageType.Warning;
                }
            }
            #endregion

            return result;
        }

        public BLResult<Dictionary<string, object>> GetSectionExcelData(Dictionary<string, object> Data)
        {
            var result = new BLResult<Dictionary<string, object>>("", MESSAGE_TITLE, ITCEnum.PopupMessageType.Success);

            #region -- 資料格式驗證 --
            if (Data == null)
            {
                result.Message = "查無資料，無法匯出。";
                result.PopupMessageType = ITCEnum.PopupMessageType.Error;
            }
            else if (Data["dt"] == null)
            {
                result.Message = "查無資料，無法匯出。";
                result.PopupMessageType = ITCEnum.PopupMessageType.Error;
            }
            else if ((Data["dt"] as DataTable).Rows.Count == 0)
            {
                result.Message = "查無資料，無法匯出。";
                result.PopupMessageType = ITCEnum.PopupMessageType.Error;
            }
            #endregion

            #region -- 執行製檔 --
            if (result.PopupMessageType != ITCEnum.PopupMessageType.Error)
            {
                var data = Data["dt"] as DataTable;
                var param = Data["param"] as Dictionary<string, object>;

                DataTable dt = new DataTable();
                OrderedDictionary colname = new OrderedDictionary();
                string filename = param["title"].ToString();
                string sheetname = "活動報名區塊";
                Dictionary<string, ITCEnum.NPOIExcelFormat> format = new Dictionary<string, ITCEnum.NPOIExcelFormat>();

                #region - 建立欄位 -
                colname.Add("acs_idn", "區塊序號");
                colname.Add("acs_act", "活動序號");
                colname.Add("acs_title", "區塊標題");
                colname.Add("acs_desc", "區塊描述");
                colname.Add("acs_seq", "區塊排序");
                colname.Add("createid", "建立者");
                colname.Add("createtime", "建立時間");
                colname.Add("updid", "異動人");
                colname.Add("updtime", "異動時間");

                dt.Columns.Add("acs_idn", typeof(int));
                dt.Columns.Add("acs_act", typeof(int));
                dt.Columns.Add("acs_title", typeof(string));
                dt.Columns.Add("acs_desc", typeof(string));
                dt.Columns.Add("acs_seq", typeof(int));
                dt.Columns.Add("createid", typeof(string));
                dt.Columns.Add("createtime", typeof(DateTime));
                dt.Columns.Add("updid", typeof(string));
                dt.Columns.Add("updtime", typeof(DateTime));

                format.Add("acs_idn", ITCEnum.NPOIExcelFormat.Integer);
                format.Add("acs_act", ITCEnum.NPOIExcelFormat.Integer);
                format.Add("acs_title", ITCEnum.NPOIExcelFormat.Other);
                format.Add("acs_desc", ITCEnum.NPOIExcelFormat.Other);
                format.Add("acs_seq", ITCEnum.NPOIExcelFormat.Integer);
                format.Add("createid", ITCEnum.NPOIExcelFormat.Other);
                format.Add("createtime", ITCEnum.NPOIExcelFormat.DateTime);
                format.Add("updid", ITCEnum.NPOIExcelFormat.Other);
                format.Add("updtime", ITCEnum.NPOIExcelFormat.DateTime);
                #endregion

                #region - 塞資料 -
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    var dr = dt.NewRow();

                    dr["acs_idn"] = data.Rows[i]["acs_idn"];
                    dr["acs_act"] = data.Rows[i]["acs_act"];
                    dr["acs_title"] = data.Rows[i]["acs_title"];
                    dr["acs_desc"] = data.Rows[i]["acs_desc"];
                    dr["acs_seq"] = data.Rows[i]["acs_seq"];
                    dr["createid"] = data.Rows[i]["createid"];
                    dr["createtime"] = data.Rows[i]["createtime"];
                    dr["updid"] = data.Rows[i]["updid"];
                    dr["updtime"] = data.Rows[i]["updtime"];

                    dt.Rows.Add(dr);
                }
                #endregion

                result.Result = new Dictionary<string, object>();
                result.Result.Add("dt", dt);
                result.Result.Add("colname", colname);
                result.Result.Add("filename", filename);
                result.Result.Add("format", format);
                result.Result.Add("sheetname", sheetname);
            }
            #endregion

            return result;
        }
        #endregion

        #region 取得活動報名欄位資訊
        public BLResult<Dictionary<string, object>> GetColumnData(int acc_act)
        {
            var result = new BLResult<Dictionary<string, object>>("", MESSAGE_TITLE, ITCEnum.PopupMessageType.Success);

            #region -- 執行查詢 --
            if (result.PopupMessageType != ITCEnum.PopupMessageType.Error)
            {
                var dt = _data.getColumnData(acc_act);
                var param = new Dictionary<string, object>();
                param["title"] = dt.Rows[0]["acc_title"].ToString();

                result.Result = new Dictionary<string, object>();
                result.Result["dt"] = dt;
                result.Result["param"] = param;

                if (dt.Rows.Count == 0)
                {
                    result.Message = "查無資料。";
                    result.PopupMessageType = ITCEnum.PopupMessageType.Warning;
                }
            }
            #endregion

            return result;
        }

        public BLResult<Dictionary<string, object>> GetColumnExcelData(Dictionary<string, object> Data)
        {
            var result = new BLResult<Dictionary<string, object>>("", MESSAGE_TITLE, ITCEnum.PopupMessageType.Success);

            #region -- 資料格式驗證 --
            if (Data == null)
            {
                result.Message = "查無資料，無法匯出。";
                result.PopupMessageType = ITCEnum.PopupMessageType.Error;
            }
            else if (Data["dt"] == null)
            {
                result.Message = "查無資料，無法匯出。";
                result.PopupMessageType = ITCEnum.PopupMessageType.Error;
            }
            else if ((Data["dt"] as DataTable).Rows.Count == 0)
            {
                result.Message = "查無資料，無法匯出。";
                result.PopupMessageType = ITCEnum.PopupMessageType.Error;
            }
            #endregion

            #region -- 執行製檔 --
            if (result.PopupMessageType != ITCEnum.PopupMessageType.Error)
            {
                var data = Data["dt"] as DataTable;
                var param = Data["param"] as Dictionary<string, object>;

                DataTable dt = new DataTable();
                OrderedDictionary colname = new OrderedDictionary();
                string filename = param["title"].ToString();
                string sheetname = "活動報名欄位";
                Dictionary<string, ITCEnum.NPOIExcelFormat> format = new Dictionary<string, ITCEnum.NPOIExcelFormat>();

                #region - 建立欄位 -
                colname.Add("acc_idn", "欄位序號");
                colname.Add("acc_asc", "區塊序號");
                colname.Add("acc_act", "活動序號");
                colname.Add("acc_title", "欄位標題");
                colname.Add("acc_desc", "欄位描述");
                colname.Add("acc_seq", "欄位排序");
                colname.Add("acc_type", "填寫方式");
                colname.Add("acc_option", "選項");
                colname.Add("acc_required", "是否必填");
                colname.Add("acc_validation", "驗證方式");
                colname.Add("createid", "建立者");
                colname.Add("createtime", "建立時間");
                colname.Add("updid", "異動人");
                colname.Add("updtime", "異動時間");

                dt.Columns.Add("acc_idn", typeof(int));
                dt.Columns.Add("acc_asc", typeof(int));
                dt.Columns.Add("acc_act", typeof(int));
                dt.Columns.Add("acc_title", typeof(string));
                dt.Columns.Add("acc_desc", typeof(string));
                dt.Columns.Add("acc_seq", typeof(int));
                dt.Columns.Add("acc_type", typeof(string));
                dt.Columns.Add("acc_option", typeof(string));
                dt.Columns.Add("acc_required", typeof(int));
                dt.Columns.Add("acc_validation", typeof(string));
                dt.Columns.Add("createid", typeof(string));
                dt.Columns.Add("createtime", typeof(DateTime));
                dt.Columns.Add("updid", typeof(string));
                dt.Columns.Add("updtime", typeof(DateTime));

                format.Add("acc_idn", ITCEnum.NPOIExcelFormat.Integer);
                format.Add("acc_asc", ITCEnum.NPOIExcelFormat.Integer);
                format.Add("acc_act", ITCEnum.NPOIExcelFormat.Integer);
                format.Add("acc_title", ITCEnum.NPOIExcelFormat.Other);
                format.Add("acc_desc", ITCEnum.NPOIExcelFormat.Other);
                format.Add("acc_seq", ITCEnum.NPOIExcelFormat.Integer);
                format.Add("acc_type", ITCEnum.NPOIExcelFormat.Other);
                format.Add("acc_option", ITCEnum.NPOIExcelFormat.Other);
                format.Add("acc_required", ITCEnum.NPOIExcelFormat.Integer);
                format.Add("acc_validation", ITCEnum.NPOIExcelFormat.Other);
                format.Add("createid", ITCEnum.NPOIExcelFormat.Other);
                format.Add("createtime", ITCEnum.NPOIExcelFormat.DateTime);
                format.Add("updid", ITCEnum.NPOIExcelFormat.Other);
                format.Add("updtime", ITCEnum.NPOIExcelFormat.DateTime);
                #endregion

                #region - 塞資料 -
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    var dr = dt.NewRow();

                    dr["acc_idn"] = data.Rows[i]["acc_idn"];
                    dr["acc_asc"] = data.Rows[i]["acc_asc"];
                    dr["acc_act"] = data.Rows[i]["acc_act"];
                    dr["acc_title"] = data.Rows[i]["acc_title"];
                    dr["acc_desc"] = data.Rows[i]["acc_desc"];
                    dr["acc_seq"] = data.Rows[i]["acc_seq"];
                    dr["acc_type"] = data.Rows[i]["acc_type"];
                    dr["acc_option"] = data.Rows[i]["acc_option"];
                    dr["acc_required"] = data.Rows[i]["acc_required"];
                    dr["acc_validation"] = data.Rows[i]["acc_validation"];
                    dr["createid"] = data.Rows[i]["createid"];
                    dr["createtime"] = data.Rows[i]["createtime"];
                    dr["updid"] = data.Rows[i]["updid"];
                    dr["updtime"] = data.Rows[i]["updtime"];

                    dt.Rows.Add(dr);
                }
                #endregion

                result.Result = new Dictionary<string, object>();
                result.Result.Add("dt", dt);
                result.Result.Add("colname", colname);
                result.Result.Add("filename", filename);
                result.Result.Add("format", format);
                result.Result.Add("sheetname", sheetname);
            }
            #endregion

            return result;
        }
        #endregion

        #endregion
    }
}
