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
    public class Sign_UpData : BaseData
    {
        CommonDbHelper Db = DAH.Db;
        Activity_sectionData _sectionData = new Activity_sectionData();
        Activity_columnData _columnData = new Activity_columnData();

        #region 查詢
        public List<Activity_sectionInfo> GetSectionList(int acs_act)
        {
            return _sectionData.GetList(acs_act);
        }

        public List<Activity_columnInfo> GetQuestionList(int acc_act)
        {
            return _columnData.GetList(acc_act);
        }
        #endregion
    }
}
