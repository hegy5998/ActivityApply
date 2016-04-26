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

namespace Web.S02
{
    public partial class S02010201 : BasePage
    {
        //private static string act_relate_file = "";
        //private static string act_image  = "";
        private static int act_idn = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            //act_relate_file = "";
            //act_image = "";
        }


        #region Control權限管理
        protected void ManageControlAuth(object sender, EventArgs e)
        {
            ManageControlAuth(sender);
        }
        #endregion

        #region 儲存報名表
        [System.Web.Services.WebMethod]
        public static string save_Activity_Form(List<Activity_columnInfo> activity_Form , List<Activity_sectionInfo> activity_Section)
        {
            if (act_idn != 0)
            {
                int as_act = 0;
                as_act = act_idn;
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
                return "活動儲存成功成功";
            }
            else
                return "儲存失敗";
        }
        #endregion

        #region 儲存活動頁面
        [System.Web.Services.WebMethod]
        public static string save_Activity(List<ActivityInfo> activity_List,List<Activity_sessionInfo> activity_Session_List)
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
            save_Activity_Information["act_short_link"] = shorterURL;
            save_Activity_Information["act_isopen"] = 0;
            CommonResult res = _bl.InsertData(save_Activity_Information);       

            //將活動場次資料insert到資料庫
            Dictionary<string, Object> save_Session_Information = new Dictionary<string, Object>();

            save_Session_Information["as_act"] = act_idn = Int32.Parse(res.Message);

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

        #region 返回首頁
        protected void Back_btn_Click(object sender, EventArgs e)
        {
            Response.Redirect("S02010101.aspx?sys_id=S01&sys_pid=S0201010");
            Response.End();
        }
        #endregion

        #region 上傳檔案
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
            string serverDirRelate =  @"C:/Users/Saki/Desktop/ActivityApply/Web/Uploads/" + as_act+"/relateFile";
            string act_relate_file = serverDirRelate + "/" + filename;
            if (Directory.Exists(serverDirRelate) == false) Directory.CreateDirectory(serverDirRelate);

            S020102BL _bl = new S020102BL();
            Dictionary<string, object> old_Activity_dict = new Dictionary<string, object>();
            old_Activity_dict["act_idn"] = act_idn;
            Dictionary<string, object> new_Activity_dict = new Dictionary<string, object>();
            new_Activity_dict["act_relate_file"] = @"../Uploads/" + as_act + "/relateFile" + "/" + filename;
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
            }
            catch (Exception ex)
            {
                //Label1.Text = "檔案上傳成功";

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
            string serverDirImg =  @"C:/Users/Saki/Desktop/ActivityApply/Web/Uploads/" + as_act + "/Img";
            string act_image = serverDirImg + "/" + filename;
            if (Directory.Exists(serverDirImg) == false) Directory.CreateDirectory(serverDirImg);

            S020102BL _bl = new S020102BL();
            Dictionary<string, object> old_Activity_dict = new Dictionary<string, object>();
            old_Activity_dict["act_idn"] = act_idn;
            Dictionary<string, object> new_Activity_dict = new Dictionary<string, object>();
            new_Activity_dict["act_image"] = @"../Uploads/" + as_act + "/img" + "/" + filename;
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion
    }
}