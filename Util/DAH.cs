namespace Util
{
    /// <summary>
    /// DataAccessHelper
    /// </summary>
    public static class DAH
    {
        #region 資料庫連線物件
        /// <summary>
        /// 主要資料庫的連接物件
        /// </summary>
        public static CommonDbHelper Db { get; set; }

        /// <summary>
        /// 其他資料庫的連接物件
        /// </summary>
        //public static CommonDbHelper DbOther { get; set; }
        #endregion
    }
}
