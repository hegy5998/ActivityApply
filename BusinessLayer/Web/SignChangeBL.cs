using System.Collections.Generic;
using DataAccess.Web;
using Util;
using Model;
using System.Data;
using DataAccess;

namespace BusinessLayer.Web
{
    public class SignChangeBL : BaseBL
    {

        SignChangeData _data = new SignChangeData();   

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

        #region 取得問題列表
        public List<Activity_apply_detailInfo> getApplyDetailList(int aad_apply_id)
        {
            return _data.getApplyDetailList(aad_apply_id);
        }
        #endregion

        #region 取得活動
        public DataTable GetActivityData(int as_act, int as_idn)
        {
            return _data.GetActivityData(as_act, as_idn);
        }
        #endregion

        public CommonResult UpdateApplyData(Dictionary<string, object> olddict, Dictionary<string, object> newdict)
        {
            
            return _data.UpdateApplyData(olddict, newdict);

        }

        public CommonResult UpdateApplyDetailData(Dictionary<string, object> olddict, Dictionary<string, object> newdict)
        {

            return _data.UpdateApplyDetailData(olddict, newdict);

        }


        public CommonResult InsertData_Activity_apply_detail(Dictionary<string, object> dict)
        {
            var res = CommonHelper.ValidateModel<Model.Activity_apply_detailInfo>(dict);

            if (res.IsSuccess)
            {
                res = _data.InsertData_apply_detail(dict);
            }
            return res;
        }
    }
}
