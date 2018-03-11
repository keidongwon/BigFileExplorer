using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;
using System.Diagnostics;

namespace BigFileExplorer
{
    class FileControl
    {
        Config config = Config.GetInstance();
        private ArrayList extensions = new ArrayList();

        public FileControl()
        {
            config.GetExtensions(ref extensions);
        }

        public bool GetFiles(String folder, ref Dictionary<string, string> dirs, ref Dictionary<string, string> files)
        {
            if (folder == "") return false;

            string[] strDirs = Directory.GetDirectories(folder);
            foreach (string s in strDirs)
            {
                int pos = s.LastIndexOf("\\");
                string dir = s.Substring(pos + 1);
                dirs.Add(dir, s);
            }

            Dictionary<string, string> tmpFiles = new Dictionary<string,string>();
            foreach (string ext in extensions)
            {
                var lstFiles = Directory.GetFiles(folder, ext).Select(f => new FileInfo(f)).OrderBy(f => f.FullName);
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
