using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.Commands
{
    public class AudioCodec
    {
        private string codec;
        public AudioCodec(string codec)
        {
            this.codec = codec;
        }

        public override string ToString()
        {
            return codec;
        }

        public static AudioCodec Copy => new AudioCodec("copy");
    }
}
