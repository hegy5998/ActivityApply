using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using Util;
using DataAccess;

namespace BusinessLayer.UserControls
{
    public class UCSystemModuleBL : BaseBL
    {
        /// <summary>
        /// 取得所有系統
        /// </summary>
        /// <returns></returns>
        public List<Sys_systemInfo> GetSystemList(bool isOnlyShowHasModule = false)
        {
            return new Sys_systemData().GetList(isOnlyShowHasModule);
        }

        /// <summary>
        /// 取得系統對應的模組
        /// </summary>
        /// <returns></returns>
        public List<Sys_moduleInfo> GetModuleList(string sys_id)
        {
            return new Sys_moduleData().GetListBySystem(sys_id);
        }
    }
}
