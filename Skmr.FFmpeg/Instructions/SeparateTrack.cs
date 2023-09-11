using Skmr.Editor.Commands;
using Skmr.Editor.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skmr.Editor.Instructions
{
    public class SeparateTrack : IInstruction<SeparateTrack>
    {
        public Info Info { get; } = new Info();
        public void Run()
        {
            var input = Info.Inputs[0].ToString();
            var output = Info.Outputs[0].ToString();

            var command = new CommandBuilder();

            switch (Stream)
            {
                case StreamType.Audio:
                    command
                        .Input(input)
                        .Map($"0:a:{Track}")
                        .Codec(Codec.Copy)
                        .Output(output);
                    break;

                case StreamType.Video:
                    command
                        .Input(input)
                        .Map($"0:v:{Track}")
                        .Codec(Codec.Copy)
                        .Output(output);
                    break;
            };

            Info.Ffmpeg.Run(command);
        }

        public SeparateTrack Input(Medium medium)
        {
            Info.Inputs = Info.Inputs.Concat(new Medium[] { medium }).ToArray();
            return this;
        }
        public SeparateTrack Output(Medium medium)
        {
            Info.Outputs = Info.Outputs.Concat(new Medium[] { medium }).ToArray();
            return this;
        }
        
        public SeparateTrack(int track, StreamType stream)
        {
            Stream = stream;
            Track = track;
        }
        public int Track { get; }
        public StreamType Stream { get; }

        public enum StreamType
        {
            Audio,
            Video,
        }

    }
}
