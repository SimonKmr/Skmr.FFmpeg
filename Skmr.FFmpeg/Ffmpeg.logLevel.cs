namespace Skmr.FFmpeg
{
    public partial class FFmpeg
    {
        public enum logLevel
        {
            quiet = -8,
            panic = 0,
            fatal = 8,
            error = 16,
            warning = 24,
            info = 32,
            verbose = 40,
            debug = 48,
            trace = 56,
        }
    }
}
