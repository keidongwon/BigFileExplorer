using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using System.Windows.Forms;

namespace BigFileExplorer
{
    class Config
    {
        private static Config config = null;
        private static bool loaded = false;
        private string filename = @"config.json";
        private string contents;

        public static Config GetInstance()
        {
            if (config == null)
            {
                config = new Config();
                loaded = config.Load();
            }
            return config;
        }

        public bool Load()
        {
            try
            {
                contents = File.ReadAllText(filename);
            }
            catch(FileNotFoundException e)
            {
                Debug.WriteLine(e.ToString());
                Generate();
                MessageBox.Show("config.json 을 생성했습니다. 폴더를 추가해주세요");
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.ToString());
                MessageBox.Show(e.ToString());
                return false;
            }
            return true;
        }
        
        public void Generate()
        {
            using (StreamWriter sw = File.CreateText(filename))
            {
                sw.WriteLine("{");
                sw.WriteLine("    \"extensions\" : [\".avi\", \".mkv\", \".mp4\", \".mpg\", \".wmv\"],");
                sw.WriteLine("    \"roots\" : ");
                sw.WriteLine("    [");
                sw.WriteLine("        [\"title\", \"c:/video/child\"]");
                sw.WriteLine("    ],");
                sw.WriteLine("    \"font_face\" : \"Tahoma\",");
                sw.WriteLine("    \"view_detail\" : 1,");
                sw.WriteLine("    \"roots_width\" : 500,");
                sw.WriteLine("    \"roots_font_size\" : 30,");
                sw.WriteLine("    \"files_column\" : 600,");
                sw.WriteLine("    \"files_font_size\" : 30");
                sw.WriteLine("}");
            }
        }

        public void GetExtensions(ref List<string> ext)
        {
            if (!loaded) return;

            ext.Clear();
            try
            {
                JObject obj = JObject.Parse(contents);
                JArray array = JArray.Parse(obj["extensions"].ToString());

                foreach (string item in array)
                {
                    ext.Add(item);
                }
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }

        public void GetRootFolders(ref Dictionary<string, string> roots)
        {
            if (!loaded) return;

            roots.Clear();
            try
            {
                JObject obj = JObject.Parse(contents);
                JArray array = JArray.Parse(obj["roots"].ToString());

                foreach (JArray arr in array)
                {
                    roots.Add(arr[0].ToString(), arr[1].ToString());
                }
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }

        public string GetFontFace()
        {
            string fontface = "Tahoma";

            if (!loaded) return fontface;

            try
            {
                JObject obj = JObject.Parse(contents);
                fontface = (string)obj["font_face"];
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
            return fontface;
        }

        public void GetComponentSize(ref int roots_width, ref int roots_font_size, ref int files_column, ref int files_font_size)
        {
            if (!loaded) return;

            try
            {
                JObject obj = JObject.Parse(contents);
                roots_width = (int)obj["roots_width"];
                roots_font_size = (int)obj["roots_font_size"];
                files_column = (int)obj["files_column"];
                files_font_size = (int)obj["files_font_size"];
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }

        public bool GetViewStyle()
        {
            if (!loaded) return false;

            bool view_detail = false;
            try
            {
                JObject obj = JObject.Parse(contents);
                if ((int)obj["view_detail"] == 1)
                    view_detail = true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
            return view_detail;
        }
    }
}
