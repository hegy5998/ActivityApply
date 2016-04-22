﻿using System;
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
using Newtonsoft.Json;
using Model.Common;
using BusinessLayer.S01;

namespace Web.S02
{
    public partial class S02010103 : CommonPages.BasePage
    {
        private static int act_idn;
        protected void Page_Load(object sender, EventArgs e)
        {
            act_idn = Int32.Parse(Request["act_idn"].ToString());
        }


        #region Control權限管理
        protected void ManageControlAuth(object sender, EventArgs e)
        {
            ManageControlAuth(sender);
        }
        #endregion

        //public static int get_Act_idn()
        //{
        //    int as_act = 0;
        //    #region --- 抓取活動序號 ---
        //    //連線到資料庫
        //    string connString = WebConfigurationManager.ConnectionStrings["DbConn"].ConnectionString;
        //    SqlConnection conn = new SqlConnection(connString);
        //    //判斷與資料庫的連線是否正常，正常才開啟連線
        //    if (conn.State != ConnectionState.Open) conn.Open();
        //    //搜尋activity中剛insert的資料的自動編號欄位
        //    using (SqlCommand cmd_data = new SqlCommand(@"SELECT  MAX(act_idn) AS act_din FROM  activity", conn))
        //    {
        //        SqlDataReader dr = cmd_data.ExecuteReader();
        //        if (dr.HasRows)
        //        {
        //            while (dr.Read())
        //            {
        //                //取得自動編號值
        //                as_act = Int32.Parse(dr[0].ToString());
        //            }
        //        }
        //        //中斷連線以及釋放資源
        //        dr.Close();
        //        cmd_data.Dispose();
        //        conn.Close();
        //        conn.Dispose();
        //    }
        //    #endregion
        //    return as_act;
        //}


        //儲存報名表

        [System.Web.Services.WebMethod]
        public static string getActivity()
        {
            S020101BL _bl = new S020101BL();
            List<ActivityInfo> activityList = _bl.getActivity(act_idn);
            string json_data = JsonConvert.SerializeObject(activityList);
            return json_data;
        }
        [System.Web.Services.WebMethod]
        public static string getSession()
        {
            S020101BL _bl = new S020101BL();
            List<Activity_sessionInfo> sessionList = _bl.getSession(act_idn);
            string json_data = JsonConvert.SerializeObject(sessionList);
            return json_data;
        }
        [System.Web.Services.WebMethod]
        public static string getSection()
        {
            S020101BL _bl = new S020101BL();
            List<Activity_sectionInfo> sectionList = _bl.getSection(act_idn);
            string json_data = JsonConvert.SerializeObject(sectionList);
            return json_data;
        }
        [System.Web.Services.WebMethod]
        public static string getColumn()
        {
            S020101BL _bl = new S020101BL();
            List<Activity_columnInfo> columnList = _bl.getColumn(act_idn);
            string json_data = JsonConvert.SerializeObject(columnList);
            return json_data;
        }

        //儲存報名表
        [System.Web.Services.WebMethod]
        public static string save_Activity_Form(List<Activity_columnInfo> activity_Form, List<Activity_sectionInfo> activity_Section,string del_acc_idn,string del_acs_idn)
        {
            int as_act = 0;
            as_act = act_idn;
            S020103BL _bl = new S020103BL();
            string[] del_acc = del_acc_idn.Split(',');
            string[] del_acs = del_acs_idn.Split(',');
            for (int count = 0; count < del_acc.Count(); count++)
            {
                if(del_acc[count] != "")
                {
                    Dictionary<string, object> del_acc_dt = new Dictionary<string, object>();
                    del_acc_dt["acc_idn"] = del_acc[count];
                    _bl.Delete_Column_Data(del_acc_dt);
                }
            }
            for (int count = 0; count < del_acs.Count(); count++)
            {
                if (del_acs[count] != "")
                {
                    Dictionary<string, object> del_acs_dt = new Dictionary<string, object>();
                    del_acs_dt["acs_idn"] = del_acs[count];
                    _bl.Delete_Section_Data(del_acs_dt);
                }
            }
            


            
            for (int count = 0; count < activity_Section.Count; count++)
            {
                Dictionary<String, Object> old_Activity_Section = new Dictionary<string, object>();
                old_Activity_Section["acs_idn"] = activity_Section[count].Acs_idn;
                Dictionary<String, Object> new_Activity_Section = new Dictionary<string, object>();
                new_Activity_Section["acs_title"] = activity_Section[count].Acs_title;
                new_Activity_Section["acs_desc"] = activity_Section[count].Acs_desc;
                new_Activity_Section["acs_seq"] = activity_Section[count].Acs_seq;
                _bl.Update_Section_Data(old_Activity_Section, new_Activity_Section);
            }


            for (int count = 0; count < activity_Form.Count; count++)
            {
                Dictionary<string, Object> old_Activity_Form = new Dictionary<string, object>();
                old_Activity_Form["acc_idn"] = activity_Form[count].Acc_idn;
                Dictionary<string, Object> new_Activity_Form = new Dictionary<string, object>();
                new_Activity_Form["acc_asc"] = activity_Form[count].Acc_asc;
                new_Activity_Form["acc_title"] = activity_Form[count].Acc_title;
                new_Activity_Form["acc_desc"] = activity_Form[count].Acc_desc;
                new_Activity_Form["acc_seq"] = activity_Form[count].Acc_seq;
                new_Activity_Form["acc_type"] = activity_Form[count].Acc_type;
                if (activity_Form[count].Acc_option != null)
                    new_Activity_Form["acc_option"] = activity_Form[count].Acc_option;
                new_Activity_Form["acc_validation"] = activity_Form[count].Acc_validation;
                new_Activity_Form["acc_required"] = activity_Form[count].Acc_required;
                CommonResult result = _bl.Update_Column_Data(old_Activity_Form, new_Activity_Form);
            }
            return "活動儲存成功成功";
        }

        //儲存活動頁面
        [System.Web.Services.WebMethod]
        public static string save_Activity(List<ActivityInfo> activity_List, List<Activity_sessionInfo> activity_Session_List)
        {
            SystemConfigInfo sysConfig = CommonHelper.GetSysConfig();
            string shorterURL = Util.CustomHelper.URLshortener(sysConfig.SOLUTION_HTTPADDR + "activity.aspx?act_class=" + activity_List[0].Act_class + "&act_idn=" + activity_List[0].Act_idn + "&act_title=" + activity_List[0].Act_title, sysConfig.URL_SHORTENER_API_KEY);

            S020102BL _bl = new S020102BL();
            //將活動資訊資料insert到資料庫
            Dictionary<string, Object> save_Activity_Information = new Dictionary<string, object>();
            save_Activity_Information["act_title"] = activity_List[0].Act_title;
            save_Activity_Information["act_desc"] = activity_List[0].Act_desc;
            save_Activity_Information["act_unit"] = activity_List[0].Act_unit;
            save_Activity_Information["act_contact_name"] = activity_List[0].Act_contact_name;
            save_Activity_Information["act_contact_phone"] = activity_List[0].Act_contact_phone;
            save_Activity_Information["act_class"] = activity_List[0].Act_class;
            save_Activity_Information["act_relate_link"] = activity_List[0].Act_relate_link;
            //save_Activity_Information["act_relate_file"] = act_relate_file;
            save_Activity_Information["act_short_link"] = shorterURL;
            //save_Activity_Information["act_image"] = act_image;
            save_Activity_Information["act_isopen"] = 0;
            CommonResult res = _bl.InsertData(save_Activity_Information);

            //將活動場次資料insert到資料庫
            Dictionary<string, Object> save_Session_Information = new Dictionary<string, Object>();

            save_Session_Information["as_act"] = act_idn = Int32.Parse(res.Message);

            //多筆場次insert到資料庫
            for (int count = 0; count < activity_Session_List.Count; count++)
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

        [System.Web.Services.WebMethod]
        public static string getClassList()
        {
            S020102BL _bl = new S020102BL();
            List<Activity_classInfo> ClassList = _bl.GetClassList();
            string json_data = JsonConvert.SerializeObject(ClassList);
            return json_data;
        }

        //返回首頁
        protected void Back_btn_Click(object sender, EventArgs e)
        {
            Response.Redirect("S02010101.aspx?sys_id=S01&sys_pid=S0201010");
            Response.End();
        }

        //儲存檔案
        protected void btnUpload_Click1(object sender, EventArgs e)
        {
            int as_act = 0;
            as_act = act_idn;
            imageUpload_btn_Click(sender, e);

            if (FileUpload.HasFile == false) return;



            // FU1.FileName 只有 "檔案名稱.附檔名"，並沒有 Client 端的完整理路徑
            string filename = FileUpload.FileName;

            string extension = Path.GetExtension(filename).ToLowerInvariant();
            // 判斷是否為允許上傳的檔案附檔名
            List<string> allowedExtextsion = new List<string> { ".jpg", ".png", ".jpeg", ".gif", ".doc", ".docx", ".txt", ".ppt", ".pptx", ".xls", ".xlsx", ".pdf", ".rar", ".zip", ".7z" };
            if (allowedExtextsion.IndexOf(extension) == -1)
            {
                string error_msg = "附加檔案不允許該類型檔案上傳!";
                Response.Write("<script language='javascript'>alert('" + error_msg + "');</script>");
                return;
            }

            // 限制檔案大小，限制為 2MB
            int filesize = FileUpload.PostedFile.ContentLength;
            if (filesize > 4100000)
            {
                string error_msg = "《附加檔案上傳失敗》，超過上限 4MB，如須重新上傳請至活動列表編輯內上傳";
                Response.Write("<script language='javascript'>alert('" + error_msg + "');</script>");
                return;
            }

            // 檢查 Server 上該資料夾是否存在，不存在就自動建立
            //string serverDir = @"C:/Users/Saki/Desktop/ActivityApply/Web/Uploads/"+as_act;
            //if (Directory.Exists(serverDir) == false) Directory.CreateDirectory(serverDir);
            string serverDirRelate = @"C:/Users/Saki/Desktop/ActivityApply/Web/Uploads/" + as_act + "/relateFile";
            string act_relate_file = serverDirRelate + "/" + filename;
            if (Directory.Exists(serverDirRelate) == false) Directory.CreateDirectory(serverDirRelate);

            S020102BL _bl = new S020102BL();
            Dictionary<string, object> old_Activity_dict = new Dictionary<string, object>();
            old_Activity_dict["act_idn"] = act_idn;
            Dictionary<string, object> new_Activity_dict = new Dictionary<string, object>();
            new_Activity_dict["act_relate_file"] = act_relate_file;
            CommonResult upres = _bl.UpdateData(old_Activity_dict, new_Activity_dict);

            // 判斷 Server 上檔案名稱是否有重覆情況，有的話必須進行更名
            // 使用 Path.Combine 來集合路徑的優點
            //  以前發生過儲存 Table 內的是 \\ServerName\Dir（最後面沒有 \ 符號），
            //  直接跟 FileName 來進行結合，會變成 \\ServerName\DirFileName 的情況，
            //  資料夾路徑的最後面有沒有 \ 符號變成還需要判斷，但用 Path.Combine 來結合的話，
            //  資料夾路徑沒有 \ 符號，會自動補上，有的話，就直接結合
            string serverFilePath = Path.Combine(serverDirRelate, filename);
            string fileNameOnly = Path.GetFileNameWithoutExtension(filename);
            int fileCount = 1;
            while (File.Exists(serverFilePath))
            {
                // 重覆檔案的命名規則為 檔名_1、檔名_2 以此類推
                filename = string.Concat(fileNameOnly, "_", fileCount, extension);
                serverFilePath = Path.Combine(serverDirRelate, filename);
                fileCount++;
            }

            // 把檔案傳入指定的 Server 內路徑
            try
            {
                FileUpload.SaveAs(serverFilePath);
                //Label1.Text = "檔案上傳成功";
            }
            catch (Exception ex)
            {
                //Label1.Text = ex.Message;
            }
        }



        //儲存照片
        protected void imageUpload_btn_Click(object sender, EventArgs e)
        {
            int as_act = 0;
            as_act = act_idn;

            if (imgUpload.HasFile == false) return;



            // FU1.FileName 只有 "檔案名稱.附檔名"，並沒有 Client 端的完整理路徑
            string filename = imgUpload.FileName;

            string extension = Path.GetExtension(filename).ToLowerInvariant();
            // 判斷是否為允許上傳的檔案附檔名
            List<string> allowedExtextsion = new List<string> { ".jpg", ".png", ".jpeg" };
            if (allowedExtextsion.IndexOf(extension) == -1)
            {
                string error_msg = "活動圖片不允許該類型檔案上傳!";
                Response.Write("<script language='javascript'>alert('" + error_msg + "');</script>");
                return;
            }

            // 限制檔案大小，限制為 2MB
            int filesize = imgUpload.PostedFile.ContentLength;
            if (filesize > 2100000)
            {
                //Label1.Text = "檔案大小上限為 2MB，該檔案無法上傳";
                string error_msg = "《圖片上傳失敗》，超過上限 2MB，如須重新上傳請至活動列表編輯內上傳";
                Response.Write("<script language='javascript'>alert('" + error_msg + "');</script>");
                return;
            }

            // 檢查 Server 上該資料夾是否存在，不存在就自動建立
            //string serverDir = @"C:/Users/Saki/Desktop/ActivityApply/Web/Uploads/" + as_act;
            //if (Directory.Exists(serverDir) == false) Directory.CreateDirectory(serverDir);
            string serverDirImg = @"C:/Users/Saki/Desktop/ActivityApply/Web/Uploads/" + as_act + "/Img";
            string act_image = serverDirImg + "/" + filename;
            if (Directory.Exists(serverDirImg) == false) Directory.CreateDirectory(serverDirImg);

            S020102BL _bl = new S020102BL();
            Dictionary<string, object> old_Activity_dict = new Dictionary<string, object>();
            old_Activity_dict["act_idn"] = act_idn;
            Dictionary<string, object> new_Activity_dict = new Dictionary<string, object>();
            new_Activity_dict["act_image"] = act_image;
            CommonResult upres = _bl.UpdateData(old_Activity_dict, new_Activity_dict);

            // 判斷 Server 上檔案名稱是否有重覆情況，有的話必須進行更名
            // 使用 Path.Combine 來集合路徑的優點
            //  以前發生過儲存 Table 內的是 \\ServerName\Dir（最後面沒有 \ 符號），
            //  直接跟 FileName 來進行結合，會變成 \\ServerName\DirFileName 的情況，
            //  資料夾路徑的最後面有沒有 \ 符號變成還需要判斷，但用 Path.Combine 來結合的話，
            //  資料夾路徑沒有 \ 符號，會自動補上，有的話，就直接結合
            string serverFilePath = Path.Combine(serverDirImg, filename);
            string fileNameOnly = Path.GetFileNameWithoutExtension(filename);
            int fileCount = 1;
            while (File.Exists(serverFilePath))
            {
                // 重覆檔案的命名規則為 檔名_1、檔名_2 以此類推
                filename = string.Concat(fileNameOnly, "_", fileCount, extension);
                serverFilePath = Path.Combine(serverDirImg, filename);
                fileCount++;
            }

            // 把檔案傳入指定的 Server 內路徑
            try
            {
                imgUpload.SaveAs(serverFilePath);
                //Label1.Text = "檔案上傳成功";
            }
            catch (Exception ex)
            {
                //Label1.Text = ex.Message;
            }
        }
    }
}