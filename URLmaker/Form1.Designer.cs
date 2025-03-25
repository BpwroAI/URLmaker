namespace URLmaker
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            button1 = new Button();
            button2 = new Button();
            checkBox1 = new CheckBox();
            checkBox2 = new CheckBox();
            label3 = new Label();
            button3 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Yu Gothic UI", 12F);
            label1.Location = new Point(12, 24);
            label1.Name = "label1";
            label1.Size = new Size(263, 42);
            label1.TabIndex = 0;
            label1.Text = "モード1：\"\"の間に/が含まれている文字を\r\n　　　　抽出しURLを作成する。";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Yu Gothic UI", 12F);
            label2.Location = new Point(12, 120);
            label2.Name = "label2";
            label2.Size = new Size(237, 42);
            label2.TabIndex = 1;
            label2.Text = "モード2：\"\"の間の文字を全て抽出し\r\n　　　　URLを作成する。";
            // 
            // button1
            // 
            button1.Font = new Font("Yu Gothic UI", 12F);
            button1.Location = new Point(323, 25);
            button1.Name = "button1";
            button1.Size = new Size(133, 45);
            button1.TabIndex = 2;
            button1.Text = "RUN";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Font = new Font("Yu Gothic UI", 12F);
            button2.Location = new Point(323, 120);
            button2.Name = "button2";
            button2.Size = new Size(133, 45);
            button2.TabIndex = 3;
            button2.Text = "RUN";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Font = new Font("Yu Gothic UI", 10F);
            checkBox1.Location = new Point(12, 292);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(212, 23);
            checkBox1.TabIndex = 4;
            checkBox1.Text = "UMOption.iniの設定を読み込む";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Font = new Font("Yu Gothic UI", 10F);
            checkBox2.Location = new Point(12, 328);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(211, 23);
            checkBox2.TabIndex = 5;
            checkBox2.Text = "UMOption.iniに設定を保存する";
            checkBox2.UseVisualStyleBackColor = true;
            checkBox2.CheckedChanged += checkBox2_CheckedChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Yu Gothic UI", 12F);
            label3.Location = new Point(12, 216);
            label3.Name = "label3";
            label3.Size = new Size(287, 42);
            label3.TabIndex = 6;
            label3.Text = "モード3：このアプリで出力した2つのファイルを\r\n　　　　比較し差分データを抽出する。";
            // 
            // button3
            // 
            button3.Font = new Font("Yu Gothic UI", 12F);
            button3.Location = new Point(323, 215);
            button3.Name = "button3";
            button3.Size = new Size(133, 45);
            button3.TabIndex = 7;
            button3.Text = "RUN";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(494, 384);
            Controls.Add(button3);
            Controls.Add(label3);
            Controls.Add(checkBox2);
            Controls.Add(checkBox1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Form1";
            Text = "モードを選択";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Button button1;
        private Button button2;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
        private Label label3;
        private Button button3;
    }
}
