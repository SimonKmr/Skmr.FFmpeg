using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.FFmpeg.Commands
{
    public class VideoFilter : BaseFilter
    {
        private VideoFilter(string str)
        {
            base.str = str;
        }

        public static VideoFilter Scale(int width, int height) 
            => new VideoFilter($"scale={width}:{height}");

    }
}
