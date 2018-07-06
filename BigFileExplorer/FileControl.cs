using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace BigFileExplorer
{
    public class FolderComparer : IComparer<string>
    {
        class Folder : IComparable<Folder>
        {
            private Folder()
            {
                Name = string.Empty;
                Index = int.MinValue;
            }

            public Folder(string folder)
                : this()
            {
                if (string.IsNullOrEmpty(folder)) return;
                var match = Regex.Matches(folder, @"[a-zA-Z]+|\d+");
                if (match.Count == 0) return;
                var alp = match[0].Value;
                if (match.Count == 1) return;
                var temp = match[1].Value;
                if (!string.IsNullOrEmpty(temp))
                {
                    Index = int.Parse(temp);
                }
            }
            public string Name { get; set; }
            public int Index { get; set; }

            public int CompareTo(Folder other)
            {
                var result = Name.CompareTo(other.Name);
                return result == 0 ? Index.CompareTo(other.Index) : result;
            }
        }

        public int Compare(string left, string right)
        {
            return new Folder(left).CompareTo(new Folder(right));
        }
    }

    class FileControl
    {
        Config config = Config.GetInstance();
        private List<string> extensions = new List<string>();

        public FileControl()
        {
            config.GetExtensions(ref extensions);
        }

        public bool GetFiles(String path, ref Dictionary<string, string> dirs, ref Dictionary<string, string> files)
        {
            if (path == "") return false;
            DirectoryInfo DirInfo = new DirectoryInfo(path);
            List<string> strDirs =DirInfo.EnumerateDirectories()
                   .OrderBy(d => d.Name)
                   .Select(d => d.FullName)
                   .ToList();

            foreach (string s in strDirs)
            {
                int pos = s.LastIndexOf("\\");
                string dir = s.Substring(pos + 1);
                dirs.Add(dir, s);
            }

            Dictionary<string, string> tmpFiles = new Dictionary<string,string>();

            var lstFiles = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories)
                    .Where(s => extensions.Contains(Path.GetExtension(s)))
                        .Select(f => new FileInfo(f)).OrderBy(f => f.FullName);

            foreach (var item in lstFiles)
            {
                string s = item.FullName;
                Debug.WriteLine(s);
                int pos1 = s.LastIndexOf("\\");
                int pos2 = s.LastIndexOf(".");
                int length = s.Length - pos1 - (s.Length - pos2) - 1;
                Debug.WriteLine(pos1 + " " + pos2 + " " + length);
                string file = s.Substring(pos1+1, length);
                tmpFiles.Add(file, s);
            }

            var list = tmpFiles.Keys.ToList();
            foreach(string key in list)
            {
                files.Add(key, tmpFiles[key]);
            }
            return true;
        }
    }
}
