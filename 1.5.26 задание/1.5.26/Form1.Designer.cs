
namespace QuizApp
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.Button button1;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            label1 = new Label();
            radioButton1 = new RadioButton();
            radioButton2 = new RadioButton();
            radioButton3 = new RadioButton();
            radioButton4 = new RadioButton();
            button1 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(360, 40);
            label1.TabIndex = 0;
            label1.Text = "Вопрос";
            // 
            // radioButton1
            // 
            radioButton1.Location = new Point(15, 60);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(350, 24);
            radioButton1.TabIndex = 1;
            // 
            // radioButton2
            // 
            radioButton2.Location = new Point(15, 90);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(350, 24);
            radioButton2.TabIndex = 2;
            // 
            // radioButton3
            // 
            radioButton3.Location = new Point(15, 120);
            radioButton3.Name = "radioButton3";
            radioButton3.Size = new Size(350, 24);
            radioButton3.TabIndex = 3;
            // 
            // radioButton4
            // 
            radioButton4.Location = new Point(15, 150);
            radioButton4.Name = "radioButton4";
            radioButton4.Size = new Size(350, 24);
            radioButton4.TabIndex = 4;
            // 
            // button1
            // 
            button1.Location = new Point(15, 190);
            button1.Name = "button1";
            button1.Size = new Size(100, 30);
            button1.TabIndex = 5;
            button1.Text = "Проверить";
            button1.Click += button1_Click;
            // 
            // Form1
            // 
            BackColor = SystemColors.GradientActiveCaption;
            ClientSize = new Size(384, 241);
            Controls.Add(label1);
            Controls.Add(radioButton1);
            Controls.Add(radioButton2);
            Controls.Add(radioButton3);
            Controls.Add(radioButton4);
            Controls.Add(button1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "Тест на знания";
            ResumeLayout(false);
        }
    }
}
