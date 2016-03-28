using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Util;

namespace Web.UserControls
{
    public partial class UCGridViewPager : System.Web.UI.UserControl
    {
        // PageIndexChanged Event
        public delegate void BindDataActionHandler();
        public event BindDataActionHandler BindDataHandler;

        public GridView GridView { get; set; }
        
        public int PageOffset = 10;

        #region 初始化
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="gv">對應的GridView</param>
        /// <param name="bindDataFunc">資料綁定的Function</param>
        public void InitGridView(GridView gv, BindDataActionHandler bindDataFunc)
        {
            this.GridView = gv;
            this.BindDataHandler = bindDataFunc;
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="gv">對應的GridView</param>
        /// <param name="pageOffset">每頁筆數</param>
        /// <param name="bindDataFunc">資料綁定的Function</param>
        public void InitGridView(GridView gv, int pageOffset, BindDataActionHandler bindDataFunc)
        {
            this.PageOffset = pageOffset;
            InitGridView(gv, bindDataFunc);
        }
        #endregion

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (GridView != null)
            {
                if (GridView.AllowPaging)
                {

                    currentIndex_hf.Value = GridView.PageIndex.ToString();
                    pager_pl.Visible = true;
                    if (GridView.PageCount == 0 || GridView.PageIndex == 0)
                    {
                        prev_lbtn.Enabled = false;
                        prev_lbtn.CssClass = prev_lbtn.CssClass + " disable";
                    }
                    if (GridView.PageCount == 0 || GridView.PageIndex == GridView.PageCount - 1)
                    {
                        next_lbtn.Enabled = false;
                        next_lbtn.CssClass = next_lbtn.CssClass + " disable";
                    }
                }
                else
                    pager_pl.Visible = false;
            }
        }

        protected void pager_Command(object sender, CommandEventArgs e)
        {
            string arg = e.CommandArgument.ToString();
            switch (arg)
            {
                case "Prev":    // 上一頁
                    if (GridView.PageIndex > 0)
                        GridView.PageIndex = GridView.PageIndex - 1;
                    break;
                case "Next":    // 下一頁
                    if (GridView.PageIndex < GridView.PageCount - 1)
                        GridView.PageIndex = GridView.PageIndex + 1;
                    break;
                default:        // 指定頁碼
                    int? targetPageIndex = CommonConvert.GetIntOrNull(Request.Form["targetIndex"].Trim(",".ToArray())) - 1;
                    if (targetPageIndex != null
                        && targetPageIndex >= 0 && targetPageIndex < GridView.PageCount)
                        GridView.PageIndex = (int)targetPageIndex;
                        break;
            }

            GridViewHelper.ChgGridViewMode(GridViewHelper.GVMode.Normal, GridView);

            BindDataHandler();
        }
    }
}