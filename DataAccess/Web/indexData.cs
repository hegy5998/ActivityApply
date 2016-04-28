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
    public class indexData : BaseData
    {
        CommonDbHelper Db = DAH.Db;

        #region 查詢
        public DataTable getActivityTopfive()
        {
            string sql = @"SELECT TOP(5)activity.act_idn,activity.act_title,activity.act_isopen, act_class,
                            ac_session.as_date_start,
                            ac_session.as_date_end, 
                            ac_session.as_apply_start, 
                            ac_session.as_apply_end,session_count.num
                            FROM activity
                            cross apply 
                                 (select top 1 *
                                  from activity_session 
                                  where   as_act = act_idn 
                                          AND act_isopen = 1 
                                          AND as_isopen = 1
                                          AND as_apply_end > CONVERT(varchar(256), GETDATE(), 121)
                                  order by as_date_start) as ac_session
                            cross apply 
                                 (select top 1 COUNT(*) as num
                                  FROM activity_session 
								  WHERE as_act = act_idn 
								  AND as_isopen = 1 
								  AND CONVERT(DATETIME, as_date_end, 121) >= CONVERT(varchar(256), GETDATE(), 121) )  as session_count
                            WHERE session_count.num > 0 
                                  AND  ac_session.as_date_start > CONVERT(varchar(256), GETDATE(), 121)
                                  AND  ac_session.as_apply_end > CONVERT(varchar(256), GETDATE(), 121)
                            ORDER BY   ac_session.as_date_start";
            return Db.GetDataTable(sql);
        }
        #endregion

    }
}
