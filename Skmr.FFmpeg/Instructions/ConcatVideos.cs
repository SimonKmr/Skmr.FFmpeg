using Skmr.FFmpeg.Commands;
using Skmr.FFmpeg.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skmr.FFmpeg.Instructions
{
    public class ConcatVideos
    {
        public void Concat(Medium[] inputs, Medium output)
        {
            switch (Method)
            {
                case ConcatMethod.Protocol:
                    Protocol(inputs, output);
                    break;
                case ConcatMethod.Filter:
                    Filter(inputs, output);
                    break;
                case ConcatMethod.Demuxer:
                    Demuxer(inputs, output);
                    break;
            };
        }

        private void Filter(Medium[] inputs, Medium output)
        {
            //https://stackoverflow.com/a/11175851
            var command = new CommandBuilder();

            for (int i = 0; i < inputs.Length; i++) 
                command.Input(inputs[i].ToString());
            
            command.Custom("-filter_complex \"");

            for (int i = 0; i < inputs.Length; i++)
                command.Custom($"[{i}:v] [{i}:a]");
            command.Custom($"concat=n={2}:v={VideoTracks}:a={AudioTracks} [v] [a]\" ");

            command.Map("\"[v]\"");
            command.Map("\"[a]\"");
            command.Output($"{output}");

            FFmpeg.Instance.Run(command);
        }

        private void Protocol(Medium[] inputs, Medium output)
        {
            var command = new CommandBuilder();

            command.Custom($"-i \"concat:");

            for (int i = 0; i < inputs.Length; i++)
            {
                command.Custom(inputs[i].ToString());
                if (i < inputs.Length - 1) command.Custom("|");
            }

            command
                .Codec(VideoCodec.Copy)
                .Codec(AudioCodec.Copy)
                .Output(output.ToString());

            FFmpeg.Instance.Run(command);
        }

        private void Demuxer(Medium[] inputs, Medium output)
        {
            throw new NotImplementedException();
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
