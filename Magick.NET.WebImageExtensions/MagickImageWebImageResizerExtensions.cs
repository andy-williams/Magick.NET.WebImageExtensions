﻿using System;
using ImageMagick;

namespace Magick.NET.WebImageExtensions
{
    public static class MagickImageWebImageResizerExtensions
    {
        public static MagickImage ScaleAndCrop(this MagickImage image, Box box, CropType cropType = CropType.Center)
        {
            var imageWHRatio = (decimal)image.Width / image.Height;
            var outputWHRatio = (decimal)box.Width / box.Height;
            var outputIsWider = outputWHRatio > imageWHRatio;

            if (outputIsWider)
                image.ScaleByWidth(box.Width);
            else
                image.ScaleByHeight(box.Height);

            image.Crop(box, cropType);

            return image;
        }

        public static MagickImage ScaleByHeight(this MagickImage image, int height)
        {
            if (height <= 0)
                throw new ArgumentException(nameof(height));

            var percentage = height / (double)image.Height;
            var newWidth = (int)Math.Round(percentage * image.Width);
            var newHeight = (int)Math.Round(percentage * image.Height);
            image.Scale(new MagickGeometry(newWidth, newHeight));
            return image;
        }

        public static MagickImage ScaleByWidth(this MagickImage image, int width)
        {
            if (width <= 0)
                throw new ArgumentException(nameof(width));

            var percentage = width / (double)image.Width;
            var newWidth = (int)Math.Round(percentage * image.Width);
            var newHeight = (int)Math.Round(percentage * image.Height);
            image.Scale(new MagickGeometry(newWidth, newHeight));
            return image;
        }

        public static MagickImage Crop(this MagickImage image, Box box, CropType cropType)
        {
            if (cropType == CropType.Center)
            {
                var imageToOutputWidthDiff = image.Width - box.Width;
                var imageToOutputHeightDiff = image.Height - box.Height;

                image.Crop(new MagickGeometry(box.Width, box.Height)
                {
                    X = (imageToOutputWidthDiff) / 2 - (imageToOutputWidthDiff) / 2 / 2,
                    Y = (imageToOutputHeightDiff) / 2 - (imageToOutputHeightDiff) / 2 / 2
                });
            }
            else if (cropType == CropType.TopLeft)
            {
                image.Crop(new MagickGeometry(box.Width, box.Height)
                {
                    X = 0,
                    Y = 0
                });
            }
            else if (cropType == CropType.TopRight)
            {
                var imageToOutputWidthDiff = image.Width - box.Width;

                image.Crop(new MagickGeometry(box.Width, box.Height)
                {
                    X = imageToOutputWidthDiff,
                    Y = 0
                });
            }
            else if (cropType == CropType.BottomLeft)
            {
                var imageToOutputHeightDiff = image.Height - box.Height;

                image.Crop(new MagickGeometry(box.Width, box.Height)
                {
                    X = 0,
                    Y = imageToOutputHeightDiff
                });
            }
            else if (cropType == CropType.BottomRight)
            {
                var imageToOutputWidthDiff = image.Width - box.Width;
                var imageToOutputHeightDiff = image.Height - box.Height;

                image.Crop(new MagickGeometry(box.Width, box.Height)
                {
                    X = imageToOutputWidthDiff,
                    Y = imageToOutputHeightDiff
                });
            }
            else if (cropType == CropType.Left)
            {
                var imageToOutputWidthDiff = image.Width - box.Width;
                var imageToOutputHeightDiff = image.Height - box.Height;

                image.Crop(new MagickGeometry(box.Width, box.Height)
                {
                    X = 0,
                    Y = imageToOutputHeightDiff / 2
                });
            }
            else if (cropType == CropType.Right)
            {
                var imageToOutputWidthDiff = image.Width - box.Width;
                var imageToOutputHeightDiff = image.Height - box.Height;

                image.Crop(new MagickGeometry(box.Width, box.Height)
                {
                    X = imageToOutputWidthDiff,
                    Y = imageToOutputHeightDiff / 2
                });
            }
            else
            {
                throw new UnknownCropTypeException();
            }
            return image;
        }
    }
}

