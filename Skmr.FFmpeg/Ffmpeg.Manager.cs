using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.FFmpeg
{
    public partial class FFmpeg
    {
        public class Manager
        {
            public static FFmpeg Ffmpeg { get; set; } = new FFmpeg();
        }
    }
}
