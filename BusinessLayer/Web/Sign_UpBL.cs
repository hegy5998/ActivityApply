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
