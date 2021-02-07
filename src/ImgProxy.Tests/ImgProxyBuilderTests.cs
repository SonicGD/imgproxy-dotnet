using Xunit;

namespace ImgProxy.Tests
{
    public class ImgProxyBuilderTests
    {
        const string Key = "736563726574";
        const string Salt = "68656C6C6F";
        const string Url = "https://upload.wikimedia.org/wikipedia/ru/2/24/Lenna.png";
        const string Host = "https://cdn.example.com";

        const string Resize = ResizingTypes.Fill;
        const int Width = 300;
        const int Height = 400;
        const string Gravity = GravityTypes.Smart;
        const bool Enlarge = false;

        [Fact]
        public void Build()
        {
            var url = ImgProxyBuilder.New
                .WithEndpoint(Host)
                .WithCredentials(Key, Salt)
                .WithResize(Resize, Width, Height, true)
                .WithFormat(Formats.JPG)
                .Build(Url, encode: false);

            Assert.Equal("https://cdn.example.com/G-wVPuU_0HLI9b2CMk6FCH464vhvIytv4UeINfVK1Xo/resize:fill:300:400:1:0/plain/https://upload.wikimedia.org/wikipedia/ru/2/24/Lenna.png@jpg", url);
        }

        [Fact]
        public void BuildWithOptions()
        {
            ImgProxyBuilder.Instance = ImgProxyBuilder.New
                .WithEndpoint(Host)
                .WithCredentials(Key, Salt)
                .WithResize(Resize, Width, Height, Enlarge);

            var additionalOptions = new ImgProxyOption[]
            {
                    new GravityOption(Gravity),
                    new FormatOption(Formats.JPG)
            };

            var url = ImgProxyBuilder.Instance.Build(Url, additionalOptions, encode: false);

            Assert.Equal("https://cdn.example.com/8oHkICIOkLKR1pWj6_qZFtccJSTUbb3o--MqLhHk9sw/resize:fill:300:400:0:0/gravity:sm/plain/https://upload.wikimedia.org/wikipedia/ru/2/24/Lenna.png@jpg", url);
        }

        [Fact]
        public void BuildEncoded()
        {
            ImgProxyBuilder.Instance = ImgProxyBuilder.New
                .WithEndpoint(Host)
                .WithCredentials(Key, Salt)
                .WithResize(Resize, Width, Height, Enlarge)
                .WithFormat(Formats.JPG);

            var url = ImgProxyBuilder.Instance.Build(Url, encode: true);

            Assert.Equal("https://cdn.example.com/Jr7OipYYhfUxUEe2FBSRwv3ojoudLOIlGabaSNubtw4/resize:fill:300:400:0:0/aHR0cHM6Ly91cGxvYWQud2lraW1lZGlhLm9yZy93aWtpcGVkaWEvcnUvMi8yNC9MZW5uYS5wbmc.jpg", url);
        }

        [Fact]
        public void BuildEncodedWithOptions()
        {
            ImgProxyBuilder.Instance = ImgProxyBuilder.New
                .WithEndpoint(Host)
                .WithCredentials(Key, Salt)
                .WithResize(Resize, Width, Height, Enlarge);

            var additionalOptions = new ImgProxyOption[]
            {
                new GravityOption(Gravity),
                new FormatOption(Formats.JPG)
            };

            var url = ImgProxyBuilder.Instance.Build(Url, additionalOptions, encode: true);

            Assert.Equal("https://cdn.example.com/H07AJg_xNzix5IB1pG9zd8WQ4_Ykld5mF0FxQaM1yrM/resize:fill:300:400:0:0/gravity:sm/aHR0cHM6Ly91cGxvYWQud2lraW1lZGlhLm9yZy93aWtpcGVkaWEvcnUvMi8yNC9MZW5uYS5wbmc.jpg", url);
        }
    }
}
