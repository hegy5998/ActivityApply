using DataAccess;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace BusinessLayer.S01
{
    [DataObject(true)]
    public class S010001BL : BaseBL
    {
        #region 取得資料
        /// <summary>
        /// 取得資料
        /// </summary>
        /// <returns>參數資料</returns>
        public List<Sys_configInfo> GetData()
        {
            return new Sys_configData().GetList();
        }
        #endregion

        #region 更新資料
        /// <summary>
        /// 更新資料
        /// </summary>
        /// <param name="info">資料物件</param>
        /// <returns>是否成功</returns>
        public void UpdateData(Sys_configInfo info)
        {
            var oldData_dict = new Dictionary<string, object>();
            oldData_dict["sys_name"] = info.Sys_name;

            var newData_dict = new Dictionary<string, object>();
            newData_dict["sys_value"] = info.Sys_value;

            new Sys_configData().UpdateData(oldData_dict, newData_dict);

            // 更新本機系統參數
            CommonHelper.GetSysConfig(true);
        }
        #endregion
    }
}
