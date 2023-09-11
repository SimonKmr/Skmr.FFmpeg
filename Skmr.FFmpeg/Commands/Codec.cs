using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.Commands
{
    public class Codec
    {
        private string codec;
        public Codec(string codec)
        {
            this.codec = codec;
        }

        public override string ToString() => codec;

        public static Codec Copy => new Codec("copy");

    }
}
