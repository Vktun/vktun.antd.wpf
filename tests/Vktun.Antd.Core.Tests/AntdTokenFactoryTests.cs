using FluentAssertions;
using Vktun.Antd;

namespace Vktun.Antd.Core.Tests;

[TestClass]
public sealed class AntdTokenFactoryTests
{
    [TestMethod]
    public void Create_ShouldResolveLightThemeTokens()
    {
        var tokens = AntdTokenFactory.Create(AntdThemeMode.Light, AntdSeedToken.Default);

        tokens.ColorPrimary.Should().Be(AntdColor.Parse("#1677FF"));
        tokens.ColorBgBase.Should().Be(AntdColor.Parse("#FFFFFF"));
        tokens.ControlHeightMiddle.Should().Be(40d);
        tokens.BorderRadiusMiddle.Should().Be(8d);
    }

    [TestMethod]
    public void Create_ShouldApplyCompactControlHeights()
    {
        var tokens = AntdTokenFactory.Create(AntdThemeMode.Compact, AntdSeedToken.Default);

        tokens.ControlHeightSmall.Should().Be(28d);
        tokens.ControlHeightMiddle.Should().Be(36d);
        tokens.ControlHeightLarge.Should().Be(44d);
    }
}
