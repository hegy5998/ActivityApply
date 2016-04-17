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

        public static bool isString () {
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
    }
}
