using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DataAccess;
using BusinessLayer;
using DataAccess.S01;
using Util;
using System.Data;

namespace BusinessLayer.S01
{
    public class S020104BL
    {
        S020104Data _data = new S020104Data();

        #region 取得場次列表
        public DataTable GetSessionList(int as_act)
        {
            return _data.GetSessionList(as_act);
        }
        #endregion

        #region 取得活動列表
        public List<ActivityInfo> GetActivityList(int act_idn)
        {
            return _data.GetActivityList(act_idn);
        }
        #endregion
    }
}
