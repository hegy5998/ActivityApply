using System;
using Util;

namespace BusinessLayer
{
    /// <summary>
    /// BL 層處理結果回傳類別
    /// </summary>
    public sealed class BLResult
    {
        /// <summary>
        /// 結果訊息
        /// </summary>
        public string Message;

        /// <summary>
        /// 結果標頭
        /// </summary>
        public string Header;

        /// <summary>
        /// 結果
        /// </summary>
        public ITCEnum.PopupMessageType PopupMessageType;

        /// <summary>
        /// 處裡方式
        /// </summary>
        public ITCEnum.DataActionType DataActionType;

        /// <summary>
        /// 是否為查詢結果而非新刪修
        /// </summary>
        public bool IsQueryResult;

        public BLResult(ITCEnum.DataActionType DataActionType, ITCEnum.PopupMessageType PopupMessageType)
        {
            this.Message = String.Empty;
            this.PopupMessageType = PopupMessageType;
            this.DataActionType = DataActionType;
            IsQueryResult = false;
        }

        public BLResult(string Message, ITCEnum.DataActionType DataActionType, ITCEnum.PopupMessageType PopupMessageType)
        {
            this.Message = Message;
            this.PopupMessageType = PopupMessageType;
            this.DataActionType = DataActionType;
            IsQueryResult = false;
        }

        public BLResult(string Message, string Header, ITCEnum.PopupMessageType PopupMessageType)
        {
            this.Message = Message;
            this.Header = Header;
            this.PopupMessageType = PopupMessageType;
            IsQueryResult = true;
        }
    }

    /// <summary>
    /// BL 層處理結果回傳類別
    /// </summary>
    public sealed class BLResult<T>
    {
        /// <summary>
        /// 結果訊息
        /// </summary>
        public string Message;

        /// <summary>
        /// 結果標頭
        /// </summary>
        public string Header;

        /// <summary>
        /// 結果
        /// </summary>
        public ITCEnum.PopupMessageType PopupMessageType;

        /// <summary>
        /// 處裡方式
        /// </summary>
        public ITCEnum.DataActionType DataActionType;

        /// <summary>
        /// 是否為查詢結果而非新刪修
        /// </summary>
        public bool IsQueryResult;

        /// <summary>
        /// 回傳結果
        /// </summary>
        public T Result;

        public BLResult(ITCEnum.DataActionType DataActionType, ITCEnum.PopupMessageType PopupMessageType)
        {
            this.Message = String.Empty;
            this.PopupMessageType = PopupMessageType;
            this.DataActionType = DataActionType;
            this.IsQueryResult = false;
        }

        public BLResult(string Message, ITCEnum.DataActionType DataActionType, ITCEnum.PopupMessageType PopupMessageType)
        {
            this.Message = Message;
            this.PopupMessageType = PopupMessageType;
            this.DataActionType = DataActionType;
            this.IsQueryResult = false;
        }

        public BLResult(string Message, string Header, ITCEnum.PopupMessageType PopupMessageType)
        {
            this.Message = Message;
            this.Header = Header;
            this.PopupMessageType = PopupMessageType;
            this.IsQueryResult = true;
        }

        public BLResult ToNonPolymorphism()
        {
            BLResult r = new BLResult(this.DataActionType, this.PopupMessageType);
            r.Message = this.Message;
            r.Header = this.Header;
            r.IsQueryResult = this.IsQueryResult;
            return r;
        }
    }
}
