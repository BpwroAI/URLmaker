using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace URLmaker
{
    public partial class Form2 : Form
    {
        private bool isClosingByButton1 = false;
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (int.TryParse(this.textBox1.Text, out int NumCharExcNum))
            {
                Program.Options["NumCharExc"] = NumCharExcNum.ToString();
            }
            else
            {
                MessageBox.Show("「スキップする文字数」に入力されているものは数値ではありません。\n半角数字で正しい数値を入力してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // 処理を中断
            }

            Program.Options["SkipChar"] = textBox2.Text;
            Program.Options["URLStr"] = textBox3.Text;
            Program.Options["URLStrEnd"] = textBox4.Text;
            isClosingByButton1 = true;
            this.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (Program.Options.ContainsKey("AutoLoad") && Program.Options["AutoLoad"] == "True")
            {
                if (int.TryParse(Program.Options["NumCharExc"], out int NumCharExcNum))
                {
                    this.textBox1.Text = NumCharExcNum.ToString();
                }
                else
                {
                    this.textBox1.Text = "0";
                }
                textBox2.Text = Program.Options.ContainsKey("SkipChar") ? Program.Options["SkipChar"] : "";
                textBox3.Text = Program.Options.ContainsKey("URLStr") ? Program.Options["URLStr"] : "";
                textBox4.Text = Program.Options.ContainsKey("URLStrEnd") ? Program.Options["URLStrEnd"] : "";

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Program.filePath) || !File.Exists(Program.filePath))
            {
                MessageBox.Show("有効なファイルが選択されていません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string inputTextPre = File.ReadAllText(Program.filePath);
            string patternPre = null;

            if (int.TryParse(this.textBox1.Text, out int NumCharExcNum))
            {
                Program.Options["NumCharExc"] = NumCharExcNum.ToString();
            }
            else
            {
                MessageBox.Show("「スキップする文字数」に入力されているものは数値ではありません。\n半角数字で正しい数値を入力してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // 処理を中断
            }
            Program.Options["SkipChar"] = textBox2.Text;
            Program.Options["URLStr"] = textBox3.Text;
            Program.Options["URLStrEnd"] = textBox4.Text;
            // 正規表現で"と"の間に"/"がある部分を抽出
            //string pattern = "\"(.*?)\".*?/(.*?)\"";

            if (Program.makemode == 1)
            {
                patternPre = "\"([^\"]*/[^\"]*)\"";
            }
            else
            {
                patternPre = "\"([^\"]+)\"";
            }

            MatchCollection matchesPre = Regex.Matches(inputTextPre, patternPre);

            HashSet<string> extractedTextsPre = new HashSet<string>();
            foreach (Match matchPre in matchesPre)
            {
                string extractedTextPre = matchPre.Groups[1].Value;
                // 条件 1: ""内の文字数が NumCharExc を超えているか
                if (Program.Options.ContainsKey("NumCharExc") && Program.Options["NumCharExc"] != "0")
                {
                    if (int.TryParse(Program.Options["NumCharExc"], out int NumCharExc2)) // NumCharExcを数値に変換
                    {
                        if (matchPre.Groups[1].Value.Length <= NumCharExc2)
                        {
                            continue; // 条件に合わない場合スキップ
                        }
                    }
                }
                // 条件 2: SkipChar に含まれる禁止文字が存在しないか
                if (Program.Options.ContainsKey("SkipChar") && !string.IsNullOrEmpty(Program.Options["SkipChar"]))
                {
                    if (ContainsSkipCharPre(matchPre.Groups[1].Value, Program.Options["SkipChar"]))
                    {
                        continue; // 条件に合わない場合スキップ
                    }
                }

                // 条件を満たす場合のみHashSetに追加 (URLshrを先頭に付ける)
                extractedTextsPre.Add(Program.Options["URLStr"] + extractedTextPre + Program.Options["URLStrEnd"]);
            }
            List<string> textLines = extractedTextsPre.Take(30).ToList();
            this.textPreBox.Text = string.Join(Environment.NewLine, textLines);
        }

        static bool ContainsSkipCharPre(string text, string skipChars)
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
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isClosingByButton1)
            {
                return; // ボタン1で閉じる場合は `FormClosing` をスキップ
            }
            DialogResult result = MessageBox.Show(
                "プログラムを終了しますか？",
                "確認",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.No)
            {
                e.Cancel = true; // 閉じるのをキャンセル
            }
            else
            {
                Environment.Exit(0); // プログラム終了
            }
        }
    }
}
