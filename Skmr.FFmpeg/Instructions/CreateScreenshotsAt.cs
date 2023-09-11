using Skmr.Editor.Commands;
using Skmr.Editor.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Skmr.Editor.Ffmpeg;

namespace Skmr.Editor.Instructions
{
    public class CreateScreenshotAt : IInstruction<CreateScreenshotAt>
    {
        public Info Info { get; } = new Info();
        public CreateScreenshotAt(TimeSpan time, Format format)
        {
            Time = time;
            Format = format;
        }

        public TimeSpan Time { get; }
        public Format Format { get; }

        public void Run()
        {
            var input = Info.Inputs[0].ToString();
            var output = Info.Outputs[0].ToString();

            var command = new CommandBuilder()
                .Input(input)
                .Seeking(Time)
                .Custom("-frames:v 1")
                .Output(output);

            Info.Ffmpeg.Run(command);
        }

        public CreateScreenshotAt Input(Medium medium)
        {
            Info.Inputs = Info.Inputs.Concat(new Medium[] { medium }).ToArray();
            return this;
        }
        public CreateScreenshotAt Output(Medium medium)
        {
            Info.Outputs = Info.Outputs.Concat(new Medium[] { medium }).ToArray();
            return this;
        }
    }
}
