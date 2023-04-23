namespace StudioSync.Core
{
    /// <summary>
    /// 接続を開始するための要件を定めます。これはサーバーから送信されます。
    /// </summary>
    public class AcceptRequirements
    {
        /// <summary>
        /// 受け入れるために必要な最小バージョン
        /// </summary>
        public int MinClientVersion { get; set; }
        /// <summary>
        /// サーバーが報告するバージョン
        /// </summary>
        public int ServerVersion { get; set; }

        /// <summary>
        /// サーバーの完全名
        /// </summary>
        public string ServerFullName { get; set; }
    }
}
