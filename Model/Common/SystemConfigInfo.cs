/*
 * 檔案路徑: Model\Common\SystemConfigInfo.cs
 */

using System;

namespace Model.Common
{
    public class SystemConfigInfo
    {
        /// <summary>
        /// AP主機IP，多台以","區隔(若只有一台可不設定)
        /// </summary>
        public String AP_IP { get; set; }
        /// <summary>
        /// 發生錯誤時，是否要寄發信件(Y: 是, N: 否)
        /// </summary>
        public String ERROR_HANDLER_NEED_SEND_NOTIFICATION { get; set; }
        /// <summary>
        /// 發生錯誤時，是否寫入log檔記錄(Y: 是, N: 否)
        /// </summary>
        public String ERROR_HANDLER_NEED_WRITE_LOG { get; set; }
        /// <summary>
        /// 發生錯誤時，要通知的管理者mail, 多個管理者請用","隔開mail(abc@abc.com,bbb@bbb.com)
        /// </summary>
        public String ERROR_HANDLER_NOTIFY_EMAIL { get; set; }
        /// <summary>
        /// 多少時間(分)內不重複移除逾時的使用者
        /// </summary>
        public String ONLINE_USER_COUNTER_DROP_INTERVAL { get; set; }
        /// <summary>
        /// 啟用線人上數統計功能(Y:是,N:否)
        /// </summary>
        public String ONLINE_USER_COUNTER_ENABLE { get; set; }
        /// <summary>
        /// 多少時間(分)內不重複同步其他AP人數
        /// </summary>
        public String ONLINE_USER_COUNTER_SYNC_INTERVAL { get; set; }
        /// <summary>
        /// Sesstion Timeout 時間(分)
        /// </summary>
        public String ONLINE_USER_COUNTER_TIMEOUT_INTERVAL { get; set; }
        /// <summary>
        /// 顯示系統選單，Y: 顯示、N: 不顯示、Auto: 自動判斷
        /// </summary>
        public String SHOW_SYSTEM_MENU { get; set; }
        /// <summary>
        /// SMTP 發送人mail
        /// </summary>
        public String SMTP_FROM_MAIL { get; set; }
        /// <summary>
        /// SMTP 發送人名稱
        /// </summary>
        public String SMTP_FROM_NAME { get; set; }
        /// <summary>
        /// SMTP 是否須帳密登入(Y:是 N:否)
        /// </summary>
        public String SMTP_AUTH { get; set; }
        /// <summary>
        /// SMTP 主機位址
        /// </summary>
        public String SMTP_HOST { get; set; }
        /// <summary>
        /// SMTP Port
        /// </summary>
        public String SMTP_PORT { get; set; }
        /// <summary>
        /// SMTP 密碼
        /// </summary>
        public String SMTP_PWD { get; set; }
        /// <summary>
        /// SMTP 是否使用SSL(Y:是 N:否)
        /// </summary>
        public String SMTP_SSL { get; set; }
        /// <summary>
        /// SMTP 帳號
        /// </summary>
        public String SMTP_USER { get; set; }
        /// <summary>
        /// 帳號建立時，預設的密碼
        /// </summary>
        public String SOLUTION_ACT_DEFAULT_PWD { get; set; }
        /// <summary>
        /// 密碼加密時，使用的 salted value
        /// </summary>
        public String SOLUTION_ACT_PWD_ENCRYPT_SALTED_VALUE { get; set; }
        /// <summary>
        /// 預設的Cache有效時間(分)
        /// </summary>
        public String SOLUTION_CACHE_DURATION_DEFAULT { get; set; }
        /// <summary>
        /// Menu的Cache資料有效時間(分)
        /// </summary>
        public String SOLUTION_CACHE_DURATION_MENU { get; set; }
        /// <summary>
        /// 方案網址
        /// </summary>
        public String SOLUTION_HTTPADDR { get; set; }
        /// <summary>
        /// 方案名稱
        /// </summary>
        public String SOLUTION_NAME { get; set; }
    }
}