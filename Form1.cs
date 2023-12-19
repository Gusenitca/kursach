using System;  // Подключение пространства имен для основных системных типов
using System.Drawing;  // Подключение пространства имен для работы с изображениями
using System.IO;  // Подключение пространства имен для работы с файлами и директориями
using System.Linq;  // Подключение пространства имен для использования LINQ запросов
using System.Threading.Tasks;  // Подключение пространства имен для асинхронных задач
using System.Windows.Forms;  // Подключение пространства имен для элементов управления Windows Forms

namespace VizualizerSortArray
{
    // Основной класс формы приложения
    public partial class Form1 : Form
    {
        // Задержка в миллисекундах для визуализации шагов сортировки
        private const int Delay = 500;

        // Фиксированная ширина прямоугольника для отображения элементов массива
        private const int FixedWidth = 150;

        // Фиксированная высота прямоугольника для отображения элементов массива
        private const int FixedHeight = 25;

        // Шрифт для отображения данных в прямоугольниках
        private readonly Font _dataFont;

        // Кисть для отрисовки основного цвета (используется при отрисовке элементов массива в обычном состоянии)
        private readonly Brush _mainBrush = Brushes.Blue;

        // Кисть для отрисовки первого дополнительного цвета (используется при выделении элементов при сортировке)
        private readonly Brush _firstBrush = Brushes.Red;

        // Кисть для отрисовки второго дополнительного цвета (используется при выделении элементов при сортировке)
        private readonly Brush _secondBrush = Brushes.Green;


        // Путь для сохранения результатов сортировки
        private const string ResultsPath = "Результаты";

        // Конструктор формы
        public Form1()
        {
            // Инициализация компонентов формы
            InitializeComponent();

            // Инициализация шрифта для отображения данных
            _dataFont = new Font(Font.FontFamily, 12);

            // Включение двойной буферизации, перерисовки при изменении размера и автопрокрутки формы
            DoubleBuffered = true; 
            ResizeRedraw = true;  // Включение перерисовки формы при изменении размера
            AutoScroll = true;   // Включение автоматической прокрутки формы
        }

        // Обработчик события для кнопки запуска сортировки пузырьком
        private async void buttonBubble_Click(object sender, EventArgs e)
        {
            // Проверка, что RichTextBox содержит текст перед запуском сортировки
            if (!string.IsNullOrWhiteSpace(richTextBox1.Text))
            {
                // Асинхронный запуск сортировки с указанием типа сортировки (пузырьковая)
                await RunSorting<BubbleSorting>();
            }
            else
            {
                MessageBox.Show("RichTextBox пуст. Введите текст для сортировки.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Обработчик события для кнопки запуска сортировки подсчетом
        private async void buttonCounting_Click(object sender, EventArgs e)
        {
            // Проверка, что RichTextBox содержит текст перед запуском сортировки
            if (!string.IsNullOrWhiteSpace(richTextBox1.Text))
            {
                // Асинхронный запуск сортировки с указанием типа сортировки (подсчетом)
                await RunSorting<CountingSorting>();
            }
            else
            {
                MessageBox.Show("RichTextBox пуст. Введите текст для сортировки.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        // Метод для асинхронного запуска сортировки
        private async Task RunSorting<T>() where T : SortingAlgorithmBase
        {
            // Получение строк из TextBox, удаление пустых строк и обрезка каждой строки
            string[] array = richTextBox1.Lines
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(line => line.Trim())
                .ToArray();

            // Создание битмапа с учетом типа сортировки (одинарная или двойная ширина)
            Bitmap bitmap = new(typeof(T) == typeof(BubbleSorting) ? FixedWidth : FixedWidth * 2, array.Length * FixedHeight);

            // Установка размера PictureBox
            pictureBox1.Size = bitmap.Size;

            // Отключение кнопок во время сортировки
            buttonCounting.Enabled = false;
            buttonBubble.Enabled = false;

            // Создание экземпляра сортировки и передача необходимых параметров
            T sortingInstance = (T)Activator.CreateInstance(typeof(T), array, pictureBox1, bitmap, FixedHeight, FixedWidth, _dataFont, _mainBrush, _firstBrush, _secondBrush, Delay);

            // Отрисовка начального массива
            sortingInstance.DrawInitialArray();

            // Асинхронный запуск сортировки
            await Task.Run(() => sortingInstance.Sort());

            // Создание папки для результатов, если её нет
            Directory.CreateDirectory(ResultsPath);

            // Сохранение результата сортировки
            sortingInstance.Save($"{ResultsPath}\\{DateTime.Now:dd.MM.yyyy HH-mm-ss} {array.Length} слов(а).txt");

            // Включение кнопок после завершения сортировки
            buttonCounting.Enabled = true;
            buttonBubble.Enabled = true;
        }

        // Обработчик события для кнопки открытия файла
        private void buttonOpenFile_Click(object sender, EventArgs e)
        {
            // Создание диалогового окна выбора файла
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Установка фильтра для выбора только текстовых файлов
            openFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt";

            // Проверка, был ли файл выбран
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Чтение содержимого файла
                    string fileContent = File.ReadAllText(openFileDialog.FileName);

                    // Установка текста в RichTextBox
                    richTextBox1.Text = fileContent;
                }
                catch (Exception ex)
                {
                    // Обработка и вывод ошибки, если чтение файла не удалось
                    MessageBox.Show($"Ошибка при открытии файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }
}
