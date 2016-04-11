using System.Collections.Generic;
using DataAccess.Web;
using Util;
using Model;
using System.Data;

namespace BusinessLayer.Web
{
    public class indexBL : BaseBL
    {

        indexData _data = new indexData();

        #region 取得活動列表
        public DataTable GetActivityAllList(string act_title,string act_class)
        {
            return _data.GetActivityAllList(act_title, act_class);
        }
        #endregion

    }
}
