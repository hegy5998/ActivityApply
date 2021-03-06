﻿using System.Collections.Generic;
using DataAccess.Web;
using Util;
using Model;
using System.Data;

namespace BusinessLayer.Web
{
    public class activityBL : BaseBL
    {

        activityData _data = new activityData();

        #region 取得活動列表
        public List<ActivityInfo> GetActivityList(int act_idn)
        {
            return _data.GetActivityList(act_idn);
        }
        #endregion

        #region 取得場次列表
        public DataTable GetSessionList(int as_act)
        {
            return _data.GetSessionList(as_act);
        }
        #endregion

        #region 取得個資聲名
        public DataTable GetStateMent(int act_idn)
        {
            return _data.GetStateMent(act_idn);
        }
        #endregion
    }
}
