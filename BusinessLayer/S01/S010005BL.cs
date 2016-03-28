using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using DataAccess;
using Util;
using System.ComponentModel;
using System.Data;

namespace BusinessLayer.S01
{
    public class S010005BL : BaseBL
    {
        #region 作業相關
        #region 查詢
        /// <summary>
        /// 取得擁有模組的系統資料
        /// </summary>
        /// <returns></returns>
        public List<Sys_systemInfo> GetSystemList()
        {
            return new Sys_systemData().GetList(true);
        }

        /// <summary>
        /// 取得模組資料
        /// </summary>
        /// <param name="sys_id">系統代碼</param>
        /// <returns>模組資料</returns>
        public List<Sys_moduleInfo> GetModuleList(string sys_id)
        {
            return new Sys_moduleData().GetListBySystem(sys_id);
        }

        /// <summary>
        /// 取得作業資料
        /// </summary>
        /// <param name="sys_id">指定系統代碼</param>
        /// <param name="sys_mid">指定模組代碼</param>
        /// <returns>作業資料</returns>
        public List<Sys_processInfo> GetProcessList(string sys_id = "", string sys_mid = "")
        {
            var lst = new Sys_processData().GetListBySystemModule(sys_id, sys_mid)
                .OrderBy(x=>x.Sys_mid).ThenBy(x=>x.Sys_seq).ToList();
            return lst;
        }

        /// <summary>
        /// 取得某作業資料
        /// </summary>
        /// <param name="sys_pid">作業代碼</param>
        /// <returns>作業資料物件</returns>
        public Sys_processInfo GetProcessData(string sys_pid)
        {
            return new Sys_processData().GetInfo(sys_pid);
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
            var res = CommonHelper.ValidateModel<Model.S01.S010005Info.Main>(dict);
            if (res.IsSuccess)
                res = new Sys_processData().InsertData(dict);
            return res;
        }
        #endregion

        #region 更新
        /// <summary>
        /// 更新資料
        /// </summary>
        /// <param name="oldData_dict">原資料PK</param>
        /// <param name="newData_dict">新資料</param>
        /// <returns></returns>
        public CommonResult UpdateData(Dictionary<string, object> oldData_dict, Dictionary<string, object> newData_dict)
        {
            var res = CommonHelper.ValidateModel<Model.S01.S010005Info.Main>(newData_dict);

            // 寫入資料
            if (res.IsSuccess)
            {
                try
                {
                    var trans = BeginTransaction();
                    res = new Sys_processData().UpdateData(trans, oldData_dict, newData_dict);

                    if (res.IsSuccess)
                    {
                        #region 若有變更作業代碼，則更新相關Table
                        if (newData_dict.ContainsKey("sys_pid"))
                        {
                            string old_sys_pid = oldData_dict["sys_pid"].ToString();
                            string new_sys_pid = newData_dict["sys_pid"].ToString();

                            if (old_sys_pid != new_sys_pid)
                            {
                                new Sys_process_roleData().UpdateSysPid(trans, old_sys_pid, new_sys_pid);
                                new Sys_processcontrolData().UpdateSysPid(trans, old_sys_pid, new_sys_pid);
                                new Sys_processcontrol_roleData().UpdateSysPid(trans, old_sys_pid, new_sys_pid);
                            }
                        }
                        #endregion
                    }

                    if (res.IsSuccess)
                        Commit();
                    else
                        Rollback();
                }
                catch
                {
                    res.IsSuccess = false;
                    Rollback();
                }
            }

            return res;
        }
        #endregion

        #region 刪除
        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="dict">資料PK</param>
        /// <returns></returns>
        public CommonResult DeleteData(Dictionary<string, object> dict)
        {
            var res = new CommonResult(true);

            try
            {
                var trans = BeginTransaction();
                res = new Sys_processData().DeleteData(trans, dict);

                if (res.IsSuccess)
                {
                    string sys_pid = dict["sys_pid"].ToString();
                    new Sys_processcontrol_roleData().DeleteBySysPid(trans, sys_pid);
                    new Sys_processcontrolData().DeleteBySysPid(trans, sys_pid);
                    new Sys_process_roleData().DeleteBySysPid(trans, sys_pid);
                }

                if (res.IsSuccess)
                    Commit();
                else
                    Rollback();
            }
            catch
            {
                res.IsSuccess = false;
                Rollback();
            }

            return res;
        }
        #endregion
        #endregion

        #region 作業子功能相關
        #region 查詢
        /// <summary>
        /// 取得某作業元件資料
        /// </summary>
        /// <param name="sys_pid">作業代碼</param>
        /// <returns>元件資料</returns>
        public List<Sys_processcontrolInfo> GetControlList(string sys_pid)
        {
            return new Sys_processcontrolData().GetList(sys_pid);
        }
        #endregion

        #region 更新
        /// <summary>
        /// 更新子功能資料
        /// </summary>
        /// <param name="oldData_dict">原資料PK</param>
        /// <param name="newData_dict">新資料</param>
        /// <returns></returns>
        public CommonResult UpdateControlData(Dictionary<string, object> oldData_dict, Dictionary<string, object> newData_dict)
        {
            var res = CommonHelper.ValidateModel<Model.S01.S010005Info.SubControl>(newData_dict);

            if (res.IsSuccess)
            {
                try
                {
                    // 寫入資料
                    var trans = BeginTransaction();
                    res = new Sys_processcontrolData().UpdateData(trans, oldData_dict, newData_dict);
                    if (res.IsSuccess)
                    {
                        #region 若有變更作業子功能代碼，則更新相關Table
                        if (oldData_dict["sys_cid"].ToString() != newData_dict["sys_cid"].ToString())
                        {
                            // 變更sys_processcontrol_role
                            new Sys_processcontrol_roleData().UpdateSysCid(
                                trans, 
                                oldData_dict["sys_pid"].ToString(), 
                                oldData_dict["sys_cid"].ToString(), 
                                newData_dict["sys_cid"].ToString()
                            );
                        }
                        #endregion
                    }

                    if (res.IsSuccess)
                        Commit();
                    else
                        Rollback();
                }
                catch
                {
                    res.IsSuccess = false;
                    Rollback();
                }
            }

            return res;
        }
        #endregion

        #region 新增
        /// <summary>
        /// 新增子功能資料
        /// </summary>
        /// <param name="dict">資料</param>
        /// <returns></returns>
        public CommonResult InsertControlData(Dictionary<string, object> dict)
        {
            var res = CommonHelper.ValidateModel<Model.S01.S010005Info.SubControl>(dict);
            if (res.IsSuccess)
                res = new Sys_processcontrolData().InsertData(dict);
            return res;
        }
        #endregion

        #region 刪除
        /// <summary>
        /// 刪除子功能
        /// </summary>
        /// <param name="dict">原資料PK</param>
        public CommonResult DeleteControlData(Dictionary<string, object> dict)
        {
            var res = new CommonResult(true);

            try
            {
                var trans = BeginTransaction();
                res = new Sys_processcontrolData().DeleteData(trans, dict);
                if (res.IsSuccess)
                {
                    // 刪除sys_processControl_role
                    new Sys_processcontrol_roleData().DeleteDataBySysCid(
                        trans,
                        dict["sys_pid"].ToString(), 
                        dict["sys_cid"].ToString()
                    );
                }

                if (res.IsSuccess)
                    Commit();
                else
                    Rollback();
            }
            catch
            {
                res.IsSuccess = false;
                Rollback();
            }

            return res;
        }
        #endregion
        #endregion


        #region 設定順序
        public void SetOrder(List<string> items)
        {
            var da = new Sys_processData();
            for (int i = 0; i < items.Count; i++)
            {
                var pk_dict = new Dictionary<string, object>();
                pk_dict["sys_pid"] = items[i];

                var new_dict = new Dictionary<string, object>();
                new_dict["sys_seq"] = i + 1;

                da.UpdateData(pk_dict, new_dict);
            }
        }
        #endregion
    }
}
