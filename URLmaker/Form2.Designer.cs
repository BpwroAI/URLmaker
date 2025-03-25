namespace URLmaker
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            label2 = new Label();
            label3 = new Label();
            textBox3 = new TextBox();
            label4 = new Label();
            groupBox1 = new GroupBox();
            button1 = new Button();
            button2 = new Button();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Yu Gothic UI", 12F);
            label1.Location = new Point(12, 16);
            label1.Name = "label1";
            label1.Size = new Size(410, 42);
            label1.TabIndex = 0;
            label1.Text = "スキップする文字数：\r\n（文字列の文字数が指定数値以下のはURLを作成しません）";
            label1.TextAlign = ContentAlignment.TopRight;
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Yu Gothic UI", 12F);
            textBox1.Location = new Point(428, 23);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(74, 29);
            textBox1.TabIndex = 1;
            textBox1.Text = "99";
            textBox1.TextAlign = HorizontalAlignment.Right;
            // 
            // textBox2
            // 
            textBox2.Font = new Font("Yu Gothic UI", 12F);
            textBox2.Location = new Point(428, 95);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(280, 29);
            textBox2.TabIndex = 2;
            textBox2.Text = ":;,";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Yu Gothic UI", 12F);
            label2.Location = new Point(82, 86);
            label2.Name = "label2";
            label2.Size = new Size(340, 42);
            label2.TabIndex = 3;
            label2.Text = "スキップする文字：\r\n（指定した文字を含む場合はURLを作成しません）";
            label2.TextAlign = ContentAlignment.TopRight;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Yu Gothic UI", 12F);
            label3.Location = new Point(205, 162);
            label3.Name = "label3";
            label3.Size = new Size(217, 21);
            label3.TabIndex = 4;
            label3.Text = "URLの先頭に付与する文字列：";
            label3.TextAlign = ContentAlignment.TopRight;
            // 
            // textBox3
            // 
            textBox3.Font = new Font("Yu Gothic UI", 12F);
            textBox3.Location = new Point(428, 159);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(280, 29);
            textBox3.TabIndex = 5;
            textBox3.Text = "https://";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Yu Gothic UI", 10F);
            label4.Location = new Point(6, 19);
            label4.Name = "label4";
            label4.Size = new Size(266, 19);
            label4.TabIndex = 6;
            label4.Text = "Previewを押すと生成サンプルが表示されます。";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label4);
            groupBox1.Location = new Point(12, 272);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(696, 166);
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            groupBox1.Text = "URLサンプル";
            // 
            // button1
            // 
            button1.Font = new Font("Yu Gothic UI", 12F);
            button1.Location = new Point(575, 221);
            button1.Name = "button1";
            button1.Size = new Size(133, 45);
            button1.TabIndex = 8;
            button1.Text = "RUN";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Font = new Font("Yu Gothic UI", 12F);
            button2.Location = new Point(12, 234);
            button2.Name = "button2";
            button2.Size = new Size(133, 32);
            button2.TabIndex = 9;
            button2.Text = "Preview";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(729, 450);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(groupBox1);
            Controls.Add(textBox3);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Name = "Form2";
            Text = "生成設定";
            FormClosing += Form2_FormClosing;
            Load += Form2_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox textBox1;
        private TextBox textBox2;
        private Label label2;
        private Label label3;
        private TextBox textBox3;
        private Label label4;
        private GroupBox groupBox1;
        private Button button1;
        private Button button2;
    }
}