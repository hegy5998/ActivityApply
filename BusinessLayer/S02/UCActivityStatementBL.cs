using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;
using Model;
using DataAccess;

namespace BusinessLayer.S02
{
    public class UCActivityStatementBL : BaseBL
    {
        Activity_statementData activityStatement_data = new Activity_statementData();

        #region 新增
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="dict">資料</param>
        /// <returns></returns>
        public CommonResult InsertData(Dictionary<string, object> dict)
        {
            var res = CommonHelper.ValidateModel<Activity_statementInfo>(dict);
            if (res.IsSuccess)
            {
                res = activityStatement_data.InsertData(dict);
            }
            return res;
        }
        #endregion

        #region 取得資料
        /// <summary>
        /// 取得資料
        /// </summary>
        /// <param name="ast_id">帳號</param>
        /// <returns></returns>
        public Activity_statementInfo GetData(int ast_id)
        {
            return activityStatement_data.GetInfo(ast_id); ;
        }
        #endregion

        #region 更新
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="old_dict">原資料</param>
        /// <param name="new_dict">新資料</param>
        /// <returns></returns>
        public CommonResult UpdateData(Dictionary<string, object> old_dict, Dictionary<string, object> new_dict)
        {
            var res = CommonHelper.ValidateModel<Activity_statementInfo>(new_dict);
            
            if (res.IsSuccess)
            {
                res = activityStatement_data.UpdateData(old_dict, new_dict);
            }
            return res;
        }

        #endregion
        
    }
}
