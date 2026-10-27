namespace Bugashydyr2
{
    public partial class Form1 : Form
    {
        int[] pieces = new int[45];
        bool[] visibleNodes = new bool[45];

        List<Point> nodes = new List<Point>();

        Dictionary<int, List<int>> connections =
            new Dictionary<int, List<int>>();

        int currentPlayer = 1;
        int selectedNode = -1;
        int bluePiecesToPlace = 16;
        // Таймер для смены фоновых изображений
        private System.Windows.Forms.Timer screenshotTimer;

        // Список файлов со скриншотами
        private string[] screenshotFiles = new string[0];

        // Индекс текущего скриншота
        private int screenshotIndex = -1;

        public Form1()
        {
            InitializeComponent();

            this.DoubleBuffered = true;

            CreateBoard();
            InitGame();

            this.Paint += Form1_Paint;
            this.MouseDown += Form1_MouseDown;
            LoadScreenshotSlideshow();
        }

        private bool IsSupportedImage(string file)
        {
            string extension = Path.GetExtension(file).ToLowerInvariant();

            return extension == ".png"
                || extension == ".jpg"
                || extension == ".jpeg"
                || extension == ".bmp";
        }

        private void LoadScreenshotSlideshow()
        {
            // Папка со скриншотами в разделе «Изображения»
            string folder = Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.MyPictures),
                "BugashydyrScreenshots");

            // Создаём папку автоматически, если её нет
            Directory.CreateDirectory(folder);

            // Находим все поддерживаемые изображения
            List<string> files = new List<string>();

            foreach (string file in Directory.GetFiles(folder))
            {
                if (IsSupportedImage(file))
                {
                    files.Add(file);
                }
            }

            // Сортируем по имени файла
            files.Sort(StringComparer.OrdinalIgnoreCase);

            screenshotFiles = files.ToArray();

            // Если изображений нет, сообщаем, куда их положить
            if (screenshotFiles.Length == 0)
            {
                MessageBox.Show(
                    "Добавь скриншоты в папку:\n" + folder);

                return;
            }

            // Создаём таймер
            screenshotTimer = new System.Windows.Forms.Timer();
            screenshotTimer.Interval = 15000;

            screenshotTimer.Tick += (sender, e) =>
            {
                ShowNextScreenshot();
            };

            // Показываем первое изображение сразу
            ShowNextScreenshot();

            if (BackgroundImage != null)
            {
                screenshotTimer.Start();
            }
        }

        private void ShowNextScreenshot()
        {
            if (screenshotFiles.Length == 0)
                return;

            // После последнего изображения возвращаемся к первому
            int nextIndex =
                (screenshotIndex + 1) % screenshotFiles.Length;

            try
            {
                Image newImage;

                // Загружаем копию, чтобы не блокировать файл
                using (Image source =
                    Image.FromFile(screenshotFiles[nextIndex]))
                {
                    newImage = new Bitmap(source);
                }

                // Меняем фоновое изображение формы
                Image oldImage = BackgroundImage;

                BackgroundImage = newImage;
                BackgroundImageLayout = ImageLayout.Stretch;

                // Освобождаем память предыдущего изображения
                if (oldImage != null)
                {
                    oldImage.Dispose();
                }

                screenshotIndex = nextIndex;

                // Перерисовываем фон и игровое поле
                Invalidate();
            }
            catch (Exception ex)
            {
                screenshotTimer?.Stop();

                MessageBox.Show(
                    "Не удалось загрузить скриншот:\n" + ex.Message);
            }
        }

        private void CreateBoard()
        {
            nodes.Clear();
            connections.Clear();

            int startX = 120;
            int startY = 120;
            int step = 90;

            int width = 9;
            int height = 5;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    nodes.Add(new Point(
                        startX + x * step,
                        startY + y * step));
                }
            }
            CreateConnections();
        }
        private void CreateConnections()
        {
            // ───── горизонтали ─────
            AddConnection(2, 3);
            AddConnection(3, 4);
            AddConnection(4, 5);
            AddConnection(5, 6);

            AddConnection(11, 12);
            AddConnection(12, 13);
            AddConnection(13, 14);
            AddConnection(14, 15);

            AddConnection(18, 19);
            AddConnection(19, 20);
            AddConnection(20, 21);
            AddConnection(21, 22);
            AddConnection(22, 23);
            AddConnection(23, 24);
            AddConnection(24, 25);
            AddConnection(25, 26);

            AddConnection(29, 30);
            AddConnection(30, 31);
            AddConnection(31, 32);
            AddConnection(32, 33);

            AddConnection(38, 39);
            AddConnection(39, 40);
            AddConnection(40, 41);
            AddConnection(41, 42);

            // ───── вертикали ─────
            AddConnection(10, 19);
            AddConnection(19, 28);

            AddConnection(2, 11);
            AddConnection(11, 20);
            AddConnection(20, 29);
            AddConnection(29, 38);

            AddConnection(3, 12);
            AddConnection(12, 21);
            AddConnection(21, 30);
            AddConnection(30, 39);

            AddConnection(4, 13);
            AddConnection(13, 22);
            AddConnection(22, 31);
            AddConnection(31, 40);

            AddConnection(5, 14);
            AddConnection(14, 23);
            AddConnection(23, 32);
            AddConnection(32, 41);

            AddConnection(6, 15);
            AddConnection(15, 24);
            AddConnection(24, 33);
            AddConnection(33, 42);

            AddConnection(16, 25);
            AddConnection(25, 34);

            AddConnection(8, 17);
            AddConnection(17, 26);
            AddConnection(26, 35);
            AddConnection(35, 44);

            // ───── диагонали ─────
            AddConnection(2, 12);
            AddConnection(4, 12);
            AddConnection(4, 14);
            AddConnection(6, 14);
            AddConnection(10, 20);
            AddConnection(12, 22);
            AddConnection(12, 20);
            AddConnection(14, 22);
            AddConnection(14, 24);
            AddConnection(16, 8);
            AddConnection(18, 10);
            AddConnection(18, 28);
            AddConnection(20, 28);
            AddConnection(20, 30);
            AddConnection(22, 30);
            AddConnection(22, 32);
            AddConnection(24, 16);
            AddConnection(24, 32);
            AddConnection(24, 34);
            AddConnection(30, 38);
            AddConnection(30, 40);
            AddConnection(32, 40);
            AddConnection(32, 42);
            AddConnection(34, 44);
        }

        private void InitGame()
        {
            for (int i = 0; i < visibleNodes.Length; i++)
            {
                visibleNodes[i] = true;
            }
            visibleNodes[0] = false;
            visibleNodes[1] = false;
            visibleNodes[7] = false;
            visibleNodes[9] = false;
            visibleNodes[27] = false;
            visibleNodes[36] = false;
            visibleNodes[37] = false;
            visibleNodes[43] = false;
            // очистка
            for (int i = 0; i < 45; i++)
                pieces[i] = 0;

            // ===== ИГРОК 1 (быки) =====

            // 2 быка
            pieces[20] = 3;
            pieces[24] = 3;

            // ===== ИГРОК 2 (охотники) =====

            // обычные синие фигуры
            pieces[12] = 2;
            pieces[13] = 2;
            pieces[14] = 2;
            pieces[21] = 2;
            pieces[23] = 2;
            pieces[30] = 2;
            pieces[31] = 2;
            pieces[32] = 2;

            bluePiecesToPlace = 16;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            Pen pen = new Pen(Color.Black, 3);

            // линии
            foreach (var pair in connections)
            {
                foreach (var target in pair.Value)
                {
                    g.DrawLine(
                        pen,
                        nodes[pair.Key],
                        nodes[target]);
                }
            }

            // точки и фигуры
            for (int i = 0; i < nodes.Count; i++)
            {
                if (!visibleNodes[i])
                    continue;
                Point p = nodes[i];

                g.FillEllipse(
                    Brushes.Black,
                    p.X - 5,
                    p.Y - 5,
                    10,
                    10);

                // фишки игрока 1
                if (pieces[i] == 1)
                {
                    g.FillEllipse(
                        Brushes.Red,
                        p.X - 20,
                        p.Y - 20,
                        40,
                        40);
                }

                // фишки игрока 2
                if (pieces[i] == 2)
                {
                    g.FillEllipse(
                        Brushes.Blue,
                        p.X - 20,
                        p.Y - 20,
                        40,
                        40);
                }

                // бык игрока 1
                if (pieces[i] == 3)
                {
                    g.FillEllipse(
                        Brushes.DarkRed,
                        p.X - 25,
                        p.Y - 25,
                        50,
                        50);

                    g.DrawString(
                        "🐂",
                        new Font("Arial", 18),
                        Brushes.White,
                        p.X - 18,
                        p.Y - 18);
                    g.DrawString(
                        "Синих фигур в руке осталось: " +
                        bluePiecesToPlace,
                        new Font("Arial", 16),
                        Brushes.Black,
                        20,
                        20);
                }
                string text;

                if (currentPlayer == 1)
                    text = "Ход: Игрок 1 (быки)";
                else
                {
                    if (bluePiecesToPlace > 0)
                        text = $"Ход: Игрок 2 (Мальчики) осталось {bluePiecesToPlace}";
                    else
                        text = "Ход: Игрок 2 (Мальчики)";
                }

                g.DrawString(
                    text,
                    new Font("Arial", 16, FontStyle.Bold),
                    Brushes.Black,
                    50,
                    50);
            }
        }

        private int GetNodeAt(Point mouse)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                if (!visibleNodes[i])
                    continue;
                Point p = nodes[i];

                double dist = Math.Sqrt(
                    Math.Pow(mouse.X - p.X, 2) +
                    Math.Pow(mouse.Y - p.Y, 2));

                if (dist < 25)
                    return i;
            }

            return -1;
        }
        private void btnRestart_Click(object sender, EventArgs e)
        {
            // сброс игры
            currentPlayer = 1;

            selectedNode = -1;

            bluePiecesToPlace = 16;

            InitGame();

            Invalidate();
        }

        private void btnRules_Click(object sender, EventArgs e)
        {
            string rules = @"ПРАВИЛА ИГРЫ «БУГА-ШЫДЫРАА»

    1. Буга-шыдыраа (тувинские «бычьи шахматы») — это традиционная тувинская логическая игра для двух игроков.

    2. Игровой набор: Расчерченная доска, 2 больших камня («буга» — быки) и 24 маленьких камня («оолдар» — пешки/мальчики).

            Задачи игроков

    1. Игрок с быками («буга»): Должен срубить все пешки соперника, перепрыгивая через них (по аналогии с шашками).

    2. Игрок с камнями-пешками делает либо ход, либо добавляет камень - что тоже считается ходом.

    3. Игрок с пешками («оолдар»): Должен окружить быков, загородить им ходы и загнать ихв глухой тупик.
            
    4. Игра заканчивается, когда один из игроков срубает все пешки соперника или окружает его быков.
            
    5. Игрок с камнями-пешками может начать передвигать фигурами, только когда выставит все фигурки в руке на поле.
            
    6. Если пешка стоит в углу, то бык ее не может срубить.";

            MessageBox.Show(
                rules,
                "Правила игры",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }


        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            int node = GetNodeAt(e.Location);

            if (node == -1)
                return;

            // ПКМ = добавить синюю фигуру
            if (e.Button == MouseButtons.Right)
            {
                // только игрок 2
                if (currentPlayer == 2)
                {
                    // ещё есть фигуры?
                    if (bluePiecesToPlace > 0)
                    {
                        // клетка пустая?
                        if (pieces[node] == 0)
                        {
                            pieces[node] = 2;

                            bluePiecesToPlace--;

                            SwitchPlayer();

                            CheckWin();

                            Invalidate();
                        }
                    }
                }

                return;
            }

            // выбор фигуры
            if (selectedNode == -1)
            {
                int piece = pieces[node];

                // игрок 2 НЕ может двигать, пока ставит фигуры
                if (currentPlayer == 2 && bluePiecesToPlace > 0)
                    return;

                bool canSelect =
                    (currentPlayer == 1 &&
                    (piece == 1 || piece == 3))

                    ||

                    (currentPlayer == 2 &&
                    (piece == 2));

                if (canSelect)
                {
                    selectedNode = node;
                }
            }
            else
            {
                int selectedPiece = pieces[selectedNode];

                // игрок 2 не двигает фигуры,
                // пока не выставит все
                if (currentPlayer == 2 &&
                    bluePiecesToPlace > 0)
                {
                    selectedNode = -1;
                    return;
                }

                // ===== ОБЫЧНЫЙ ХОД =====
                if (connections[selectedNode].Contains(node)
                    && pieces[node] == 0)
                {
                    pieces[node] = pieces[selectedNode];
                    pieces[selectedNode] = 0;

                    selectedNode = -1;

                    SwitchPlayer();

                    CheckWin();

                    Invalidate();

                    return;
                }

                // ===== ЗАХВАТ БЫКОМ =====
                if (selectedPiece == 3 || selectedPiece == 4)
                {
                    TryCapture(selectedNode, node);
                }

                selectedNode = -1;

                Invalidate();
            }
        }
        private void TryCapture(int from, int to)
        {
            if (pieces[to] != 0)
                return;

            Point p1 = nodes[from];
            Point p2 = nodes[to];

            int dx = p2.X - p1.X;
            int dy = p2.Y - p1.Y;

            // прыжок должен быть длинным
            if (Math.Abs(dx) < 120 &&
                Math.Abs(dy) < 120)
                return;

            // ищем середину
            int midX = (p1.X + p2.X) / 2;
            int midY = (p1.Y + p2.Y) / 2;

            int enemyNode = -1;

            for (int i = 0; i < nodes.Count; i++)
            {
                if (Math.Abs(nodes[i].X - midX) < 10 &&
                    Math.Abs(nodes[i].Y - midY) < 10)
                {
                    enemyNode = i;
                    break;
                }
            }

            if (enemyNode == -1)
                return;

            int piece = pieces[from];
            int enemy = pieces[enemyNode];

            bool isEnemy =
                (piece == 3 && enemy == 2)
                ||

                (piece == 4 && (enemy == 1 || enemy == 3));

            if (isEnemy)
            {
                // удалить врага
                pieces[enemyNode] = 0;

                // двигать быка
                pieces[to] = piece;
                pieces[from] = 0;

                SwitchPlayer();
                CheckWin();
            }
        }
        private void CheckWin()
        {
            // ===== ПОБЕДА БЫКОВ =====

            int blueCount = 0;

            for (int i = 0; i < pieces.Length; i++)
            {
                if (pieces[i] == 2)
                    blueCount++;
            }

            // синие закончились
            if (blueCount == 0 &&
                bluePiecesToPlace == 0)
            {
                MessageBox.Show("🐂 Быки победили!");

                InitGame();

                Invalidate();

                return;
            }

            // ===== ПРОВЕРКА ХОДОВ БЫКОВ =====

            bool bullsCanMove = false;

            for (int i = 0; i < pieces.Length; i++)
            {
                // ищем быков
                if (pieces[i] == 3)
                {
                    // есть ли связи
                    if (connections.ContainsKey(i))
                    {
                        foreach (int target in connections[i])
                        {
                            // пустая клетка
                            if (pieces[target] == 0)
                            {
                                bullsCanMove = true;
                                break;
                            }
                        }
                    }
                }
            }

            // быки не могут ходить
            if (!bullsCanMove)
            {
                MessageBox.Show("🔵 Мальчики победили!");

                InitGame();

                Invalidate();
            }
        }
        private void AddConnection(int a, int b)
        {
            if (!connections.ContainsKey(a))
                connections[a] = new List<int>();

            if (!connections.ContainsKey(b))
                connections[b] = new List<int>();

            if (!connections[a].Contains(b))
                connections[a].Add(b);

            if (!connections[b].Contains(a))
                connections[b].Add(a);
        }
        private void SwitchPlayer()
        {
            currentPlayer =
                (currentPlayer == 1) ? 2 : 1;
        }
    }
}