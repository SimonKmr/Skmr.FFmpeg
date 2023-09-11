﻿using Skmr.Editor.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skmr.Editor.Instructions
{
    public class LayerVideos : IInstruction<LayerVideos>
    {
        public Info Info { get; } = new Info();
        public void Run()
        {
            //Implementation of             
            // https://stackoverflow.com/questions/35269387/ffmpeg-overlay-one-video-onto-another-video

            // the clips have to be scaled to the same size best case -> same size as base video

            string time = Start.TotalSeconds.ToString();
            Info.Ffmpeg.Run($"-i {Info.Inputs[0]} -i {Overlay} " +
                $"-filter_complex \"" +
                $"[1:v]setpts=PTS+{time}/TB[a];" +
                $"[0:v][a]overlay[out]\" " +
                $"-map [out] -map 0:a " +
                $"-c:v libx264 -crf 18 -pix_fmt yuv420p " +
                $"-c:a copy " +
                $"{Info.Outputs[0]}");
        }

        
        public Medium Overlay { get; set; }
        public LayerVideos(TimeSpan start)
        {
            Start = start;
        }
        public TimeSpan Start { get; }
        public LayerVideos()
        {
            Start = TimeSpan.Zero;
        }

        public LayerVideos Input(Medium medium)
        {
            Info.Inputs = Info.Inputs.Concat(new Medium[] { medium }).ToArray();
            return this;
        }
        public LayerVideos Output(Medium medium)
        {
            Info.Outputs = Info.Outputs.Concat(new Medium[] { medium }).ToArray();
            return this;
        }
    }
}
