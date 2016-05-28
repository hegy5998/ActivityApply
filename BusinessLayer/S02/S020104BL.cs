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
        //Email資料
        Activity_apply_emailData _emaildata = new Activity_apply_emailData();

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

        #region 取得信箱
        public List<Activity_apply_emailInfo> getEmail(string aae_email)
        {
            return _data.getEmail(aae_email);
        }
        #endregion

        #region 更改密碼
        public CommonResult UpdateData(Dictionary<string, object> olddict, Dictionary<string, object> newdict)
        {
            return _emaildata.UpdateData(olddict, newdict);
        }
        #endregion

    }
}
