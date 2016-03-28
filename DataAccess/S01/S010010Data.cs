using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;
using Model;
using DataAccess;

namespace DataAccess.S01
{
    public class S010010Data
    {
        #region 取得帳號列表
        /// <summary>
        /// 取得帳號列表
        /// </summary>
        /// <param name="cond_dict">查詢條件</param>
        /// <returns></returns>
        public List<Model.S01.S010010Info.Main> GetList(Dictionary<string, string> cond_dict)
        {
            var db = DAH.Db;

            StringBuilder sb = new StringBuilder();
            sb.Append(@"
                select act_id, act_name, act_status
                from sys_account
                order by act_id");

            var lst = db.GetEnumerable<Model.S01.S010010Info.Main>(sb.ToString()).ToList();
            return lst;
        }
        #endregion
    }
}
