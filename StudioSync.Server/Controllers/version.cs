namespace StudioSync.Server.Controllers
{
    public static class version
    {
        public static int Version { get; set; } = 32;
        public static int MinClientVersion { get; set; } = 30;

        public static string CodeName { get; set; } = "Takuan";

        public static string ProductName { get; set; } = "WSOFT StudioSyncServer";
        public static string FullName => ProductName + " v" + Version + "(" + CodeName + ")";
    }
}
