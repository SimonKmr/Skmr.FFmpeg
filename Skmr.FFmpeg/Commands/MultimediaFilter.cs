using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.FFmpeg.Commands
{
    internal class MultimediaFilter : BaseFilter
    {
        private MultimediaFilter(string str)
        {
            base.str = str;
        }

        public static MultimediaFilter SetPts(double speed)
        {
            var ci = new CultureInfo("en-US");
            double invertedSpeed = 1 / speed;
            return new MultimediaFilter($"setpts={invertedSpeed.ToString(ci)}*PTS"); 
        }
    }
}
