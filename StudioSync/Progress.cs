namespace StudioSync
{
    public class Progress
    {
        public Dictionary<TimeSpan,string> Progresses { get; set; }
        public TimeSpan Total { get; set; }
        public bool Reverse { get; set; }
        public string Title { get; set; }
        public Progress() { }
        public Progress(string text)
        {
            Total = TimeSpan.Zero;
            string[] lines = text.Split(Environment.NewLine,StringSplitOptions.None);
            Progresses=new Dictionary<TimeSpan,string>();
            foreach(string line in lines)
            {
                string rline = line.Replace("：", ":").Trim();
                if (rline.StartsWith("#"))
                {
                    //あるとき
                    if (rline.StartsWith("#!"))
                    {
                        var time = ConvertSpan(rline.Substring(2));
                        Progresses.Add(time, "");
                        Total = time;
                        Reverse= true;
                    }
                    else
                    {
                        var time = ConvertSpan(rline.Substring(1));
                        Progresses.Add(time, "");
                        Total = time;
                    }
                }
                else if (rline.StartsWith("「「"))
                {
                    Title = rline;
                }
            }
        }
        private TimeSpan ConvertSpan(string text)
        {
            double sec = 0;
            int i = text.IndexOf(":");
            if (i > 0)
            {
                string r1=text.Substring(0, i);
                string r2=text.Substring(i+1);
                sec = double.Parse(r2)+(double.Parse(r1)*60);
            }
            else
            {
                sec = double.Parse(text);
            }
            return TimeSpan.FromSeconds(sec);
        }
    }
}
