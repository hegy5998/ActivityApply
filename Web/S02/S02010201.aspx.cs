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
using System.Design;
using System.Web.Services;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace Web.S02
{
    public partial class S02010201 : CommonPages.BasePage
    {
        
        string a = null;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //GetData();  
            }

        }

        #region Control權限管理
        protected void ManageControlAuth(object sender, EventArgs e)
        {
            ManageControlAuth(sender);
        }
        #endregion

        [System.Web.Services.WebMethod]
        public static string save_Activity_Form(List<Activity_columnInfo> activity_Form , List<Activity_sectionInfo> activity_Section)
        {
            int as_act=0;
            #region --- 抓取活動序號 ---
            //連線到資料庫
            string connString = WebConfigurationManager.ConnectionStrings["DbConn"].ConnectionString;
            SqlConnection conn = new SqlConnection(connString);
            //判斷與資料庫的連線是否正常，正常才開啟連線
            if (conn.State != ConnectionState.Open) conn.Open();
            //搜尋activity中剛insert的資料的自動編號欄位
            using (SqlCommand cmd_data = new SqlCommand(@"SELECT  MAX(act_idn) AS act_din FROM  activity", conn))
            {
                SqlDataReader dr = cmd_data.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        //取得自動編號值
                        as_act = Int32.Parse(dr[0].ToString());
                    }
                }
                //中斷連線以及釋放資源
                dr.Close();
                cmd_data.Dispose();
                conn.Close();
                conn.Dispose();
            }
            #endregion


            S020102BL _bl = new S020102BL();
            Dictionary<String, Object> save_Activity_Section = new Dictionary<string, object>();
            for (int count = 0; count < activity_Section.Count; count++)
            {
                save_Activity_Section["acs_act"] = as_act;
                save_Activity_Section["acs_title"] = activity_Section[count].Acs_title;
                save_Activity_Section["acs_desc"] = activity_Section[count].Acs_desc;
                save_Activity_Section["acs_seq"] = activity_Section[count].Acs_seq;
                _bl.InsertData_Activity_Section(save_Activity_Section);
            }


            Dictionary<string, Object> save_Activity_Form = new Dictionary<string, object>();
            for (int count = 0; count < activity_Form.Count; count++)
            {
                save_Activity_Form["acc_asc"] = activity_Form[count].Acc_asc;
                save_Activity_Form["acc_act"] = as_act;
                save_Activity_Form["acc_title"] = activity_Form[count].Acc_title;
                save_Activity_Form["acc_desc"] = activity_Form[count].Acc_desc;
                save_Activity_Form["acc_seq"] = activity_Form[count].Acc_seq;
                save_Activity_Form["acc_type"] = activity_Form[count].Acc_type;
                if(activity_Form[count].Acc_option!=null)
                save_Activity_Form["acc_option"] = activity_Form[count].Acc_option;
                save_Activity_Form["acc_validation"] = activity_Form[count].Acc_validation;
                save_Activity_Form["acc_required"] = activity_Form[count].Acc_required;
                _bl.InsertData_Activity_Column(save_Activity_Form);
            }
            return "成功";
        }



        [System.Web.Services.WebMethod]
        public static string save_Activity(List<ActivityInfo> activity_List,List<Activity_sessionInfo> activity_Session_List)
        {
            S020102BL _bl = new S020102BL();
            //將活動資訊資料insert到資料庫
            Dictionary<string, Object> save_Activity_Information = new Dictionary<string, object>();
            save_Activity_Information["act_title"] = activity_List[0].Act_title;
            save_Activity_Information["act_desc"] = activity_List[0].Act_desc;
            save_Activity_Information["act_unit"] = activity_List[0].Act_unit;
            save_Activity_Information["act_contact_name"] = activity_List[0].Act_contact_name;
            save_Activity_Information["act_contact_phone"] = activity_List[0].Act_contact_phone;
            save_Activity_Information["act_relate_link"] = activity_List[0].Act_relate_link;
            //save_Activity_Information["act_isopen"] = 0;
            _bl.InsertData(save_Activity_Information);
            //將活動場次資料insert到資料庫
            Dictionary<string, Object> save_Session_Information = new Dictionary<string, Object>();

            #region --- 抓取活動序號 ---
            //連線到資料庫
            string connString = WebConfigurationManager.ConnectionStrings["DbConn"].ConnectionString;
            SqlConnection conn = new SqlConnection(connString);
            //判斷與資料庫的連線是否正常，正常才開啟連線
            if (conn.State != ConnectionState.Open) conn.Open();
            //搜尋activity中剛insert的資料的自動編號欄位
            using (SqlCommand cmd_data = new SqlCommand(@"SELECT  MAX(act_idn) AS act_din FROM  activity", conn))
            {
                SqlDataReader dr = cmd_data.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        //取得自動編號值
                        save_Session_Information["as_act"] = Int32.Parse(dr[0].ToString());
                    }
                }
                //中斷連線以及釋放資源
                dr.Close();
                cmd_data.Dispose();
                conn.Close();
                conn.Dispose();
            }
            #endregion
            //多筆場次insert到資料庫
            for (int count = 0; count < activity_Session_List.Count ; count++)
            {
                save_Session_Information["as_title"] = activity_Session_List[count].As_title;
                save_Session_Information["as_date_start"] = activity_Session_List[count].As_date_start;
                save_Session_Information["as_date_end"] = activity_Session_List[count].As_date_end;
                save_Session_Information["as_apply_start"] = activity_Session_List[count].As_apply_start;
                save_Session_Information["as_apply_end"] = activity_Session_List[count].As_apply_end;
                save_Session_Information["as_position"] = activity_Session_List[count].As_position;
                save_Session_Information["as_num_limit"] = activity_Session_List[count].As_num_limit;
                save_Session_Information["as_isopen"] = 0;
                _bl.InsertData_session(save_Session_Information);
            }
            return "成功";
        }

        protected void Back(object sender, EventArgs e)
        {
            
        }

        protected void Save_btn_Click(object sender, EventArgs e)
        {

            //if (string.IsNullOrWhiteSpace(activity_Name_txt.Value.Trim()))
            //{
            //    ShowPopupMessage(ITCEnum.PopupMessageType.Error, "請輸入活動標題!");
            //    return;
            //}

            
            //Dictionary<string, Object> save_Activity_Infirmation = new Dictionary<string, Object>();
            //save_Activity_Infirmation["act_title"] = activity_Name_txt.Value;
            //save_Activity_Infirmation["act_unit"] = unit_txt.Value;
            //save_Activity_Infirmation["act_contact_name"] = contact_Person_txt.Value;
            //save_Activity_Infirmation["act_contact_phone"] = contact_Person_Phone_txt.Value;
            //save_Activity_Infirmation["act_relate_link"] = relate_Link.Value;
            //save_Activity_Infirmation["act_isopen"] = 0;

            //CommonResult result = _bl.InsertData(save_Activity_Infirmation);




            //Dictionary<string, object> ValDic = new Dictionary<string, object>();
            //ValDic["act_title"] = f_act_title_tb.Text.Trim();

            //CommonResult result = _bl.InsertData(ValDic);

            //if (result.IsSuccess)
            //{
            //    //Response.Redirect("S02010201.aspx?sys_id=S01&sys_pid=S02010101");
            //    //Response.End();
            //    return;
            //}
            //else {
            //    ShowPopupMessage(ITCEnum.PopupMessageType.Error, "儲存失敗! " + (string.IsNullOrWhiteSpace(result.Message) ? "" : "原因: " + result.Message));
            //    return;
            //}
        }



        protected void Back_btn_Click(object sender, EventArgs e)
        {
            //Response.Redirect("S02010201.aspx?sys_id=S01&sys_pid=S02010201");
            //Response.End();
            //Label1.Text = CKEditorControl1.Text;
        }

        protected void q_query_btn_Click(object sender, EventArgs e)
        {
            
        }

    }
}