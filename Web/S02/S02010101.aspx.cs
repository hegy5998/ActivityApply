using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using Model;
using Util;
using BusinessLayer.S02;
using System.Data;
using System.IO;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

namespace Web.S02
{
    public partial class S02010101 : CommonPages.BasePage
    {
        S020101BL _bl = new S020101BL();

        protected void Page_Load(object sender, EventArgs e)
        {
            
            //已發佈活動
            ucGridViewPager.GridView = main_gv;
            ucGridViewPager.BindDataHandler += () => { BindGridView(GetAlreadyData(), GetReadyData(), GetEndData()); };

            //未發佈活動
            ucGridViewPagerReady.GridView = ready_gv;
            ucGridViewPagerReady.BindDataHandler += () => { BindGridView(GetAlreadyData(), GetReadyData(), GetEndData()); };

            //已結束活動
            ucGridViewPagerEnd.GridView = end_gv;
            ucGridViewPagerEnd.BindDataHandler += () => { BindGridView(GetAlreadyData(), GetReadyData(), GetEndData()); };

            //取消GridView分頁
            controlSet_gv.AllowPaging = false;
            ready_copperate.AllowPaging = false;

            //加入搜尋的分類選項
            DataTable data = GetClassData();
            q_keyword_ddl.Items.Insert(0, "全部");
            for (int i = 1; i <= data.Rows.Count; i++)
            {
                q_keyword_ddl.Items.Insert(i, data.Rows[i - 1][0].ToString());
            }

            //main_gv.Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
            //ready_gv.Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
            //end_gv.Attributes.Add("style", "word-break:break-all;word-wrap:break-word");

            if (!IsPostBack)
            {
                //設定tabs的Cookie預設值
                HttpCookie myCookie = new HttpCookie("tabs");
                myCookie.Value = "1";
                Response.Cookies.Add(myCookie);

                BindGridView(GetAlreadyData(), GetReadyData(), GetEndData());
            }
        }

        #region Control權限管理
        protected void ManageControlAuth(object sender, EventArgs e)
        {            
            ManageControlCopperate(sender);
        }
        #endregion

        #region 取得資料
        /// <summary>
        /// 取得資料
        /// </summary>
        /// <returns>資料</returns>
        /// 取得已發佈活動資料
        private DataTable GetAlreadyData()
        {
            //顯示分頁
            ucGridViewPager.Visible = true;
            return _bl.GetAlreadyData();
        }

        //取得未發佈活動資料
        private DataTable GetReadyData()
        {
            ucGridViewPagerReady.Visible = true;
            return _bl.GetReadyData();
        }

        //取得已結束活動資料
        private DataTable GetEndData()
        {
            ucGridViewPagerEnd.Visible = true;
            return _bl.GetEndData();
        }

        //取得分類資料
        private DataTable GetClassData()
        {
            return _bl.GetClassData();
        }
        #endregion

        #region Gridview(main_gv)事件
        /// <summary>
        /// Bind資料
        /// </summary>
        /// <param name="alreadylst">資料</param>
        private void BindGridView(DataTable alreadylst, DataTable readylst, DataTable endlst)
        {
            //已發佈活動
            main_gv.DataSource = alreadylst;
            main_gv.DataBind();

            //未發佈活動
            ready_gv.DataSource = readylst;
            ready_gv.DataBind();

            //已結束活動
            end_gv.DataSource = endlst;
            end_gv.DataBind();
        }

        //GridView排序
        protected void main_gv_Sorting(object sender, GridViewSortEventArgs e)
        {
            //已發佈活動
            if (Request.Cookies["tabs"].Value == "0")
            {
                var lst = GridViewHelper.SortGridView(sender as GridView, e, GetAlreadyData());
                BindGridView(lst, GetReadyData(), GetEndData());
            }
            //未發佈活動
            else if (Request.Cookies["tabs"].Value == "1")
            {
                var lst = GridViewHelper.SortGridView(sender as GridView, e, GetReadyData());
                BindGridView(GetAlreadyData(), lst, GetReadyData());
            }
            //已結束活動
            else if (Request.Cookies["tabs"].Value == "2")
            {
                var lst = GridViewHelper.SortGridView(sender as GridView, e, GetEndData());
                BindGridView(GetAlreadyData(), GetReadyData(), lst);
            }
        }

        //關閉or發佈活動
        protected void main_gv_RowClose(object sender, GridViewEditEventArgs e)
        {
            GridViewHelper.ChgGridViewMode(GridViewHelper.GVMode.Edit, sender as GridView, e.NewEditIndex);

            GridViewRow gvr = (sender as GridView).Rows[e.NewEditIndex];
            var data_dict = new Dictionary<string, object>();
            data_dict["as_idn"] = CommonConvert.GetStringOrEmptyString((gvr.FindControl("as_idn_hf") as HiddenField).Value);

            var data = new Dictionary<string, object>();
            data["as_isopen"] = CommonConvert.GetStringOrEmptyString((gvr.FindControl("as_isopen_hf") as HiddenField).Value);

            //判斷是已發佈or未發佈活動
            if (data["as_isopen"].ToString() == "0")
            {
                data["as_isopen"] = 1;
            }
            else if (data["as_isopen"].ToString() == "1")
            {
                data["as_isopen"] = 0;
            }

            var res = _bl.UpdateSessionData(data_dict, data);

            //檢查活動的場次發佈情形
            int check = CheckActivityClose(Convert.ToInt32((gvr.FindControl("act_idn_hf") as HiddenField).Value));

            //取得活動的發佈值
            var data_dictActivity = new Dictionary<string, object>();
            data_dictActivity["act_idn"] = CommonConvert.GetStringOrEmptyString((gvr.FindControl("act_idn_hf") as HiddenField).Value);
            var dataActivity = new Dictionary<string, object>();
            dataActivity["act_isopen"] = CommonConvert.GetStringOrEmptyString((gvr.FindControl("act_isopen_hf") as HiddenField).Value);

            //活動關閉
            if (check == 0)
            {
                dataActivity["act_isopen"] = 0;
            }
            //活動發佈
            else
            {
                dataActivity["act_isopen"] = 1;
            }

            //更改活動資料
            var res_act = _bl.UpdateData(data_dictActivity, dataActivity);

            if (res.IsSuccess)
            {
                BindGridView(GetAlreadyData(), GetReadyData(), GetEndData());
                ShowPopupMessage(ITCEnum.PopupMessageType.Success, ITCEnum.DataActionType.Update);
            }
            else
            {
                ShowPopupMessage(ITCEnum.PopupMessageType.Error, ITCEnum.DataActionType.Delete, res.Message);
            }
        }

        //檢查活動裡的場次的發佈狀況(若是有一場次發佈則活動發佈，若是都沒有場次發佈則活動關閉)
        protected int CheckActivityClose(int i)
        {
            DataTable data = _bl.CheckCloseData(i);
            int check = 0;

            foreach (DataRow c in data.Rows)
            {
                if (c.ItemArray.GetValue(3).ToString() == "1")
                {
                    check = 1;
                }
            }

            //都沒有場次發佈，活動關閉
            if (check == 0)
            {
                return 0;
            }
            //至少有一場次發佈，活動發佈
            else
            {
                return 1;
            }
        }

        //命令處理
        protected void main_gv_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int i;

            switch (e.CommandName)
            {
                // 修改活動
                case "EditActivity":
                    i = Convert.ToInt32(e.CommandArgument);

                    Response.Redirect("S02010103.aspx?sys_id=S01&sys_pid=S02010103&act_idn=" + i);
                    Response.End();
                    break;

                // 編輯協作者
                case "Set":                    
                    // 設定彈出視窗
                    InitControlSetPopupWindow(sender, e);
                    break;

                // 編輯協作者(Ready)
                case "Set_ready":
                    // 設定彈出視窗
                    InitControlSetReadyPopupWindow(sender, e);
                    break;

                //下載報名資料
                case "download":
                    downloadApply(CommonConvert.GetIntOrZero(e.CommandArgument));
                    break;
            }
        }

        //下載報名資料
        protected void downloadApply(int i)
        {
            DataTable data = _bl.GetApplyData(i);
            DataTable title = _bl.Getactas(i);

            //新增Excel檔案
            XSSFWorkbook workbook = new XSSFWorkbook();
            ISheet u_sheet = workbook.CreateSheet(title.Rows[0][0].ToString() + "_" + title.Rows[0][1].ToString());

            //增加標頭列
            IRow u_row1 = u_sheet.CreateRow(0);
            for (int j = 1; j < data.Columns.Count; j++)
            {
                u_row1.CreateCell(j - 1).SetCellValue(data.Columns[j].ColumnName);
            }

            //產生內容資料列
            int x = 1;
            foreach (DataRow r in data.Rows)
            {
                IRow u_row = u_sheet.CreateRow(x);    // 在工作表裡面，產生一列。
                x++;
                for (int j = 1; j < data.Columns.Count; j++)
                {
                    u_row.CreateCell(j - 1).SetCellValue(r.ItemArray.GetValue(j).ToString());     // 在這一列裡面，產生格子（儲存格）並寫入資料。
                }
            }

            MemoryStream MS = new MemoryStream();   //==需要 System.IO命名空間
            workbook.Write(MS);

            Response.AddHeader("Content-Disposition", "attachment; filename=" + title.Rows[0][0].ToString() + ".xlsx");
            Response.BinaryWrite(MS.ToArray());

            workbook = null;   //== VB為 Nothing
            MS.Close();
            MS.Dispose();

            Response.Flush();
            Response.End();
        }

        //刪除活動(場次)
        protected void main_gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (ProcessModifyAuth)
            {
                GridViewRow gvr = (sender as GridView).Rows[e.RowIndex];
                //欲刪除場次序號
                var data_dict = new Dictionary<string, object>();
                data_dict["as_idn"] = CommonConvert.GetStringOrEmptyString((gvr.FindControl("as_idn_hf") as HiddenField).Value);
                //欲刪除場次的活動序號
                var data_dict_act = new Dictionary<string, object>();
                data_dict_act["act_idn"] = CommonConvert.GetStringOrEmptyString((gvr.FindControl("act_idn_hf") as HiddenField).Value);

                var res = _bl.DeleteData(data_dict, data_dict_act);
                if (res.IsSuccess)
                {
                    // 刪除成功，切換回一般模式
                    BindGridView(GetAlreadyData(), GetReadyData(), GetEndData());
                    ShowPopupMessage(ITCEnum.PopupMessageType.Success, ITCEnum.DataActionType.Delete);
                }
                else
                {
                    // 刪除失敗，顯示錯誤訊息
                    ShowPopupMessage(ITCEnum.PopupMessageType.Error, ITCEnum.DataActionType.Delete, res.Message);
                }
            }
        }

        // 設定編輯協作者彈出視窗
        private void InitControlSetPopupWindow(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow gvr = (e.CommandSource as Button).NamingContainer as GridViewRow;

            string cop_act = (gvr.FindControl("act_idn_hf") as HiddenField).Value;
            Account_copperateInfo process_info = _bl.GetCopperateData(cop_act);
            copperate_cop_act_hf.Value = cop_act;

            GridViewHelper.ChgGridViewMode(GridViewHelper.GVMode.Normal, controlSet_gv);
            BindControlSetGridView(GetControlSetData());
            controlSet_mpe.Show();  // Popup Window
            controlSet_pl.Visible = true;
            main_pl.Visible = true;
            mv.SetActiveView(main_view);
        }

        // 設定編輯協作者彈出視窗(Ready)
        private void InitControlSetReadyPopupWindow(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow gvr = (e.CommandSource as Button).NamingContainer as GridViewRow;

            string cop_act = (gvr.FindControl("act_idn_hf") as HiddenField).Value;
            Account_copperateInfo process_info = _bl.GetCopperateData(cop_act);
            cop_act_ready_hf.Value = cop_act;

            GridViewHelper.ChgGridViewMode(GridViewHelper.GVMode.Normal, ready_copperate);
            BindControlSetReadyGridView(GetControlSetReadyData());
            ready_copperate_pop.Show();  // Popup Window
            readyCopperate_pl.Visible = true;
            ready_pl.Visible = true;
            ready_mv.SetActiveView(ready_view);
        }
        #endregion

        #region 協作者GridView相關
        //協作者視窗關閉
        protected void controlSet_cancel_btn_Click(object sender, EventArgs e)
        {
            BindGridView(GetAlreadyData(), GetReadyData(), GetEndData());
        }

        //取得協作者List資料
        private List<Account_copperateInfo> GetControlSetData()
        {
            return _bl.GetControlList(copperate_cop_act_hf.Value);
        }

        //取得協作者List資料(Ready)
        private List<Account_copperateInfo> GetControlSetReadyData()
        {
            return _bl.GetControlList(cop_act_ready_hf.Value);
        }

        //設定協作者GridView資料
        private void BindControlSetGridView(List<Account_copperateInfo> lst)
        {
            controlSet_gv.DataSource = lst;
            controlSet_gv.DataBind();

            if (controlSet_gv.Rows.Count == 0)
            {
                // 避免無資料列時，無法顯示footer新增資料
                var new_lst = new List<Account_copperateInfo>();
                new_lst.Add(new Account_copperateInfo());
                controlSet_gv.DataSource = new_lst;
                controlSet_gv.DataBind();
                controlSet_gv.Rows[0].Attributes.Add("style", "display: none");
            }
        }

        //設定協作者GridView資料(Ready)
        private void BindControlSetReadyGridView(List<Account_copperateInfo> lst)
        {
            ready_copperate.DataSource = lst;
            ready_copperate.DataBind();

            if (ready_copperate.Rows.Count == 0)
            {
                // 避免無資料列時，無法顯示footer新增資料
                var new_lst = new List<Account_copperateInfo>();
                new_lst.Add(new Account_copperateInfo());
                ready_copperate.DataSource = new_lst;
                ready_copperate.DataBind();
                ready_copperate.Rows[0].Attributes.Add("style", "display: none");
            }
        }

        //協作者排序
        protected void controlSet_gv_Sorting(object sender, GridViewSortEventArgs e)
        {
            GridView gv = (GridView)sender;
            var lst = GridViewHelper.SortGridView<Account_copperateInfo>(gv, e, _bl.GetControlList(copperate_cop_act_hf.Value));
            
            //已發佈活動
            if (Request.Cookies["tabs"].Value == "0")
            {
                BindControlSetGridView(lst);
            }
            //未發佈活動
            else if (Request.Cookies["tabs"].Value == "1")
            {
                BindControlSetReadyGridView(lst);
            }
        }

        //協作者排序(Ready)
        protected void controlSetReady_gv_Sorting(object sender, GridViewSortEventArgs e)
        {
            GridView gv = (GridView)sender;
            var lst = GridViewHelper.SortGridView<Account_copperateInfo>(gv, e, _bl.GetControlList(cop_act_ready_hf.Value));
            BindControlSetReadyGridView(lst);
        }

        //編輯前
        protected void controlSet_gv_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewHelper.ChgGridViewMode(GridViewHelper.GVMode.Edit, controlSet_gv, e.NewEditIndex);
            BindControlSetGridView(GetControlSetData());
        }

        //編輯前(Ready)
        protected void controlSetReady_gv_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewHelper.ChgGridViewMode(GridViewHelper.GVMode.Edit, ready_copperate, e.NewEditIndex);
            BindControlSetReadyGridView(GetControlSetReadyData());
        }

        //取消編輯
        protected void controlSet_gv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewHelper.ChgGridViewMode(GridViewHelper.GVMode.Normal, controlSet_gv);
            BindControlSetGridView(GetControlSetData());
        }

        //取消編輯(Ready)
        protected void controlSetReady_gv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewHelper.ChgGridViewMode(GridViewHelper.GVMode.Normal, ready_copperate);
            BindControlSetReadyGridView(GetControlSetReadyData());
        }

        //RowDataBound事件
        protected void controlSet_gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            switch (e.Row.RowType)
            {
                //一般資料列
                case DataControlRowType.DataRow:
                    if (e.Row.RowState.ToString().Contains("Edit") == false)
                    {
                        // 一般資料列
                        Label label = e.Row.FindControl("cop_id_lbl") as Label;
                        string cop_id = (e.Row.FindControl("cop_id_lbl") as Label).Text;
                        string cop_authority = (e.Row.FindControl("cop_authority_lbl") as Label).Text;
                        (e.Row.FindControl("delete_btn") as Button).OnClientClick = "if (!confirm(\"帳號：" + cop_id + "\\n權限：" + cop_authority + "\\n\\n確定要刪除嗎?\")) return false";
                    }
                    else
                    {
                        new_hf.Value = "Edit";

                        //抓取帳號的值
                        HiddenField h = e.Row.FindControl("old_cop_id_hf") as HiddenField;
                        string s = h.Value.ToString();
                        Label label = e.Row.FindControl("accountEdit_lbl") as Label;
                        label.Text = s;

                        account_radiobuttonlist.SelectedValue = s;

                        //抓取權限的值
                        DropDownList dropdownlistedit = e.Row.FindControl("copEdit_authority_dll") as DropDownList;

                        Account_copperateInfo rowView = (Account_copperateInfo)e.Row.DataItem;
                        String state = rowView.Cop_authority.ToString();
                        dropdownlistedit.SelectedValue = state;
                    }
                    break;
                //頁尾列
                case DataControlRowType.Footer :
                    DropDownList dropdownlist = e.Row.FindControl("cop_authority_dll") as DropDownList;
                    dropdownlist.Items.Insert(0, new ListItem("請選擇", ""));
                    dropdownlist.Items.Insert(1, new ListItem("編輯", "編輯"));
                    dropdownlist.Items.Insert(2, new ListItem("閱讀", "閱讀"));
                    break;
            }
        }

        //RowDataBound事件(Ready)
        protected void ready_gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            switch (e.Row.RowType)
            {
                //一般資料列
                case DataControlRowType.DataRow:
                    if (e.Row.RowState.ToString().Contains("Edit") == false)
                    {
                        // 一般資料列
                        string readycop_id = (e.Row.FindControl("readycop_id_lbl") as Label).Text;
                        string cop_authority = (e.Row.FindControl("readycop_authority_lbl") as Label).Text;
                        (e.Row.FindControl("readydelete_btn") as Button).OnClientClick = "if (!confirm(\"帳號：" + readycop_id + "\\n權限：" + cop_authority + "\\n\\n確定要刪除嗎?\")) return false";
                    }
                    else
                    {
                        readynew_hf.Value = "Edit";

                        //抓取帳號的值
                        HiddenField h = e.Row.FindControl("readyold_cop_id_hf") as HiddenField;
                        string r = (e.Row.FindControl("readyold_cop_id_hf") as HiddenField).Value.ToString();
                        Label label = e.Row.FindControl("readyaccountEdit_lbl") as Label;
                        label.Text = r;

                        readyaccount_radiobuttonlist.SelectedValue = r;

                        //抓取權限的值
                        DropDownList dropdownlistedit = e.Row.FindControl("readycopEdit_authority_dll") as DropDownList;

                        Account_copperateInfo rowView = (Account_copperateInfo)e.Row.DataItem;
                        String state = rowView.Cop_authority.ToString();
                        dropdownlistedit.SelectedValue = state;
                    }
                    break;
                //頁尾列
                case DataControlRowType.Footer:
                    DropDownList ready = e.Row.FindControl("readycop_authority_dll") as DropDownList;
                    ready.Items.Insert(0, new ListItem("請選擇", ""));
                    ready.Items.Insert(1, new ListItem("編輯", "編輯"));
                    ready.Items.Insert(2, new ListItem("閱讀", "閱讀"));
                    break;
            }
        }

        //RowCommand事件
        protected void controlSet_gv_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                //新增協作者
                case "Add":
                    GridViewHelper.ChgGridViewMode(GridViewHelper.GVMode.Insert, controlSet_gv);
                    BindControlSetGridView(GetControlSetData());
                    new_hf.Value = "Add";
                    break;

                //新增協作者(Ready)
                case "AddReady":
                    GridViewHelper.ChgGridViewMode(GridViewHelper.GVMode.Insert, ready_copperate);
                    BindControlSetReadyGridView(GetControlSetReadyData());
                    readynew_hf.Value = "Add";
                    break;

                //儲存新增協作者
                case "AddSave":
                    AddSaveControl();
                    break;

                //儲存新增協作者(Ready)
                case "AddSaveReady":
                    AddSaveReadyControl();
                    break;

                //編輯帳號
                case "account" :
                    SetAccount();
                    break;
            }
        }

        //設定帳號彈出視窗
        private void SetAccount()
        {
            mv.SetActiveView(auth_view);
            ready_mv.SetActiveView(readyaccount_view);
        }

        //新增協作者
        private void AddSaveControl()
        {
            if (ProcessModifyAuth)
            {
                GridViewRow gvr = controlSet_gv.FooterRow;
                var dict = new Dictionary<string, object>();
                dict["cop_act"] = copperate_cop_act_hf.Value;
                Label label = gvr.FindControl("account_lbl") as Label;
                dict["cop_id"] = label.Text;
                DropDownList dropdownlist = gvr.FindControl("cop_authority_dll") as DropDownList;
                dict["cop_authority"] = dropdownlist.SelectedValue;

                //判斷協作者不能為空資料
                if (dict["cop_id"] == "" || dict["cop_authority"] == "")
                {
                    ShowPopupMessage(ITCEnum.PopupMessageType.Error, ITCEnum.DataActionType.Insert, "協作者資料為必填");
                }
                //新增協作者
                else
                {
                    // 新增資料
                    var res = _bl.InsertData_cop(dict);
                    if (res.IsSuccess)
                    {
                        // 新增成功，切換回一般模式
                        GridViewHelper.ChgGridViewMode(GridViewHelper.GVMode.Normal, controlSet_gv);
                        BindControlSetGridView(GetControlSetData());
                        ShowPopupMessage(ITCEnum.PopupMessageType.Success, ITCEnum.DataActionType.Insert);
                    }
                    else
                    {
                        // 新增失敗，顯示錯誤訊息
                        ShowPopupMessage(ITCEnum.PopupMessageType.Error, ITCEnum.DataActionType.Insert, res.Message);
                    }
                }
            }
        }

        //新增協作者(Ready)
        private void AddSaveReadyControl()
        {
            if (ProcessModifyAuth)
            {
                GridViewRow gvr = ready_copperate.FooterRow;
                var dict = new Dictionary<string, object>();
                dict["cop_act"] = cop_act_ready_hf.Value;
                Label label = gvr.FindControl("readyaccount_lbl") as Label;
                dict["cop_id"] = label.Text;
                DropDownList dropdownlist = gvr.FindControl("readycop_authority_dll") as DropDownList;
                dict["cop_authority"] = dropdownlist.SelectedValue;

                // 新增資料
                var res = _bl.InsertData_cop(dict);
                if (res.IsSuccess)
                {
                    // 新增成功，切換回一般模式
                    GridViewHelper.ChgGridViewMode(GridViewHelper.GVMode.Normal, ready_copperate);
                    BindControlSetReadyGridView(GetControlSetReadyData());
                    ShowPopupMessage(ITCEnum.PopupMessageType.Success, ITCEnum.DataActionType.Insert);
                }
                else
                {
                    // 新增失敗，顯示錯誤訊息
                    ShowPopupMessage(ITCEnum.PopupMessageType.Error, ITCEnum.DataActionType.Insert, res.Message);
                }
            }
        }

        //更新協作者
        protected void controlSet_gv_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (ProcessModifyAuth)
            {
                new_hf.Value = "Edit";
                readynew_hf.Value = "Edit";

                var oldData_dict = new Dictionary<string, object>();
                var newData_dict = new Dictionary<string, object>();

                //新資料
                GridViewRow gvr = controlSet_gv.Rows[e.RowIndex];
                newData_dict["cop_act"] = (gvr.FindControl("old_cop_act_hf") as HiddenField).Value;
                newData_dict["cop_id"] = (gvr.FindControl("accountEdit_lbl") as Label).Text.Trim();
                newData_dict["cop_authority"] = (gvr.FindControl("copEdit_authority_dll") as DropDownList).SelectedValue;

                //舊資料的key
                oldData_dict["cop_act"] = (gvr.FindControl("old_cop_act_hf") as HiddenField).Value;
                oldData_dict["cop_id"] = (gvr.FindControl("old_cop_id_hf") as HiddenField).Value;


                var res = _bl.UpdateCopData(oldData_dict, newData_dict);
                if (res.IsSuccess)
                {
                    // 更新成功，切換回一般模式
                    GridViewHelper.ChgGridViewMode(GridViewHelper.GVMode.Normal, controlSet_gv);
                    BindControlSetGridView(GetControlSetData());
                    ShowPopupMessage(ITCEnum.PopupMessageType.Success, ITCEnum.DataActionType.Update);
                }
                else
                {
                    // 更新失敗，顯示錯誤訊息
                    e.Cancel = true;
                    ShowPopupMessage(ITCEnum.PopupMessageType.Error, ITCEnum.DataActionType.Update, res.Message);
                }
            }
        }

        //更新協作者(Ready)
        protected void controlSetReady_gv_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (ProcessModifyAuth)
            {
                //新資料
                GridViewRow gvr = ready_copperate.Rows[e.RowIndex];
                var newData_dict = new Dictionary<string, object>();
                newData_dict["cop_act"] = (gvr.FindControl("readyold_cop_act_hf") as HiddenField).Value;
                newData_dict["cop_id"] = (gvr.FindControl("readyaccountEdit_lbl") as Label).Text.Trim();
                newData_dict["cop_authority"] = (gvr.FindControl("readycopEdit_authority_dll") as DropDownList).SelectedValue;

                //舊資料的key
                var oldData_dict = new Dictionary<string, object>();
                oldData_dict["cop_act"] = (gvr.FindControl("readyold_cop_act_hf") as HiddenField).Value;
                oldData_dict["cop_id"] = (gvr.FindControl("readyold_cop_id_hf") as HiddenField).Value;

                var res = _bl.UpdateCopData(oldData_dict, newData_dict);
                if (res.IsSuccess)
                {
                    // 更新成功，切換回一般模式
                    GridViewHelper.ChgGridViewMode(GridViewHelper.GVMode.Normal, ready_copperate);
                    BindControlSetReadyGridView(GetControlSetReadyData());
                    ShowPopupMessage(ITCEnum.PopupMessageType.Success, ITCEnum.DataActionType.Update);
                }
                else
                {
                    // 更新失敗，顯示錯誤訊息
                    e.Cancel = true;
                    ShowPopupMessage(ITCEnum.PopupMessageType.Error, ITCEnum.DataActionType.Update, res.Message);
                }
            }
        }

        //刪除協作者
        protected void controlSet_gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (ProcessModifyAuth)
            {
                GridViewRow gvr = (sender as GridView).Rows[e.RowIndex];
                var dict = new Dictionary<string, object>();
                dict["cop_act"] = copperate_cop_act_hf.Value;
                dict["cop_id"] = (gvr.FindControl("cop_id_lbl") as Label).Text;
                var res = _bl.DeleteCopData(dict);

                if (res.IsSuccess)
                {
                    BindControlSetGridView(GetControlSetData());
                    ShowPopupMessage(ITCEnum.PopupMessageType.Success, ITCEnum.DataActionType.Delete);
                }
                else
                    ShowPopupMessage(ITCEnum.PopupMessageType.Error, ITCEnum.DataActionType.Delete, res.Message);
            }
        }

        //刪除協作者(Ready)
        protected void controlSetReady_gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (ProcessModifyAuth)
            {
                GridViewRow gvr = (sender as GridView).Rows[e.RowIndex];
                var dict = new Dictionary<string, object>();
                dict["cop_act"] = cop_act_ready_hf.Value;
                dict["cop_id"] = (gvr.FindControl("readycop_id_lbl") as Label).Text;
                var res = _bl.DeleteCopData(dict);

                if (res.IsSuccess)
                {
                    BindControlSetReadyGridView(GetControlSetReadyData());
                    ShowPopupMessage(ITCEnum.PopupMessageType.Success, ITCEnum.DataActionType.Delete);
                }
                else
                    ShowPopupMessage(ITCEnum.PopupMessageType.Error, ITCEnum.DataActionType.Delete, res.Message);
            }
        }

        //RowCreated事件
        protected void controlSet_gv_RowCreated(object sender, GridViewRowEventArgs e)
        {
            switch (e.Row.RowType)
            {
                //資料列
                case DataControlRowType.DataRow:
                    //編輯列
                    if (e.Row.RowState.ToString().Contains("Edit") == true)
                    {
                        //抓取權限的值
                        if (Request.Cookies["tabs"].Value.ToString() == "0")
                        {
                            DropDownList dropdownlistedit = e.Row.FindControl("copEdit_authority_dll") as DropDownList;
                            dropdownlistedit.Items.Insert(0, new ListItem("編輯", "編輯"));
                            dropdownlistedit.Items.Insert(1, new ListItem("閱讀", "閱讀"));
                        }
                        else
                        {
                            DropDownList dropdownlistedit = e.Row.FindControl("readycopEdit_authority_dll") as DropDownList;
                            dropdownlistedit.Items.Insert(0, new ListItem("編輯", "編輯"));
                            dropdownlistedit.Items.Insert(1, new ListItem("閱讀", "閱讀"));
                        }

                        //記錄Row的index
                        row_idn_hf.Value = e.Row.RowIndex.ToString();
                        readyrow_idn_hf.Value = e.Row.RowIndex.ToString();
                    }
                    break;

                //頁尾列
                case DataControlRowType.Footer:
                    DataTable accountData = _bl.GetAccountData();
                    account_radiobuttonlist.Items.Clear();
                    readyaccount_radiobuttonlist.Items.Clear();

                    for (int i = 0; i < accountData.Rows.Count; i++)
                    {
                        ListItem listitem = new ListItem();
                        listitem.Text = accountData.Rows[i][0].ToString();
                        listitem.Selected = false;
                        account_radiobuttonlist.Items.Add(listitem);
                        readyaccount_radiobuttonlist.Items.Add(listitem);

                        Literal literal = new Literal();
                        account_pl.Controls.Add(literal);
                        readyAccount.Controls.Add(literal);
                    }
                    break;
            }
        }

        //確認設定帳號
        protected void setAccount_btn_Click(object sender, EventArgs e)
        {
            //關閉設定密碼視窗
            mv.SetActiveView(main_view);
            ready_mv.SetActiveView(ready_view);

            //已發佈協作者
            if (Request.Cookies["tabs"].Value.ToString() == "0")
            {
                //編輯列
                if (new_hf.Value == "Edit")
                {
                    Label label = controlSet_gv.Rows[CommonConvert.GetIntOrZero(readyrow_idn_hf.Value)].FindControl("accountEdit_lbl") as Label;
                    label.Text = account_radiobuttonlist.SelectedValue.ToString();
                }
                //新增列
                else if (new_hf.Value == "Add")
                {
                    Label label = controlSet_gv.FooterRow.FindControl("account_lbl") as Label;
                    label.Text = account_radiobuttonlist.SelectedValue.ToString();
                }
            }
            //未發佈協作者
            else if (Request.Cookies["tabs"].Value.ToString() == "1")
            {
                //編輯
                if (readynew_hf.Value == "Edit")
                {
                    Label label = ready_copperate.Rows[CommonConvert.GetIntOrZero(readyrow_idn_hf.Value)].FindControl("readyaccountEdit_lbl") as Label;
                    label.Text = readyaccount_radiobuttonlist.SelectedValue.ToString();
                }
                //新增
                else if (readynew_hf.Value == "Add")
                {
                    Label label = ready_copperate.FooterRow.FindControl("readyaccount_lbl") as Label;
                    label.Text = readyaccount_radiobuttonlist.SelectedValue.ToString();
                }
            }
        }

        //協作者新增帳號返回
        protected void back_btn_Click(object sender, EventArgs e)
        {
            //關閉設定帳號視窗
            mv.SetActiveView(main_view);
            ready_mv.SetActiveView(ready_view);
        }
        #endregion

        #region 查詢按鈕
        protected void q_query_btn_Click(object sender, EventArgs e)
        {
            int i = Convert.ToInt32(Request.Cookies["tabs"].Value);
            DataTable data = new DataTable();

            //搜尋全部
            if (q_keyword_ddl.SelectedValue.ToString() == "全部" && q_keyword_tb.Text == "")
            {
                BindGridView(GetAlreadyData(), GetReadyData(), GetEndData());
            }
            //普通搜尋
            else
            {
                data = _bl.GetQueryClassData(q_keyword_tb.Text, i, q_keyword_ddl.SelectedValue.ToString());

                //依頁簽區分查詢條件(已發佈)
                if (i == 0)
                {
                    BindGridView(data, GetReadyData(), GetEndData());
                }
                //未發佈
                else if (i == 1)
                {
                    BindGridView(GetAlreadyData(), data, GetEndData());
                }
                //已結束
                else if (i == 2)
                {
                    BindGridView(GetAlreadyData(), GetReadyData(), data);
                }
            }
        }
        #endregion
    }
}