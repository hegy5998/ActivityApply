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
        public List<ActivityInfo> GetQuestionList(int act_idn)
        {
            return _ActivityData.GetActivityList(act_idn);
        }
        public List<Activity_sessionInfo> GetSessionList(int as_act)
        {
            return _sessionData.GetList(as_act);
        }

        #endregion




    }
}
