using Avalonia.Media;
using FluentAssertions;
using Vktun.Antd;
using Vktun.Antd.Avalonia;

namespace Vktun.Antd.Avalonia.Tests;

[TestClass]
public sealed class AntdThemeResourcesTests
{
    [TestMethod]
    public void Constructor_ShouldLoadBrushAndMetricResources()
    {
        var resources = new AntdThemeResources();

        resources.ContainsKey(AntdResourceKeys.BrushPrimary).Should().BeTrue();
        resources.ContainsKey(AntdResourceKeys.ControlHeightMiddle).Should().BeTrue();
        resources[AntdResourceKeys.BrushPrimary].Should().BeOfType<SolidColorBrush>();
    }

    [TestMethod]
    public void Seed_ShouldReloadPrimaryBrush()
    {
        var resources = new AntdThemeResources
        {
            Seed = new AntdSeedToken { PrimaryColor = AntdColor.Parse("#FF69B4") },
        };

        var brush = resources[AntdResourceKeys.BrushPrimary].Should().BeOfType<SolidColorBrush>().Subject;
        brush.Color.Should().Be(Color.FromRgb(255, 105, 180));
    }
}
