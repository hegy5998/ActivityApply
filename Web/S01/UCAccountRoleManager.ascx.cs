using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Util;
using Web.App_Code;
using Web.UserControls;

namespace Web.S01
{
    public partial class UCAccountRoleManager : System.Web.UI.UserControl
    {
        private BusinessLayer.S01.UCAccountRoleManagerBL _bl = new BusinessLayer.S01.UCAccountRoleManagerBL();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        #region 綁定資料
        /// <summary>
        /// 綁定資料
        /// <para>※ 如果是新增資料，則不需傳入act</para>
        /// </summary>
        /// <param name="act">帳號資料</param>
        public void BindData(Model.S01.UCAccountManagerInfo.Main act = null)
        {
            var lst = new List<Model.S01.UCAccountRoleManagerInfo.Main>();
            if (act == null)
            {
                // 新增帳號時，先預設給一筆身分資料
                lst.Add(new Model.S01.UCAccountRoleManagerInfo.Main());
            }
            else
            { 
                // 編輯模式時，從資料庫取得身分資料
                lst = act.Role_lst;
            }
            BindListView(lst);
        }
        #endregion

        #region ListView事件
        private void BindListView(List<Model.S01.UCAccountRoleManagerInfo.Main> lst)
        {
            main_lv.DataSource = lst;
            main_lv.DataBind();
        }
        public List<Model.S01.UCAccountRoleManagerInfo.Main> GetData()
        {
            // 從ListView取得目前的身分配置資料
            var lst = new List<Model.S01.UCAccountRoleManagerInfo.Main>();
            foreach (var item in main_lv.Items)
            {
                var i = new Model.S01.UCAccountRoleManagerInfo.Main()
                {
                    Sys_rid = (item.FindControl("ucRoleDDL") as UCRoleDDL).SelectedValue,
                    Sys_uid = (item.FindControl("ucRoleUnitDDL") as UCRoleUnitDDL).SelectedValue
                };

                i.RolePosition_lst = new List<string>();
                var rpid_cbl = item.FindControl("rpid_cbl") as CheckBoxList;
                foreach (ListItem li in rpid_cbl.Items)
                {
                    if (li.Selected)
                        i.RolePosition_lst.Add(li.Value);
                }

                lst.Add(i);
            }

            return lst;
        }
        protected void main_lv_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            var lst = GetData();
            lst.RemoveAt(e.ItemIndex);
            BindListView(lst);
        }
        protected void main_lv_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                var lst = GetData();
                var newItem = new Model.S01.UCAccountRoleManagerInfo.Main(){
                    Sys_rid = lst[e.Item.DataItemIndex].Sys_rid,
                    Sys_uid = lst[e.Item.DataItemIndex].Sys_uid
                };
                lst.Insert(e.Item.DataItemIndex + 1, newItem);
                BindListView(lst);
            }
        }
        protected void main_lv_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            var item = e.Item.DataItem as Model.S01.UCAccountRoleManagerInfo.Main;
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                #region 綁定角色
                var ucRoleDDL = e.Item.FindControl("ucRoleDDL") as UCRoleDDL;
                ucRoleDDL.ItemTextHideCode = true;
                ucRoleDDL.BindData();
                // 若無代碼，則表示是新增的資料，不須指定角色
                if (item.Sys_rid.IsNullOrWhiteSpace())
                    ucRoleDDL_SelectedIndexChanged(ucRoleDDL, new EventArgs());
                else
                {
                    ucRoleDDL.SelectedValue = item.Sys_rid;

                    var ucRoleUnitDDL = e.Item.FindControl("ucRoleUnitDDL") as UCRoleUnitDDL;
                    ucRoleUnitDDL.BindData(item.Sys_rid);
                    ucRoleUnitDDL.SelectedValue = item.Sys_uid;

                    // 職位資料綁定
                    var rpid_cbl = e.Item.FindControl("rpid_cbl") as CheckBoxList;
                    BindPositionCBL(ucRoleDDL.SelectedValue, ucRoleUnitDDL.SelectedValue, rpid_cbl);

                    foreach (var p in item.RolePosition_lst)
                    {
                        if (p.IsNullOrWhiteSpace() == false)
                        {
                            rpid_cbl.Items.FindByValue(p).Selected = true;
                        }
                    }
                }
                #endregion
            }
        }

        protected void main_lv_PreRender(object sender, EventArgs e)
        {
            var lv = sender as ListView;

            #region 如果項目只有一項時，隱藏刪除按鈕
            if (lv.Items.Count == 1)
            {
                var item = lv.Items[0];
                (item.FindControl("delete_btn") as Button).Visible = false;
                (item.FindControl("idx_lbl") as Label).Visible = false;
            }
            #endregion

            #region 將只有單一單位，或單一職位的欄位隱藏
            foreach (var item in lv.Items)
            {
                (item.FindControl("unit_pl") as Panel).Visible = ((item.FindControl("ucRoleUnitDDL") as UCRoleUnitDDL).Items.Count != 1);
                (item.FindControl("position_pl") as Panel).Visible = ((item.FindControl("rpid_cbl") as CheckBoxList).Items.Count != 1);
            }
            #endregion

            #region script設定
            // 設定最後一列不用有下邊線
            string script = @"
                $('#UCAccountRoleManager .lvRow').last().css('border-bottom', '');";

            // 只有一列時不用滑過變色
            if (main_lv.Items.Count == 1)
                script += "$('#UCAccountRoleManager .lvRow').removeClass('lvRowHover');";

            // 職位選項變色
            script += @"
                $('input[type=checkbox][id*=rpid_cbl]').change(function(){
                    if ($(this).is(':checked'))
                        $(this).siblings('label').addClass('highlight');
                    else
                        $(this).siblings('label').removeClass('highlight');
                }).change();
                ";
            WebHelper.CallJavascript("UCAccountRoleManager", script);
            #endregion
        }

        #region 角色選單連動
        protected void ucRoleDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ucRoleDDL = sender as UCRoleDDL;
            var item = ucRoleDDL.NamingContainer as ListViewDataItem;
            var ucRoleUnitDDL = item.FindControl("ucRoleUnitDDL") as UCRoleUnitDDL;
            ucRoleUnitDDL.BindData(ucRoleDDL.SelectedValue);
            ucRoleUnitDDL_SelectedIndexChanged(ucRoleUnitDDL, new EventArgs());
        }
        #endregion

        #region 單位選單連動
        protected void ucRoleUnitDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ucRoleUnitDDL = sender as UCRoleUnitDDL;
            var item = ucRoleUnitDDL.NamingContainer as ListViewDataItem;
            var ucRoleDDL = item.FindControl("ucRoleDDL") as UCRoleDDL;

            // 職位資料綁定
            BindPositionCBL(ucRoleDDL.SelectedValue, ucRoleUnitDDL.SelectedValue, item.FindControl("rpid_cbl") as CheckBoxList);
        }
        #endregion

        #region 職位選單綁定
        private void BindPositionCBL(string sys_rid, string sys_uid, CheckBoxList cbl)
        {
            var rpidDataSource = _bl.GetRpidList(sys_rid, sys_uid);
            cbl.DataSource = rpidDataSource;
            cbl.DataTextField = "sys_rpname";
            cbl.DataValueField = "sys_rpid";
            cbl.DataBind();
            if (cbl.Items.Count == 1)
            {
                cbl.Items[0].Selected = true;
                cbl.Enabled = false;
            }
            else
            {
                cbl.Enabled = true;
            }
        }
        #endregion
        #endregion
    }
}