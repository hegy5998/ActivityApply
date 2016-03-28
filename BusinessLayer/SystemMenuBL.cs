using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using DataAccess;
using Util;
using System.Web.UI;
using System.Web;

namespace BusinessLayer
{
    public class SystemMenuBL : BaseBL
    {
        #region 取得目前登入的使用者身份可操作的所有系統
        /// <summary>
        /// 取得目前登入的使用者身份可操作的所有系統的系統選單html
        /// </summary>
        /// <returns></returns>
        public string GetSystemListHtml()
        {
            Page curPage = HttpContext.Current.Handler as Page;

            #region 產生html
            StringBuilder html_sb = new StringBuilder();
            html_sb.Append("<ul>");

            foreach (var info in GetSystemList())
            {
                string href = curPage.ResolveUrl(info.Sys_url);
                string showName = string.IsNullOrWhiteSpace(info.Sys_menuimg) ? info.Sys_name : "";

                html_sb.Append("<li>");
                //檢查是否連結到外部網站
                if (href.ToLower().StartsWith("http://") || href.ToLower().StartsWith("https://"))
                    html_sb.Append("<a class=\"systemButton\" style=\"background-image:url(" + curPage.ResolveUrl("~/Images/SystemButton/" + info.Sys_menuimg) + ")\" href=\"" + curPage.ResolveUrl(info.Sys_url) + "\" target=\"_blank\">" + showName + "</a>");
                else if (href.Contains("?newPage=Y"))
                {
                    html_sb.Append("<a class=\"systemButton\" style=\"background-image:url(" + curPage.ResolveUrl("~/Images/SystemButton/" + info.Sys_menuimg) + ")\" href=\"" + curPage.ResolveUrl(info.Sys_url) + "\" target=\"_blank\">" + showName + "</a>");
                }
                else
                {
                    html_sb.Append("<a class=\"systemButton\" style=\"background-image:url(" + curPage.ResolveUrl("~/Images/SystemButton/" + info.Sys_menuimg) + ")\" href=\"" + curPage.ResolveUrl(info.Sys_url) + "\">" + showName + "</a>");
                }
                html_sb.Append("</li>");
            }
            html_sb.Append("</ul>");
            #endregion

            return html_sb.ToString();
        }
        /// <summary>
        /// 取得具有權限的系統
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Sys_systemInfo> GetSystemList()
        {
            Page curPage = HttpContext.Current.Handler as Page;

            #region 取得具有權限的系統
            var tmp_lst = from s in AuthBL.GetUserRoleUnitPositionAuth()
                          group s by new { s.Sys_id, s.Sys_name, s.Sys_url, s.Sys_menuimg, s.Sys_bannerimg } into g
                          select g;

            foreach (var t in tmp_lst)
            {
                var i = new Sys_systemInfo();
                i.Sys_id = t.Key.Sys_id;
                i.Sys_name = t.Key.Sys_name;

                var href = curPage.ResolveUrl(t.Key.Sys_url);
                if (href.ToLower().StartsWith("http://") || href.ToLower().StartsWith("https://"))
                    i.Sys_url = t.Key.Sys_url;
                else if (href.Contains("?"))
                    i.Sys_url = curPage.ResolveUrl(t.Key.Sys_url) + "&sys_id=" + t.Key.Sys_id;
                else
                    i.Sys_url = curPage.ResolveUrl(t.Key.Sys_url) + "?sys_id=" + t.Key.Sys_id;

                i.Sys_menuimg = t.Key.Sys_menuimg;
                i.Sys_bannerimg = t.Key.Sys_bannerimg;
                yield return i;
            }
            #endregion
        }

        #endregion

        #region 取得公告資料
        public IEnumerable<Sys_announceInfo> GetNewsList()
        {
            foreach (var item in new Sys_announceData().GetEnableList()) yield return item;
        }
        #endregion

        #region 是否須顯示顯示選單
        /// <summary>
        /// 是否須顯示顯示選單
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, string> IsShowSystemMenu()
        {
            var res = true;
            var url = "";
            var showSystemMenuConfig = CommonHelper.GetSysConfig().SHOW_SYSTEM_MENU;
            var curContext = HttpContext.Current;
            var sessionKey = "ShowSystemMenu";
            var sessionVal = curContext.Session[sessionKey] as Tuple<bool, string>;

            if (sessionVal != null)
            {
                res = sessionVal.Item1;
                url = sessionVal.Item2;
            }
            else 
            {
                if (showSystemMenuConfig.EqualsIgnoreCase("Y"))     // 強制顯示系統選單
                    res = true;
                else
                {
                    var systems = GetSystemList().ToList();
                    if (showSystemMenuConfig.EqualsIgnoreCase("N")) // 不顯示系統選單，將所有有權限的模組顯示在同一頁面中
                    {
                        res = false;
                        if (systems.Count() == 0)
                        {
                            // 若不顯示系統選單，但有無任何作業使用權限，則顯示錯誤畫面
                            var user = CommonHelper.GetLoginUser();
                            throw new Exception("系統設置為自動導入系統頁，但登入的身分無任何作業使用權限，無法自動導入。[角色:" + user.Login_sys_rid + ",單位:" + user.Login_sys_uid + "]");
                        }
                        else
                            url = systems[0].Sys_url;
                    }
                    else // 自動判斷
                    {
                        if (systems.Count() == 1)
                        {
                            // 只有一個系統的使用權限，直接導入該系統中
                            res = false;
                            url = systems[0].Sys_url;
                        }
                        else
                        {
                            // 若使用者可使用的系統不只一個，則顯示系統選單
                            res = true;
                        }
                    }
                }
            }

            curContext.Session[sessionKey] = Tuple.Create<bool, string>(res, url);

            return curContext.Session[sessionKey] as Tuple<bool, string>;
        }
        #endregion
    }
}
