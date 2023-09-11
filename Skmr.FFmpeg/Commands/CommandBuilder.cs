using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.Commands
{
    public class CommandBuilder
    {
        public const string TimeFormat = @"hh\:mm\:ss\.\0";
        private StringBuilder commandBuilder = new StringBuilder();

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
        public CommandBuilder Input(string file)
        {
            commandBuilder.Append($"-i {file} ");
            return this;
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

        public CommandBuilder VCodec(VideoCodec codec)
        {
            commandBuilder.Append($"-c:v {codec} ");
            return this;
        }

        public CommandBuilder ACodec(AudioCodec codec)
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
    }
}
