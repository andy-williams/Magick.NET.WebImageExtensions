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
