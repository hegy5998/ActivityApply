using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;
using Model;
using DataAccess;
using System.Data;

namespace DataAccess.Web
{
    public class activityData : BaseData
    {
        CommonDbHelper Db = DAH.Db;
        ActivityData _ActivityData = new ActivityData();
        Activity_sessionData _sessionData = new Activity_sessionData();

        #region 查詢
        public List<ActivityInfo> GetActivityList(int act_idn)
        {
            return _ActivityData.GetActivityList(act_idn);
        }
        public DataTable GetSessionList(int as_act)
        {
            return _sessionData.GetList(as_act);
        }
        public DataTable GetStateMent(int act_idn)
        {
            string sql = @" SELECT Replace(ast_content, CHAR(10), '<br>') as ast_content
                            FROM activity_statement,activity
                            WHERE ast_id = act_as AND act_idn  = @act_idn  ";
            IDataParameter[] param = { Db.GetParam("@act_idn", act_idn) };
            return Db.GetDataTable(sql, param);
        }
        #endregion
    }
}
