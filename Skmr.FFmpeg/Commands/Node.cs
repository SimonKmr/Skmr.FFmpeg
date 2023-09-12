namespace Skmr.FFmpeg.Commands
{
    public class Node
    {
        public int Id { get; set; }
        public Node(int id)
        {
            Id = id;
        }

        public override string ToString()
        {
            return Id.ToString();
        }

        public string Video => $"{Id}:v";
        public string Audio => $"{Id}:a";

        public string GetVideoTrack(int track)
            => $"{Id}:v:{track}";

        public string GetAudioTrack(int track)
            => $"{Id}:a:{track}";
    }
}