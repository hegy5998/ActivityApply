using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using DataAccess;
using Util;
using System.Web;
using System.Web.UI;

namespace BusinessLayer.MasterPages
{
    public class MainBL : BaseBL
    {
        Page _curPage = HttpContext.Current.Handler as Page;
        string qsSysIdKey = "sys_id";               // 選單點選時，QueryString中傳遞系統代碼的參數名稱
        string qsSysPidKey = "sys_pid";             // 選單點選時，QueryString中傳遞作業代碼的參數名稱

        #region 取得要顯示的作業相關資訊
        /// <summary>
        /// 取得要顯示的作業相關資訊
        /// </summary>
        /// <returns>process info</returns>
        public Sys_processInfo GetShowProcessInfo()
        {
            string sys_id = (HttpContext.Current.Handler as Page).Request[qsSysIdKey];
            string sys_pid = (HttpContext.Current.Handler as Page).Request[qsSysPidKey];
            Sys_processInfo showProcess_info = null;

            if (!String.IsNullOrEmpty(sys_id))
            {
                // 取得使用者在該系統具有使用權限的作業清單
                List<Sys_processInfo> rawProcess_lst = GetProcessList(sys_id);

                // 取得作業資訊
                if (((HttpContext.Current.Handler as Page).Request.FilePath.Contains("/Index.aspx")
                        || (HttpContext.Current.Handler as Page).Request.FilePath.Contains("/DefaultSystemIndex.aspx"))
                    && rawProcess_lst.Count() > 0)
                {
                    // 為某系統的首頁
                    showProcess_info = new Sys_processInfo();
                    showProcess_info.Sys_id = rawProcess_lst.First().Sys_id;
                    showProcess_info.Sys_name = rawProcess_lst.First().Sys_name;
                    showProcess_info.Sys_bannerimg = rawProcess_lst.First().Sys_bannerimg;
                }
                else if (!String.IsNullOrEmpty(sys_pid))
                {
                    var process_lnq = (
                        from s in rawProcess_lst
                        where s.Sys_pid == sys_pid
                        select s);
                    if (process_lnq.Count() > 0)
                    {
                        showProcess_info = process_lnq.First();

                        if (!(HttpContext.Current.Handler as Page).Request.FilePath.Contains("/Manual.aspx")
                            && !(HttpContext.Current.Handler as Page).Request.FilePath.Contains("/CommonPages/ManualImg.aspx"))
                        {
                            // 檢查QueryString中作業代碼及檔案網址是否與資料庫中資料相同
                            if ((HttpContext.Current.Handler as Page).Request.FilePath.Equals((HttpContext.Current.Handler as Page).Request.ApplicationPath.TrimEnd('/') + "/" + showProcess_info.Sys_purl.Replace("~/", "")) == false)
                                showProcess_info = null;
                        }
                    }
                }
            }

            return showProcess_info;
        }
        #endregion

        #region 取得模組及作業選單HTML
        /// <summary>
        /// 取得模組及作業選單HTML
        /// </summary>
        public string GetModuleAndProcessHTML()
        {
            string sys_id = (new SystemMenuBL().IsShowSystemMenu().Item1) ? (HttpContext.Current.Handler as Page).Request[qsSysIdKey] : "";
            string sys_pid = (HttpContext.Current.Handler as Page).Request[qsSysPidKey];
            StringBuilder html_sb = new StringBuilder();
            List<Sys_processInfo> rawProcess_lst = GetProcessList(sys_id).Where(x=>x.Sys_show=="Y").ToList();

            // 取得目前的作業代碼對應的模組代碼
            string sys_mid = "";
            if (sys_pid != null)
            {
                var tmp = rawProcess_lst.Where(x => x.Sys_pid == sys_pid).FirstOrDefault();
                if (tmp != null) sys_mid = tmp.Sys_mid;
            }

            #region 建立模組項目
            var module_lst = (
                from s in rawProcess_lst
                group s by new { Sys_mid = s.Sys_mid, Sys_mname = s.Sys_mname } into g
                select new Sys_processInfo
                {
                    Sys_mid = g.Key.Sys_mid,
                    Sys_mname = g.Key.Sys_mname
                }).ToList();
            html_sb.Append("<ul class=\"mainMenuModule\">");
            foreach (var module_info in module_lst)
            {
                html_sb.Append("<li>");
                html_sb.Append("<a href=\"javascript: void(0)\"><div><i class=\"fa fa-folder\" style=\"margin-right:5px;\"></i>" + module_info.Sys_mname + "</div></a>");

                #region 建立作業項目
                var process_lst = (
                    from s in rawProcess_lst
                    where s.Sys_mid == module_info.Sys_mid
                    select s).ToList();
                html_sb.Append("<ul class=\"mainMenuProcess\"");

                // 非執行的作業模組，則預設隱藏作業選單
                if (module_info.Sys_mid != sys_mid)
                {
                    if (!string.IsNullOrWhiteSpace(sys_mid) || module_info != module_lst[0])
                        html_sb.Append(" style=\"display:none\"");
                }

                html_sb.Append(">");
                foreach (var process_info in process_lst)
                {
                    html_sb.Append("<li>");

                    //檢查是否連結到外部網站
                    if (process_info.Sys_purl.ToLower().StartsWith("http://") || process_info.Sys_purl.ToLower().StartsWith("https://"))
                        html_sb.Append("<a href=\"" + (HttpContext.Current.Handler as Page).ResolveUrl(process_info.Sys_purl) + "\" target=\"_blank\"><div>" + process_info.Sys_pname + "</div></a>");
                    else
                    {
                        html_sb.Append("<a href=\"" + (HttpContext.Current.Handler as Page).ResolveUrl(process_info.Sys_purl));
                        if (process_info.Sys_purl.Contains("?"))
                            html_sb.Append("&");
                        else
                            html_sb.Append("?");

                        // 於連結中附上系統及作業代碼
                        html_sb.Append(qsSysIdKey + "=" + process_info.Sys_id
                            + "&" + qsSysPidKey + "=" + process_info.Sys_pid);
                        html_sb.Append("\" sys_pid=\"" + process_info.Sys_pid + "\"><div>" + process_info.Sys_pname + "</div></a>");
                    }

                    html_sb.Append("</li>");
                }
                html_sb.Append("</ul>");
                #endregion

                html_sb.Append("</li>");
            }
            html_sb.Append("</ul>");
            #endregion

            return html_sb.ToString();
        }
        #endregion

        #region 取得使用者對應的選單資料
        /// <summary>
        /// 取得使用者對應的選單資料
        /// </summary>
        /// <param name="sys_id">系統代碼</param>
        /// <returns></returns>
        private List<Sys_processInfo> GetProcessList(string sys_id)
        {
            Page curPage = HttpContext.Current.Handler as Page;

            var tmp_lst = from s in AuthBL.GetUserRoleUnitPositionAuth()
                          where sys_id == "" || s.Sys_id == sys_id
                          group s by new {
                              sys_id = s.Sys_id,
                              sys_name = s.Sys_name,
                              sys_bannerimg = s.Sys_bannerimg,
                              sys_url = s.Sys_url,
                              sys_mid = s.Sys_mid,
                              sys_mname = s.Sys_mname,
                              sys_pid = s.Sys_pid,
                              sys_pname = s.Sys_pname,
                              sys_purl = s.Sys_purl,
                              sys_show = s.Sys_show
                          } into g select g;

            List<Sys_processInfo> data_lst = new List<Sys_processInfo>();
            foreach (var t in tmp_lst)
            {
                var i = new Sys_processInfo();
                i.Sys_id = t.Key.sys_id;
                i.Sys_name = t.Key.sys_name;
                i.Sys_bannerimg = t.Key.sys_bannerimg;
                i.Sys_url = t.Key.sys_url;
                i.Sys_mid = t.Key.sys_mid;
                i.Sys_mname = t.Key.sys_mname;
                i.Sys_pid = t.Key.sys_pid;
                i.Sys_pname = t.Key.sys_pname;
                i.Sys_purl = t.Key.sys_purl;
                i.Sys_show = t.Key.sys_show;
                data_lst.Add(i);
            }

            return data_lst;
        }
        #endregion

        #region 取得系統首頁網址
        public string GetSystemIndexPageURL()
        {
            var info = GetProcessList((HttpContext.Current.Handler as Page).Request[qsSysIdKey])[0];
            return info.Sys_url + "?sys_id=" + info.Sys_id;
        }
        #endregion
    }
}
