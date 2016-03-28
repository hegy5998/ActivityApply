using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Model.S01
{
    public class UCAccountManagerInfo
    {
        public class Main
        {
            [Required(ErrorMessage="[帳號]不可為空白!")]
            public string Act_id { get; set; }
            public string Act_pwd { get; set; }
            [Required(ErrorMessage = "[名稱]不可為空白!")]
            public string Act_name { get; set; }
            public string Act_mail { get; set; }
            public string Act_status { get; set; }
            public DateTime? Act_pwd_date { get; set; }
            public int? Act_pwd_expire_login_times { get; set; }
            public List<Model.S01.UCAccountRoleManagerInfo.Main> Role_lst { get; set; }
            public Main()
            {
                Role_lst = new List<Model.S01.UCAccountRoleManagerInfo.Main>();
            }
        }
    }
}
