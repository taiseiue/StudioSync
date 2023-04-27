namespace StudioSync.Giphy
{
    /// <summary>
    /// リクエストのすべてを表すクラスです
    /// </summary>
    public class Giphy
    {
        public Data[] Data { get; set; }
    }
    /// <summary>
    /// 画像データを表します
    /// </summary>
    public class Data
    {
        public Images Images { get; set; }
    }
    public class Images
    {
        public Gif fixed_height_downsampled { get; set; }
    }
    public class Gif
    {
        public string Url { get; set; }
        public string Webp { get; set; }
    }
}
