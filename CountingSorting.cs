using System.Collections.Generic;  // Импорт пространства имен для работы с коллекциями
using System.Drawing;  // Импорт пространства имен для работы с графикой
using System.Linq;  // Импорт пространства имен для работы с LINQ
using System.Threading;  // Импорт пространства имен для работы с потоками
using System.Windows.Forms;  // Импорт пространства имен для работы с элементами управления Windows Forms

namespace VizualizerSortArray
{
    /// <summary>
    /// Класс, представляющий собой алгоритм сортировки подсчетом с визуализацией.
    /// </summary>
    internal class CountingSorting : SortingAlgorithmBase
    {
        /// <summary>
        /// Конструктор класса CountingSorting.
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
        public CountingSorting(string[] array, PictureBox pictureBox, Bitmap bitmap, int fixedHeight, int fixedWidth, Font font, Brush mainBrush, Brush firstBrush, Brush secondBrush, int delay)
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
                    Rectangle rect1 = new(FixedWidth, i * FixedHeight, FixedWidth, FixedHeight);

                    g.FillRectangle(Brushes.Blue, rect);  // Заполняем прямоугольник синим цветом
                    g.FillRectangle(Brushes.Blue, rect1);  // Заполняем второй прямоугольник синим цветом

                    using StringFormat stringFormat = new();  // Инициализация объекта форматирования строки
                    stringFormat.Alignment = StringAlignment.Center;  // Центрирование по горизонтали
                    stringFormat.LineAlignment = StringAlignment.Center;  // Центрирование по вертикали

                    g.DrawString(Array[i], Font, Brushes.White, rect, stringFormat);  // Рисуем текст в первом прямоугольнике
                    g.DrawString(Array[i], Font, Brushes.White, rect1, stringFormat);  // Рисуем текст во втором прямоугольнике
                }
            }

            PictureBox.Image = Bitmap.Clone() as Image;  // Обновляем изображение в PictureBox
        }

        /// <summary>
        /// Реализация метода для выполнения сортировки.
        /// </summary>
        public override void Sort()
        {
            Dictionary<string, int> frequencyDict = new();  // Создаем словарь для хранения частоты каждого элемента

            // Заполняем словарь частотами элементов массива
            foreach (var item in Array)
            {
                if (frequencyDict.ContainsKey(item))
                {
                    frequencyDict[item]++;
                }
                else
                {
                    frequencyDict[item] = 1;
                }
            }

            string[] sortedArray = (string[])Array.Clone();  // Создаем копию массива для сортировки
            int position = 0;  // Индекс позиции в отсортированном массиве

            // Сортировка массива по частоте элементов
            foreach (var kvp in frequencyDict.OrderBy(x => x.Key))
            {
                for (int i = 0; i < kvp.Value; i++)
                {
                    int originalIndex = System.Array.IndexOf(Array, kvp.Key);

                    // Визуализация шагов сортировки
                    DrawArrayElement(Array[originalIndex], 0, originalIndex * FixedHeight, FixedWidth, FixedHeight, FirstBrush, PictureBox, Bitmap, Font);
                    PictureBox.Image = Bitmap.Clone() as Image;
                    Thread.Sleep(Delay);

                    DrawArrayElement(sortedArray[position], FixedWidth, position * FixedHeight, FixedWidth, FixedHeight, SecondBrush, PictureBox, Bitmap, Font);
                    PictureBox.Image = Bitmap.Clone() as Image;
                    Thread.Sleep(Delay);

                    sortedArray[position] = kvp.Key;  // Заменяем элемент в отсортированном массиве
                    position++;  // Увеличиваем индекс позиции

                    // Визуализация шагов сортировки
                    DrawArrayElement(Array[originalIndex], 0, originalIndex * FixedHeight, FixedWidth, FixedHeight, MainBrush, PictureBox, Bitmap, Font);
                    DrawArrayElement(sortedArray[position - 1], FixedWidth, (position - 1) * FixedHeight, FixedWidth, FixedHeight, MainBrush, PictureBox, Bitmap, Font);
                    PictureBox.Image = Bitmap.Clone() as Image;
                    Thread.Sleep(Delay);
                }
            }

            // Сохраняем отсортированный массив
            System.Array.Copy(sortedArray, Array, Array.Length);
        }
    }
}
