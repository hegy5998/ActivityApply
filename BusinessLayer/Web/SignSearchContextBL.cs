using System.Collections.Generic;
using DataAccess.Web;
using Util;
using Model;
using System.Data;

namespace BusinessLayer.Web
{
    public class SignSearchContextBL : BaseBL
    {

        SignSearchContextData _data = new SignSearchContextData();

        #region 取得報名資訊
        public DataTable GetActivityData(string aa_email)
        {
            return _data.GetActivityData(aa_email);
        }
        #endregion

        #region 取得email密碼
        public DataTable GetEmailData(string aae_email,string aae_password)
        {
            return _data.GetEmailData(aae_email, aae_password);
        }
        #endregion

        #region 取得email密碼
        public CommonResult DeleteData(Dictionary<string,object> data_dict)
        {
            return _data.DeleteData(data_dict);
        }
        #endregion


    }
}
