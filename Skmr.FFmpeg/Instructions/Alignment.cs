using Skmr.FFmpeg.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skmr.FFmpeg.Instructions
{
    public class Alignment
    {
        public class Vertical
        {
            public static Vertical Top { get => new Vertical("0");  }
            public static Vertical Center { get => new Vertical("(H-h)/2"); }
            public static Vertical Bottom { get => new Vertical("(H-h)"); }


            public string Value { get; set; }
            public Vertical(string value)
            {
                Value = value;
            }
            public Vertical()
            {
                Value = Vertical.Center.Value;
            }
            public override string ToString() => Value;
        }

    }
}
