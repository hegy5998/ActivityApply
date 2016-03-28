using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;
using Model;
using DataAccess;

namespace BusinessLayer.S01
{
    public class UCAccountManagerBL : BaseBL
    {
        DataAccess.S01.UCAccountManagerData _da = new DataAccess.S01.UCAccountManagerData();

        #region 新增
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="dict">資料</param>
        /// <returns></returns>
        public CommonResult InsertData(Dictionary<string, object> dict, List<Model.S01.UCAccountRoleManagerInfo.Main> rpid_lst)
        {
            var res = CommonHelper.ValidateModel<Model.S01.UCAccountManagerInfo.Main>(dict);

            if (res.IsSuccess)
            {
                // 檢查身分資料
                res = CheckRole(rpid_lst);
            }

            if (res.IsSuccess)
            {
                dict["act_status"] = "Y";
                dict["act_pwd_expire_login_times"] = 0;
                try
                {
                    var t = BeginTransaction();
                    res = new Sys_accountData().InsertData(t, dict);
                    if (!res.IsSuccess)
                        res.Message = "帳號已存在!";
                    var acountRole_data = new Sys_account_roleData();
                    if (res.IsSuccess)
                    {
                        foreach (var item in rpid_lst)
                        {
                            foreach (var rp in item.RolePosition_lst)
                            {
                                var rp_dict = new Dictionary<string, object>();
                                rp_dict["act_id"] = dict["act_id"];
                                rp_dict["sys_rid"] = item.Sys_rid;
                                rp_dict["sys_uid"] = item.Sys_uid;
                                rp_dict["sys_rpid"] = rp;
                                res = acountRole_data.InsertData(t, rp_dict);
                                if (!res.IsSuccess)
                                    break;
                            }
                            if (!res.IsSuccess)
                                break;
                        }
                    }

                    if (res.IsSuccess)
                        Commit();
                    else
                        Rollback();
                }
                catch (Exception ex)
                {
                    if (!CustomHelper.IsDebugMode)
                        ErrorHelper.ErrorProcess(ex);
                    Rollback();
                    res.IsSuccess = false;
                }
            }
            return res;
        }
        #endregion

        #region 取得帳號資料
        /// <summary>
        /// 取得帳號資料
        /// </summary>
        /// <param name="act_id">帳號</param>
        /// <returns></returns>
        public Model.S01.UCAccountManagerInfo.Main GetData(string act_id)
        {
            var rawInfo = new Sys_accountData().GetInfo(act_id);
            var info = new Model.S01.UCAccountManagerInfo.Main() { 
                Act_id = rawInfo.Act_id,
                Act_name = rawInfo.Act_name,
                Act_mail = rawInfo.Act_mail,
                Act_status = rawInfo.Act_status,
                Act_pwd_date = rawInfo.Act_pwd_date,
                Act_pwd_expire_login_times = rawInfo.Act_pwd_expire_login_times
            };

            var roleGrouped = rawInfo.Role_lst.GroupBy(x => new { x.Sys_rid, x.Sys_uid }).Select(x => x);

            #region 載入對應的身分資料
            foreach (var item in roleGrouped)
            {
                var n = new Model.S01.UCAccountRoleManagerInfo.Main()
                {
                    Sys_rid = item.Key.Sys_rid,
                    Sys_uid = item.Key.Sys_uid,
                    RolePosition_lst = item.Select(x => x.Sys_rpid).ToList()
                };
                info.Role_lst.Add(n);
            }
            #endregion

            return info;
        }
        #endregion

        #region 更新
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="old_dict">原資料</param>
        /// <param name="new_dict">新資料</param>
        /// <param name="rpid_lst">身分資料</param>
        /// <returns></returns>
        public CommonResult UpdateData(Dictionary<string, object> old_dict, Dictionary<string, object> new_dict, List<Model.S01.UCAccountRoleManagerInfo.Main> rpid_lst)
        {
            var res = CommonHelper.ValidateModel<Model.S01.UCAccountManagerInfo.Main>(new_dict);

            if (res.IsSuccess)
            {
                // 檢查身分資料
                res = CheckRole(rpid_lst);
            }

            if (res.IsSuccess)
            {
                try
                {
                    string act_id = old_dict["act_id"].ToString();
                    var t = BeginTransaction();
                    res = new Sys_accountData().UpdateData(t, old_dict, new_dict, false);
                    var acountRole_data = new Sys_account_roleData();
                    if (res.IsSuccess)
                    {
                        // 刪除帳號所具有的身分
                        acountRole_data.DeleteDataByAct(t, act_id);

                        // 建立此次的身分資料
                        foreach (var item in rpid_lst)
                        {
                            foreach (var rp in item.RolePosition_lst)
                            {
                                var rp_dict = new Dictionary<string, object>();
                                rp_dict["act_id"] = act_id;
                                rp_dict["sys_rid"] = item.Sys_rid;
                                rp_dict["sys_uid"] = item.Sys_uid;
                                rp_dict["sys_rpid"] = rp;
                                res = acountRole_data.InsertData(t, rp_dict);
                                if (!res.IsSuccess)
                                    break;
                            }
                            if (!res.IsSuccess)
                                break;
                        }
                    }

                    if (res.IsSuccess)
                        Commit();
                    else
                        Rollback();
                }
                catch (Exception ex)
                {
                    if (!CustomHelper.IsDebugMode)
                        ErrorHelper.ErrorProcess(ex);
                    Rollback();
                    res.IsSuccess = false;
                }
            }
            return res;
        }

        #endregion

        #region 檢查身分資料
        private CommonResult CheckRole(List<Model.S01.UCAccountRoleManagerInfo.Main> rpid_lst)
        {
            var res = new CommonResult(true);

            #region 檢查身分資料
            #region 檢查角色單位是否重複
            var dupRoleUnit = rpid_lst.GroupBy(x => new { x.Sys_rid, x.Sys_uid })
                .Select(x => new
                {
                    x.Key.Sys_rid,
                    x.Key.Sys_uid,
                    Count = x.Count()
                })
                .Where(x => x.Count > 1).ToList();

            if (dupRoleUnit.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < dupRoleUnit.Count; i++)
                {
                    if (i > 0)
                        sb.Append("<br/>");

                    var item = dupRoleUnit[i];
                    var info = rpid_lst.Last(x => x.Sys_rid == item.Sys_rid && x.Sys_uid == item.Sys_uid);
                    sb.Append((rpid_lst.Count > 1) ? "第" + (rpid_lst.IndexOf(info) + 1) + "筆身分資料，" : "");
                    sb.Append("[角色]、[單位]資料重複!");
                }
                res.IsSuccess = false;
                res.Message = sb.ToString();
            }
            #endregion

            if (res.IsSuccess)
            {
                #region 檢查角色、單位、職位是否都有選取
                List<string> msg_lst = new List<string>();
                foreach (var item in rpid_lst)
                {
                    string msg = "";
                    if (item.Sys_rid.IsNullOrWhiteSpace())
                        msg = "請選擇[角色]!";
                    else if (item.Sys_uid.IsNullOrWhiteSpace())
                        msg = "請選擇[單位]!";
                    else if (item.RolePosition_lst.Count == 0)
                        msg = "請選擇[職位]!";

                    if (msg.IsNullOrWhiteSpace() == false)
                    {
                        string prefix = (rpid_lst.Count > 1) ? "第" + (rpid_lst.IndexOf(item) + 1) + "筆身分資料，" : "";
                        msg_lst.Add(prefix + msg);
                    }
                }
                if (msg_lst.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < msg_lst.Count; i++)
                    {
                        if (i > 0)
                            sb.Append("<br/>");
                        sb.Append(msg_lst[i]);
                    }
                    res.IsSuccess = false;
                    res.Message = sb.ToString();
                }
                #endregion
            }

            #endregion

            return res;
        }
        #endregion
    }
}
