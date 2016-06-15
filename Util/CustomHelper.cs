using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.Common;
using System.Web;
using System.Web.UI;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Data;
using System.Collections.Specialized;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Collections;

namespace Util
{
    /// <summary>
    /// 自定義輔助方法
    /// </summary>
    public static class CustomHelper
    {
        #region 取得目前是否為Debug模式
        /// <summary>
        /// 取得目前是否為Debug模式
        /// </summary>
        public static bool IsDebugMode
        {
            get
            {
#if DEBUG
                return true;
#else
                return false;
#endif
            }
        }
        #endregion

        public static bool isString()
        {
            return false;
        }

        #region 寄發E-mail
        /// <summary>
        /// 寄發E-mail
        /// </summary>
        /// <param name="fromMail">寄件人mail</param>
        /// <param name="fromName">寄件人名稱</param>
        /// <param name="toMails">收件者mail(可多筆，以","分隔，例: aaa@aaa.com,bbb@bbb.com)</param>
        /// <param name="subject">主旨</param>
        /// <param name="mailContnet">信件內容</param>
        /// <param name="mailPriority">優先權(預設為Normal</param>
        public static void SendMail(String fromMail, string fromName, string toMails, string subject, string mailContnet, MailPriority mailPriority = MailPriority.Normal)
        {
            SystemConfigInfo config_info = CommonHelper.GetSysConfig();

            char[] mailDelimiter = { ',' };
            mailContnet = mailContnet.Replace(Environment.NewLine, "<br />");

            MailMessage EmailHtmlContent = new MailMessage();
            EmailHtmlContent.BodyEncoding = Encoding.UTF8;      // 郵件內容編碼
            EmailHtmlContent.SubjectEncoding = Encoding.UTF8;   // 郵件標題編碼
            EmailHtmlContent.Priority = mailPriority;           // 郵件優先級
            EmailHtmlContent.IsBodyHtml = true;                 // 信件內容是否為HTML
            EmailHtmlContent.From = new MailAddress(fromMail, fromName, Encoding.UTF8);

            if (toMails != null)
            {
                foreach (string Email in toMails.Split(mailDelimiter))
                {
                    try
                    {
                        EmailHtmlContent.To.Add(Email.Trim());
                    }
                    catch { }
                }
            }

            if (EmailHtmlContent.To.Count > 0)
            {
                EmailHtmlContent.Subject = subject;
                EmailHtmlContent.Body = mailContnet;

                SmtpClient EmailConnection = new SmtpClient(config_info.SMTP_HOST, Convert.ToInt32(config_info.SMTP_PORT));
                if (config_info.SMTP_AUTH.Equals("Y"))
                    EmailConnection.Credentials = new NetworkCredential(config_info.SMTP_USER, config_info.SMTP_PWD);

                if (config_info.SMTP_SSL.Equals("Y"))
                    EmailConnection.EnableSsl = true;

                try
                {
                    EmailConnection.Send(EmailHtmlContent);
                    EmailHtmlContent.Dispose();
                }
                catch (Exception Ex)
                {
                    //Log.WriteLog(Ex.ToString(), "", "Email");
                }
            }
        }
        #endregion

        #region 取得短網址
        public static string URLshortener(string url, string m_APIKey)
        {
            if (String.IsNullOrEmpty(url))
                throw new ArgumentNullException("url");

            if (m_APIKey.Length == 0)
                throw new Exception("APIKey not set!");

            #region Const
            const string BASE_API_URL = @"https://www.googleapis.com/urlshortener/v1/url";
            const string SHORTENER_URL_PATTERN = BASE_API_URL + @"?key={0}";
            const string POST_PATTERN = @"{{""longUrl"": ""{0}""}}";
            const string MATCH_PATTERN = @"""id"": ?""(?<id>.+)""";
            #endregion

            var post = string.Format(POST_PATTERN, url);
            var request = (HttpWebRequest)WebRequest.Create(string.Format(SHORTENER_URL_PATTERN, m_APIKey));

            request.Method = "POST";
            request.ContentLength = post.Length;
            request.ContentType = "application/json";
            request.Headers.Add("Cache-Control", "no-cache");

            using (Stream requestStream = request.GetRequestStream())
            {
                var buffer = Encoding.ASCII.GetBytes(post);
                requestStream.Write(buffer, 0, buffer.Length);
            }

            using (var responseStream = request.GetResponse().GetResponseStream())
            {
                using (StreamReader sr = new StreamReader(responseStream))
                {
                    return Regex.Match(sr.ReadToEnd(), MATCH_PATTERN).Groups["id"].Value;
                }
            }
        }
        #endregion

        #region 匯出 Excel
        public static string ExportExcelFromDataTable(String FileName, List<Dictionary<string, object>> excelList, String ExportPath = "~/")
        {
            HSSFWorkbook workbook = new HSSFWorkbook();

            #region 設定格式
            ICellStyle BasicStyle = workbook.CreateCellStyle();
            BasicStyle.BorderRight = BasicStyle.BorderBottom = BasicStyle.BorderBottom = BasicStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            BasicStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
            //BasicStyle.WrapText = true;

            ICellStyle StyleInteger = workbook.CreateCellStyle();
            StyleInteger.CloneStyleFrom(BasicStyle);
            StyleInteger.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0");

            ICellStyle StyleDecimal1 = workbook.CreateCellStyle();
            StyleDecimal1.CloneStyleFrom(BasicStyle);
            StyleDecimal1.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0.0");

            ICellStyle StyleDecimal2 = workbook.CreateCellStyle();
            StyleDecimal2.CloneStyleFrom(BasicStyle);
            StyleDecimal2.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0.00");

            ICellStyle StyleDecimal4 = workbook.CreateCellStyle();
            StyleDecimal4.CloneStyleFrom(BasicStyle);
            StyleDecimal4.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0.0000");

            ICellStyle StyleDecimal6 = workbook.CreateCellStyle();
            StyleDecimal6.CloneStyleFrom(BasicStyle);
            StyleDecimal6.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0.000000");

            ICellStyle StyleDateTime = workbook.CreateCellStyle();
            StyleDateTime.CloneStyleFrom(BasicStyle);
            StyleDateTime.DataFormat = workbook.CreateDataFormat().GetFormat("yyyy/mm/dd hh:mm");

            ICellStyle StyleDate = workbook.CreateCellStyle();
            StyleDate.CloneStyleFrom(BasicStyle);
            StyleDate.DataFormat = workbook.CreateDataFormat().GetFormat("yyyy/mm/dd");
            #endregion
            
            foreach(Dictionary<string, object> data in excelList)
            {
                var dt = data["dt"] as DataTable;
                var ColumnNames = data["colname"] as OrderedDictionary;
                var sheetname = data["sheetname"] as string;
                var Format = data["format"] as Dictionary<string, ITCEnum.NPOIExcelFormat>;

                ISheet sheet = workbook.CreateSheet(sheetname);

                DataColumnCollection dcc = dt.Columns;
                int RowCounter = 0, i = 0;

                //若沒有表頭資料, 則從DataTable中取得表頭資料
                if (ColumnNames == null)
                {
                    ColumnNames = new OrderedDictionary();
                    foreach (DataColumn dc in dcc)
                    {
                        String ColName = dc.ColumnName.ToUpper().Substring(0, 1) + dc.ColumnName.Substring(1);
                        ColumnNames[ColName] = ColName;
                    }
                }

                IRow row = sheet.CreateRow(RowCounter);

                //寫入表頭
                foreach (DictionaryEntry item in ColumnNames)
                {
                    ICell cell = row.CreateCell(i);
                    cell.SetCellValue(item.Value.ToString());
                    cell.CellStyle = BasicStyle;
                    i++;
                }

                //若有表頭則需要+1行, 可不要有表頭
                if (ColumnNames.Count > 0)
                    RowCounter++;

                //若資料表不是空的
                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        i = 0;

                        row = sheet.CreateRow(RowCounter);
                        foreach (DictionaryEntry item in ColumnNames)
                        {
                            ICell cell = row.CreateCell(i);
                            cell.CellStyle = BasicStyle;
                            string CurColName = item.Key.ToString();
                            if (dcc.Contains(CurColName))
                            {
                                //若有指定格式
                                if (Format != null && Format.ContainsKey(CurColName))
                                {
                                    switch (Format[CurColName])
                                    {
                                        case ITCEnum.NPOIExcelFormat.Integer:
                                            cell.SetCellType(CellType.Numeric);
                                            cell.CellStyle = StyleInteger;
                                            break;
                                        case ITCEnum.NPOIExcelFormat.Decimal1:
                                            cell.SetCellType(CellType.Numeric);
                                            cell.CellStyle = StyleDecimal1;
                                            break;
                                        case ITCEnum.NPOIExcelFormat.Decimal2:
                                            cell.SetCellType(CellType.Numeric);
                                            cell.CellStyle = StyleDecimal2;
                                            break;
                                        case ITCEnum.NPOIExcelFormat.Decimal4:
                                            cell.SetCellType(CellType.Numeric);
                                            cell.CellStyle = StyleDecimal4;
                                            break;
                                        case ITCEnum.NPOIExcelFormat.Decimal6:
                                            cell.SetCellType(CellType.Numeric);
                                            cell.CellStyle = StyleDecimal6;
                                            break;
                                        case ITCEnum.NPOIExcelFormat.DateTime:
                                            cell.SetCellType(CellType.String);
                                            cell.CellStyle = StyleDateTime;
                                            break;
                                        case ITCEnum.NPOIExcelFormat.Date:
                                            cell.SetCellType(CellType.String);
                                            cell.CellStyle = StyleDate;
                                            break;
                                        case ITCEnum.NPOIExcelFormat.Formula:
                                            cell.SetCellType(CellType.Formula);
                                            break;
                                        case ITCEnum.NPOIExcelFormat.Other:
                                            cell.SetCellType(CellType.String);
                                            //ICellStyle newStyle = workbook.CreateCellStyle();
                                            //newStyle.CloneStyleFrom(BasicStyle);
                                            //newStyle.DataFormat = workbook.CreateDataFormat().GetFormat("yyyy/mm/dd");
                                            break;
                                    }

                                    ITCEnum.NPOIExcelFormat[] IntArray = { ITCEnum.NPOIExcelFormat.Integer, ITCEnum.NPOIExcelFormat.Decimal2, ITCEnum.NPOIExcelFormat.Decimal4, ITCEnum.NPOIExcelFormat.Decimal6 };
                                    if (IntArray.Contains(Format[CurColName]))
                                        cell.SetCellValue((Double)CommonConvert.GetDecimalOrZero(dr[CurColName]));
                                    else
                                        cell.SetCellValue(dr[CurColName].ToString());
                                }
                                //若為數字
                                else if (dcc[CurColName].DataType.FullName == "System.Decimal" || dcc[CurColName].DataType.FullName == "System.Integer" || dcc[CurColName].DataType.FullName == "System.Int32")
                                {
                                    cell.SetCellType(CellType.Numeric);
                                    cell.SetCellValue((Double)CommonConvert.GetDecimalOrZero(dr[CurColName]));
                                }
                                //若為時間
                                else if (dcc[CurColName].DataType.FullName == "System.DateTime")
                                {
                                    cell.CellStyle.DataFormat = workbook.CreateDataFormat().GetFormat("yyyy/MM/dd HH:mm:ss");
                                    cell.SetCellValue((DateTime)dr[CurColName]);
                                }
                                else
                                {
                                    string str = dr[CurColName] == null ? "" : dr[CurColName].ToString();

                                    //若字串第一個字是文字，則設定為公式
                                    if (!string.IsNullOrWhiteSpace(str) && str.Length > 0 && str.Substring(0, 1) == "=")
                                    {
                                        cell.SetCellFormula(str);
                                    }
                                    else
                                        cell.SetCellValue(str);
                                }
                            }

                            i++;
                        }
                        RowCounter++;
                    }

                    //設定自動欄寬
                    for (int j = 0; j < i; j++)
                    {
                        sheet.AutoSizeColumn(j);
                    }
                }
            }

            // 儲存檔案路徑
            string saveFilePath = ExportPath + FileName + ".xls";
            // 報表寫入資料流
            FileStream file = new FileStream(HttpContext.Current.Server.MapPath(saveFilePath), FileMode.Create);
            workbook.Write(file);
            file.Close();
            // 回傳產出檔案路徑
            return saveFilePath;
        }
        #endregion
    }
}
