using System;

namespace Magick.NET.WebImageExtensions
{
    public class Box
    {
        public Box(int width, int height)
        {
            if (width <= 1)
                throw new ArgumentException(nameof(width));

            if (height <= 1)
                throw new ArgumentException(nameof(height));

            Width = width;
            Height = height;
        }

        public int Width { get; }
        public int Height { get; }
    }
}