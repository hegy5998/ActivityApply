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
using Newtonsoft.Json;
using Model.Common;
using BusinessLayer.S01;
using System.Text;

namespace Web.S02
{
    public partial class S02010103 : CommonPages.BasePage
    {
        private static int act_idn;
        protected void Page_Load(object sender, EventArgs e)
        {
            //取得活動ID
            act_idn = Int32.Parse(Request["act_idn"].ToString());
        }


        #region Control權限管理
        protected void ManageControlAuth(object sender, EventArgs e)
        {
            ManageControlAuth(sender);
        }
        #endregion

        #region 抓取活動資料、場次資料、區塊資料、問題資料、分類資料
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
        [System.Web.Services.WebMethod]
        public static string getClassList()
        {
            S020102BL _bl = new S020102BL();
            List<Activity_classInfo> ClassList = _bl.GetClassList();
            string json_data = JsonConvert.SerializeObject(ClassList);
            return json_data;
        }
        #endregion

        #region  儲存報名表
        [System.Web.Services.WebMethod]
        public static string save_Activity_Form(List<Activity_columnInfo> activity_Form, List<Activity_sectionInfo> activity_Section,string del_acc_idn,string del_acs_idn)
        {
            int as_act = 0;
            as_act = act_idn;
            S020101BL _bl = new S020101BL();
            //判斷是否有刪除區塊以及問題
            string[] del_acc = del_acc_idn.Split(',');
            string[] del_acs = del_acs_idn.Split(',');
            //刪除問題
            for (int count = 0; count < del_acc.Count()-1; count++)
            {
                if(del_acc[count] != "")
                {
                    //刪除問題的所有報名資料
                    Dictionary<string, object> del_acc_dt = new Dictionary<string, object>();
                    del_acc_dt["acc_idn"] = del_acc[count];
                    _bl.Delete_Column_Data(del_acc_dt);
                    //刪除問題
                    Dictionary<string, object> del_apply_detail = new Dictionary<string, object>();
                    del_apply_detail["aad_col_id"] = del_acc[count];
                    _bl.Delete_apply_detail(del_acc[count]);
                }
            }
            //刪除區塊
            for (int count = 0; count < del_acs.Count()-1; count++)
            {
                if (del_acs[count] != "")
                {
                    //刪除區塊
                    Dictionary<string, object> del_acs_dt = new Dictionary<string, object>();
                    del_acs_dt["acs_idn"] = del_acs[count];
                    _bl.Delete_Section_Data(del_acs_dt);
                }
            }

            for (int count = 0; count < activity_Section.Count; count++)
            {
                //update舊區塊
                if(activity_Section[count].Acs_idn != 0)
                {
                    Dictionary<String, Object> old_Activity_Section = new Dictionary<string, object>();
                    old_Activity_Section["acs_idn"] = activity_Section[count].Acs_idn;
                    Dictionary<String, Object> new_Activity_Section = new Dictionary<string, object>();
                    new_Activity_Section["acs_title"] = activity_Section[count].Acs_title;
                    new_Activity_Section["acs_desc"] = activity_Section[count].Acs_desc;
                    new_Activity_Section["acs_seq"] = activity_Section[count].Acs_seq;
                    _bl.Update_Section_Data(old_Activity_Section, new_Activity_Section);
                }
                //新增新區塊
                else
                {
                    Dictionary<String, Object> save_Activity_Section = new Dictionary<string, object>();
                    save_Activity_Section["acs_act"] = as_act;
                    save_Activity_Section["acs_title"] = activity_Section[count].Acs_title;
                    save_Activity_Section["acs_desc"] = activity_Section[count].Acs_desc;
                    save_Activity_Section["acs_seq"] = activity_Section[count].Acs_seq;
                    _bl.InsertData_Activity_Section(save_Activity_Section);
                }
            }


            for (int count = 0; count < activity_Form.Count; count++)
            {
                if (activity_Form[count].Acc_idn != 0)
                {
                    //update舊問題
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
                //新增新問題
                else
                {
                    Dictionary<string, Object> save_Activity_Form = new Dictionary<string, object>();
                    save_Activity_Form["acc_asc"] = activity_Form[count].Acc_asc;
                    save_Activity_Form["acc_act"] = as_act;
                    save_Activity_Form["acc_title"] = activity_Form[count].Acc_title;
                    save_Activity_Form["acc_desc"] = activity_Form[count].Acc_desc;
                    save_Activity_Form["acc_seq"] = activity_Form[count].Acc_seq;
                    save_Activity_Form["acc_type"] = activity_Form[count].Acc_type;
                    if (activity_Form[count].Acc_option != null)
                        save_Activity_Form["acc_option"] = activity_Form[count].Acc_option;
                    save_Activity_Form["acc_validation"] = activity_Form[count].Acc_validation;
                    save_Activity_Form["acc_required"] = activity_Form[count].Acc_required;
                    CommonResult result = _bl.InsertData_Activity_Column(save_Activity_Form);
                }
            }

            return "修改活動成功，如有新增場次請至活動列表發佈";
        }
        #endregion

        #region 儲存活動頁面
        [System.Web.Services.WebMethod]
        public static string save_Activity(List<ActivityInfo> activity_List, List<Activity_sessionInfo> activity_Session_List,string del_as_idn,string if_delete_file,string if_img_file)
        {
            SystemConfigInfo sysConfig = CommonHelper.GetSysConfig();
            string shorterURL = Util.CustomHelper.URLshortener(sysConfig.SOLUTION_HTTPADDR + "activity.aspx?act_class=" + activity_List[0].Act_class + "&act_idn=" + act_idn + "&act_title=" + HttpUtility.UrlEncode(activity_List[0].Act_title), sysConfig.URL_SHORTENER_API_KEY);

            S020101BL _bl = new S020101BL();
            //將活動資訊資料Update到資料庫
            Dictionary<string, Object> old_Activity_Information = new Dictionary<string, object>();
            old_Activity_Information["act_idn"] = act_idn;
            Dictionary<string, Object> new_Activity_Information = new Dictionary<string, object>();
            new_Activity_Information["act_title"] = activity_List[0].Act_title;
            new_Activity_Information["act_desc"] = activity_List[0].Act_desc;
            new_Activity_Information["act_unit"] = activity_List[0].Act_unit;
            new_Activity_Information["act_contact_name"] = activity_List[0].Act_contact_name;
            new_Activity_Information["act_contact_phone"] = activity_List[0].Act_contact_phone;
            new_Activity_Information["act_class"] = activity_List[0].Act_class;
            new_Activity_Information["act_num_limit"] = activity_List[0].Act_num_limit;
            new_Activity_Information["act_relate_link"] = activity_List[0].Act_relate_link;
            new_Activity_Information["act_short_link"] = shorterURL;
            //new_Activity_Information["act_isopen"] = 0;
            CommonResult res = _bl.Update_Activity_Data(old_Activity_Information,new_Activity_Information);

            if(if_delete_file == "true")
            {
                //String fi = (@"C:/Users/Saki/Desktop/ActivityApply/Web/Uploads/" + act_idn + "/relateFile");
                String fi = (@"C:/inetpub/wwwroot/Uploads/" + act_idn + "/relateFile");
                DirectoryInfo delete_fi = new DirectoryInfo(fi);
                //判斷目錄是否存在，存在才刪除
                if (delete_fi.Exists)
                {
                    try
                    {
                        delete_fi.Delete(true);
                        Dictionary<string, object> old_Activity_dict = new Dictionary<string, object>();
                        old_Activity_dict["act_idn"] = act_idn;
                        Dictionary<string, object> new_Activity_dict = new Dictionary<string, object>();
                        new_Activity_dict["act_relate_file"] = "";
                        CommonResult upres = _bl.UpdateData(old_Activity_dict, new_Activity_dict);
                    }
                    catch (System.IO.IOException e)
                    {
                        //Console.WriteLine(e.Message);
                    }
                }
            }

            if (if_img_file == "true")
            {
                //String fi = (@"C:/Users/Saki/Desktop/ActivityApply/Web/Uploads/" + act_idn + "/img");
                String fi = (@"C:/inetpub/wwwroot/Uploads/" + act_idn + "/Img");
                DirectoryInfo delete_fi = new DirectoryInfo(fi);
                //判斷目錄是否存在，存在才刪除
                if (delete_fi.Exists)
                {
                    try
                    {
                        delete_fi.Delete(true);
                        Dictionary<string, object> old_Activity_dict = new Dictionary<string, object>();
                        old_Activity_dict["act_idn"] = act_idn;
                        Dictionary<string, object> new_Activity_dict = new Dictionary<string, object>();
                        new_Activity_dict["act_image"] = "";
                        CommonResult upres = _bl.UpdateData(old_Activity_dict, new_Activity_dict);
                    }
                    catch (System.IO.IOException e)
                    {
                        //Console.WriteLine(e.Message);
                    }
                }
            }

            //判斷使否有場次被刪除
            string[] del_as = del_as_idn.Split(',');
            for (int count = 0; count < del_as.Count()-1; count++)
            {
                if (del_as[count] != "")
                {
                    //刪除場次所有報名資料
                    _bl.Delete_Session_apply_Data(del_as[count]);
                    //刪除場次
                    Dictionary<string, object> del_session = new Dictionary<string, object>();
                    del_session["as_idn"] = del_as[count];
                    _bl.Delete_session(del_session);
                }
            }

            //多筆場次
            for (int count = 0; count < activity_Session_List.Count; count++)
            {
                //Update場次資料
                if (activity_Session_List[count].As_idn != 0)
                {
                    Dictionary<string, Object> old_Session_Information = new Dictionary<string, Object>();
                    old_Session_Information["as_idn"] = activity_Session_List[count].As_idn;
                    Dictionary<string, Object> new_Session_Information = new Dictionary<string, Object>();
                    new_Session_Information["as_title"] = activity_Session_List[count].As_title;
                    new_Session_Information["as_date_start"] = activity_Session_List[count].As_date_start;
                    new_Session_Information["as_date_end"] = activity_Session_List[count].As_date_end;
                    new_Session_Information["as_apply_start"] = activity_Session_List[count].As_apply_start;
                    new_Session_Information["as_apply_end"] = activity_Session_List[count].As_apply_end;
                    new_Session_Information["as_position"] = activity_Session_List[count].As_position;
                    new_Session_Information["as_num_limit"] = activity_Session_List[count].As_num_limit;
                    new_Session_Information["as_remark"] = activity_Session_List[count].As_remark;
                    _bl.Update_Session_Data(old_Session_Information, new_Session_Information);
                }
                //新增場次資料
                else
                {
                    Dictionary<string, Object> save_Session_Information = new Dictionary<string, Object>();
                    save_Session_Information["as_act"] = act_idn;
                    save_Session_Information["as_title"] = activity_Session_List[count].As_title;
                    save_Session_Information["as_date_start"] = activity_Session_List[count].As_date_start;
                    save_Session_Information["as_date_end"] = activity_Session_List[count].As_date_end;
                    save_Session_Information["as_apply_start"] = activity_Session_List[count].As_apply_start;
                    save_Session_Information["as_apply_end"] = activity_Session_List[count].As_apply_end;
                    save_Session_Information["as_position"] = activity_Session_List[count].As_position;
                    save_Session_Information["as_num_limit"] = activity_Session_List[count].As_num_limit;
                    save_Session_Information["as_remark"] = activity_Session_List[count].As_remark;
                    save_Session_Information["as_isopen"] = 0;
                    _bl.InsertData_session(save_Session_Information);
                }
            }
            return "true";
        }
        #endregion

        #region 返回首頁
        protected void Back_btn_Click(object sender, EventArgs e)
        {
            Response.Redirect("S02010101.aspx?sys_id=S01&sys_pid=S02010101");
            Response.End();
        }
        #endregion

        #region 儲存檔案
        protected void btnUpload_Click1(object sender, EventArgs e)
        {
            int as_act = 0;
            as_act = act_idn;
            imageUpload_btn_Click(sender, e);

            if (FileUpload.HasFile == false)
            {
                Response.Redirect("S02010101.aspx?sys_id=S01&sys_pid=S02010101");
                return;
            }

            deletefile();

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

            // 限制檔案大小，限制為 4MB
            int filesize = FileUpload.PostedFile.ContentLength;
            if (filesize > 4100000)
            {
                string error_msg = "《附加檔案上傳失敗》，超過上限 4MB，如須重新上傳請至活動列表編輯內上傳";
                Response.Write("<script language='javascript'>alert('" + error_msg + "');</script>");
                return;
            }

            // 檢查 Server 上該資料夾是否存在，不存在就自動建立
            //string serverDirRelate = @"C:/Users/Saki/Desktop/ActivityApply/Web/Uploads/" + as_act + "/relateFile";
            string serverDirRelate = @"C:/inetpub/wwwroot/Uploads/" + as_act + "/relateFile";
            string act_relate_file = serverDirRelate + "/" + filename;
            if (Directory.Exists(serverDirRelate) == false) Directory.CreateDirectory(serverDirRelate);

            S020102BL _bl = new S020102BL();
            

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
                Dictionary<string, object> old_Activity_dict = new Dictionary<string, object>();
                old_Activity_dict["act_idn"] = act_idn;
                Dictionary<string, object> new_Activity_dict = new Dictionary<string, object>();
                //new_Activity_dict["act_relate_file"] = @"../Uploads/" + as_act + "/relateFile" + "/" + filename;
                new_Activity_dict["act_relate_file"] = @"http:///140.134.23.127/Uploads/" + as_act + "/relateFile" + "/" + filename;
                CommonResult upres = _bl.UpdateData(old_Activity_dict, new_Activity_dict);
                Response.Redirect("S02010101.aspx?sys_id=S01&sys_pid=S02010101");
            }
            catch (Exception ex)
            {
                string msg = "附加檔案上傳失敗，請重新上傳謝謝!!";
                StringBuilder sb = new StringBuilder();
                sb.Append("<script language='javascript'>");
                sb.Append("alert('" + msg + "')");
                sb.Append("</script>");
                ClientScript.RegisterStartupScript(this.GetType(), "LoadPicScript", sb.ToString());
            }
        }
        #endregion

        #region 刪除附加檔案目錄
        private void deletefile()
        {
            int as_act = 0;
            as_act = act_idn;
            //String fi = (@"C:/Users/Saki/Desktop/ActivityApply/Web/Uploads/" + as_act + "/relateFile");
            String fi = (@"C:/inetpub/wwwroot/Uploads/" + as_act + "/relateFile");
            DirectoryInfo delete_fi = new DirectoryInfo(fi);
            //判斷目錄是否存在，存在才刪除
            if (delete_fi.Exists)
            {
                try
                {
                    delete_fi.Delete(true);
                }
                catch (System.IO.IOException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
        #endregion

        #region 儲存照片
        protected void imageUpload_btn_Click(object sender, EventArgs e)
        {
            int as_act = 0;
            as_act = act_idn;

            if (imgUpload.HasFile == false) return;

            deleteimg();

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
            //string serverDirImg = @"C:/Users/Saki/Desktop/ActivityApply/Web/Uploads/" + as_act + "/Img";
            string serverDirImg = @"C:/inetpub/wwwroot/Uploads/" + as_act + "/Img";
            string act_image = serverDirImg + "/" + filename;
            if (Directory.Exists(serverDirImg) == false) Directory.CreateDirectory(serverDirImg);

            S020102BL _bl = new S020102BL();
            

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
                Dictionary<string, object> old_Activity_dict = new Dictionary<string, object>();
                old_Activity_dict["act_idn"] = act_idn;
                Dictionary<string, object> new_Activity_dict = new Dictionary<string, object>();
                //new_Activity_dict["act_image"] = @"../Uploads/" + as_act + "/img" + "/" + filename;
                new_Activity_dict["act_image"] = @"http:///140.134.23.127/Uploads/" + as_act + "/img" + "/" + filename;
                CommonResult upres = _bl.UpdateData(old_Activity_dict, new_Activity_dict);
            }
            catch (Exception ex)
            {
                string msg = "活動圖片上傳失敗，請重新上傳謝謝!!";
                StringBuilder sb = new StringBuilder();
                sb.Append("<script language='javascript'>");
                sb.Append("alert('" + msg + "')");
                sb.Append("</script>");
                ClientScript.RegisterStartupScript(this.GetType(), "LoadPicScript", sb.ToString());
            }
        }
        #endregion

        #region 刪除活動圖片目錄
        private void deleteimg()
        {
            int as_act = 0;
            as_act = act_idn; 
            //String fi = (@"C:/Users/Saki/Desktop/ActivityApply/Web/Uploads/" + as_act + "/Img");
            String fi = (@"C:/inetpub/wwwroot/Uploads/" + as_act + "/Img");
            DirectoryInfo delete_img = new DirectoryInfo(fi);
            //判斷目錄是否存在，存在才刪除
            if (delete_img.Exists)
            {
                try
                {
                    delete_img.Delete(true);
                }
                catch (System.IO.IOException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
        #endregion
    }
}