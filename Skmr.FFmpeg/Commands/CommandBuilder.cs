using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.FFmpeg.Commands
{
    public class CommandBuilder : BaseBuilder
    {
        public CommandBuilder Custom(string str)
        {
            commandBuilder.Append($"{str} ");
            return this;
        }

        public override string ToString()
        {
            return commandBuilder.ToString();
        }

        #region I/O

        private int inputs = 0;
        public CommandBuilder Input(string file)
        {
            commandBuilder.Append($"-i {file} ");
            inputs++;
            return this;
        }

        public int GetInputs()
        {
            return inputs;
        }

        public CommandBuilder Input(string file, out Node medium)
        {
            medium = new Node(inputs);
            return Input(file);
        }

        public CommandBuilder Output(string file)
        {
            commandBuilder.Append($"{file}");
            return this;
        }
        #endregion

        #region Codecs
        public CommandBuilder Codec(Codec codec)
        {
            commandBuilder.Append($"-c {codec} ");
            return this;
        }

        public CommandBuilder Codec(VideoCodec codec)
        {
            commandBuilder.Append($"-c:v {codec} ");
            return this;
        }

        public CommandBuilder Codec(AudioCodec codec)
        {
            commandBuilder.Append($"-c:a {codec} ");
            return this;
        }

        public CommandBuilder NoVideo()
        {
            commandBuilder.Append($"-nv ");
            return this;
        }

        public CommandBuilder NoAudio()
        {
            commandBuilder.Append($"-na ");
            return this;
        }

        public CommandBuilder PixelFormat(string format)
        {
            commandBuilder.Append($"-pix_fmt {format} ");
            return this;
        }

        public CommandBuilder ConstantRateFactor(int range)
        {
            commandBuilder.Append($"-crf {range} ");
            return this;
        }

        public CommandBuilder Map(string mapping)
        {
            commandBuilder.Append($"-map {mapping} ");
            return this;
        }
        #endregion

        #region Time
        public CommandBuilder Seeking(TimeSpan time)
        {
            commandBuilder.Append($"-ss {time.ToString(TimeFormat)} ");
            return this;
        }

        public CommandBuilder Duration(TimeSpan time)
        {
            commandBuilder.Append($"-t {time.ToString(TimeFormat)} ");
            return this;
        }

        public CommandBuilder To(TimeSpan time)
        {
            commandBuilder.Append($"-to {time.ToString(TimeFormat)} ");
            return this;
        }
        #endregion

        #region Filter

        public CommandBuilder Filter( FilterBuilder filter)
        {
            return this;
        }
        #endregion
    }
}
