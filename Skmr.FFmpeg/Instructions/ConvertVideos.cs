using Skmr.Editor.Commands;
using Skmr.Editor.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Skmr.Editor.Ffmpeg;

namespace Skmr.Editor.Instructions
{
    public class ConvertVideos : IInstruction<ConvertVideos>
    {
        public Info Info { get; } = new Info();

        public void Run()
        {
            //old params : -i {Inputs[i].Path} -c copy -bsf:v h264_mp4toannexb -f mpegts {Outputs[i].Path}
            //$"-i {Info.Inputs[i]} -map 0:v -map 0:a -c copy -bsf:v h264_mp4toannexb -f mpegts {Info.Outputs[i]}"

            for (int i = 0; i < Info.Inputs.Length; i++)
            {
                var input = Info.Inputs[i].ToString();
                var output = Info.Outputs[i].ToString();

                var command = new CommandBuilder()
                    .Input(input)
                    .Map("0:v")
                    .Map("0:a")
                    .Codec(Codec.Copy)
                    .Custom("-bsf:v h264_mp4toannexb")
                    .Custom("-f mpegts")
                    .Output(output);

                Info.Ffmpeg.Run(command);
            }
        }

        public ConvertVideos Input(Medium medium)
        {
            Info.Inputs = Info.Inputs.Concat(new Medium[] { medium }).ToArray();
            return this;
        }
        public ConvertVideos Output(Medium medium)
        {
            Info.Outputs = Info.Outputs.Concat(new Medium[] { medium }).ToArray();
            return this;
        }
    }
}
