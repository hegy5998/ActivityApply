﻿using System.Collections.Generic;
using DataAccess.Web;
using Util;
using Model;
using System.Data;

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

        public CommonResult InsertData_Activity_apply(Dictionary<string, object> dict)
        {
            var res = CommonHelper.ValidateModel<Model.Activity_applyInfo>(dict);

            if (res.IsSuccess)
            {
                res = _data.InsertData_apply(dict);
            }
            return res;
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
