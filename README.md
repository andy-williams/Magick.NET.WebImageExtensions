# Magick.NET.WebImageExtensions

Lightweight extension methods using `Magick.NET` for resizing, cropping and scaling images for the web.

This works for both windows and linux.

## Example

### Input Image

Example input

![](./tests/Magick.NET.WebImageExtensions.Integration.Tests/test-images/alpine-lake_1920.jpg)

### Code



```c#
using(var image = new MagickImage("/path/to/image.jpg"))
{
    image.ScaleAndCrop(new Box(width, height), CropType.Center);
    image.Write("/path/to/output.jpg");
}
```

## Example Output

### 250 x 250

![](./tests/Magick.NET.WebImageExtensions.Integration.Tests/test-images/expected-output/alpine-lake_250x250.jpg)

### 1000 x 20

![](./tests/Magick.NET.WebImageExtensions.Integration.Tests/test-images/expected-output/alpine-lake_1000x20.jpg)

### 5000 x 1200

![](./tests/Magick.NET.WebImageExtensions.Integration.Tests/test-images/expected-output/alpine-lake_5000x1200.jpg)

### 400 x 1200

![](./tests/Magick.NET.WebImageExtensions.Integration.Tests/test-images/expected-output/alpine-lake_400x1200.jpg)

### 1200 x 5000

![](./tests/Magick.NET.WebImageExtensions.Integration.Tests/test-images/expected-output/alpine-lake_1200x5000.jpg)