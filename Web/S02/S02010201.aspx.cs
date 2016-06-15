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
using Newtonsoft.Json;
using Model.Common;
using Web.CommonPages;
using System.Text;

namespace Web.S02
{
    public partial class S02010201 : BasePage
    {
        private static int act_idn = 0;
        //活動如果儲存成功才上傳檔案
        private static Boolean if_upload = true;
        //儲存場次List
        private static List<int> sessioncount = new List<int>();
        //儲存區塊List
        private static List<int> sectioncount = new List<int>();
        //檔案上傳路徑
        string upload_path = CommonHelper.GetSysConfig().UPLOAD_PATH;
        //檔案預覽路徑
        string upload_path_view = CommonHelper.GetSysConfig().UPLOAD_PATH_VIEW;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                act_idn = 0;
        }

        #region Control權限管理
        protected void ManageControlAuth(object sender, EventArgs e)
        {
            ManageControlAuth(sender);
        }
        #endregion

        #region 儲存報名表
        [System.Web.Services.WebMethod]
        public static string save_Activity_Form(List<Activity_columnInfo> activity_Form, List<Activity_sectionInfo> activity_Section)
        {
            if (act_idn != 0)
            {
                int as_act = 0;
                as_act = act_idn;
                S020102BL _bl = new S020102BL();
                Boolean sectionSuccess = true;
                Dictionary<String, Object> save_Activity_Section = new Dictionary<string, object>();
                for (int count = 0; count < activity_Section.Count; count++)
                {
                    save_Activity_Section["acs_act"] = as_act;
                    save_Activity_Section["acs_title"] = activity_Section[count].Acs_title;
                    save_Activity_Section["acs_desc"] = activity_Section[count].Acs_desc;
                    save_Activity_Section["acs_seq"] = activity_Section[count].Acs_seq;
                    CommonResult section_res = _bl.InsertData_Activity_Section(save_Activity_Section);
                    if (!section_res.IsSuccess)
                        sectionSuccess = false;
                    else
                        sectioncount.Add(Int32.Parse(section_res.Message));
                }
                if (sectionSuccess == true)
                {
                    for (int count = 0; count < activity_Form.Count; count++)
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
                    return "true";
                }
                else
                {
                    Dictionary<string, Object> delete_Activity_Information = new Dictionary<string, object>();
                    delete_Activity_Information["act_idn"] = act_idn;
                    _bl.DeleteActivityData(delete_Activity_Information);
                    for (int count = 0; count < sectioncount.Count; count++)
                    {
                        Dictionary<String, Object> delete_Activity_Section = new Dictionary<string, object>();
                        delete_Activity_Section["acs_idn"] = sectioncount[count];
                        _bl.DeleteSectionData(delete_Activity_Section);
                    }
                    for (int count = 0; count < sessioncount.Count; count++)
                    {
                        Dictionary<String, Object> delete_Activity_Session = new Dictionary<string, object>();
                        delete_Activity_Session["as_idn"] = sessioncount[count];
                        _bl.DeleteSessionData(delete_Activity_Session);
                    }
                    if_upload = false;
                    return "false";
                }
            }
            else
                return "false";
        }
        #endregion

        #region 儲存活動頁面
        [System.Web.Services.WebMethod]
        public static string save_Activity(List<ActivityInfo> activity_List, List<Activity_sessionInfo> activity_Session_List)
        {
            SystemConfigInfo sysConfig = CommonHelper.GetSysConfig();
            

            S020102BL _bl = new S020102BL();
            //將活動資訊資料insert到資料庫
            Dictionary<string, Object> save_Activity_Information = new Dictionary<string, object>();
            save_Activity_Information["act_title"] = activity_List[0].Act_title;
            save_Activity_Information["act_desc"] = activity_List[0].Act_desc;
            save_Activity_Information["act_unit"] = activity_List[0].Act_unit;
            save_Activity_Information["act_contact_name"] = activity_List[0].Act_contact_name;
            save_Activity_Information["act_contact_phone"] = activity_List[0].Act_contact_phone;
            save_Activity_Information["act_class"] = activity_List[0].Act_class;
            save_Activity_Information["act_as"] = activity_List[0].Act_as;
            save_Activity_Information["act_relate_link"] = activity_List[0].Act_relate_link;
            save_Activity_Information["act_num_limit"] = activity_List[0].Act_num_limit;
            save_Activity_Information["act_relate_file"] = "";
            save_Activity_Information["act_image"] = sysConfig.PRESET_PICTURE;

            save_Activity_Information["act_isopen"] = 0;
            CommonResult res = _bl.InsertData(save_Activity_Information);

            if (res.IsSuccess)
            {
                
                //將活動場次資料insert到資料庫
                Dictionary<string, Object> save_Session_Information = new Dictionary<string, Object>();
                save_Session_Information["as_act"] = act_idn = Int32.Parse(res.Message);
                Boolean sessionSuccess = true;

                string shorterURL = Util.CustomHelper.URLshortener(sysConfig.SOLUTION_HTTPADDR + "activity.aspx?act_class=" + activity_List[0].Act_class + "&act_idn=" + act_idn + "&act_title=" + HttpUtility.UrlEncode(activity_List[0].Act_title), sysConfig.URL_SHORTENER_API_KEY);
                Dictionary<string, Object> old_shorlink = new Dictionary<string, Object>();
                old_shorlink["act_idn"] = act_idn;
                Dictionary<string, Object> new_shorlink = new Dictionary<string, Object>();
                new_shorlink["act_short_link"] = shorterURL;
                _bl.UpdateData(old_shorlink, new_shorlink);

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
                    save_Session_Information["as_relate_link"] = activity_Session_List[count].As_relate_link;
                    save_Session_Information["as_remark"] = activity_Session_List[count].As_remark;
                    save_Session_Information["as_isopen"] = 0;
                    CommonResult session_res = _bl.InsertData_session(save_Session_Information);
                    if (!session_res.IsSuccess)
                        sessionSuccess = false;
                    else
                    {
                        string as_shorterURL = Util.CustomHelper.URLshortener(sysConfig.SOLUTION_HTTPADDR + "Sign_Up.aspx?act_class=" + activity_List[0].Act_class+ "&act_idn=" + act_idn + "&as_idn=" + Int32.Parse(session_res.Message) + "&act_title=" + HttpUtility.UrlEncode(activity_List[0].Act_title)+"&short=1", sysConfig.URL_SHORTENER_API_KEY);
                        Dictionary<string, Object> as_old_shorlink = new Dictionary<string, Object>();
                        as_old_shorlink["as_idn"] = Int32.Parse(session_res.Message);
                        Dictionary<string, Object> as_new_shorlink = new Dictionary<string, Object>();
                        as_new_shorlink["as_short_link"] = as_shorterURL;
                        _bl.Session_UpdateData(as_old_shorlink, as_new_shorlink);
                        sessioncount.Add(Int32.Parse(session_res.Message));
                    }
                        
                }
                if (sessionSuccess == false)
                {
                    Dictionary<string, Object> delete_Activity_Information = new Dictionary<string, object>();
                    delete_Activity_Information["act_idn"] = act_idn;
                    _bl.DeleteActivityData(delete_Activity_Information);
                    for (int count = 0; count < sessioncount.Count; count++)
                    {
                        Dictionary<String, Object> delete_Activity_Session = new Dictionary<string, object>();
                        delete_Activity_Session["as_idn"] = sessioncount[count];
                        _bl.DeleteSessionData(delete_Activity_Session);
                    }
                    act_idn = 0;
                    if_upload = false;
                    return "false";
                }
                else
                    return "true";
            }
            else
            {
                if_upload = false;
                return "false";
            }

        }
        #endregion

        #region 取得分類資料
        [System.Web.Services.WebMethod]
        public static string getClassList()
        {
            S020102BL _bl = new S020102BL();
            List<Activity_classInfo> ClassList = _bl.GetClassList();
            string json_data = JsonConvert.SerializeObject(ClassList);
            return json_data;
        }
        #endregion

        #region 取得個資聲明
        [System.Web.Services.WebMethod]
        public static string getStatement()
        {
            S020102BL _bl = new S020102BL();
            List<Activity_statementInfo> StatementList = _bl.getStatement();
            string json_data = JsonConvert.SerializeObject(StatementList);
            return json_data;
        }
        #endregion

        #region 返回首頁
        protected void Back_btn_Click(object sender, EventArgs e)
        {
            Response.Redirect("S02010101.aspx?sys_id=S01&sys_pid=S02010101");
            Response.End();
        }
        #endregion

        #region 上傳檔案
        protected void btnUpload_Click1(object sender, EventArgs e)
        {
            int as_act = 0;
            as_act = act_idn;
            if(if_upload == true)
            {
                imageUpload_btn_Click(sender, e);

                if (FileUpload.HasFile == false)
                {
                    act_idn = 0;
                    Response.Redirect("S02010101.aspx?sys_id=S01&sys_pid=S02010101");
                    return;
                }


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
                string serverDirRelate = upload_path+ + as_act + "/relateFile";
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
                    new_Activity_dict["act_relate_file"] = @upload_path_view + as_act + "/relateFile" + "/" + filename;
                    CommonResult upres = _bl.UpdateData(old_Activity_dict, new_Activity_dict);
                    Response.Redirect("S02010101.aspx?sys_id=S01&sys_pid=S02010101");
                }
                catch (Exception ex)
                {
                    string msg = "附加檔案上傳失敗，如需重新上傳請至修改頁面重新上傳，謝謝!!";
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<script language='javascript'>");
                    sb.Append("alert('" + msg + "')");
                    sb.Append("</script>");
                    ClientScript.RegisterStartupScript(this.GetType(), "LoadPicScript", sb.ToString());
                }
                act_idn = 0;
            }
        }
        #endregion

        #region 上傳照片
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
                string error_msg = "《圖片上傳失敗》，超過上限 2MB，如須重新上傳請至活動列表編輯內上傳";
                Response.Write("<script language='javascript'>alert('" + error_msg + "');</script>");
                return;
            }

            // 檢查 Server 上該資料夾是否存在，不存在就自動建立
            string serverDirImg = @upload_path + as_act + "/Img";
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
                new_Activity_dict["act_image"] = @upload_path_view + as_act + "/img" + "/" + filename;
                CommonResult upres = _bl.UpdateData(old_Activity_dict, new_Activity_dict);
            }
            catch (Exception ex)
            {
                string msg = "活動圖片上傳失敗，如需重新上傳請至修改頁面重新上傳，謝謝!!";
                StringBuilder sb = new StringBuilder();
                sb.Append("<script language='javascript'>");
                sb.Append("alert('" + msg + "')");
                sb.Append("</script>");
                ClientScript.RegisterStartupScript(this.GetType(), "LoadPicScript", sb.ToString());
            }
        }
        #endregion

    }
}