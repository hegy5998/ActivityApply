using System.Collections.Generic;
using DataAccess.Web;
using Util;
using Model;
using System.Data;

namespace BusinessLayer.Web
{
    public class activity_ListBL : BaseBL
    {

        activity_ListData _data = new activity_ListData();

        #region 取得活動列表
        public DataTable GetActivityAllList(string act_title,string act_class)
        {
            return _data.GetActivityAllList(act_title, act_class);
        }
        #endregion

        #region 取得活動列表
        public List<Activity_classInfo> GetClassList()
        {
            return _data.GetClassList();
        }
        #endregion

    }
}
