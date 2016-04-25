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

        DataTable data;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int i = Convert.ToInt32(Request.QueryString["i"]);

                main_gv.AllowPaging = false;
                BindGridView(GetData(i));

                //Response.Write(i.ToString());

                //test.Add("a");
                //test.Add("b");
                //test.Add("c");

                //aab.Text = "<div>"+ test[0] + "</div>";
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            //if (IsPostBack)
            //{
            //    int i = Convert.ToInt32(Request.QueryString["i"]);

            //    DataTable col = _bl.GetApplyDataDetail(i);
            //    GridViewRow gvr = main_gv.FooterRow;
            //    int check = 2;
            //    TextBox test = new TextBox();

            //    foreach (DataRow r in col.Rows)
            //    {
            //        TextBox textbox = new TextBox();
            //        textbox.ID = r.ItemArray.GetValue(2).ToString();
            //        gvr.Cells[check].Controls.Add(textbox);
            //        check++;
            //    }
            //}
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
            for (int i = 1; i < main_gv.Columns.Count; i++)
            {
                main_gv.Columns.RemoveAt(i);
            }

            //TemplateField oTemplateField = null;

            //foreach (DataColumn c in lst.Columns)
            //{
            //    oTemplateField = new TemplateField();
            //    oTemplateField.HeaderTemplate = new GridViewTemplate(DataControlRowType.Header, c.ColumnName);
            //    oTemplateField.ItemTemplate = new GridViewTemplate(DataControlRowType.DataRow, c.ColumnName);
            //    oTemplateField.ShowHeader = true;
            //    main_gv.Columns.Add(oTemplateField);
            //}

            main_gv.AutoGenerateColumns = true;
            main_gv.DataSource = lst;
            main_gv.DataBind();
        }

        protected void main_gv_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int i = Convert.ToInt32(Request.QueryString["i"]);

            GridViewHelper.ChgGridViewMode(GridViewHelper.GVMode.Edit, main_gv, e.NewEditIndex);
            BindGridView(GetData(i));
        }

        protected void main_gv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            int i = Convert.ToInt32(Request.QueryString["i"]);

            GridViewHelper.ChgGridViewMode(GridViewHelper.GVMode.Normal, main_gv);
            BindGridView(GetData(i));
        }

        protected void main_gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.DataRow:
                    if (e.Row.RowState.ToString().Contains("Edit") == false)
                    {
                        // 一般資料列
                        //string cop_id = (e.Row.FindControl("cop_id_lbl") as Label).Text;
                        //string cop_authority = (e.Row.FindControl("cop_authority_lbl") as Label).Text;
                        //(e.Row.FindControl("delete_btn") as Button).OnClientClick = "if (!confirm(\"帳號：" + cop_id + "\\n權限：" + cop_authority + "\\n\\n確定要刪除嗎?\")) return false";
                    }
                    break;

                case DataControlRowType.Footer :
                    int i = Convert.ToInt32(Request.QueryString["i"]);

                    DataTable col = _bl.GetApplyDataDetail(i);
                    GridViewRow gvr = e.Row;
                    int check = 2;

                    foreach (DataRow r in col.Rows)
                    {
                        TextBox textbox = new TextBox();
                        textbox.ID = r.ItemArray.GetValue(2).ToString();
                        gvr.Cells[check].Controls.Add(textbox);
                        check++;
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

                    GridViewHelper.ChgGridViewMode(GridViewHelper.GVMode.Insert, main_gv);
                    BindGridView(GetData(i));
                    break;

                //新增資料
                case "AddSave":
                    AddSave(sender, e);
                    break;
            }
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

            var dict = new Dictionary<string, object>();
            dict["aa_act"] = _bl.Getactidn(i);

            textbox = gvr.Cells[2].Controls[0] as TextBox;
            //textbox = gvr.FindControl(colData.Rows[0][2].ToString()) as TextBox;
            //textbox = gv.FindControl(colData.Rows[0][2].ToString()) as TextBox;
            dict["aa_name"] = textbox.Text;

            //textbox = gvr.Cells[3].Controls[0] as TextBox;
            textbox = gvr.FindControl(colData.Rows[1][2].ToString()) as TextBox;
            dict["aa_email"] = textbox.Text;

            dict["aa_as"] = Convert.ToInt32(Request.QueryString["i"]);

            int j = _bl.InsertData_apply(dict);

            var dict_col = new Dictionary<string, object>();
            dict_col["aad_apply_id"] = j;
            var res = new CommonResult(false);
            foreach (DataRow r in colData.Rows)
            {
                dict_col["aad_col_id"] = r.ItemArray.GetValue(0).ToString();

                textbox = gvr.Cells[check].Controls[0] as TextBox;
                dict_col["aad_val"] = textbox.Text;
                check++;

                res = _bl.InsertData_column(dict_col);
            }

            if (res.IsSuccess)
            {
                // 新增成功，切換回一般模式
                GridViewHelper.ChgGridViewMode(GridViewHelper.GVMode.Normal, main_gv);
                BindGridView(GetData(i));
                ShowPopupMessage(ITCEnum.PopupMessageType.Success, ITCEnum.DataActionType.Insert);
            }
            else
            {
                // 新增失敗，顯示錯誤訊息
                ShowPopupMessage(ITCEnum.PopupMessageType.Error, ITCEnum.DataActionType.Insert, res.Message);
            }
        }

        //刪除資料
        protected void main_gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int i = Convert.ToInt32(Request.QueryString["i"]);

            GridViewRow gvr = (sender as GridView).Rows[e.RowIndex];
            //欲刪除報名資料的場次序號
            var data_dict = new Dictionary<string, object>();
            data_dict["as_idn"] = i;
            //欲刪除報名資料的報名序號
            var data_dict_aa = new Dictionary<string, object>();
            data_dict_aa["aa_idn"] = CommonConvert.GetStringOrEmptyString(gvr.Cells[1].Text);

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

            DataTable col_id = _bl.GetApplyDeleteData(i, Convert.ToInt32((gvr.FindControl("old_aa_idn_hf") as HiddenField).Value));
            
            var newData_dict = new Dictionary<string, object>();
            newData_dict["aad_apply_id"] = (gvr.FindControl("old_aa_idn_hf") as HiddenField).Value;

            var oldData_dict = new Dictionary<string, object>();
            oldData_dict["aad_apply_id"] = (gvr.FindControl("old_aa_idn_hf") as HiddenField).Value;
            foreach (DataRow r in col_id.Rows)
            {
                newData_dict["aad_col_id"] = r.ItemArray.GetValue(0).ToString();
                textbox = gvr.Cells[check].Controls[0] as TextBox;
                newData_dict["aad_val"] = textbox.Text;

                oldData_dict["aad_col_id"] = r.ItemArray.GetValue(0).ToString();
                
                res = _bl.UpdateApplyData(oldData_dict, newData_dict);

                check++;
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

        //建立Row
        protected void main_gv_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Cells.Count > 3)
            {
                e.Row.Cells[1].Visible = false;
            }
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

        //動態新增Template
        public class GridViewTemplate : ITemplate
        {
            private DataControlRowType templateType;
            private String columnName;

            //建構函式
            public GridViewTemplate(DataControlRowType InType, String InColName)
            {
                this.templateType = InType;
                columnName = InColName;
            }

            #region Template成員
            //設定Template
            public void InstantiateIn(Control container)
            {
                //若為標頭資料列
                switch (templateType)
                {
                    case DataControlRowType.Header:
                        Literal oLiteral = new Literal();
                        oLiteral.Text = string.Format("{0}", columnName);
                        container.Controls.Add(oLiteral);
                        break;

                    case DataControlRowType.DataRow:
                        Label oLabel = new Label();
                        oLabel.DataBinding += new EventHandler(oLabel_DataBinding);
                        container.Controls.Add(oLabel);

                        //container.Controls.Add(new LiteralControl("<EditItemTemplate>"));
                        //TextBox oTextbox = new TextBox();
                        //oTextbox.DataBinding += new EventHandler(oTextBox_DataBinding);
                        //container.Controls.Add(oTextbox);
                        //container.Controls.Add(new LiteralControl("</EditItemTemplate>"));

                        container.Controls.Add(new LiteralControl("<ItemStyle CssClass='rowTrigger center' />"));
                        break;

                    case DataControlRowType.Footer:
                        //container.Controls.Add(new LiteralControl("<FooterTemplate>"));
                        TextBox oTextboxF = new TextBox();
                        oTextboxF.DataBinding += new EventHandler(oTextBox_DataBinding);
                        container.Controls.Add(oTextboxF);
                        //container.Controls.Add(new LiteralControl("</FooterTemplate>"));
                        break;

                    case DataControlRowType.EmptyDataRow:
                        break;
                }
            }

            //Bind Label資料
            private void oLabel_DataBinding(object sender, EventArgs e)
            {
                Label oLabel = sender as Label;
                GridViewRow oGridViewRow = (GridViewRow)oLabel.NamingContainer;
                oLabel.Text = DataBinder.Eval(oGridViewRow.DataItem, columnName).ToString();
            }

            //Bind TextBox 資料
            private void oTextBox_DataBinding(object sender, EventArgs e)
            {
                TextBox oTextbox = sender as TextBox;
                GridViewRow oGridViewRow = (GridViewRow)oTextbox.NamingContainer;
                oTextbox.Text = DataBinder.Eval(oGridViewRow.DataItem, columnName).ToString();
            }
            #endregion
        }
    }
}