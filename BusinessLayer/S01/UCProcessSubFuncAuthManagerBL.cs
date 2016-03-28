using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;
using DataAccess;

namespace BusinessLayer.S01
{
    public class UCProcessSubFuncAuthManagerBL
    {
        DataAccess.S01.UCProcessSubFuncAuthManagerData _da = new DataAccess.S01.UCProcessSubFuncAuthManagerData();

        #region 取得特定作業的子功能列表
        /// <summary>
        /// 取得特定作業的子功能列表
        /// </summary>
        /// <param name="sys_pid">作業代碼</param>
        /// <returns></returns>
        public List<Model.S01.UCProcessSubFuncAuthManagerInfo.Main> GetList(string sys_pid)
        {
            return _da.GetList(sys_pid);
        }
        #endregion

        #region 取得特定作業子功能的權限清單
        /// <summary>
        /// 取得特定作業子功能的權限清單
        /// </summary>
        /// <param name="sys_pid">作業代碼</param>
        /// <param name="sys_cid">子功能代碼</param>
        /// <returns></returns>
        public List<Model.S01.UCProcessSubFuncAuthManagerInfo.Auth> GetAuthList(string sys_pid, string sys_cid)
        {
            return _da.GetAuthList(sys_pid, sys_cid);
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
            var res = CommonHelper.ValidateModel<Model.S01.UCProcessSubFuncAuthManagerInfo.Auth>(dict);
            if (res.IsSuccess)
                res = new Sys_processcontrol_roleData().InsertData(dict);
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
            var res = CommonHelper.ValidateModel<Model.S01.UCProcessSubFuncAuthManagerInfo.Auth>(dict);
            if (res.IsSuccess)
                res = new Sys_processcontrol_roleData().DeleteData(dict);
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
            var res = CommonHelper.ValidateModel<Model.S01.UCProcessSubFuncAuthManagerInfo.Auth>(newData_dict);
            if (res.IsSuccess)
                res = new Sys_processcontrol_roleData().UpdateData(oldData_dict, newData_dict);
            return res;
        }
        #endregion
    }
}
