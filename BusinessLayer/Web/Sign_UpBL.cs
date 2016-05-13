using System.Collections.Generic;
using DataAccess.Web;
using Util;
using Model;
using System.Data;
using DataAccess;

namespace BusinessLayer.Web
{
    public class Sign_UpBL : BaseBL
    {

        Sign_UpData _data = new Sign_UpData();
        Activity_apply_emailData _emaildata = new Activity_apply_emailData();

        #region  --查詢--

        #region 取得區塊列表
        public List<Activity_sectionInfo> GetSectionList(int acs_act, int as_idn)
        {
            return _data.GetSectionList(acs_act, as_idn);
        }
        #endregion

        #region 取得問題列表
        public List<Activity_columnInfo> GetQuestionList(int acc_act,int as_idn)
        {
            return _data.GetQuestionList(acc_act,as_idn);
        }
        #endregion

        #region 取得活動
        public DataTable GetActivityData(int as_act, int as_idn)
        {
            return _data.GetActivityData(as_act, as_idn);
        }
        #endregion

        #region 取得email密碼
        public DataTable GetEmailData(string aae_email)
        {
            return _emaildata.getPassword(aae_email);
        }
        #endregion
        
        #region 取得活動證明資訊
        public DataTable GetApplyProve(int aa_idn)
        {
            return _data.GetApplyProve(aa_idn);
        }
        #endregion

        #region 判斷是否在報名日期內
        public bool isBetweenApplyDate(int as_idn)
        {
            List<Activity_sessionInfo> asList = _data.isBetweenApplyDate(as_idn);            
            return asList.Count > 0 ? true : false;
        }
        #endregion
        #region 判斷場次是否開放
        public bool isOpen(int as_idn)
        {
            List<Activity_sessionInfo> asList = _data.isOpen(as_idn);
            return asList.Count > 0 ? true : false;
        }
        #endregion
        #region 判斷報名人數是否額滿
        public bool isFull(int aa_as)
        {
            List<Activity_sessionInfo> asList = _data.isFull(aa_as);
            return asList.Count > 0 ? false : true;
        }
        #endregion
        #region 判斷是否重複報名
        public bool isRepeatApply(int aa_as, string aa_email, string aa_name)
        {
            List<Activity_applyInfo> aaList = _data.isRepeatApply(aa_as, aa_email, aa_name);
            return aaList.Count > 0 ? true : false;
        }
        #endregion
        #region 判斷是否超過報名限制
        public bool isOverApplyLimit(int act_idn, string aa_email, string aa_name)
        {
            List<Activity_applyInfo> aaList = _data.isOverApplyLimit(act_idn, aa_email, aa_name);
            return aaList.Count > 0 ? true : false;
        }
        #endregion
        #endregion

        #region  --新增--

        #region 新增報名資料
        public CommonResult InsertData_Activity_apply(Dictionary<string, object> dict)
        {
            var res = CommonHelper.ValidateModel<Model.Activity_applyInfo>(dict);

            if (res.IsSuccess)
            {
                res = _data.InsertData_apply(dict);
            }
            return res;
        }
        #endregion

        #region 新增報名資料細目
        public CommonResult InsertData_Activity_apply_detail(Dictionary<string, object> dict)
        {
            var res = CommonHelper.ValidateModel<Model.Activity_apply_detailInfo>(dict);

            if (res.IsSuccess)
            {
                res = _data.InsertData_apply_detail(dict);
            }
            return res;
        }
        #endregion

        #region 新增密碼
        public CommonResult InsertData_Password(Dictionary<string, object> dict)
        {
            var res = CommonHelper.ValidateModel<Activity_apply_emailInfo>(dict);

            if (res.IsSuccess)
            {
                res = _emaildata.InsertData(dict);
            }
            return res;
        }
        #endregion
        #endregion

    }
}
