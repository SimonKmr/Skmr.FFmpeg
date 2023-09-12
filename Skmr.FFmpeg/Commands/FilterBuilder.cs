using System;

namespace Skmr.FFmpeg.Commands
{
    public class FilterBuilder : BaseBuilder
    {
        public FilterBuilder Add(string input, string output, BaseFilter filter)
        {
            throw new NotImplementedException();
        }

        public FilterBuilder Scale(int width, int height)
        {
            commandBuilder.Append($"\"scale={width}:{height}\"");
            return this;
        }

        public FilterBuilder Crop(int width, int height, int x, int y)
        {
            commandBuilder.Append($"\"crop={width}:{height}:{x}:{y}\"");
            return this;
        }

        public FilterBuilder FramesPerSecond(int frames, int seconds)
        {
            commandBuilder.Append($"fps={frames}/{seconds}");
            return this;
        }

        public override string ToString()
        {
            //also -vf
            return $"-filter:v {commandBuilder} ";
        }
    }
}
