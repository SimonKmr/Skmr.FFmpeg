using Skmr.FFmpeg.Commands;
using Skmr.FFmpeg.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Skmr.FFmpeg.FFmpeg;

namespace Skmr.FFmpeg.Instructions
{
    public class Preset
    {
        public void VStack(Medium[] inputs, Medium output)
        {
            var outp = output.ToString();
            var command = new CommandBuilder();

            for (int i = 0; i < inputs.Length; i++)
                command.Input(inputs[i].ToString());

            command.Custom("-filter_complex \"")
                .Custom("vstack,")
                .Custom("format=yuv420p\"")
                .Codec(VideoCodec.Libx264)
                .ConstantRateFactor(18)
                .Output(outp);

            FFmpeg.Instance.Run(command);
        }

        public void HStack(Medium[] inputs, Medium output)
        {
            var outp = output.ToString();
            var command = new CommandBuilder();

            for (int i = 0; i < inputs.Length; i++)
                command.Input(inputs[i].ToString());

            command.Custom("-filter_complex \"")
                .Custom("hstack,")
                .Custom("format=yuv420p\"")
                .Codec(VideoCodec.Libx264)
                .ConstantRateFactor(18)
                .Output(outp);

            FFmpeg.Instance.Run(command);
        }

        public void Rezise(Medium input, Medium output, int width, int height)
        {
            //$"-i {Info.Inputs[0]} -filter:v \"scale={Width}:{Height}\" -codec:a copy {Info.Outputs[0]}"

            var inp = input.ToString();
            var outp = output.ToString();

            var filter = new FilterBuilder()
                .Scale(width, height);

            var command = new CommandBuilder()
                .Input(inp)
                .Filter(filter)
                .Codec(AudioCodec.Copy)
                .Output(outp);

            FFmpeg.Instance.Run(command);
        }

        public void ChangeSpeed(Medium input, Medium output, double speed)
        {
            // $"-i input -filter_complex \"[0:v]setpts=speed*PTS[v];[0:a]atempo=speed[a]\" -map \"[v]\" -map \"[a]\" output";

            var i = input.ToString();
            var o = output.ToString();

            var filter = new FilterBuilder()
                .Add("[0:v]", "[v]", MultimediaFilter.SetPts(speed))
                .Add("[0:a]", "[a]", AudioFilter.Tempo(speed));

            var command = new CommandBuilder()
                .Input(i)
                .Filter(filter)
                .Map("\"[v]\"")
                .Map("\"[a]\"")
                .Output(o);

            FFmpeg.Instance.Run(command);
        }

        public void SeparateTrack(Medium input, Medium output, StreamType stream, int track = 0)
        {
            var inp = input.ToString();
            var outp = output.ToString();

            var command = new CommandBuilder();

            switch (stream)
            {
                case StreamType.Audio:
                    command
                        .Input(inp, out Node i1)
                        .Map(i1.GetAudioTrack(track))
                        .Codec(Codec.Copy)
                        .Output(outp);
                    break;

                case StreamType.Video:
                    command
                        .Input(inp, out Node i2)
                        .Map(i2.GetVideoTrack(track))
                        .Codec(Codec.Copy)
                        .Output(outp);
                    break;
            };

            FFmpeg.Instance.Run(command);
        }

        public enum StreamType
        {
            Audio,
            Video,
        }

        public void Convert(Medium[] inputs, Medium output)
        {
            //old params : -i {Inputs[i].Path} -c copy -bsf:v h264_mp4toannexb -f mpegts {Outputs[i].Path}
            //$"-i {Info.Inputs[i]} -map 0:v -map 0:a -c copy -bsf:v h264_mp4toannexb -f mpegts {Info.Outputs[i]}"

            for (int i = 0; i < inputs.Length; i++)
            {
                var input = inputs[i].ToString();
                var outp = output.ToString();

                var command = new CommandBuilder()
                    .Input(input, out Node i1)
                    .Map(i1.Video)
                    .Map(i1.Audio)
                    .Codec(Codec.Copy)
                    .Custom("-bsf:v h264_mp4toannexb")
                    .Custom("-f mpegts")
                    .Output(outp);

                FFmpeg.Instance.Run(command);
            }
        }

        public void CreateScreenshotAt(Medium input, Medium output, TimeSpan Time, Format Format)
        {
            var inp = input.ToString();
            var outp = output.ToString();

            var command = new CommandBuilder()
                .Input(inp)
                .Seeking(Time)
                .Custom("-frames:v 1")
                .Output(outp);

            FFmpeg.Instance.Run(command);
        }

        public void Cut(Medium input, Medium output, TimeSpan start, TimeSpan duration)
        {
            //-ss {Directions.Item1.ToString(format)} -t {Directions.Item2.ToString(format)} -i {Info.Inputs[0]} -c copy {Info.Outputs[0]}

            var inp = input.ToString();
            var outp = output.ToString();

            var command = new CommandBuilder()
                .Seeking(start)
                .Duration(duration)
                .Input(inp)
                .Codec(VideoCodec.Copy)
                .Output(outp);

            FFmpeg.Instance.Run(command);
        }

        public void Crop(Medium input, Medium output, int width, int height, int x, int y)
        {
            //$"-i {Info.Inputs[0]} -filter:v \"crop={width}:{height}:{x}:{y}\" -codec:a copy {Info.Outputs[0]}"

            var inp = input.ToString();
            var outp = output.ToString();

            var filter = new FilterBuilder()
                .Crop(width, height, x, y);

            var command = new CommandBuilder()
                .Input(inp)
                .Filter(filter)
                .Codec(AudioCodec.Copy)
                .Output(outp);

            FFmpeg.Instance.Run(command);
        }

        public void AddBackgroundImage(Medium input, Medium image, Medium output, Alignment.Vertical alignmentVert)
        {
            //Implementation of             
            // https://stackoverflow.com/questions/35269387/ffmpeg-overlay-one-video-onto-another-video

            // the clips have to be scaled to the same size best case -> same size as base video

            FFmpeg.Instance.Run($"-loop 1 -i {image} " +
                $"-i {input} " +
                $"-filter_complex \"overlay=(W-w)/2:{alignmentVert}:shortest=1,format=yuv420p\" " +
                $"{output}");
        }

        public void OverlayImage(Medium input, Medium output, Medium overlay, int x, int y)
        {
            //Implementation of             
            // https://stackoverflow.com/questions/35269387/ffmpeg-overlay-one-video-onto-another-video
            // the clips have to be scaled to the same size best case -> same size as base video

            /* Old command
             * $"-i {input} " +
             * $"-i {overlay} " +
             * $"-filter_complex \"[0:v][1:v] overlay = {x}:{y}\" " +
             * $"-pix_fmt yuv420p -c:a copy " +
             * $"{output}"
            */

            var inp = input.ToString();
            var overl = output.ToString();
            var outp = overlay.ToString();

            var command = new CommandBuilder()
                .Input(inp,out Node i1)
                .Input(overl, out Node ol)
                .Custom($"-filter_complex \"[{i1.Video}][{ol.Video}] overlay = {x}:{y}\"")
                .PixelFormat("yuv420p")
                .Codec(AudioCodec.Copy)
                .Output(outp);

            FFmpeg.Instance.Run(command);
        }

        public void ExportScreenshots(Medium input, Format.Image format, int frames, int seconds, string folder)
        {
            //"-i {input} -vf fps={frames}/{seconds} {folder}\\{input.Name}_%05d{format}"

            var inp = input.ToString();

            var filter = new FilterBuilder()
                .FramesPerSecond(frames, seconds);

            var command = new CommandBuilder()
                .Input(inp)
                .Filter(filter)
                .Output($"{folder}\\{input.Name}_%05d{format}");

            FFmpeg.Instance.Run(command);
        }

        public void LayerVideos(Medium input, Medium output, Medium overlay, TimeSpan start)
        {
            //Implementation of             
            // https://stackoverflow.com/questions/35269387/ffmpeg-overlay-one-video-onto-another-video

            // the clips have to be scaled to the same size best case -> same size as base video

            //$"-i {input} -i {overlay} -filter_complex \" [1:v]setpts=PTS+{time}/TB[a];[0:v][a]overlay[out]\"
            //-map [out] -map 0:a  -c:v libx264 -crf 18 -pix_fmt yuv420p -c:a copy  {output}"

            string time = start.TotalSeconds.ToString();
            var inp = input.ToString();
            var outp = output.ToString();

            var command = new CommandBuilder()
                .Input(inp)
                .Map("[out]")
                .Map("0:a")
                .Custom("-filter_complex \"" +
                    $"[1:v]setpts=PTS+{time}/TB[a];" +
                    $"[0:v][a]overlay[out]\"")
                .ConstantRateFactor(18)
                .PixelFormat("yuv420")
                .Codec(VideoCodec.Libx264)
                .Codec(AudioCodec.Copy)
                .Output(outp);

            FFmpeg.Instance.Run(command);
        }
    }   
}
