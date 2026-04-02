using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using FluentAssertions;
using Vktun.Antd.Wpf;

namespace Vktun.Antd.Wpf.Tests;

[TestClass]
public class AntdThemeTests
{
    [TestMethod]
    public void ThemeResources_ShouldExposeCoreKeys()
    {
        WpfTestHost.Run(() =>
        {
            var resources = new AntdThemeResources();

            resources.Contains(AntdResourceKeys.BrushPrimary).Should().BeTrue();
            resources.Contains(AntdResourceKeys.BrushText).Should().BeTrue();
            resources.Contains(AntdResourceKeys.ControlHeightMiddle).Should().BeTrue();
            resources.Contains(AntdResourceKeys.ShadowPopup).Should().BeTrue();
            resources[AntdResourceKeys.BorderRadiusMiddle].Should().BeOfType<CornerRadius>();
        });
    }

    [TestMethod]
    public void ThemeManager_ShouldUpdateExistingThemeResources()
    {
        WpfTestHost.Run(() =>
        {
            Application.Current!.Resources = new ResourceDictionary();
            var themeResources = new AntdThemeResources();
            Application.Current.Resources.MergedDictionaries.Add(themeResources);

            AntdThemeManager.Current.Apply(Application.Current, AntdThemeMode.Dark, new AntdSeedToken { PrimaryColor = Colors.HotPink });

            themeResources.Theme.Should().Be(AntdThemeMode.Dark);
            (((SolidColorBrush)themeResources[AntdResourceKeys.BrushPrimary]).Color).Should().Be(Colors.HotPink);
        });
    }

    [TestMethod]
    public void AttachedProperties_ShouldRoundTrip()
    {
        WpfTestHost.Run(() =>
        {
            var button = new Button();
            var textBox = new TextBox();

            ButtonAssist.SetType(button, AntdButtonType.Primary);
            ControlAssist.SetSize(button, AntdControlSize.Large);
            InputAssist.SetPrefix(textBox, "@");
            InputAssist.SetSuffix(textBox, ".com");
            StatusAssist.SetStatus(textBox, AntdStatus.Success);

            ButtonAssist.GetType(button).Should().Be(AntdButtonType.Primary);
            ControlAssist.GetSize(button).Should().Be(AntdControlSize.Large);
            InputAssist.GetPrefix(textBox).Should().Be("@");
            InputAssist.GetSuffix(textBox).Should().Be(".com");
            StatusAssist.GetStatus(textBox).Should().Be(AntdStatus.Success);
        });
    }

    [TestMethod]
    public void Services_ShouldAttachOverlayHostToWindow()
    {
        WpfTestHost.Run(() =>
        {
            Application.Current!.Resources = new ResourceDictionary();
            Application.Current.Resources.MergedDictionaries.Add(new AntdThemeResources());

            var window = new Window
            {
                Content = new Grid(),
                Width = 600,
                Height = 400,
            };

            var messageService = new MessageService();
            var notificationService = new NotificationService();
            var modalService = new ModalService();

            messageService.Show(window, "test");
            notificationService.Show(window, new NotificationRequest { Title = "title", Description = "body" });
            var modalTask = modalService.ShowAsync(window, new ModalRequest { Title = "modal", Content = "body" });

            WpfTestHost.Pump();

            var root = window.Content.Should().BeOfType<Grid>().Subject;
            root.Children.Count.Should().BeGreaterThan(1);
            OverlayHost.Get(window).Should().NotBeNull();
            modalTask.IsCompleted.Should().BeFalse();
        });
    }
}

