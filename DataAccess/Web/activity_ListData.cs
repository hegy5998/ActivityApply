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
        Activity_classData _classData = new Activity_classData();

        #region 查詢所有開放活動
        public DataTable GetActivityAllList(string act_title,string act_class)
        {
            return _ActivityData.GetActivityAllList(act_title, act_class);
        }
        #endregion

        #region 查詢抓分類
        public List<Activity_classInfo> GetClassList()
        {
            return _classData.GetClassList();
        }
        #endregion

    }
}
