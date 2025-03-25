using System;
using System.IO; // ファイル書き込み用
using System.Reflection;
using System.Linq;
using System.Text.RegularExpressions;

namespace URLmaker
{
    internal static class Program
    {
        public static int makemode = 0;
        public static string combinedPath2 = null;
        public static string filePath = null;
        public static Dictionary<string, string> Options = new Dictionary<string, string>();

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // 実行ファイルのフルパスを取得
            string executablePath = Assembly.GetExecutingAssembly().Location;

                // ディレクトリのパスを取得
                string directoryPath = Path.GetDirectoryName(executablePath);
                string folderName = "Output";
                string combinedPath1 = Path.Combine(directoryPath, folderName);
                try
                {
                    // ディレクトリが存在しない場合に作成
                    if (!Directory.Exists(combinedPath1))
                    {
                        Directory.CreateDirectory(combinedPath1);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"フォルダ作成時にエラーが発生しました: {ex.Message}");
                }

                //iniファイルの読み込み
                string IniFilePath = "UMOption.ini";
                combinedPath2 = Path.Combine(directoryPath, IniFilePath);
                if (File.Exists(combinedPath2))
                {
                    CreateIniFile(combinedPath2);
                }

                Program.Options = LoadIniFile(combinedPath2);

                EnsureOptionExists("AutoLoad", "True");
                EnsureOptionExists("AutoSave", "True");
                EnsureOptionExists("NumCharExc", "2");
                EnsureOptionExists("SkipChar", ":;,");
                EnsureOptionExists("URLStr", "https://");

                ApplicationConfiguration.Initialize();
                Form1 form1 = new Form1();
                Application.Run(form1);


            if (makemode == 1 || makemode == 2)
                {
                    
                    if (args.Length == 0) 
                    {
                        OpenFileDialog openFileDialog = new OpenFileDialog
                        {
                            Title = "ファイルを選択してください",
                            Filter = "すべてのファイル (*.*)|*.*", // 必要に応じて拡張子を指定
                            Multiselect = false // 1つのファイルのみ選択可能
                        };

                        if (openFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            // 選択されたファイルを擬似的に args に格納
                            args = new string[] { openFileDialog.FileName };

                        }
                        else
                        {
                            Console.WriteLine("ファイルが選択されませんでした。");
                            return;
                        }
                    }
                if (args.Length > 0)
                {
                    Program.filePath = args[0];
                    Form2 form2 = new Form2();
                    form2.ShowDialog();

                    for (int i = 0; i < args.Length; i++)
                    {
                        filePath = args[i];
                        if (File.Exists(filePath))
                        {

                            // 入力データを読み込み
                            string inputText = File.ReadAllText(filePath);
                            string pattern = null;
                            // 正規表現で"と"の間に"/"がある部分を抽出
                            //string pattern = "\"(.*?)\".*?/(.*?)\"";
                            if (makemode == 1)
                            {
                                pattern = "\"([^\"]*/[^\"]*)\"";
                            }
                            else
                            {
                                pattern = "\"([^\"]+)\"";
                            }


                            MatchCollection matches = Regex.Matches(inputText, pattern);

                            HashSet<string> extractedTexts = new HashSet<string>(); // 重複を排除するためHashSetを使用

                            // 結果を保存するテキストファイルのパス
                            string content = File.ReadAllText(filePath);
                            string fileName = Path.GetFileName(filePath);
                            string inputFilePath = filePath;
                            string originalFileName = Path.GetFileNameWithoutExtension(inputFilePath);
                            string fileExtension = Path.GetExtension(inputFilePath);
                            string timestamp = DateTime.Now.ToString("yyyyMMddHHmm");
                            string fileNameAdd = $"{originalFileName} - {timestamp}{fileExtension}";
                            string outputFilePath = Path.Combine(combinedPath1, fileNameAdd);

                            // 抽出したテキストを条件に基づいてフィルタリングしてHashSetに追加
                            foreach (Match match in matches)
                            {
                                string extractedText1 = match.Groups[1].Value;
                                // 条件 1: ""内の文字数が NumCharExc を超えているか
                                if (Program.Options.ContainsKey("NumCharExc") && Program.Options["NumCharExc"] != "0")
                                {
                                    if (int.TryParse(Program.Options["NumCharExc"], out int NumCharExc2)) // NumCharExcを数値に変換
                                    {
                                        if (match.Groups[1].Value.Length <= NumCharExc2)
                                        {
                                            continue; // 条件に合わない場合スキップ
                                        }
                                    }
                                }
                                // 条件 2: SkipChar に含まれる禁止文字が存在しないか
                                if (Program.Options.ContainsKey("SkipChar") && Program.Options["SkipChar"] != "")
                                {
                                    if (ContainsSkipChar(match.Groups[1].Value, Program.Options["SkipChar"]))
                                    {
                                        continue; // 条件に合わない場合スキップ
                                    }
                                }

                                // 条件を満たす場合のみHashSetに追加 (URLshrを先頭に付ける)
                                extractedTexts.Add(Program.Options["URLStr"] + extractedText1);
                            }

                            // 重複を排除したテキストをファイルに書き込み
                            File.WriteAllLines(outputFilePath, extractedTexts);
                        }
                    }
                    if (Program.Options["AutoSave"] == "True")
                    {
                        SaveIniFile(combinedPath2, Program.Options);
                    }
                }
                }
                else if (makemode == 3)
                {
                    string filePath1 = SelectFile("最初のファイルを選択してください");
                    if (string.IsNullOrEmpty(filePath1)) return; // ファイル選択キャンセル時は終了

                    string filePath2;
                    do
                    {
                        filePath2 = SelectFile("比較する2つ目のファイルを選択してください");
                        if (string.IsNullOrEmpty(filePath2)) return; // キャンセルされた場合は終了

                        if (filePath1 == filePath2)
                        {
                            MessageBox.Show("同じファイルが選択されました。別のファイルを選択してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    } while (filePath1 == filePath2); // 同じファイルが選ばれたら再選択

                    //2つのファイルを比較して重複しないURLを抽出
                    HashSet<string> uniqueUrls = ExtractUniqueUrls(filePath1, filePath2);

                    // 出力ファイルを作成
                    string inputFilePath3 = filePath1;
                    string originalFileName3 = Path.GetFileNameWithoutExtension(inputFilePath3);
                    string fileExtension3 = Path.GetExtension(inputFilePath3);
                    string timestamp3 = DateTime.Now.ToString("yyyyMMdd");
                    string fileNameAdd3 = $"{originalFileName3} - {timestamp3}[差分]{fileExtension3}";
                    string outputFilePath3 = Path.Combine(combinedPath1, fileNameAdd3);
                    File.WriteAllLines(outputFilePath3, uniqueUrls);
                }
                else {
                    Console.WriteLine("ファイルが選択されませんでした");
                }
            MessageBox.Show("処理が完了しました！", "完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        static void CreateIniFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine("AutoLoad=True");
                    writer.WriteLine("AutoSave=True");
                    writer.WriteLine("NumCharExc=2");
                    writer.WriteLine("SkipChar=:;,");
                    writer.WriteLine("URLStr=https://");
                }
                Console.WriteLine($"{filePath} を作成しました。");
            }
        }

        static Dictionary<string, string> LoadIniFile(string IniFilePath)
        {
            var options = new Dictionary<string, string>();
            if (File.Exists(IniFilePath))
            {
                foreach (var line in File.ReadLines(IniFilePath))
                {
                    if (line.Contains("="))
                    {
                        var parts = line.Split('=');
                        if (parts.Length == 2)
                        {
                            options[parts[0].Trim()] = parts[1].Trim();
                        }
                    }
                }
            }
            return options;
        }

        static void SaveIniFile(string filePath, Dictionary<string, string> options)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var kvp in options)
                {
                    writer.WriteLine($"{kvp.Key}={kvp.Value}");
                }
            }
            Console.WriteLine($"{filePath} を更新しました。");
        }

        static void EnsureOptionExists(string key, string defaultValue)
        {
            if (!Program.Options.ContainsKey(key))
            {
                Program.Options[key] = defaultValue;
            }
        }

        // 禁止文字が含まれているかを判定する関数
        static bool ContainsSkipChar(string text, string skipChars)
        {
            foreach (char c in skipChars)
            {
                if (text.Contains(c.ToString()))
                {
                    return true; // 禁止文字が含まれている場合
                }
            }
            return false;
        }
        static string SelectFile(string title)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = title;
                openFileDialog.Filter = "すべてのファイル (*.*)|*.*";
                openFileDialog.Multiselect = false;

                return (openFileDialog.ShowDialog() == DialogResult.OK) ? openFileDialog.FileName : null;
            }
        }

        // 2つのファイルを比較して、重複しないURLを抽出
        static HashSet<string> ExtractUniqueUrls(string file1, string file2)
        {
            HashSet<string> urls1 = new HashSet<string>(File.ReadAllLines(file1));
            HashSet<string> urls2 = new HashSet<string>(File.ReadAllLines(file2));

            // `urls1` にあって `urls2` にないもの & `urls2` にあって `urls1` にないもの
            HashSet<string> uniqueUrls = new HashSet<string>(urls1.Except(urls2).Concat(urls2.Except(urls1)));

            return uniqueUrls;
        }
    }
}