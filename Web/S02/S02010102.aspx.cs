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

namespace Web.S02
{
    public partial class S02010102 : System.Web.UI.Page
    {
        S020101BL _bl = new S020101BL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int i = Convert.ToInt32(Request.QueryString["i"]);

                main_gv.AllowPaging = false;
                BindGridView(GetData(i));
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
                                Label label = new Label();
                                label.Text = state;
                                gvrEdit.Cells[checkEdit].Controls.Add(label);

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
                    new_hf.Value = "Add";

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
            GridViewRow gvr = (e.CommandSource as Button).NamingContainer as GridViewRow;

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

                if (r.ItemArray.GetValue(6).ToString() == "text")
                {
                    textbox = gvr.Cells[check].Controls[0] as TextBox;
                    dict_col["aad_val"] = textbox.Text;
                    check++;

                    res_col = _bl.InsertData_column(dict_col);
                }
                else if ((r.ItemArray.GetValue(6).ToString() == "singleSelect") || (r.ItemArray.GetValue(6).ToString() == "dropDownList"))
                {
                    DropDownList d = gvr.Cells[check].Controls[0] as DropDownList;
                    dict_col["aad_val"] = d.SelectedValue;
                    check++;

                    res_col = _bl.InsertData_column(dict_col);
                }
                else if (r.ItemArray.GetValue(6).ToString() == "multiSelect")
                {
                    Label s = gvr.Cells[check].Controls[1] as Label;
                    dict_col["aad_val"] = s.Text;
                    check++;

                    res_col = _bl.InsertData_column(dict_col);
                }
            }

            if (res_col.IsSuccess)
            {
                // 新增成功，切換回一般模式
                GridViewHelper.ChgGridViewMode(GridViewHelper.GVMode.Normal, main_gv);
                BindGridView(GetData(i));
                ShowPopupMessage(ITCEnum.PopupMessageType.Success, ITCEnum.DataActionType.Insert);
            }
            else
            {
                // 新增失敗，顯示錯誤訊息
                ShowPopupMessage(ITCEnum.PopupMessageType.Error, ITCEnum.DataActionType.Insert, res_col.Message);
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
            Label aa_idn = gvr.Cells[1].Controls[0] as Label;
            data_dict_aa["aa_idn"] = CommonConvert.GetStringOrEmptyString(aa_idn.Text);

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

            //取得欄位資料
            DataTable col_id = _bl.GetApplyDataDetail(i);
            
            //修改後的資料
            var newData_dict = new Dictionary<string, object>();
            newData_dict["aad_apply_id"] = (gvr.FindControl("old_aa_idn_hf") as HiddenField).Value;

            //修改前的資料
            var oldData_dict = new Dictionary<string, object>();
            oldData_dict["aad_apply_id"] = (gvr.FindControl("old_aa_idn_hf") as HiddenField).Value;
            
            //根據欄位資料對應修改資料
            foreach (DataRow r in col_id.Rows)
            {
                newData_dict["aad_col_id"] = r.ItemArray.GetValue(2).ToString();
                oldData_dict["aad_col_id"] = r.ItemArray.GetValue(2).ToString();

                if (r.ItemArray.GetValue(6).ToString() == "text")
                {
                    textbox = gvr.Cells[check].Controls[0] as TextBox;
                    newData_dict["aad_val"] = textbox.Text;
                
                    res = _bl.UpdateApplyData(oldData_dict, newData_dict);

                    check++;
                }
                else if ((r.ItemArray.GetValue(6).ToString() == "singleSelect") || (r.ItemArray.GetValue(6).ToString() == "dropDownList"))
                {
                    DropDownList dropdownlist = gvr.Cells[check].Controls[0] as DropDownList;
                    newData_dict["aad_val"] = dropdownlist.SelectedValue;

                    res = _bl.UpdateApplyData(oldData_dict, newData_dict);

                    check++;
                }
                else if (r.ItemArray.GetValue(6).ToString() == "multiSelect")
                {
                    string s = (gvr.Cells[check].Controls[1] as Label).Text;
                    newData_dict["aad_val"] = s;

                    res = _bl.UpdateApplyData(oldData_dict, newData_dict);

                    check++;
                }
            }

            if (res.IsSuccess)
            {
                // 更新成功，切換回一般模式
                GridViewHelper.ChgGridViewMode(GridViewHelper.GVMode.Normal, main_gv);
                BindGridView(GetData(i));
                ShowPopupMessage(ITCEnum.PopupMessageType.Success, ITCEnum.DataActionType.Update);
            }
            else
            {
                // 更新失敗，顯示錯誤訊息
                e.Cancel = true;
                ShowPopupMessage(ITCEnum.PopupMessageType.Error, ITCEnum.DataActionType.Update, res.Message);
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
                                    gvrEdit.Cells[checkEdit].Controls.Add(label);

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

        //多選跳出視窗關閉按鈕
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
            
            //判斷是否為頁尾列
            if (new_hf.Value == "Edit")
            {
                Label l = main_gv.Rows[CommonConvert.GetIntOrZero(row_idn_hf.Value)].Cells[CommonConvert.GetIntOrZero(col_idn_hf.Value)].Controls[1] as Label;
                l.Text = s.Substring(1);
                main_gv.Rows[CommonConvert.GetIntOrZero(row_idn_hf.Value)].Cells[CommonConvert.GetIntOrZero(col_idn_hf.Value)].Controls.Add(l);
            }
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
    }    
}