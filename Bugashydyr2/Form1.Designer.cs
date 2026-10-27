using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Bugashydyr2
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить ресурсы.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing &&
                (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.BurlyWood;
            ClientSize = new Size(900, 850);
            Name = "Form1";
            Text = "Буга-Шыдыраа";
            this.btnRestart = new System.Windows.Forms.Button();

            this.btnRestart.Location =
                new System.Drawing.Point(800, 30);

            this.btnRestart.Size =
                new System.Drawing.Size(170, 40);

            this.btnRestart.Text = "Начать заново";

            this.btnRestart.Click +=
                new System.EventHandler(this.btnRestart_Click);

            this.Controls.Add(this.btnRestart);
            
            this.btnRules = new System.Windows.Forms.Button();

            this.btnRules.Location =
                new System.Drawing.Point(1000, 30);

            this.btnRules.Size =
                new System.Drawing.Size(170, 40);

            this.btnRules.Text = "Правила";

            this.btnRules.Click +=
                new System.EventHandler(this.btnRules_Click);

            this.Controls.Add(this.btnRules);
            ResumeLayout(false);
        }
        private System.Windows.Forms.Button btnRestart;
        private System.Windows.Forms.Button btnRules;
        #endregion
    }
}