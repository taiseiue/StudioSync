namespace StudioSync
{
    public static class version
    {
        public static int Version { get; set; } = 16;
        public static string CodeName { get; set; } = "Takuan";

        public static string ProductName { get; set; } = "WSOFT StudioSync";
        public static string FullName => ProductName + " v" + Version + "(" + CodeName + ")";
    }
}
