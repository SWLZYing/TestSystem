namespace TestSystem.WebApi.Enums
{
    /*
     * 傳入值相關 1
     * 資料庫相關 2
     * 系統相關 9
     */
    public enum ErrorCodeType
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success = 9999,

        /// <summary>
        /// 必填欄位缺失
        /// </summary>
        FieldsMiss = 1001,

        /// <summary>
        /// 密碼驗證錯誤
        /// </summary>
        PwdError = 1002,

        /// <summary>
        /// 資料庫執行失敗
        /// </summary>
        DBError = 2001,

        /// <summary>
        /// 查無資料
        /// </summary>
        NotFound = 2002,

        /// <summary>
        /// 系統錯誤
        /// </summary>
        SystemError = 9001,
    }
}