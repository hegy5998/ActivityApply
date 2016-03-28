using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using Util;
using Model;

namespace BusinessLayer.S01
{
    public class UCProcessAuthManagerBL
    {
        #region 取得作業資訊
        public Model.Sys_processInfo GetProcessInfo(string sys_pid)
        {
            var obj = new Sys_processData().GetInfo(sys_pid);
            return obj;
        }
        #endregion

        #region 取得作業的權限資料
        /// <summary>
        /// 取得作業的權限資料
        /// </summary>
        /// <param name="sys_pid">作業代碼</param>
        /// <returns></returns>
        public List<Model.S01.UCProcessAuthManagerInfo.Main> GetList(string sys_pid)
        {
            return new DataAccess.S01.UCProcessAuthManagerData().GetList(sys_pid);
        }
        #endregion

        #region 新增
        /// <summary>
        /// 新增資料
        /// </summary>
        /// <param name="dict">資料</param>
        /// <returns></returns>
        public CommonResult InsertData(Dictionary<string, object> dict)
        {
            var res = CommonHelper.ValidateModel<Model.S01.UCProcessAuthManagerInfo.Main>(dict);
            if (res.IsSuccess)
                res = new Sys_process_roleData().InsertData(dict);
            return res;
        }
        #endregion

        #region 刪除
        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="dict">資料</param>
        /// <returns></returns>
        public CommonResult DeleteData(Dictionary<string, object> dict)
        {
            var res = CommonHelper.ValidateModel<Model.S01.UCProcessAuthManagerInfo.Main>(dict);
            if (res.IsSuccess)
                res = new Sys_process_roleData().DeleteData(dict);
            return res;
        }
        #endregion

        #region 更新
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="oldData_dict">原資料</param>
        /// <param name="newData_dict">新資料</param>
        /// <returns></returns>
        public CommonResult UpdateData(Dictionary<string, object> oldData_dict, Dictionary<string, object> newData_dict)
        {
            var res = CommonHelper.ValidateModel<Model.S01.UCProcessAuthManagerInfo.Main>(newData_dict);
            if (res.IsSuccess)
                res = new Sys_process_roleData().UpdateData(oldData_dict, newData_dict);
            return res;
        }
        #endregion
    }
}
