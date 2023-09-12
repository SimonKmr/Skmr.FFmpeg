using Skmr.FFmpeg.Commands;
using Skmr.FFmpeg.Media;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.FFmpeg.Instructions
{
    public class ColorGradeVideo { 

        public float Contrast { get; set; } = 1;
        public float Brighness { get; set; } = 0;
        public float Saturation { get; set; } = 1;
        public float Gamma { get; set; } = 1;
        public float GammaR { get; set; } = 1;
        public float GammaG { get; set; } = 1;
        public float GammaB { get; set; } = 1;
        public float GammaWeight { get; set; } = 1;
        

        public void Run(Medium input, Medium output)
        {
            var inp = input.ToString();
            var outp = output.ToString();

            var ci = new CultureInfo("en-US");
            var sb = new CommandBuilder();
            sb.Input(inp)
                .Custom("-vf eq=")
                .Custom($"contrast={Contrast.ToString(ci)}:")
                .Custom($"brightness={Brighness.ToString(ci)}:")
                .Custom($"saturation={Saturation.ToString(ci)}:")
                .Custom($"gamma={Gamma.ToString(ci)}:")
                .Custom($"gamma_r={GammaR.ToString(ci)}:")
                .Custom($"gamma_g={GammaG.ToString(ci)}:")
                .Custom($"gamma_b={GammaB.ToString(ci)}:")
                .Custom($"gamma_weight={GammaWeight.ToString(ci)}")
                .Output(outp);

            FFmpeg.Instance.Run(sb.ToString());
        }
    }
}
