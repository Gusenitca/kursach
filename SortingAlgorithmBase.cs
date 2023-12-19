using System.Drawing;  // Импорт пространства имен для работы с графикой
using System.Windows.Forms;  // Импорт пространства имен для работы с элементами управления Windows Forms
using System.IO;  // Импорт пространства имен для работы с файловой системой

namespace VizualizerSortArray
{
    /// <summary>
    /// Базовый класс для алгоритмов сортировки с визуализацией.
    /// </summary>
    internal abstract class SortingAlgorithmBase
    {
        // Поля и свойства, характеризующие алгоритм сортировки
        protected string[] Array { get; }  // Массив, который будет сортироваться
        protected PictureBox PictureBox { get; }  // Элемент управления для отображения изображения
        protected Bitmap Bitmap { get; }  // Изображение для отрисовки
        protected int FixedHeight { get; }  // Фиксированная высота прямоугольников
        protected int FixedWidth { get; }  // Фиксированная ширина прямоугольников
        protected Font Font { get; }  // Шрифт для текста на прямоугольниках
        protected Brush MainBrush { get; }  // Кисть для отрисовки основного цвета
        protected Brush FirstBrush { get; }  // Кисть для отрисовки первого дополнительного цвета
        protected Brush SecondBrush { get; }  // Кисть для отрисовки второго дополнительного цвета
        protected int Delay { get; }  // Задержка между шагами сортировки

        // Конструктор для инициализации полей и свойств
        protected SortingAlgorithmBase(string[] array, PictureBox pictureBox, Bitmap bitmap, int fixedHeight, int fixedWidth, Font font, Brush mainBrush, Brush firstBrush, Brush secondBrush, int delay)
        {
            Array = array;
            PictureBox = pictureBox;
            Bitmap = bitmap;
            FixedHeight = fixedHeight;
            FixedWidth = fixedWidth;
            Font = font;
            MainBrush = mainBrush;
            FirstBrush = firstBrush;
            SecondBrush = secondBrush;
            Delay = delay;
        }

        /// <summary>
        /// Метод для сохранения результата в файл.
        /// </summary>
        /// <param name="path">Путь к файлу для сохранения.</param>
        public void Save(string path)
        {
            File.WriteAllLines(path, Array);  // Запись массива в файл
            MessageBox.Show("Результат сохранён в файл: " + path, "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);  // Отображение сообщения об успешном сохранении
        }

        /// <summary>
        /// Метод для отрисовки элемента массива.
        /// </summary>
        /// <param name="value">Значение элемента.</param>
        /// <param name="x">Координата X прямоугольника.</param>
        /// <param name="y">Координата Y прямоугольника.</param>
        /// <param name="width">Ширина прямоугольника.</param>
        /// <param name="height">Высота прямоугольника.</param>
        /// <param name="brush">Кисть для отрисовки.</param>
        /// <param name="pictureBox">Элемент управления PictureBox.</param>
        /// <param name="bitmap">Изображение для отрисовки.</param>
        /// <param name="font">Шрифт для текста.</param>
        protected void DrawArrayElement(string value, int x, int y, int width, int height, Brush brush, PictureBox pictureBox, Bitmap bitmap, Font font)
        {
            Rectangle rect = new(x, y, width, height);  // Создание прямоугольника для отрисовки

            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.FillRectangle(brush, rect);  // Заполнение прямоугольника указанной кистью

                using StringFormat stringFormat = new();  // Создание формата для отображения текста по центру
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;
                g.DrawString(value, font, Brushes.White, rect, stringFormat);  // Отрисовка текста в центре прямоугольника
            }
            pictureBox.Image = bitmap.Clone() as Image;  // Обновление изображения в PictureBox
        }

        /// <summary>
        /// Абстрактный метод для отрисовки начального массива.
        /// </summary>
        public abstract void DrawInitialArray();

        /// <summary>
        /// Абстрактный метод для выполнения сортировки.
        /// </summary>
        public abstract void Sort();
    }
}
