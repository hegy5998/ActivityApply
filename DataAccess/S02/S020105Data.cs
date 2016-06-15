using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace DataAccess.S02
{
    

    public class S020105Data
    {
        CommonDbHelper Db = DAH.Db;

        #region 查詢
        public List<Activity_statementInfo> GetList(string act_id)
        {
            string sql = @" SELECT *
                            FROM activity_statement
                            WHERE createid = @act_id
                            ORDER BY ast_id;";
            IDataParameter[] param = { Db.GetParam("@act_id", act_id) };
            return Db.GetEnumerable<Activity_statementInfo>(sql, param).ToList();
        }
        #endregion
    }
}
