using Skmr.Editor.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skmr.Editor.Commands;

namespace Skmr.Editor.Instructions
{
    public class CutVideo : IInstruction<CutVideo>
    {
        public Info Info { get; } = new Info();
        public CutVideo((TimeSpan,TimeSpan) directions)
        {
            Directions = directions;
        }

        public (TimeSpan,TimeSpan) Directions { get; }

        public void Run()
        {
            //-ss {Directions.Item1.ToString(format)} -t {Directions.Item2.ToString(format)} -i {Info.Inputs[0]} -c copy {Info.Outputs[0]}

            var input = Info.Inputs[0].ToString();
            var output = Info.Outputs[0].ToString();

            var command = new CommandBuilder()
                .Seeking(Directions.Item1)
                .Duration(Directions.Item2)
                .Input(input)
                .VCodec(VideoCodec.Copy)
                .Output(output);

            Info.Ffmpeg.Run(command);
        }

        public CutVideo Input(Medium medium)
        {
            Info.Inputs = Info.Inputs.Concat(new Medium[] { medium }).ToArray();
            return this;
        }
        public CutVideo Output(Medium medium)
        {
            Info.Outputs = Info.Outputs.Concat(new Medium[] { medium }).ToArray();
            return this;
        }
    }
}
