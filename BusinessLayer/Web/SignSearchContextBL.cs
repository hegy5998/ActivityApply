using System.Collections.Generic;
using DataAccess.Web;
using Util;
using Model;
using System.Data;
using DataAccess;

namespace BusinessLayer.Web
{
    public class SignSearchContextBL : BaseBL
    {

        SignSearchContextData _data = new SignSearchContextData();
        Activity_applyData _applydata = new Activity_applyData();
        Activity_apply_detailData _detaildata = new Activity_apply_detailData();
        Activity_apply_emailData _emaildata = new Activity_apply_emailData();

        #region 取得報名資訊
        public DataTable GetActivityData(string aa_email)
        {
            return _data.GetActivityData(aa_email);
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

        #region 取得報名欄位序號
        public DataTable GetColumnData(string acc_act)
        {
            return _data.GetColumnData(acc_act);
        }
        #endregion

        #region 刪除報名資料
        public CommonResult DeleteApplyData(Dictionary<string,object> dict)
        {
            return _applydata.DeleteData(dict);
        }
        #endregion

        #region 刪除報名資料欄位
        public CommonResult DeleteDetailData(Dictionary<string, object> dict)
        {
            return _detaildata.DeleteData(dict);
        }
        #endregion

        #region 更改密碼
        public CommonResult UpdateData(Dictionary<string, object> olddict, Dictionary<string, object> newdict)
        {
            return _emaildata.UpdateData(olddict,newdict);
        }
        #endregion

    }
}
