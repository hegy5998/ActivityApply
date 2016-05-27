using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer.S02;
using System.Data;
using Util;
using AjaxControlToolkit;
using NPOI.HSSF.UserModel;
using NPOI;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;

namespace Web.S02
{
    public partial class S02010102 : CommonPages.BasePage
    {
        S020101BL _bl = new S020101BL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int i = Convert.ToInt32(Request.QueryString["i"]);
                int act_idn = _bl.Getactidn(i);
                DataTable copperate = _bl.GetCopData(act_idn);
                int checkcop = 0;

                //判斷是建立者or協作者
                for (int j = 0; j < copperate.Rows.Count; j++) 
                {
                    if (copperate.Rows[j][1].ToString() == CommonHelper.GetLoginUser().Act_id)
                    {
                        if (copperate.Rows[j][2].ToString() == "閱讀")
                        {
                            checkcop = 1;
                        }
                    }
                }

                //建立者
                if (checkcop == 0)
                {
                    main_gv.AllowPaging = false;
                    main_gv.Visible = true;
                    BindGridView(GetData(i));
                }
                //協作者
                else if (checkcop == 1)
                {
                    copperate_gv.Visible = true;
                    BindCopperateGridView(GetData(i));
                }
            }

        }

        #region main_gv事件
        //取得報名資料
        private DataTable GetData(int i)
        {
            return _bl.GetApplyData(i);
        }

        //Bind資料
        private void BindGridView(DataTable lst)
        {
            int count = main_gv.Columns.Count - 1;
            DataTable data = _bl.GetApplyDataDetail(CommonConvert.GetIntOrZero(Request.QueryString["i"]));

            //清除之前的資料
            for (int i = count; i > 0; i--)
            {
                main_gv.Columns.RemoveAt(i);
            }

            main_gv.AutoGenerateColumns = true;
            main_gv.DataSource = lst;
            main_gv.DataBind();

            if (main_gv.Rows.Count == 0)
            {
                // 避免無資料列時，無法顯示footer新增資料
                DataRow row = lst.NewRow();
                for (int i = 0; i < lst.Columns.Count; i++)
                {
                    row[i] = null;
                }
                lst.Rows.Add(row);

                main_gv.DataSource = lst;
                main_gv.DataBind();
                main_gv.Rows[0].Attributes.Add("style", "display: none");
            }
        }

        //BindGridView(協作者)
        private void BindCopperateGridView(DataTable lst)
        {
            int count = copperate_gv.Columns.Count - 1;
            DataTable data = _bl.GetApplyDataDetail(CommonConvert.GetIntOrZero(Request.QueryString["i"]));

            //清除之前的資料
            for (int i = count; i > 0; i--)
            {
                copperate_gv.Columns.RemoveAt(i);
            }

            copperate_gv.AutoGenerateColumns = true;
            copperate_gv.DataSource = lst;
            copperate_gv.DataBind();
        }

        //編輯
        protected void main_gv_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int i = Convert.ToInt32(Request.QueryString["i"]);

            GridViewHelper.ChgGridViewMode(GridViewHelper.GVMode.Edit, main_gv, e.NewEditIndex);
            BindGridView(GetData(i));
        }

        //取消編輯
        protected void main_gv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            int i = CommonConvert.GetIntOrZero(Request.QueryString["i"]);

            GridViewHelper.ChgGridViewMode(GridViewHelper.GVMode.Normal, main_gv);
            BindGridView(GetData(i));
        }

        //RowDataBound事件
        protected void main_gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            switch (e.Row.RowType)
            {
                //資料列
                case DataControlRowType.DataRow:
                    if (e.Row.RowState.ToString().Contains("Edit") == true)
                    {
                        //取得必要的資料
                        int iEdit = Convert.ToInt32(Request.QueryString["i"]);
                        GridViewRow gvrEdit = e.Row;
                        DataTable colEdit = _bl.GetApplyDataDetail(iEdit);
                        new_hf.Value = "Edit";

                        //設定起始參數
                        int checkEdit = 2;

                        //新增修改輸入框
                        foreach (DataRow r in colEdit.Rows)
                        {
                            //文字輸入框
                            if (r.ItemArray.GetValue(6).ToString() == "text")
                            {
                                checkEdit++;
                            }
                            //單選 & 下拉式選單
                            if ((r.ItemArray.GetValue(6).ToString() == "singleSelect") || (r.ItemArray.GetValue(6).ToString() == "dropDownList"))
                            {
                                //取得已建立好的下拉式選單
                                DropDownList dropdownlist = gvrEdit.Cells[checkEdit].Controls[0] as DropDownList;

                                //取得未修改前的值
                                DataRowView rowView = (DataRowView)e.Row.DataItem;
                                String state = rowView[checkEdit - 1].ToString();
                                if (state != "")
                                {
                                    ListItem item = dropdownlist.Items.FindByText(state);
                                    dropdownlist.SelectedIndex = dropdownlist.Items.IndexOf(item);
                                }
                                else
                                {
                                    dropdownlist.Items.Insert(0, new ListItem("請選擇", ""));
                                }

                                checkEdit++;
                            }
                            //多選
                            else if (r.ItemArray.GetValue(6).ToString() == "multiSelect")
                            {
                                //取出未修改前的值
                                DataRowView rowView = (DataRowView)e.Row.DataItem;
                                String state = rowView[checkEdit - 1].ToString();

                                //顯示未修改的值在GridView上
                                Label label = gvrEdit.Cells[checkEdit].Controls[1] as Label;
                                label.Text = state;
                                //gvrEdit.Cells[checkEdit].Controls.Add(label);

                                //判斷選項是否已被勾選
                                string[] states = state.Split(',');
                                int j = 0;
                                for (int i = 0; i < multioption_pl.Controls.Count; i++)
                                {
                                    CheckBox option = multioption_pl.Controls[i] as CheckBox;
                                    if ((option != null) && (j < states.Count()))
                                    {
                                        if (option.Text == states[j])
                                        {
                                            option.Checked = true;
                                            j++;
                                        }
                                        else
                                        {
                                            option.Checked = false;
                                        }
                                    }
                                }

                                checkEdit++;
                            }
                        }
                    }
                    break;
            }
        }

        //命令處理
        protected void main_gv_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                //新增Row
                case "Add":
                    int i = Convert.ToInt32(Request.QueryString["i"]);

                    //若是超出場次限制人數則跳出提醒
                    int limit = _bl.GetApplyLimit(i);
                    if (main_gv.Rows.Count >= limit)
                    {
                        ShowPopupMessage(ITCEnum.PopupMessageType.Error, "警告", "已超過此場次限制人數，請謹慎思考是否要新增");
                    }

                    new_hf.Value = "Add";

                    //將多選的選項清空
                    for (int j = 0; j < multioption_pl.Controls.Count; j++)
                    {
                        CheckBox checkbox = multioption_pl.Controls[j] as CheckBox;
                        if (checkbox != null)
                        {
                            checkbox.Checked = false;
                        }
                    }

                    GridViewHelper.ChgGridViewMode(GridViewHelper.GVMode.Insert, main_gv);
                    BindGridView(GetData(i));
                    break;

                //新增資料
                case "AddSave":
                    AddSave(sender, e);
                    break;

                //多選
                case "multiSelect":
                    // 設定彈出視窗
                    InitControlPopupWindow(sender, e);
                    break;
            }
        }

        //多選跳出視窗
        private void InitControlPopupWindow(object sender, GridViewCommandEventArgs e)
        {
            multi_mpe.Show();  // Popup Window
            multi_pl.Visible = true;
        }

        //新增資料
        private void AddSave(object sender, GridViewCommandEventArgs e)
        {
            //新增報名資料
            int i = Convert.ToInt32(Request.QueryString["i"]);
            GridViewRow gvr = main_gv.FooterRow;
            GridView gv = sender as GridView;
            TextBox textbox = new TextBox();
            int check = 2;
            DataTable colData = _bl.GetApplyDataDetail(i);
            string required = "";
            int checkrequired = 0;

            //判斷必填
            for (int j = 0; j < colData.Rows.Count; j++)
            {
                //此欄位為必填
                if (colData.Rows[j][8].ToString() == "1")
                {
                    //文字輸入框
                    if (colData.Rows[j][6].ToString() == "text")
                    {
                        textbox = gvr.Cells[j + 2].Controls[0] as TextBox;
                        if (textbox.Text.IsNullOrEmpty())
                        {
                            required = required + "," + colData.Rows[j][3].ToString();
                            checkrequired = 1;
                        }
                    }
                    //單選 & 下拉式選單
                    else if ((colData.Rows[j][6].ToString() == "singleSelect") || (colData.Rows[j][6].ToString() == "dropDownList"))
                    {
                        DropDownList d = gvr.Cells[j + 2].Controls[0] as DropDownList;
                        if (d.SelectedValue.IsNullOrEmpty())
                        {
                            required = required + "," + colData.Rows[j][3].ToString();
                            checkrequired = 1;
                        }
                    }
                    //多選
                    else if (colData.Rows[j][6].ToString() == "multiSelect")
                    {
                        Label s = gvr.Cells[check].Controls[1] as Label;
                        if (s.Text.IsNullOrEmpty())
                        {
                            required = required + "," + colData.Rows[j][3].ToString();
                            checkrequired = 1;
                        }
                    }
                }
            }

            //必填驗證成功
            if (checkrequired == 0)
            {
                //取得各欄位的值
                var dict = new Dictionary<string, object>();
                dict["aa_act"] = _bl.Getactidn(i);

                //判斷姓名跟電子信箱的位置
                for (int j = 0; j < colData.Rows.Count; j++)
                {
                    if (colData.Rows[j][3].ToString() == "姓名")
                    {
                        textbox = gvr.Cells[j + 2].Controls[0] as TextBox;
                        dict["aa_name"] = textbox.Text;
                    }

                    if (colData.Rows[j][3].ToString() == "電子信箱Email")
                    {
                        textbox = gvr.Cells[j + 2].Controls[0] as TextBox;
                        dict["aa_email"] = textbox.Text;
                    }
                }

                dict["aa_as"] = Convert.ToInt32(Request.QueryString["i"]);

                var res_aa = _bl.InsertData_apply(dict);

                //新增報名表欄位資料
                var dict_col = new Dictionary<string, object>();
                dict_col["aad_apply_id"] = CommonConvert.GetIntOrZero(res_aa.Message);
                var res_col = new CommonResult(false);

                //對應欄位塞值
                foreach (DataRow r in colData.Rows)
                {
                    dict_col["aad_col_id"] = r.ItemArray.GetValue(2).ToString();

                    //文字輸入
                    if (r.ItemArray.GetValue(6).ToString() == "text")
                    {
                        textbox = gvr.Cells[check].Controls[0] as TextBox;
                        dict_col["aad_val"] = textbox.Text;
                        check++;

                        res_col = _bl.InsertData_column(dict_col);
                    }
                    //單選 & 下拉式選單
                    else if ((r.ItemArray.GetValue(6).ToString() == "singleSelect") || (r.ItemArray.GetValue(6).ToString() == "dropDownList"))
                    {
                        DropDownList d = gvr.Cells[check].Controls[0] as DropDownList;
                        dict_col["aad_val"] = d.SelectedValue;
                        check++;

                        res_col = _bl.InsertData_column(dict_col);
                    }
                    //多選
                    else if (r.ItemArray.GetValue(6).ToString() == "multiSelect")
                    {
                        Label s = gvr.Cells[check].Controls[1] as Label;
                        dict_col["aad_val"] = s.Text;
                        check++;

                        res_col = _bl.InsertData_column(dict_col);
                    }
                }

                //若是第一次申請email則需要新增密碼
                DataTable emailData = _bl.GetEmailData();
                int emailcheck = 0;
                foreach (DataRow r in emailData.Rows)
                {
                    if (r[0].ToString() == dict["aa_email"].ToString())
                    {
                        emailcheck = 1;
                        break;
                    }
                }
                //跳出設定密碼的視窗
                if (emailcheck == 0)
                {
                    email_hf.Value = dict["aa_email"].ToString();
                    password_txt.Text = null;
                    passwordcheck_txt.Text = null;

                    emailpassword_mpe.Show();  // Popup Window
                    emailpassword_pl.Visible = true;
                }
                //新增成功
                else
                {
                    if (res_col.IsSuccess)
                    {
                        // 新增成功，切換回一般模式
                        GridViewHelper.ChgGridViewMode(GridViewHelper.GVMode.Normal, main_gv);
                        BindGridView(GetData(CommonConvert.GetIntOrZero(Request.QueryString["i"])));
                        ShowPopupMessage(ITCEnum.PopupMessageType.Success, ITCEnum.DataActionType.Insert);
                    }
                    else
                    {
                        // 新增失敗，顯示錯誤訊息
                        ShowPopupMessage(ITCEnum.PopupMessageType.Error, ITCEnum.DataActionType.Insert, res_col.Message);
                    }
                }
            }
            //必填有資料沒填
            else if (checkrequired == 1)
            {
                required = required.Substring(1);
                ShowPopupMessage(ITCEnum.PopupMessageType.Error, ITCEnum.DataActionType.Insert, required + "為必填資料");
            }
        }

        //刪除資料
        protected void main_gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int i = CommonConvert.GetIntOrZero(Request.QueryString["i"]);
            GridViewRow gvr = (sender as GridView).Rows[e.RowIndex];

            //欲刪除報名資料的場次序號
            var data_dict = new Dictionary<string, object>();
            data_dict["as_idn"] = i;

            //欲刪除報名資料的報名序號
            var data_dict_aa = new Dictionary<string, object>();
            string aa_idn = gvr.Cells[1].Text;
            data_dict_aa["aa_idn"] = aa_idn;

            //刪除資料
            var res = _bl.DeleteApply(data_dict, data_dict_aa);
            if (res.IsSuccess)
            {
                // 刪除成功，切換回一般模式
                BindGridView(GetData(i));
                ShowPopupMessage(ITCEnum.PopupMessageType.Success, ITCEnum.DataActionType.Delete);
            }
            else
            {
                // 刪除失敗，顯示錯誤訊息
                ShowPopupMessage(ITCEnum.PopupMessageType.Error, ITCEnum.DataActionType.Delete, res.Message);
            }
        }

        //更新資料
        protected void main_gv_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int i = Convert.ToInt32(Request.QueryString["i"]);
            GridViewRow gvr = main_gv.Rows[e.RowIndex];
            int check = 2;
            var res = new CommonResult(false);
            TextBox textbox = new TextBox();
            new_hf.Value = "Edit";
            string required = ""; //必填沒填的選項
            int checkrequired = 0; //判斷必填是否有填資料

            //修改Apply資料
            var newApplyData_dict = new Dictionary<string, object>();
            var oldApplyData_dict = new Dictionary<string, object>();
            newApplyData_dict["aa_act"] = _bl.Getactidn(i);
            newApplyData_dict["aa_as"] = i;
            oldApplyData_dict["aa_idn"] = (gvr.FindControl("old_aa_idn_hf") as HiddenField).Value;

            //取得欄位資料
            DataTable col_id = _bl.GetApplyDataDetail(i);
            
            //修改後的資料
            var newData_dict = new Dictionary<string, object>();
            newData_dict["aad_apply_id"] = (gvr.FindControl("old_aa_idn_hf") as HiddenField).Value;

            //修改前的資料
            var oldData_dict = new Dictionary<string, object>();
            oldData_dict["aad_apply_id"] = (gvr.FindControl("old_aa_idn_hf") as HiddenField).Value;

            //判斷必填
            for (int j = 0; j < col_id.Rows.Count; j++)
            {
                //此欄位為必填
                if (col_id.Rows[j][8].ToString() == "1")
                {
                    //文字輸入框
                    if (col_id.Rows[j][6].ToString() == "text")
                    {
                        textbox = gvr.Cells[j + 2].Controls[0] as TextBox;
                        if (textbox.Text.IsNullOrEmpty())
                        {
                            required = required + "," + col_id.Rows[j][3].ToString();
                            checkrequired = 1;
                        }
                    }
                    //單選 & 下拉式選單
                    else if ((col_id.Rows[j][6].ToString() == "singleSelect") || (col_id.Rows[j][6].ToString() == "dropDownList"))
                    {
                        DropDownList d = gvr.Cells[j + 2].Controls[0] as DropDownList;
                        if (d.SelectedValue.IsNullOrEmpty())
                        {
                            required = required + "," + col_id.Rows[j][3].ToString();
                            checkrequired = 1;
                        }
                    }
                    //多選
                    else if (col_id.Rows[j][6].ToString() == "multiSelect")
                    {
                        Label s = gvr.Cells[check].Controls[1] as Label;
                        if (s.Text.IsNullOrEmpty())
                        {
                            required = required + "," + col_id.Rows[j][3].ToString();
                            checkrequired = 1;
                        }
                    }
                }
            }

            int emailcheck = 0;
            //必填驗證成功
            if (checkrequired == 0)
            {
                //根據欄位資料對應修改資料
                foreach (DataRow r in col_id.Rows)
                {
                    newData_dict["aad_col_id"] = r.ItemArray.GetValue(2).ToString();
                    oldData_dict["aad_col_id"] = r.ItemArray.GetValue(2).ToString();

                    //文字輸入
                    if (r.ItemArray.GetValue(6).ToString() == "text")
                    {
                        //尋找姓名 & email
                        textbox = gvr.Cells[check].Controls[0] as TextBox;
                        if (r.ItemArray.GetValue(3).ToString() == "姓名")
                        {
                            newApplyData_dict["aa_name"] = textbox.Text;
                        }

                        if (r.ItemArray.GetValue(3).ToString() == "電子信箱Email")
                        {
                            newApplyData_dict["aa_email"] = textbox.Text;

                            //若是第一次申請email則需要新增密碼
                            DataTable emailData = _bl.GetEmailData();
                            foreach (DataRow rr in emailData.Rows)
                            {
                                if (rr[0].ToString() == textbox.Text)
                                {
                                    emailcheck = 1;
                                    break;
                                }
                            }

                            //跳出設定密碼的視窗
                            if (emailcheck == 0)
                            {
                                email_hf.Value = textbox.Text;
                                password_txt.Text = null;
                                passwordcheck_txt.Text = null;

                                emailpassword_mpe.Show();  // Popup Window
                                emailpassword_pl.Visible = true;
                            }
                        }
                    
                        newData_dict["aad_val"] = textbox.Text;
                
                        res = _bl.UpdateApplyDetailData(oldData_dict, newData_dict);

                        check++;
                    }
                    //單選 & 下拉式選單
                    else if ((r.ItemArray.GetValue(6).ToString() == "singleSelect") || (r.ItemArray.GetValue(6).ToString() == "dropDownList"))
                    {
                        DropDownList dropdownlist = gvr.Cells[check].Controls[0] as DropDownList;
                        newData_dict["aad_val"] = dropdownlist.SelectedValue;

                        res = _bl.UpdateApplyDetailData(oldData_dict, newData_dict);

                        check++;
                    }
                    //多選
                    else if (r.ItemArray.GetValue(6).ToString() == "multiSelect")
                    {
                        string s = (gvr.Cells[check].Controls[1] as Label).Text;
                        newData_dict["aad_val"] = s;

                        res = _bl.UpdateApplyDetailData(oldData_dict, newData_dict);

                        check++;
                    }

                    //修改失敗，此問題為後來新增的問題(需用新增的方式)
                    if (!res.IsSuccess)
                    {
                        res = _bl.InsertData_column(newData_dict);
                    }

                    res = _bl.UpdateApplyData(oldApplyData_dict, newApplyData_dict);
                }

                if (emailcheck != 0)
                {
                    if (res.IsSuccess)
                    {
                        // 修改成功，切換回一般模式
                        GridViewHelper.ChgGridViewMode(GridViewHelper.GVMode.Normal, main_gv);
                        BindGridView(GetData(CommonConvert.GetIntOrZero(Request.QueryString["i"])));
                        ShowPopupMessage(ITCEnum.PopupMessageType.Success, ITCEnum.DataActionType.Update);
                    }
                    else
                    {
                        // 修改失敗，顯示錯誤訊息
                        ShowPopupMessage(ITCEnum.PopupMessageType.Error, ITCEnum.DataActionType.Update, res.Message);
                    }
                }
            }
            //必填有資料沒填
            else if (checkrequired == 1)
            {
                required = required.Substring(1);
                ShowPopupMessage(ITCEnum.PopupMessageType.Error, ITCEnum.DataActionType.Update, required + "為必填資料");
            }
        }

        //RowCreated事件
        protected void main_gv_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Cells.Count > 3)
            {
                e.Row.Cells[1].Visible = false;

                switch (e.Row.RowType)
                {
                    //資料列
                    case DataControlRowType.DataRow:
                        //編輯列
                        if (e.Row.RowState.ToString().Contains("Edit") == true)
                        {
                            int iEdit = Convert.ToInt32(Request.QueryString["i"]);
                            GridViewRow gvrEdit = e.Row;
                            DataTable colEdit = _bl.GetApplyDataDetail(iEdit);

                            int checkEdit = 2;

                            //新增空白輸入框
                            foreach (DataRow r in colEdit.Rows)
                            {
                                //文字輸入框
                                if (r.ItemArray.GetValue(6).ToString() == "text")
                                {
                                    checkEdit++;
                                }
                                //單選 & 下拉式選單
                                if ((r.ItemArray.GetValue(6).ToString() == "singleSelect") || (r.ItemArray.GetValue(6).ToString() == "dropDownList"))
                                {
                                    //建立新的下拉式選單
                                    DropDownList dropdownlist = new DropDownList();

                                    //反序列化
                                    string dec = r.ItemArray.GetValue(7).ToString();
                                    dec = Uri.UnescapeDataString(dec);

                                    //加入下拉式選單選項
                                    int j = 0;
                                    string[] optionArray = dec.Split('&');
                                    foreach (string s in optionArray)
                                    {
                                        string[] optionArrayS = s.Split('=');
                                        string option = optionArrayS[1];
                                        dropdownlist.Items.Insert(j, new ListItem(option, option));
                                        j++;
                                    }

                                    gvrEdit.Cells[checkEdit].Controls.Clear();
                                    gvrEdit.Cells[checkEdit].Controls.Add(dropdownlist);
                                    checkEdit++;
                                }
                                //多選
                                else if (r.ItemArray.GetValue(6).ToString() == "multiSelect")
                                {
                                    //加入按鈕(跳出另外視窗做選擇)
                                    Button button = new Button();
                                    button.ID = "multi_btn";
                                    button.CommandName = "multiSelect";
                                    button.CssClass = "btn-link";
                                    button.Text = "請選擇";
                                    button.UseSubmitBehavior = false;
                                    button.CommandArgument = r.ItemArray.GetValue(2).ToString();
                                    col_idn_hf.Value = checkEdit.ToString();

                                    gvrEdit.Cells[checkEdit].Controls.Clear();
                                    gvrEdit.Cells[checkEdit].Controls.Add(button);

                                    //加入選項的值(第一次為空)
                                    Label label = new Label();
                                    label.Text = null;
                                    gvrEdit.Cells[checkEdit].Controls.Add(label);

                                    //記錄Row的index
                                    row_idn_hf.Value = e.Row.RowIndex.ToString();

                                    checkEdit++;
                                }
                            }
                        }
                        break;

                    //頁尾列
                    case DataControlRowType.Footer:
                        int i = Convert.ToInt32(Request.QueryString["i"]);
                        GridViewRow gvr = e.Row;
                        DataTable col = _bl.GetApplyDataDetail(i);
                        
                        int check = 2;

                        //新增空白輸入框
                        foreach (DataRow r in col.Rows)
                        {
                            //文字輸入框
                            if (r.ItemArray.GetValue(6).ToString() == "text")
                            {
                                TextBox textbox = new TextBox();
                                textbox.ID = r.ItemArray.GetValue(2).ToString();
                                gvr.Cells[check].Controls.Add(textbox);
                                check++;
                            }
                            //單選 & 下拉式選單
                            else if ((r.ItemArray.GetValue(6).ToString() == "singleSelect") || (r.ItemArray.GetValue(6).ToString() == "dropDownList"))
                            {
                                DropDownList dropdownlist = new DropDownList();
                                dropdownlist.Items.Insert(0, new ListItem("請選擇", ""));

                                //反序列化
                                string dec = r.ItemArray.GetValue(7).ToString();
                                dec = Uri.UnescapeDataString(dec);

                                //加入下拉式選單選項
                                int j = 1;
                                string[] optionArray = dec.Split('&');
                                foreach (string s in optionArray)
                                {
                                    string[] optionArrayS = s.Split('=');
                                    string option = optionArrayS[1];
                                    dropdownlist.Items.Insert(j, new ListItem(option, option));
                                    j++;
                                }

                                gvr.Cells[check].Controls.Add(dropdownlist);
                                check++;
                            }
                            //多選
                            else if (r.ItemArray.GetValue(6).ToString() == "multiSelect")
                            {
                                //加入按鈕(跳出另外視窗做選擇)
                                Button button = new Button();
                                button.ID = "multi_btn";
                                button.CommandName = "multiSelect";
                                button.CssClass = "btn-link";
                                button.Text = "請選擇";
                                button.UseSubmitBehavior = false;
                                button.CommandArgument = r.ItemArray.GetValue(2).ToString();
                                col_idn_hf.Value = check.ToString();

                                gvr.Cells[check].Controls.Add(button);

                                //加入選項的值(第一次為空)
                                Label label = new Label();
                                gvr.Cells[check].Controls.Add(label);

                                string multioption = _bl.GetMultiOption(r.ItemArray.GetValue(2).ToString());

                                //反序列化
                                multioption = Uri.UnescapeDataString(multioption);

                                //加入多選選項
                                string[] optionArray = multioption.Split('&');
                                foreach (string s in optionArray)
                                {
                                    string[] optionArrayS = s.Split('=');
                                    string option = optionArrayS[1];
                                    CheckBox checkbox = new CheckBox();
                                    checkbox.Text = option;
                                    checkbox.Checked = false;

                                    multioption_pl.Controls.Add(checkbox);
                                    Literal literal = new Literal();
                                    literal.Text = "<br />";
                                    multioption_pl.Controls.Add(literal);
                                }

                                check++;
                            }
                        }
                        break;
                }
            }
        }

        //多選 & 設定密碼跳出視窗關閉按鈕
        protected void control_cancel_btn_Click(object sender, EventArgs e)
        {
            BindGridView(GetData(CommonConvert.GetIntOrZero(Request.QueryString["i"])));
        }

        //確認多選的資料
        protected void checkmulti_btn_Click(object sender, EventArgs e)
        {
            string s = null;

            //將資料顯示在GridView
            foreach (Control c in multioption_pl.Controls)
            {
                if (c.GetType() == typeof(CheckBox))
                {
                    CheckBox r = c as CheckBox;

                    if (r.Checked == true)
                    {
                        s = s + "," + r.Text;
                    }
                }
            }
            
            //判斷是否為頁尾列(編輯列)
            if (new_hf.Value == "Edit")
            {
                Label l = main_gv.Rows[CommonConvert.GetIntOrZero(row_idn_hf.Value)].Cells[CommonConvert.GetIntOrZero(col_idn_hf.Value)].Controls[1] as Label;
                //若有選資料
                if (s != null)
                {
                    l.Text = s.Substring(1);
                }
                //若沒選資料
                else
                {
                    l.Text = null;
                }
            }
            //新增列(頁尾列)
            else if (new_hf.Value == "Add")
            {
                Label l = main_gv.FooterRow.Cells[CommonConvert.GetIntOrZero(col_idn_hf.Value)].Controls[1] as Label;
                l.Text = s.Substring(1);
                main_gv.FooterRow.Cells[CommonConvert.GetIntOrZero(col_idn_hf.Value)].Controls.Add(l);
            }

            //隱藏跳出視窗
            multi_mpe.Hide();
            multi_pl.Visible = false;
        }

        //設定密碼確認
        protected void checkpassword_btn_Click(object sender, EventArgs e)
        {
            if (password_txt.Text == passwordcheck_txt.Text)
            {
                //新增密碼
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict["aae_email"] = email_hf.Value;
                dict["aae_password"] = password_txt.Text;

                var res = _bl.InsertEmailData(dict);

                if (res.IsSuccess)
                {
                    // 新增成功，切換回一般模式
                    GridViewHelper.ChgGridViewMode(GridViewHelper.GVMode.Normal, main_gv);
                    BindGridView(GetData(CommonConvert.GetIntOrZero(Request.QueryString["i"])));

                    //修改
                    if (new_hf.Value == "Edit")
                    {
                        ShowPopupMessage(ITCEnum.PopupMessageType.Success, ITCEnum.DataActionType.Update);
                    }
                    //新增
                    else
                    {
                        ShowPopupMessage(ITCEnum.PopupMessageType.Success, ITCEnum.DataActionType.Insert);
                    }
                }
                else
                {
                    // 新增失敗，顯示錯誤訊息
                    ShowPopupMessage(ITCEnum.PopupMessageType.Error, ITCEnum.DataActionType.Insert, res.Message);
                }

                //隱藏跳出視窗
                emailpassword_mpe.Hide();
                emailpassword_pl.Visible = false;
            }
        }

        //main_gv排序
        protected void main_gv_Sorting(object sender, GridViewSortEventArgs e)
        {
            var lst = GridViewHelper.SortGridView(sender as GridView, e, GetData(CommonConvert.GetIntOrZero(Request.QueryString["i"])));
            BindGridView(lst);
        }
        #endregion

        #region 下載
        protected void download_btn_Click(object sender, EventArgs e)
        {
            int i = CommonConvert.GetIntOrZero(Request.QueryString["i"]);
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
        #endregion

        #region 顯示資料處理提示訊息
        //顯示資料處理提示訊息
        protected void ShowPopupMessage(ITCEnum.PopupMessageType pmType, ITCEnum.DataActionType dataActType, string msg = "")
        {
            string header = "";
            switch (dataActType)
            {
                case ITCEnum.DataActionType.Insert:
                    header = "新增";
                    break;
                case ITCEnum.DataActionType.Update:
                    header = "儲存";
                    break;
                case ITCEnum.DataActionType.Delete:
                    header = "刪除";
                    break;
            }
            switch (pmType)
            {
                case ITCEnum.PopupMessageType.Success:
                    header += "成功!";
                    break;
                case ITCEnum.PopupMessageType.Error:
                    header += "失敗!";
                    break;
            }
            ShowPopupMessage(pmType, header, msg);
        }

        //顯示資料處理提示訊息
        protected void ShowPopupMessage(ITCEnum.PopupMessageType type, string header, string msg = "")
        {
            string theme = "";          // CSS
            string sticky = "false";    // 是否保持顯示
            string speed = "normal";    // 顯示速度
            switch (type)
            {
                case ITCEnum.PopupMessageType.Success:  // 成功(綠色)
                    theme = "success";
                    speed = "fast";
                    break;
                case ITCEnum.PopupMessageType.Error:    // 錯誤(紅色)
                    theme = "error";
                    sticky = "true";
                    speed = "fast";
                    break;
                case ITCEnum.PopupMessageType.Warning:  // 警告(黃色)
                    theme = "warning";
                    sticky = "true";
                    speed = "fast";
                    break;
                case ITCEnum.PopupMessageType.Info:     // 資訊(藍色)
                    theme = "info";
                    sticky = "true";
                    speed = "fast";
                    break;
            }
            CallJavascript("jgrowl", @"
                $.jGrowl('" + msg + @"', {
                    theme: '" + theme + @"',
                    header: '" + header + @"',
                    sticky: " + sticky + @",
                    position: 'center',
                    speed: '" + speed + @"',
                    beforeOpen: function(e, m) {
                        $('div.jGrowl').find('div.jGrowl-notification').children().parent().remove();
                    }
                })");
        }

        //在UpdataPanel Response後執行Javascript
        protected void CallJavascript(string key, string script)
        {
            ToolkitScriptManager.RegisterClientScriptBlock(this, this.GetType(), key, script, true);
        }
        #endregion

        #region Control權限管理
        protected void ManageControlAuth(object sender, EventArgs e)
        {
            ManageControlCopperate(sender);
        }
        #endregion
    }    
}