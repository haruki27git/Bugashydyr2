using Bugashydyr2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Bugashydyr2
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();

            string imagePath = System.IO.Path.Combine(
            Environment.GetFolderPath(
            Environment.SpecialFolder.MyPictures),
            "BugashydyrScreenshots", "menu.jpg");

            if (System.IO.File.Exists(imagePath))
            {
                this.BackgroundImage = Image.FromFile(imagePath);
            }
            else
            {
                MessageBox.Show("Не найден файл:\n" + imagePath);
            }

            this.Text = "Буга-Шыдыраа";
            this.Size = new Size(900, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackgroundImageLayout = ImageLayout.Stretch;

            CreateMenu();
        }

        private void CreateMenu()
        {
            Label title = new Label();

            title.Text = "БУГА-ШЫДЫРАА";
            title.Font = new Font("Arial", 32, FontStyle.Bold);
            title.ForeColor = Color.DarkGoldenrod;
            title.AutoSize = true;
            title.Location = new Point(250, 100);

            this.Controls.Add(title);

            Button play = new Button();

            play.Text = "НОВАЯ ИГРА";
            play.Size = new Size(250, 60);
            play.Location = new Point(310, 250);
            play.Font = new Font("Arial", 16, FontStyle.Bold);

            play.Click += Play_Click;

            this.Controls.Add(play);

            Button rules = new Button();

            rules.Text = "ПРАВИЛА ИГРЫ";
            rules.Size = new Size(250, 60);
            rules.Location = new Point(310, 330);
            rules.Font = new Font("Arial", 16);

            rules.Click += Rules_Click;

            this.Controls.Add(rules);

            Button exit = new Button();

            exit.Text = "ВЫХОД";
            exit.Size = new Size(250, 60);
            exit.Location = new Point(310, 410);
            exit.Font = new Font("Arial", 16);

            exit.Click += (s, e) => Application.Exit();

            this.Controls.Add(exit);
        }

        private void Play_Click(object sender, EventArgs e)
        {
            Form1 game = new Form1();

            game.FormClosed += (s, args) =>
            {
                this.Show();
            };

            game.Show();

            this.Hide();
        }

        private void Rules_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Игрок 1 управляет быками.\n" +
                "Игрок 2 управляет синими фигурами.\n\n" +
                "Цель быков — захватить все фигуры противника.\n" +
                "Цель мальчиков — лишить быков возможных ходов.",
                "Правила Буга-Шыдыраа");
        }
    }
}