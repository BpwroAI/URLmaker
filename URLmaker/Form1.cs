using System;
using System.Windows.Forms;

namespace URLmaker
{
    public partial class Form1 : Form
    {
        private bool isClosingByButton1 = false;
        private bool isClosingByButton2 = false;
        private bool isClosingByButton3 = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.makemode = 1;
            isClosingByButton1 = true;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Program.makemode = 2;
            isClosingByButton2 = true;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Program.makemode = 3;
            isClosingByButton3 = true;
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Program.Options["AutoLoad"] = checkBox1.Checked ? "True" : "False";
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            Program.Options["AutoSave"] = checkBox2.Checked ? "True" : "False";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            checkBox1.Checked = Program.Options.ContainsKey("AutoLoad") && Program.Options["AutoLoad"] == "True";
            checkBox2.Checked = Program.Options.ContainsKey("AutoSave") && Program.Options["AutoSave"] == "True";
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isClosingByButton1)
            {
                return; // ボタン1で閉じる場合は `FormClosing` をスキップ
            }
            if (isClosingByButton2)
            {
                return; // ボタン2で閉じる場合は `FormClosing` をスキップ
            }
            if (isClosingByButton3)
            {
                return; // ボタン3で閉じる場合は `FormClosing` をスキップ
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
