using System;
using System.IO;
using System.Reflection;
using ImageMagick;
using Xunit;

namespace Magick.NET.WebImageExtensions.Integration.Tests
{
    public class CropAndScaleTests
    {
        private readonly string _testFileDir;

        public CropAndScaleTests()
        {
            var appRoot = AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf("bin") - 1);
            _testFileDir = Path.Join(appRoot, "test-images");
        }

        [Theory]
        [InlineData(CropType.Center, 250, 250, "alpine-lake_250x250.jpg")]
        [InlineData(CropType.Center, 1000, 20, "alpine-lake_1000x20.jpg")]
        [InlineData(CropType.Center, 5000, 1200, "alpine-lake_5000x1200.jpg")]
        [InlineData(CropType.Center, 400, 1200, "alpine-lake_400x1200.jpg")]
        [InlineData(CropType.Center, 1200, 5000, "alpine-lake_1200x5000.jpg")]

        [InlineData(CropType.TopLeft, 960, 640, "alpine-lake_topleft_960x640.jpg")]
        [InlineData(CropType.TopLeft, 250, 250, "alpine-lake_topleft_250x250.jpg")]
        [InlineData(CropType.TopLeft, 1000, 20, "alpine-lake_topleft_1000x20.jpg")]
        [InlineData(CropType.TopLeft, 5000, 1200, "alpine-lake_topleft_5000x1200.jpg")]
        [InlineData(CropType.TopLeft, 400, 1200, "alpine-lake_topleft_400x1200.jpg")]
        [InlineData(CropType.TopLeft, 1200, 5000, "alpine-lake_topleft_1200x5000.jpg")]

        [InlineData(CropType.TopRight, 960, 640, "alpine-lake_topright_960x640.jpg")]
        [InlineData(CropType.TopRight, 250, 250, "alpine-lake_topright_250x250.jpg")]
        [InlineData(CropType.TopRight, 1000, 20, "alpine-lake_topright_1000x20.jpg")]
        [InlineData(CropType.TopRight, 5000, 1200, "alpine-lake_topright_5000x1200.jpg")]
        [InlineData(CropType.TopRight, 400, 1200, "alpine-lake_topright_400x1200.jpg")]
        [InlineData(CropType.TopRight, 1200, 5000, "alpine-lake_topright_1200x5000.jpg")]

        [InlineData(CropType.BottomLeft, 960, 640, "alpine-lake_bottomleft_960x640.jpg")]
        [InlineData(CropType.BottomLeft, 250, 250, "alpine-lake_bottomleft_250x250.jpg")]
        [InlineData(CropType.BottomLeft, 1000, 20, "alpine-lake_bottomleft_1000x20.jpg")]
        [InlineData(CropType.BottomLeft, 5000, 1200, "alpine-lake_bottomleft_5000x1200.jpg")]
        [InlineData(CropType.BottomLeft, 400, 1200, "alpine-lake_bottomleft_400x1200.jpg")]
        [InlineData(CropType.BottomLeft, 1200, 5000, "alpine-lake_bottomleft_1200x5000.jpg")]

        [InlineData(CropType.BottomRight, 960, 640, "alpine-lake_bottomright_960x640.jpg")]
        [InlineData(CropType.BottomRight, 250, 250, "alpine-lake_bottomright_250x250.jpg")]
        [InlineData(CropType.BottomRight, 1000, 20, "alpine-lake_bottomright_1000x20.jpg")]
        [InlineData(CropType.BottomRight, 5000, 1200, "alpine-lake_bottomright_5000x1200.jpg")]
        [InlineData(CropType.BottomRight, 400, 1200, "alpine-lake_bottomright_400x1200.jpg")]
        [InlineData(CropType.BottomRight, 1200, 5000, "alpine-lake_bottomright_1200x5000.jpg")]

        [InlineData(CropType.Left, 960, 640, "alpine-lake_left_960x640.jpg")]
        [InlineData(CropType.Left, 250, 250, "alpine-lake_left_250x250.jpg")]
        [InlineData(CropType.Left, 1000, 20, "alpine-lake_left_1000x20.jpg")]
        [InlineData(CropType.Left, 5000, 1200, "alpine-lake_left_5000x1200.jpg")]
        [InlineData(CropType.Left, 400, 1200, "alpine-lake_left_400x1200.jpg")]
        [InlineData(CropType.Left, 1200, 5000, "alpine-lake_left_1200x5000.jpg")]

        [InlineData(CropType.Right, 960, 640, "alpine-lake_right_960x640.jpg")]
        [InlineData(CropType.Right, 250, 250, "alpine-lake_right_250x250.jpg")]
        [InlineData(CropType.Right, 1000, 20, "alpine-lake_right_1000x20.jpg")]
        [InlineData(CropType.Right, 5000, 1200, "alpine-lake_right_5000x1200.jpg")]
        [InlineData(CropType.Right, 400, 1200, "alpine-lake_right_400x1200.jpg")]
        [InlineData(CropType.Right, 1200, 5000, "alpine-lake_right_1200x5000.jpg")]
        public void Should_CropAndScale(CropType cropType, int width, int height, string expectedOutput)
        {
            using (var image = new MagickImage(Path.Join(_testFileDir, "alpine-lake_1920.jpg")))
            {
                image.ScaleAndCrop(new Box(width, height), cropType);

                var actualBytes = image.ToByteArray();

                var expectedOutputPath = Path.Join(_testFileDir, $"expected-output\\{expectedOutput}");

                Assert.Equal(width, image.Width);
                Assert.Equal(height, image.Height);

                if (!File.Exists(expectedOutputPath))
                {
                    image.Write(expectedOutputPath);
                }
                else
                {
                    using (var expectedOutputStream = File.OpenRead(Path.Join(_testFileDir, $"expected-output\\{expectedOutput}")))
                    {
                        Assert.Equal(expectedOutputStream.ReadAllBytes(), actualBytes);
                    }
                }
                
            }
        }
    }

    public static class StreamExtensions
    {
        public static byte[] ReadAllBytes(this Stream instream)
        {
            if (instream is MemoryStream)
                return ((MemoryStream)instream).ToArray();

            using (var memoryStream = new MemoryStream())
            {
                instream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }

}
