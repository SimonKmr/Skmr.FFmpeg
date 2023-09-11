using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.Commands
{
    public static class FFMpegExtention
    {
        public static void Run(this Ffmpeg ffmpeg, CommandBuilder command)
        {
            ffmpeg.Run(command.ToString());
        }
    }
}
