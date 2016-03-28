using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Util;
using System.Data;

namespace DataAccess.Common
{
    public class CommonData
    {
        #region 從DB取得系統參數
        /// <summary>
        /// 從DB取得系統參數
        /// </summary>
        /// <returns></returns>
        public static Model.Common.SystemConfigInfo GetSysConfig()
        {
            var raw_dt = DAH.Db.GetDataTable("select * from [sys_config]");
            var datas = raw_dt.AsEnumerable();
            var info = new Model.Common.SystemConfigInfo();
            var properties = info.GetType().GetProperties();

            #region 檢查Model.Common.SystemConfigInfo中的欄位，是否DB中都有設定
            var lostColumns = properties.Where(x => datas.FirstOrDefault(p => p.Field<string>("sys_name") == x.Name) == null);
            if (lostColumns.Count() > 0)
            {
                var lostColumnNames = new List<string>();
                foreach (var item in lostColumns)
                {
                    lostColumnNames.Add(item.Name);
                }
                throw new Exception("系統參數表sys_config缺少設定資料: \n" + string.Join("、\n", lostColumnNames));
            }
            #endregion

            #region 設定資料
            foreach (DataRow item in raw_dt.Rows)
            {
                if (item["sys_value"] != DBNull.Value)
                    properties.FirstOrDefault(x => x.Name == item["sys_name"].ToString()).SetValue(info, item["sys_value"]);
            }
            #endregion

            return info;
        }
        #endregion
    }
}
