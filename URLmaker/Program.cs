using System;
using System.IO; // �t�@�C���������ݗp
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
            // ���s�t�@�C���̃t���p�X���擾
            string executablePath = Assembly.GetExecutingAssembly().Location;

                // �f�B���N�g���̃p�X���擾
                string directoryPath = Path.GetDirectoryName(executablePath);
                string folderName = "Output";
                string combinedPath1 = Path.Combine(directoryPath, folderName);
                try
                {
                    // �f�B���N�g�������݂��Ȃ��ꍇ�ɍ쐬
                    if (!Directory.Exists(combinedPath1))
                    {
                        Directory.CreateDirectory(combinedPath1);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"�t�H���_�쐬���ɃG���[���������܂���: {ex.Message}");
                }

                //ini�t�@�C���̓ǂݍ���
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
                            Title = "�t�@�C����I�����Ă�������",
                            Filter = "���ׂẴt�@�C�� (*.*)|*.*", // �K�v�ɉ����Ċg���q���w��
                            Multiselect = false // 1�̃t�@�C���̂ݑI���\
                        };

                        if (openFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            // �I�����ꂽ�t�@�C�����[���I�� args �Ɋi�[
                            args = new string[] { openFileDialog.FileName };

                        }
                        else
                        {
                            Console.WriteLine("�t�@�C�����I������܂���ł����B");
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

                            // ���̓f�[�^��ǂݍ���
                            string inputText = File.ReadAllText(filePath);
                            string pattern = null;
                            // ���K�\����"��"�̊Ԃ�"/"�����镔���𒊏o
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

                            HashSet<string> extractedTexts = new HashSet<string>(); // �d����r�����邽��HashSet���g�p

                            // ���ʂ�ۑ�����e�L�X�g�t�@�C���̃p�X
                            string content = File.ReadAllText(filePath);
                            string fileName = Path.GetFileName(filePath);
                            string inputFilePath = filePath;
                            string originalFileName = Path.GetFileNameWithoutExtension(inputFilePath);
                            string fileExtension = Path.GetExtension(inputFilePath);
                            string timestamp = DateTime.Now.ToString("yyyyMMddHHmm");
                            string fileNameAdd = $"{originalFileName} - {timestamp}{fileExtension}";
                            string outputFilePath = Path.Combine(combinedPath1, fileNameAdd);

                            // ���o�����e�L�X�g�������Ɋ�Â��ăt�B���^�����O����HashSet�ɒǉ�
                            foreach (Match match in matches)
                            {
                                string extractedText1 = match.Groups[1].Value;
                                // ���� 1: ""���̕������� NumCharExc �𒴂��Ă��邩
                                if (Program.Options.ContainsKey("NumCharExc") && Program.Options["NumCharExc"] != "0")
                                {
                                    if (int.TryParse(Program.Options["NumCharExc"], out int NumCharExc2)) // NumCharExc�𐔒l�ɕϊ�
                                    {
                                        if (match.Groups[1].Value.Length <= NumCharExc2)
                                        {
                                            continue; // �����ɍ���Ȃ��ꍇ�X�L�b�v
                                        }
                                    }
                                }
                                // ���� 2: SkipChar �Ɋ܂܂��֎~���������݂��Ȃ���
                                if (Program.Options.ContainsKey("SkipChar") && Program.Options["SkipChar"] != "")
                                {
                                    if (ContainsSkipChar(match.Groups[1].Value, Program.Options["SkipChar"]))
                                    {
                                        continue; // �����ɍ���Ȃ��ꍇ�X�L�b�v
                                    }
                                }

                                // �����𖞂����ꍇ�̂�HashSet�ɒǉ� (URLshr��擪�ɕt����)
                                extractedTexts.Add(Program.Options["URLStr"] + extractedText1);
                            }

                            // �d����r�������e�L�X�g���t�@�C���ɏ�������
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
                    string filePath1 = SelectFile("�ŏ��̃t�@�C����I�����Ă�������");
                    if (string.IsNullOrEmpty(filePath1)) return; // �t�@�C���I���L�����Z�����͏I��

                    string filePath2;
                    do
                    {
                        filePath2 = SelectFile("��r����2�ڂ̃t�@�C����I�����Ă�������");
                        if (string.IsNullOrEmpty(filePath2)) return; // �L�����Z�����ꂽ�ꍇ�͏I��

                        if (filePath1 == filePath2)
                        {
                            MessageBox.Show("�����t�@�C�����I������܂����B�ʂ̃t�@�C����I�����Ă��������B", "�G���[", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    } while (filePath1 == filePath2); // �����t�@�C�����I�΂ꂽ��đI��

                    //2�̃t�@�C�����r���ďd�����Ȃ�URL�𒊏o
                    HashSet<string> uniqueUrls = ExtractUniqueUrls(filePath1, filePath2);

                    // �o�̓t�@�C�����쐬
                    string inputFilePath3 = filePath1;
                    string originalFileName3 = Path.GetFileNameWithoutExtension(inputFilePath3);
                    string fileExtension3 = Path.GetExtension(inputFilePath3);
                    string timestamp3 = DateTime.Now.ToString("yyyyMMdd");
                    string fileNameAdd3 = $"{originalFileName3} - {timestamp3}[����]{fileExtension3}";
                    string outputFilePath3 = Path.Combine(combinedPath1, fileNameAdd3);
                    File.WriteAllLines(outputFilePath3, uniqueUrls);
                }
                else {
                    Console.WriteLine("�t�@�C�����I������܂���ł���");
                }
            MessageBox.Show("�������������܂����I", "����", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                Console.WriteLine($"{filePath} ���쐬���܂����B");
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
            Console.WriteLine($"{filePath} ���X�V���܂����B");
        }

        static void EnsureOptionExists(string key, string defaultValue)
        {
            if (!Program.Options.ContainsKey(key))
            {
                Program.Options[key] = defaultValue;
            }
        }

        // �֎~�������܂܂�Ă��邩�𔻒肷��֐�
        static bool ContainsSkipChar(string text, string skipChars)
        {
            foreach (char c in skipChars)
            {
                if (text.Contains(c.ToString()))
                {
                    return true; // �֎~�������܂܂�Ă���ꍇ
                }
            }
            return false;
        }
        static string SelectFile(string title)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = title;
                openFileDialog.Filter = "���ׂẴt�@�C�� (*.*)|*.*";
                openFileDialog.Multiselect = false;

                return (openFileDialog.ShowDialog() == DialogResult.OK) ? openFileDialog.FileName : null;
            }
        }

        // 2�̃t�@�C�����r���āA�d�����Ȃ�URL�𒊏o
        static HashSet<string> ExtractUniqueUrls(string file1, string file2)
        {
            HashSet<string> urls1 = new HashSet<string>(File.ReadAllLines(file1));
            HashSet<string> urls2 = new HashSet<string>(File.ReadAllLines(file2));

            // `urls1` �ɂ����� `urls2` �ɂȂ����� & `urls2` �ɂ����� `urls1` �ɂȂ�����
            HashSet<string> uniqueUrls = new HashSet<string>(urls1.Except(urls2).Concat(urls2.Except(urls1)));

            return uniqueUrls;
        }
    }
}