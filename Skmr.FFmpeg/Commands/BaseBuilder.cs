using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.FFmpeg.Commands
{
    public abstract class BaseBuilder
    {
        public const string TimeFormat = @"hh\:mm\:ss\.\0";
        internal StringBuilder commandBuilder = new StringBuilder();
    }
}
