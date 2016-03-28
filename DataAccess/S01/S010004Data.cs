using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;
using Model;
using DataAccess;
using System.Data;

namespace DataAccess.S01
{
    public class S010004Data
    {
        CommonDbHelper _db = DAH.Db;

        #region 取得模組下的作業數
        /// <summary>
        /// 取得模組下的作業數
        /// </summary>
        /// <param name="sys_mid">模組代碼</param>
        /// <returns></returns>
        public int GetProcessCountBySysMid(string sys_mid)
        {
            string sql = @"select count(*) from sys_process where sys_mid = @sys_mid";
            return CommonConvert.GetIntOrZero(_db.ExecuteScalar(sql, _db.GetParam("@sys_mid", sys_mid)));
        }
        #endregion
    }
}
