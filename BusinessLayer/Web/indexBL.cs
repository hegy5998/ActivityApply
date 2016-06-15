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
        public DataTable GetActivityAllList()
        {
            return _data.GetActivityAllList();
        }
        #endregion

    }
}
