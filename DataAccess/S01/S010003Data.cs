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
    public class S010003Data : BaseData
    {
        CommonDbHelper _db = DAH.Db;

        #region 取得系統下的模組數
        /// <summary>
        /// 取得系統下的模組數
        /// </summary>
        /// <param name="sys_id">系統代碼</param>
        /// <returns></returns>
        public int GetModuleCountBySysId(string sys_id)
        {
            string sql = @"select count(*) from sys_module where sys_id = @sys_id";
            return CommonConvert.GetIntOrZero(_db.ExecuteScalar(sql, _db.GetParam("@sys_id", sys_id)));
        }
        #endregion
    }
}
