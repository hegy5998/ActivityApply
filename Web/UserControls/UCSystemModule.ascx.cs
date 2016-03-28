using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer.UserControls;
using Model;
using Util;

namespace Web.UserControls
{
    public partial class UCSystemModule : System.Web.UI.UserControl
    {
        public delegate void ChangeEventHandler();
        public event ChangeEventHandler SelectedIndexChanged;

        UCSystemModuleBL _bl = new UCSystemModuleBL();

        /// <summary>
        /// 系統代碼
        /// </summary>
        public string Sys_id { get { return system_ddl.SelectedValue; } }

        /// <summary>
        /// 系統名稱
        /// </summary>
        public string Sys_name { get { return system_ddl.SelectedItem.Text; } }

        /// <summary>
        /// 是否提供系統的[全部]選項
        /// </summary>
        private bool _enableShowAllSystem = false;
        public bool EnableShowAllSystem { get { return _enableShowAllSystem; } set { _enableShowAllSystem = value; } }

        /// <summary>
        /// 模組代碼
        /// </summary>
        public string Sys_mid { get { return module_ddl.SelectedValue; } }

        /// <summary>
        /// 模組名稱
        /// </summary>
        public string Sys_mname { get { return module_ddl.SelectedItem.Text; } }

        /// <summary>
        /// 是否提供模組的[全部]選項
        /// </summary>
        private bool _enableShowAllModule = false;
        public bool EnableShowAllModule { get { return _enableShowAllModule; } set { _enableShowAllModule = value; } }

        /// <summary>
        /// 是否顯示模組選單
        /// </summary>
        public bool EnableModule
        {
            set
            {
                module_pl.Visible = value;
            }
            get
            {
                return module_pl.Visible;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (system_ddl.Items.Count == 0
                || !IsPostBack)
                BindSystemDDL(true);
        }

        /// <summary>
        /// Bind 系統選單
        /// </summary>
        public void BindSystemDDL(bool isRefresh = false)
        {
            List<Sys_systemInfo> lst = _bl.GetSystemList(EnableModule);

            if (_enableShowAllSystem)
            {
                if (lst.Count > 0 && lst[0].Sys_id.Equals("") == false)
                {
                    Sys_systemInfo info = new Sys_systemInfo();
                    info.Sys_id = "";
                    info.Sys_name = "全部";
                    lst.Insert(0, info);
                }
            }

            foreach (var item in lst)
            {
                system_ddl.Items.Add(new ListItem(item.Sys_id + " " + item.Sys_name, item.Sys_id));
            }

            if (lst.Count > 0 && system_ddl.SelectedValue.Length > 0)
                BindModuleDDL(lst[0].Sys_id);
            else
                module_ddl.Items.Clear();
        }

        /// <summary>
        /// Bind 模組選單
        /// </summary>
        /// <param name="sys_id">系統代碼</param>
        private void BindModuleDDL(string sys_id, bool isRefresh = false)
        {
            List<Sys_moduleInfo> lst = _bl.GetModuleList(sys_id);

            if (_enableShowAllModule)
            {
                if ((lst.Count > 0 && lst[0].Sys_mid.Equals("") == false))
                {
                    Sys_moduleInfo info = new Sys_moduleInfo();
                    info.Sys_mid = "";
                    info.Sys_mname = "全部";
                    lst.Insert(0, info);
                }
            }

            module_ddl.DataSource = lst;
            module_ddl.DataTextField = "sys_mname";
            module_ddl.DataValueField = "sys_mid";
            module_ddl.DataBind();
        }

        /// <summary>
        /// 系統選單連動模組選單
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void system_ddl_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindModuleDDL(system_ddl.SelectedValue);
            SelectedIndexChanged();
        }

        /// <summary>
        /// 模組選單連動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void module_ddl_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedIndexChanged();
        }
    }
}