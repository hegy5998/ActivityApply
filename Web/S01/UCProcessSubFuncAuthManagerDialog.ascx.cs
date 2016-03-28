using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.S01
{
    public partial class UCProcessSubFuncAuthManagerDialog : System.Web.UI.UserControl
    {
        public void Show(string sys_pid)
        {
            ucProcessSubFuncAuthManager.Show(sys_pid);
            popupWindow_mpe.Show();
        }
    }
}