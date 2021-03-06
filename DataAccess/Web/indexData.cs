﻿using System;
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
            string sql = @"SELECT TOP(9)activity.act_idn,activity.act_title,activity.act_isopen, act_class,act_image,
                            ac_session.as_date_start,
                            ac_session.as_date_end, 
                            ac_session.as_apply_start, 
                            ac_session.as_apply_end,session_count.num
                            FROM activity
                            OUTER apply (SELECT TOP 1 * 
                                FROM   (SELECT TOP 1 * 
                                        FROM   activity_session 
                                        WHERE  as_act = act_idn 
                                               AND act_isopen = 1 
                                               AND as_isopen = 1 
                                               AND CONVERT(DATETIME, as_apply_end, 121) >= CONVERT(VARCHAR(256), Getdate(), 121)
                                        ORDER  BY as_date_start
                                        UNION ALL 
                                        SELECT TOP 1 * 
                                        FROM   activity_session 
                                        WHERE  as_act = act_idn 
                                               AND act_isopen = 1 
                                               AND as_isopen = 1 
                                        ORDER  BY as_date_start) d 
                                ORDER  BY as_apply_end DESC) AS ac_session 
                            cross apply 
                                    (select COUNT(*) as num
                                    FROM activity_session 
								    WHERE as_act = act_idn 
								    AND as_isopen = 1 
								    AND CONVERT(DATETIME, as_date_end, 121) >= CONVERT(varchar(256), GETDATE(), 121) )  as session_count
                            WHERE session_count.num > 0 
                                  AND  ac_session.as_apply_end > CONVERT(varchar(256), GETDATE(), 121)
                            ORDER BY   ac_session.as_date_start";
            return Db.GetDataTable(sql);
        }
        #endregion

    }
}
