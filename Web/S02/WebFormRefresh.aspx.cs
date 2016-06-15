using BusinessLayer;
using BusinessLayer.S02;
using Ionic.Zip;
using Model;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using Util;

namespace Web.S02
{
    public partial class WebFormRefresh : System.Web.UI.Page
    {
        private NPOI.HSSF.UserModel.HSSFWorkbook hssfworkbook;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void download_Click(object sender, EventArgs e)
        {
            Sys_accountInfo loginUser = CommonHelper.GetLoginUser();
            var memoryStream = new MemoryStream();
            S020101BL _bl = new S020101BL();
            string upload_path = CommonHelper.GetSysConfig().UPLOAD_PATH;

            List<BLResult<Dictionary<string, object>>> resultList = new List<BLResult<Dictionary< string, object>>> ();
            var dt = _bl.GetActivityData(2163);
            var excelResult = _bl.GetActivityExcelData(dt.Result);
            var filename = excelResult.Result["filename"] as string;
            resultList.Add(excelResult);

            dt = _bl.GetSessionData(2163);
            excelResult = _bl.GetSessionExcelData(dt.Result);
            resultList.Add(excelResult);

            dt = _bl.GetSectionData(2163);
            excelResult = _bl.GetSectionExcelData(dt.Result);
            resultList.Add(excelResult);

            dt = _bl.GetColumnData(2163);
            excelResult = _bl.GetColumnExcelData(dt.Result);
            resultList.Add(excelResult);

            List<Dictionary<string, object>> excelList = new List<Dictionary<string, object>>();
            foreach (BLResult<Dictionary<string, object>> result in resultList) {
                if (result.PopupMessageType == ITCEnum.PopupMessageType.Success)
                {
                    Dictionary<string, object> excel = new Dictionary<string, object>();
                    excel.Add("dt", result.Result["dt"]);
                    excel.Add("colname", result.Result["colname"]);
                    excel.Add("sheetname", result.Result["sheetname"]);
                    excel.Add("format", result.Result["format"]);
                    excelList.Add(excel);
                }
            }
            CustomHelper.ExportExcelFromDataTable(filename, excelList, "~/");

            List<ActivityInfo> activitylist = getActivity(2146);
            ZipFile zip = new ZipFile(Encoding.UTF8);
            //檔案上傳路徑
            

            if (File.Exists(upload_path + "2146/Img/" + activitylist[0].Act_image.Split('/')[activitylist[0].Act_image.Split('/').Count() - 1]))
            {
                zip.AddFile(upload_path + "2146/Img/" + activitylist[0].Act_image.Split('/')[activitylist[0].Act_image.Split('/').Count() - 1], "Img");
            }
            if (File.Exists(upload_path + "2146/relateFile/" + activitylist[0].Act_relate_file.Split('/')[activitylist[0].Act_relate_file.Split('/').Count() - 1]))
            {
                zip.AddFile(upload_path + "2146/relateFile/" + activitylist[0].Act_relate_file.Split('/')[activitylist[0].Act_relate_file.Split('/').Count() - 1], "relateFile");
            }
            if (File.Exists("C:/Users/Saki/Desktop/ActivityApply/Web/" + filename + ".xls"))
            {
                zip.AddFile("C:/Users/Saki/Desktop/ActivityApply/Web/" + filename + ".xls", "");
            }
            zip.Save(memoryStream);
            //Content-Disposition檔案標頭設定，attachment為附件檔
            Response.AddHeader("Content-Disposition", "attachment;filename=MyZip.zip");
            Response.BinaryWrite(memoryStream.ToArray());
        }
        protected List<ActivityInfo> getActivity(int act_idn)
        {
            CommonDbHelper Db = DAH.Db;
            string sql = @" SELECT   activity.*
                            FROM    activity
                            cross apply 
                                    (select top 1 COUNT(*) as num
                                    from activity_session 
                                    where   as_act = @act_idn ) as ac_session
                            WHERE   (act_idn = @act_idn)  AND ac_session.num > 0";
            IDataParameter[] param = { Db.GetParam("@act_idn", act_idn) };
            return Db.GetEnumerable<ActivityInfo>(sql, param).ToList();
        }



    }
}
