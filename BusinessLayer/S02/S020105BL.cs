using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;
using DataAccess;
using DataAccess.S02;
using Model;
using System.Data;

namespace BusinessLayer.S02
{
    public class S020105BL : BaseBL
    {
        S020105Data _da = new S020105Data();
        Activity_statementData ast_data = new Activity_statementData();

        #region 取得資料
        /// <summary>
        /// 取得資料
        /// </summary>
        /// <param name="cond_dict">查詢條件</param>
        /// <returns></returns>
        public List<Activity_statementInfo> GetList(string act_id)
        {
            return _da.GetList(act_id);
        }
        #endregion

        public CommonResult DeleteData(Dictionary<string, object> dict)
        {
            return ast_data.DeleteData(dict);
        }
        public DataTable GetCountData(string dict)
        {
            return ast_data.GetCountData(dict);
        }
    }
}
