using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VizualizerSortArray
{
    internal abstract class SortingAlgorithmBase : ISortingVisualizer
    {
        public string[] Array { get; private set; }
        public PictureBox PictureBox { get; private set; }
        public Bitmap Bitmap { get; private set; }
        public int FixedHeight { get; private set; }
        public int FixedWidth { get; private set; }
        public Font Font { get; private set; }
        public Brush MainBrush { get; private set; }
        public Brush FirstBrush { get; private set; }
        public Brush SecondBrush { get; private set; }
        public int Delay { get; private set; }

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

        public abstract void DrawInitialArray();
        public abstract void Sort();
    }
}
