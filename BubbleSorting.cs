using System.Drawing;  // Импорт пространства имен для работы с графикой
using System.Threading;  // Импорт пространства имен для работы с потоками
using System.Windows.Forms;  // Импорт пространства имен для работы с элементами управления Windows Forms

namespace VizualizerSortArray
{
    /// <summary>
    /// Класс, представляющий собой алгоритм сортировки пузырьком с визуализацией.
    /// </summary>
    internal class BubbleSorting : SortingAlgorithmBase
    {
        /// <summary>
        /// Конструктор класса BubbleSorting.
        /// </summary>
        /// <param name="array">Массив, который будет сортироваться.</param>
        /// <param name="pictureBox">Элемент управления для отображения изображения.</param>
        /// <param name="bitmap">Изображение для отрисовки.</param>
        /// <param name="fixedHeight">Фиксированная высота прямоугольников.</param>
        /// <param name="fixedWidth">Фиксированная ширина прямоугольников.</param>
        /// <param name="font">Шрифт для текста на прямоугольниках.</param>
        /// <param name="mainBrush">Кисть для отрисовки основного цвета.</param>
        /// <param name="firstBrush">Кисть для отрисовки первого дополнительного цвета.</param>
        /// <param name="secondBrush">Кисть для отрисовки второго дополнительного цвета.</param>
        /// <param name="delay">Задержка между шагами сортировки.</param>
        public BubbleSorting(string[] array, PictureBox pictureBox, Bitmap bitmap, int fixedHeight, int fixedWidth, Font font, Brush mainBrush, Brush firstBrush, Brush secondBrush, int delay)
            : base(array, pictureBox, bitmap, fixedHeight, fixedWidth, font, mainBrush, firstBrush, secondBrush, delay)
        {

        }

        /// <summary>
        /// Реализация метода для отрисовки начального массива.
        /// </summary>
        public override void DrawInitialArray()
        {
            using (Graphics g = Graphics.FromImage(Bitmap))
            {
                for (int i = 0; i < Array.Length; i++)
                {
                    Rectangle rect = new(0, i * FixedHeight, FixedWidth, FixedHeight);

                    g.FillRectangle(Brushes.Blue, rect);  // Заполняем прямоугольник синим цветом

                    using StringFormat stringFormat = new();  // Инициализация объекта форматирования строки
                    stringFormat.Alignment = StringAlignment.Center;  // Центрирование по горизонтали
                    stringFormat.LineAlignment = StringAlignment.Center;  // Центрирование по вертикали

                    g.DrawString(Array[i], Font, Brushes.White, rect, stringFormat);  // Рисуем текст в прямоугольнике
                }
            }

            PictureBox.Image = Bitmap.Clone() as Image;  // Обновляем изображение в PictureBox
        }

        /// <summary>
        /// Реализация метода для выполнения сортировки пузырьком.
        /// </summary>
        public override void Sort()
        {
            for (int i = 0; i < Array.Length - 1; i++)
            {
                for (int j = 0; j < Array.Length - i - 1; j++)
                {
                    if (string.Compare(Array[j], Array[j + 1]) > 0)
                    {
                        string temp = Array[j];

                        // Визуализация шагов сортировки
                        DrawArrayElement(Array[j], 0, j * FixedHeight, FixedWidth, FixedHeight, FirstBrush, PictureBox, Bitmap, Font);
                        DrawArrayElement(Array[j + 1], 0, (j + 1) * FixedHeight, FixedWidth, FixedHeight, FirstBrush, PictureBox, Bitmap, Font);
                        Thread.Sleep(Delay);

                        Array[j] = Array[j + 1];

                        // Визуализация шагов сортировки
                        DrawArrayElement(Array[j], 0, j * FixedHeight, FixedWidth, FixedHeight, FirstBrush, PictureBox, Bitmap, Font);
                        DrawArrayElement(Array[j + 1], 0, (j + 1) * FixedHeight, FixedWidth, FixedHeight, FirstBrush, PictureBox, Bitmap, Font);
                        Thread.Sleep(Delay);

                        Array[j + 1] = temp;

                        // Визуализация шагов сортировки
                        DrawArrayElement(Array[j], 0, j * FixedHeight, FixedWidth, FixedHeight, SecondBrush, PictureBox, Bitmap, Font);
                        DrawArrayElement(Array[j + 1], 0, (j + 1) * FixedHeight, FixedWidth, FixedHeight, SecondBrush, PictureBox, Bitmap, Font);
                        Thread.Sleep(Delay);

                        // Визуализация возвращения в исходное состояние
                        DrawArrayElement(Array[j], 0, j * FixedHeight, FixedWidth, FixedHeight, MainBrush, PictureBox, Bitmap, Font);
                        DrawArrayElement(Array[j + 1], 0, (j + 1) * FixedHeight, FixedWidth, FixedHeight, MainBrush, PictureBox, Bitmap, Font);
                        PictureBox.Image = Bitmap.Clone() as Image;
                        Thread.Sleep(Delay);
                    }
                }
            }
        }
    }
}
