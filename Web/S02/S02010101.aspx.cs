using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using Util;
using BusinessLayer.S02;
using System.Data;
using System.Collections;
using AjaxControlToolkit;
using System.IO;
using System.Text;
using Web.App_Code;
using NPOI.HSSF.UserModel;
using NPOI;
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

            if (!IsPostBack)
            {
                BindGridView(GetAlreadyData(), GetReadyData(), GetEndData());
            }
        }

        #region Control權限管理
        protected void ManageControlAuth(object sender, EventArgs e)
        {
            ManageControlAuth(sender);
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
            BindControlSetGridView(lst);
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
                case DataControlRowType.DataRow:
                    if (e.Row.RowState.ToString().Contains("Edit") == false)
                    {
                        // 一般資料列
                        string cop_id = (e.Row.FindControl("cop_id_lbl") as Label).Text;
                        string cop_authority = (e.Row.FindControl("cop_authority_lbl") as Label).Text;
                        (e.Row.FindControl("delete_btn") as Button).OnClientClick = "if (!confirm(\"帳號：" + cop_id + "\\n權限：" + cop_authority + "\\n\\n確定要刪除嗎?\")) return false";
                    }
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
                    break;

                //新增協作者(Ready)
                case "AddReady":
                    GridViewHelper.ChgGridViewMode(GridViewHelper.GVMode.Insert, ready_copperate);
                    BindControlSetReadyGridView(GetControlSetReadyData());
                    break;

                //儲存新增協作者
                case "AddSave":
                    AddSaveControl();
                    break;

                //儲存新增協作者(Ready)
                case "AddSaveReady":
                    AddSaveReadyControl();
                    break;
            }
        }

        //新增協作者
        private void AddSaveControl()
        {
            if (ProcessModifyAuth)
            {
                GridViewRow gvr = controlSet_gv.FooterRow;
                var dict = new Dictionary<string, object>();
                dict["cop_act"] = copperate_cop_act_hf.Value;
                dict["cop_id"] = (gvr.FindControl("cop_id_txt") as TextBox).Text.Trim();
                dict["cop_authority"] = (gvr.FindControl("cop_authority_txt") as TextBox).Text.Trim();

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

        //新增協作者(Ready)
        private void AddSaveReadyControl()
        {
            if (ProcessModifyAuth)
            {
                GridViewRow gvr = ready_copperate.FooterRow;
                var dict = new Dictionary<string, object>();
                dict["cop_act"] = cop_act_ready_hf.Value;
                dict["cop_id"] = (gvr.FindControl("cop_id_txt") as TextBox).Text.Trim();
                dict["cop_authority"] = (gvr.FindControl("cop_authority_txt") as TextBox).Text.Trim();

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
                //新資料
                GridViewRow gvr = controlSet_gv.Rows[e.RowIndex];
                var newData_dict = new Dictionary<string, object>();
                newData_dict["cop_act"] = (gvr.FindControl("old_cop_act_hf") as HiddenField).Value;
                newData_dict["cop_id"] = (gvr.FindControl("cop_id_txt") as TextBox).Text.Trim();
                newData_dict["cop_authority"] = (gvr.FindControl("cop_authority_txt") as TextBox).Text.Trim();

                //舊資料的key
                var oldData_dict = new Dictionary<string, object>();
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
                newData_dict["cop_act"] = (gvr.FindControl("old_cop_act_hf") as HiddenField).Value;
                newData_dict["cop_id"] = (gvr.FindControl("cop_id_txt") as TextBox).Text.Trim();
                newData_dict["cop_authority"] = (gvr.FindControl("cop_authority_txt") as TextBox).Text.Trim();

                //就資料的key
                var oldData_dict = new Dictionary<string, object>();
                oldData_dict["cop_act"] = (gvr.FindControl("old_cop_act_hf") as HiddenField).Value;
                oldData_dict["cop_id"] = (gvr.FindControl("old_cop_id_hf") as HiddenField).Value;

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
                dict["cop_id"] = (gvr.FindControl("cop_id_lbl") as Label).Text;
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
        #endregion

        #region 查詢按鈕
        protected void q_query_btn_Click(object sender, EventArgs e)
        {
            //BindGridView(GetAlreadyData(), GetReadyData(), GetEndData());
            int i = Convert.ToInt32(Request.Cookies["tabs"].Value);

            DataTable data = _bl.GetQueryData(q_keyword_tb.Text, i);

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
        #endregion
    }
}