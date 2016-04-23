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

namespace BusinessLayer.S01
{
    public class S020103BL
    {
        S010009Data _da = new S010009Data();
        S020103Data _classdata = new S020103Data();
        

        #region 取得作業列表
        /// <summary>
        /// 取得作業資料
        /// </summary>
        /// <param name="sys_id">指定系統代碼</param>
        /// <param name="sys_mid">指定模組代碼</param>
        /// <returns>作業資料</returns>
        public List<Model.S01.S010009Info.Main> GetProcessList(string sys_id = "", string sys_mid = "")
        {
            var lst = _da.GetListBySystemModule(sys_id, sys_mid);
            return lst;
        }
        #endregion

        #region 取得特定作業的子功能列表
        /// <summary>
        /// 取得特定作業的所有子功能資料
        /// </summary>
        /// <param name="sys_pid">作業代碼</param>
        /// <returns></returns>
        public List<Sys_processcontrolInfo> GetSubFunc(string sys_pid)
        {
            return new Sys_processcontrolData().GetList(sys_pid);
        }
        #endregion

        #region 取得分類資料
        public List<Activity_classInfo> GetClassList()
        {
            return _classdata.GetClassList();
        }
        #endregion

        #region 新增分類資料
        public CommonResult InsertData(Dictionary<string, object> dict)
        {
            var res = CommonHelper.ValidateModel<Model.Activity_classInfo>(dict);

            if (res.IsSuccess)
            {
                res = _classdata.InsertData(dict);
            }
            return res;
        }
        #endregion

        #region 刪除分類資料
        public CommonResult DeleteData(Dictionary<string, object> dict)
        {
            var res = CommonHelper.ValidateModel<Model.Activity_classInfo>(dict);

            if (res.IsSuccess)
            {
                res = _classdata.DeleteData(dict);
            }
            return res;
        }
        #endregion

        #region 更新分類資料
        public CommonResult UpdateData(Dictionary<string, object> old_dict, Dictionary<string, object> new_dict)
        {
            var res = CommonHelper.ValidateModel<Model.Activity_classInfo>(new_dict);

            if (res.IsSuccess)
            {
                res = _classdata.UpdateData(old_dict, new_dict);
            }
            return res;
        }
        #endregion

        
    }
}
