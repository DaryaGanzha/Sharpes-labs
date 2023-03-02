using System;

namespace Backups.Services
{
    public class JobObject
    {
        public JobObject(string path)
        {
            int piece = path.LastIndexOf(@"\", StringComparison.Ordinal);
            Path = path;
            Name = Path[(piece + 1) ..];
            piece = Name.LastIndexOf(@".", StringComparison.Ordinal);
            ShortName = Name.Remove(piece, Name.Length - piece);
        }

        public string Path { get; }
        public string ShortName { get; }
        public string Name { get; }
    }
}