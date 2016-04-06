using System.Collections.Generic;
using DataAccess.Web;
using Util;
using Model;
using System.Data;

namespace BusinessLayer.Web
{
    public class Sign_UpBL : BaseBL
    {

        Sign_UpData _data = new Sign_UpData();

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
    }
}
