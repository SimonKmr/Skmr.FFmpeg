using System.Diagnostics;

namespace Skmr.FFmpeg
{
    public interface IFfProcess
    {
        public Process Process { get; }
        public string Executable { get; set; }
        public void Run(string args);
    }
}