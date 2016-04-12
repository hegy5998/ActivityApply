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
    public class activity_ListData : BaseData
    {
        CommonDbHelper Db = DAH.Db;
        ActivityData _ActivityData = new ActivityData();

        #region 查詢
        public DataTable GetActivityAllList(string act_title,string act_class)
        {
            return _ActivityData.GetActivityAllList(act_title, act_class);
        }
        #endregion

        #region 查詢
        public List<Activity_classInfo> GetClassList()
        {
            return _ActivityData.GetClassList();
        }
        #endregion

    }
}
