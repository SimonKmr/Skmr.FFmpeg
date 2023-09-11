using Skmr.Editor.Commands;
using Skmr.Editor.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skmr.Editor.Instructions
{
    public class ConcatVideos : IInstruction<ConcatVideos>
    {
        public Info Info { get; } = new Info();
        public void Execute2()
        {

        }

        public void Run()
        {
            switch (Method)
            {
                case ConcatMethod.Protocol:
                    Protocol();
                    break;
                case ConcatMethod.Filter:
                    Filter();
                    break;
                case ConcatMethod.Demuxer:
                    Demuxer();
                    break;
            };
        }

        private void Filter()
        {
            //https://stackoverflow.com/a/11175851
            var command = new CommandBuilder();

            for (int i = 0; i < Info.Inputs.Length; i++) 
                command.Input(Info.Inputs[i].ToString());
            
            command.Custom("-filter_complex \"");

            for (int i = 0; i < Info.Inputs.Length; i++)
                command.Custom($"[{i}:v] [{i}:a]");
            command.Custom($"concat=n={2}:v={VideoTracks}:a={AudioTracks} [v] [a]\" ");

            command.Map("\"[v]\"");
            command.Map("\"[a]\"");
            command.Output($"{Info.Outputs[0]}");

            Info.Ffmpeg.Run(command);
        }

        private void Protocol()
        {
            var output = Info.Outputs[0].ToString();
            var command = new CommandBuilder();

            command.Custom($"-i \"concat:");

            for (int i = 0; i < Info.Inputs.Length; i++)
            {
                command.Custom(Info.Inputs[i].ToString());
                if (i < Info.Inputs.Length - 1) command.Custom("|");
            }

            command
                .VCodec(VideoCodec.Copy)
                .ACodec(AudioCodec.Copy)
                .Output(Info.Outputs[0].ToString());

            Info.Ffmpeg.Run(command);
        }

        private void Demuxer()
        {
            throw new NotImplementedException();
        }

        public ConcatVideos Input(Medium medium)
        {
            Info.Inputs = Info.Inputs.Concat(new Medium[] { medium }).ToArray();
            return this;
        }
        public ConcatVideos Output(Medium medium)
        {
            Info.Outputs = Info.Outputs.Concat(new Medium[] { medium }).ToArray();
            return this;
        }


        public ConcatVideos(int videoTracks = 1, int audioTracks = 1, ConcatMethod concatMethod = ConcatMethod.Protocol)
        {
            VideoTracks = videoTracks;
            AudioTracks = audioTracks;
            Method = concatMethod;
        }
        public int VideoTracks { get; }
        public int AudioTracks { get; }
        public ConcatMethod Method { get; }

        public enum ConcatMethod
        {
            Demuxer,
            Protocol,
            Filter
        }

        //public override void Execute()
        //{
        //    //if input format not .ts throw error

        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("-i \"concat:");

        //    for (int i = 0; i < Inputs.Length; i++)
        //    {
        //        sb.Append($"{Inputs[i].Path}");
        //        if ((i < Inputs.Length - 1)) sb.Append("|");
        //    }
        //    //-vcodec copy -acodec copy
        //    sb.Append($"\" -vcodec copy -acodec copy {Outputs[0].Path}");

        //    Ffmpeg.StartFfmpeg(sb.ToString());
        //    Ffmpeg.WaitTillDone();
        //}


        //public override void Execute()
        //{
        //    string arguments =
        //        $"-i {Inputs[0].Path} -i {Inputs[1].Path} -i {Inputs[2].Path} " +
        //        "-filter_complex \"[0:v] [0:a] [1:v] [1:a] [2:v] [2:a] " +
        //        "concat=n=3:v=1:a=1 [v] [a]\" " +
        //        $"-map \"[v]\" -map \"[a]\" {Outputs[0].Path}";
        //    Ffmpeg.StartFfmpeg(arguments);
        //    Ffmpeg.WaitTillDone();
        //}
    }
}
