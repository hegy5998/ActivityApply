using BusinessLayer.S01;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.S02
{
    public partial class S02010401 : System.Web.UI.Page
    {
        S020104BL _bl = new S020104BL();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void set_bt_Click(object sender, EventArgs e)
        {
            string password_rd = getRandStringEx(6);

            string password = password_rd;
            List<Activity_apply_emailInfo> salt = _bl.getEmail(email_txt.Text);

            if (salt.Count > 0 )
            {
                byte[] passwordAndSaltBytes = System.Text.Encoding.UTF8.GetBytes(password + salt[0].Aae_salt.Trim());
                byte[] hashBytes = new System.Security.Cryptography.SHA256Managed().ComputeHash(passwordAndSaltBytes);

                string hashString = Convert.ToBase64String(hashBytes);
                Dictionary<string, object> old_dt = new Dictionary<string, object>();
                old_dt["aae_email"] = email_txt.Text;
                Dictionary<string, object> new_dt = new Dictionary<string, object>();
                new_dt["aae_password"] = hashString;
                _bl.UpdateData(old_dt, new_dt);
                password_lb.Text = password_rd + " (請立即請使用者更改密碼!!)";
            }
            else
                password_lb.Text = "無此信箱";
        }

        #region 亂數產生
        public static String getRandStringEx(int length)
        {
            char[] charList = { '0','1','2','3','4','5','6','7','8','9',
                                'A','B','C','D','E','F','G','H','I','J','K','L','M',
                                'N','O','P','Q','R','S','T','U','V','W','X','Y','Z',
                                'a','b','c','d','e','f','g','h','i','j','k','l','m',
                                'n','o','p','q','r','s','t','u','v','w','x','y','z'};
            char[] rev = new char[length];
            Random f = new Random();
            for (int i = 0; i < length; i++)
            {
                rev[i] = charList[Math.Abs(f.Next(0,9))];
            }
            return new String(rev);
        }
        #endregion
    }
}