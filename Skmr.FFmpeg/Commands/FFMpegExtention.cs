﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.FFmpeg.Commands
{
    public static class FFMpegExtention
    {
        public static void Run(this FFmpeg ffmpeg, CommandBuilder command)
        {
            ffmpeg.Run(command.ToString());
        }
    }
}
