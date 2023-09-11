using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.Commands
{
    public class VideoCodec
    {
        private string codec;
        public VideoCodec(string codec)
        {
            this.codec = codec;
        }

        public override string ToString()
        {
            return codec;
        }

        public static VideoCodec Copy => new VideoCodec("copy");
        public static VideoCodec Libx264() => new VideoCodec("libx264");
    }
}
