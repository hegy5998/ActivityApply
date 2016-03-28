using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Web;
using System.Web.UI;

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
    }
}
