namespace StudioSync
{
    public class Progress
    {
        public Dictionary<TimeSpan,string> Progresses { get; set; }
        public TimeSpan Total { get; set; }
        public Progress() { }
        public Progress(string text)
        {
            Total = TimeSpan.Zero;
            string[] lines = text.Split(Environment.NewLine,StringSplitOptions.None);
            Progresses=new Dictionary<TimeSpan,string>();
            foreach(string line in lines)
            {
                int i = line.IndexOf('#');
                string rline = line.Replace("：", ":");
                if (i > 0)
                {
                    //あるとき
                    var time = ConvertSpan(text);
                        Progresses.Add(time, rline.Substring(i + 1));
                        Total = time;
                    
                }
                else
                {
                    //ないとき
                    var time = ConvertSpan(text);
                    Progresses.Add(time,"");
                        Total = time;
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
