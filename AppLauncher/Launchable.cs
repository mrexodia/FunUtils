using System.Linq;

namespace AppLauncher
{
    public class Launchable
    {
        public int PriorityBias { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public string Arguments { get; set; }
        public string WorkingDirectory { get; set; }

        public bool MatchesFilter(string filter) => Name.ToLower().Contains(filter.ToLower());

        public int Order(string filter)
        {
            var words = Name.Split();
            var startBias = words.Count(w => w.ToLower().StartsWith(filter.ToLower()));
            return PriorityBias + startBias;
        }

        public void Launch()
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = FileName,
                Arguments = string.IsNullOrEmpty(Arguments) ? null : Arguments,
                WorkingDirectory = string.IsNullOrEmpty(WorkingDirectory) ? null : WorkingDirectory,
            });
        }

        public override string ToString() => Name;
    }

}
