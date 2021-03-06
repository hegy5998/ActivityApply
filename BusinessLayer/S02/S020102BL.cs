﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using DataAccess;
using Util;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Data;
using DataAccess.S01;

namespace BusinessLayer.S02
{
    public class S020102BL : BaseBL
    {
        ActivityData _data = new ActivityData();
        Activity_sessionData _sessiondata = new Activity_sessionData();
        Activity_applyData _applydata = new Activity_applyData();
        Activity_columnData _columnData = new Activity_columnData();
        Activity_sectionData _sectionData = new Activity_sectionData();
        Activity_classData _classData = new Activity_classData();
        Activity_statementData _stateData = new Activity_statementData();

        #region 更新
        /// <summary>
        /// 更新資料
        /// </summary>
        /// <param name="oldData_dict">原資料PK</param>
        /// <param name="newData_dict">新資料</param>
        /// <returns></returns>
        public CommonResult UpdateData(Dictionary<string, object> oldData_dict, Dictionary<string, object> newData_dict)
        {
            var res = CommonHelper.ValidateModel<Model.ActivityInfo>(newData_dict);

            if (res.IsSuccess)
            {
                return _data.UpdateData(oldData_dict, newData_dict);
            }

            return res;
        }
        public CommonResult Session_UpdateData(Dictionary<string, object> oldData_dict, Dictionary<string, object> newData_dict)
        {
            var res = CommonHelper.ValidateModel<Model.Activity_sessionInfo>(newData_dict);

            if (res.IsSuccess)
            {
                return _sessiondata.UpdateData(oldData_dict, newData_dict);
            }

            return res;
        }
        #endregion



        #region 新增
        /// <summary>
        /// 新增資料
        /// </summary>
        /// <param name="dict">資料</param>
        /// <returns></returns>
        public CommonResult InsertData(Dictionary<string, object> dict)
        {
            var res = CommonHelper.ValidateModel<Model.ActivityInfo>(dict);
            if (res.IsSuccess)
            {
                res = _data.InsertData(dict);
            }
            return res;
        }
        public CommonResult InsertData_Activity_Section(Dictionary<string, object> dict)
        {
            var res = CommonHelper.ValidateModel<Model.Activity_sectionInfo>(dict);

            if (res.IsSuccess)
            {
                res = _sectionData.InsertData(dict);
            }

            return res;
        }
        public CommonResult InsertData_Activity_Column(Dictionary<string, object> dict)
        {
            var res = CommonHelper.ValidateModel<Model.Activity_columnInfo>(dict);

            if (res.IsSuccess)
            {
                res = _columnData.InsertData(dict);
            }

            return res;
        }
        public CommonResult InsertData_session(Dictionary<string, object> dict)
        {
            var res = CommonHelper.ValidateModel<Model.Activity_sessionInfo>(dict);

            if (res.IsSuccess)
            {
                res = _sessiondata.InsertData(dict);
            }

            return res;
        }

        public CommonResult InsertData_apply(Dictionary<string, object> dict)
        {
            var res = CommonHelper.ValidateModel<Model.Activity_applyInfo>(dict);

            if (res.IsSuccess)
            {
                res = _applydata.InsertData(dict);
            }

            return res;
        }
        #endregion

        #region 刪除
        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="dict">資料</param>
        /// <returns></returns>
        public CommonResult DeleteActivityData(Dictionary<string, object> dict)
        {
            return _data.DeleteData(dict);
        }

        public CommonResult DeleteSectionData(Dictionary<string, object> dict)
        {
            return _sectionData.DeleteData(dict);
        }
        public CommonResult DeleteSessionData(Dictionary<string, object> dict)
        {
            return _sessiondata.DeleteData(dict);
        }
        #endregion

        #region 查詢活動
        /// <summary>
        /// 取得所有資料
        /// </summary>
        /// <returns>所有資料</returns>
        public DataTable GetData(Dictionary<string, object> Cond)
        {
            return _data.GetList(Cond);
        }
        #endregion

        #region 取得活動列表
        public List<Activity_classInfo> GetClassList()
        {
            return _classData.GetClassList();
        }
        #endregion

        #region 取得個資聲明
        public List<Activity_statementInfo> getStatement()
        {
            return _stateData.getStatement();
        }
        #endregion
    }
}
