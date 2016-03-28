using BusinessLayer;
using Model;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using Util;
using DataAccess;

namespace Web
{
    public partial class Login : System.Web.UI.Page
    {
        private LoginBL _bl = new LoginBL();
        private bool _isDbOnline = true;
        public string DbTime;

        #region RSA加密Key值
        public string rsaPublicKey = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCFWMV7w/4UK67T13c1Y0fdVZ0ux+DDGsaTZI3rx7TYCH+WCx4XvVWKyJJU2dPvosOmT2OkzHy4jhCA1Z5TyxSlBQa5CeWB1X9j0RxTzOBUvim+kMlRQhtGw5VI6lw02KH5wbU9imKS7F4TNeL4q2dlfTlt7KhaeF6q7tnxVbIYzwIDAQAB";
        string rsaPrivateKeyXML = "<RSAKeyValue><Modulus>hVjFe8P+FCuu09d3NWNH3VWdLsfgwxrGk2SN68e02Ah/lgseF71VisiSVNnT76LDpk9jpMx8uI4QgNWeU8sUpQUGuQnlgdV/Y9EcU8zgVL4pvpDJUUIbRsOVSOpcNNih+cG1PYpikuxeEzXi+KtnZX05beyoWnhequ7Z8VWyGM8=</Modulus><Exponent>AQAB</Exponent><P>uW9b1Pg5SKesLChA5P9VVqdxKQRv5i2agxn95mxXkqRxzRHuGEykChNLxmp8LylpcrgS1TeQFH4+ljjfw/v0fQ==</P><Q>uBcZgSyufVDbgJTh7YZeWF2c+UdcBGmTU5D/+uEiW6y9h2/gVLqIzgVWHfR/N3rV+lRMtWzI0foeyJvuEr/AOw==</Q><DP>OkOzJweXeCy2/Gjpewp/Verms2yhfEF3+xl/nZcNLRZea4DmvtvV7xSBCqcKvgbVCyarRDNhIg9IuwrDxGC2QQ==</DP><DQ>RSUoB0TpJVjBmcJSOg1GUpqW42rSPTYKiFDmVS1K4nQ3nC+Ba1HFIN0QQ6AaSJRy2tvfFCYQKA5ykZdADPrJEQ==</DQ><InverseQ>TvEQKqbD/0lNHtRKZ/KJ9t0O/D9rsJWrV+XywFy5eANfIS/kxuv9zILsm6AYJ3vG257r8t06snGi730o92PdSg==</InverseQ><D>SUuVKUV+yCmGrEyX6tGKc9+WSVt2cOer1OKFm98myURKlPfBb70TYviCbn9ZHjQyJJ0ooTIBMBRuf9Jrd5V937e43f3U1RnJwAGCDVLfM/415YCmIAANzQGUevAcl6MqC0pg5ieV0JM95F6xFSqRtb9YdLpth0th8qpCGNO1rSk=</D></RSAKeyValue>";
        #endregion

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session.Clear();
            }

            #region 檢查資料庫連線是否正常
            try
            {
                DbTime = DAH.Db.GetDbNowTime().ToYMDHMS();
                Page.Title = CommonHelper.GetSysConfig().SOLUTION_NAME;
            }
            catch (Exception ex)
            {
                // Debug模式時，直接拋出錯誤
                if (HttpContext.Current.IsDebuggingEnabled)
                    throw ex;

                _isDbOnline = false;

                LogHelper.WriteLog("Login:Page_Load，發生錯誤: 無法連接資料庫!" + Environment.NewLine + ex.ToString(), "Error_" + DateTime.Now.ToString("yyyyMMdd"), "System");
            }
            #endregion

            // 依據資料庫是否可正常連線，決定是否顯示系統維護中的訊息、帳密輸入的畫面
            normal_pl.Visible = _isDbOnline;
            dbFail_pl.Visible = !_isDbOnline;
        }
        #endregion

        #region 登入按鈕
        protected void signIn_btn_Click(object sender, EventArgs e)
        {
            if (_isDbOnline)
            {
                string act_id = act_id_txt.Text.Trim().ToUpper();
                string captcha = (string)Session[CommonPages.Captcha.CaptchaSessionKey];    // 產生的驗證碼

                #region 密碼解密
                string act_pwd = "";
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(rsaPrivateKeyXML);
                if (string.IsNullOrEmpty(act_pwd_hf.Value) == false)
                    act_pwd = Encoding.GetEncoding("UTF-8").GetString(rsa.Decrypt(Convert.FromBase64String(act_pwd_hf.Value), false));
                #endregion

                #region 檢查驗證碼(若是Debug模式，則忽略驗證碼)
                if (CustomHelper.IsDebugMode
                    || (!captcha.IsNullOrWhiteSpace() && confirm_txt.Text.Trim().EqualsIgnoreCase(captcha.ToUpper())))
                {
                    #region 檢查帳密
                    var res = _bl.ValidateAccount(act_id, act_pwd);
                    if (res.IsSuccess)
                    {
                        SetActAndRedirect("normal", act_id);
                    }
                    else
                    {
                        // 記錄作業登入資訊
                        CommonBL.WriteLoginOrProcessLog("Login", Sys_login_logInfo.StatusType.Fail, "[帳密]錯誤!(輸入帳號:" + act_id + ")");
                        loginErrorMsg_lbl.Text = "[帳號]或[密碼]錯誤! ";
                        loginErrorMsg_lbl.Visible = true;
                    }
                    #endregion
                }
                else
                {
                    #region 驗證碼錯誤，顯示對應訊息，並記錄
                    string logMsg = "";
                    string showMsg = "";

                    if (captcha.IsNullOrWhiteSpace())
                    {
                        showMsg = "閒置時間過長，驗證碼已失效，請重新輸入";
                        logMsg = "驗證碼已失效(原頁面瀏覽時間:" + broseWebTime_hf.Value + ", 輸入帳號:" + act_id + ")";
                    }
                    else if (confirm_txt.Text.IsNullOrWhiteSpace())
                    {
                        showMsg = "請輸入驗證碼";
                        logMsg = "未輸入驗證碼(輸入帳號:" + act_id + ")";
                    }
                    else if (captcha != confirm_txt.Text.Trim().ToUpper())
                    {
                        showMsg = "驗證碼錯誤";
                        logMsg = "驗證碼錯誤(輸入值:" + confirm_txt.Text.Trim().ToUpper() + ", 正確值:" + captcha.ToUpper() + ", 輸入帳號:" + act_id + ")";
                    }

                    // 記錄作業登入資訊
                    BusinessLayer.CommonBL.WriteLoginOrProcessLog("Login", Sys_login_logInfo.StatusType.Fail, logMsg);

                    loginErrorMsg_lbl.Text = showMsg;
                    loginErrorMsg_lbl.Visible = true;
                    #endregion
                }
                #endregion

                // 清空驗證碼欄位
                confirm_txt.Text = "";
            }
        }
        #endregion

        #region 設定登入帳號，並導入系統
        private void SetActAndRedirect(string loginType, string act_id)
        {
            var user_info = new Sys_accountData().GetInfo(act_id);
            // 檢查帳號是否停用
            if (user_info.Act_status == "Y")
            {
                Session[CommonHelper.LoginUserApplicationKey] = user_info;

                // 記錄作業登入資訊
                BusinessLayer.CommonBL.WriteLoginOrProcessLog("Login", Sys_login_logInfo.StatusType.Success);

                Response.Redirect("~/Index.aspx");
            }
            else if (user_info.Act_status == "X")
            {
                loginErrorMsg_lbl.Text = "該[帳號]已停用。";
                loginErrorMsg_lbl.Visible = true;
            }
        }
        #endregion
    }
}