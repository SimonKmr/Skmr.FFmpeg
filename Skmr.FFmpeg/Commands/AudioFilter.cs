using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.FFmpeg.Commands
{
    public class AudioFilter : BaseFilter
    {
        public AudioFilter(string str)
        {
            base.str = str;
        }

        public static AudioFilter Tempo(double speed)
        {
            var ci = new CultureInfo("en-US");
            return new AudioFilter($"atempo={speed.ToString(ci)}");
        }
    }
}
