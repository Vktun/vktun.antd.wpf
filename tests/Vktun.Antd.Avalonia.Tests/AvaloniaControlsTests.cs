using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using FluentAssertions;
using Vktun.Antd.Avalonia;
using AntdButton = Vktun.Antd.Avalonia.Button;
using AntdComboBox = Vktun.Antd.Avalonia.ComboBox;
using AntdProgress = Vktun.Antd.Avalonia.Progress;
using AntdSlider = Vktun.Antd.Avalonia.Slider;
using AntdSwitch = Vktun.Antd.Avalonia.Switch;

namespace Vktun.Antd.Avalonia.Tests;

[TestClass]
public sealed class AvaloniaControlsTests
{
    [TestMethod]
    public void AvaloniaControls_ShouldExposeWpfAlignedSurface()
    {
        var controlTypes = new[]
        {
            typeof(Alert),
            typeof(Avatar),
            typeof(Badge),
            typeof(Breadcrumb),
            typeof(AntdButton),
            typeof(Calendar),
            typeof(Card),
            typeof(Checkbox),
            typeof(Collapse),
            typeof(AntdComboBox),
            typeof(DatePicker),
            typeof(Descriptions),
            typeof(Divider),
            typeof(Drawer),
            typeof(Dropdown),
            typeof(Empty),
            typeof(Flex),
            typeof(FloatButton),
            typeof(Form),
            typeof(Grid),
#pragma warning disable CS0618
            typeof(IconButton),
#pragma warning restore CS0618
            typeof(Input),
            typeof(InputNumber),
            typeof(Layout),
            typeof(List),
            typeof(Menu),
            typeof(Pagination),
            typeof(PasswordInput),
            typeof(Popconfirm),
            typeof(AntdProgress),
            typeof(Radio),
            typeof(Result),
            typeof(Segmented),
            typeof(Skeleton),
            typeof(AntdSlider),
            typeof(Space),
            typeof(Spin),
            typeof(Splitter),
            typeof(Statistic),
            typeof(Steps),
            typeof(AntdSwitch),
            typeof(Table),
            typeof(Tabs),
            typeof(Tag),
            typeof(Timeline),
            typeof(Tooltip),
            typeof(Typography),
            typeof(Watermark),
        };

        controlTypes.Should().OnlyContain(static type => type.IsPublic);
    }

    [TestMethod]
    public void StyledProperties_ShouldRoundTripCommonControls()
    {
        var button = new AntdButton
        {
            Type = AntdButtonType.Primary,
            Size = AntdControlSize.Large,
            Status = AntdStatus.Warning,
            Icon = "+",
        };

        var input = new Input
        {
            Prefix = "@",
            Suffix = ".com",
            Status = AntdStatus.Success,
            Size = AntdControlSize.Small,
            Variant = AntdInputVariant.Filled,
        };

        var tag = new Tag
        {
            Color = AntdTagColor.Success,
            IsClosable = true,
            IsCheckable = true,
            IsChecked = true,
            Borderless = true,
            Content = "Ready",
        };

        button.Type.Should().Be(AntdButtonType.Primary);
        button.Size.Should().Be(AntdControlSize.Large);
        button.Status.Should().Be(AntdStatus.Warning);
        button.Icon.Should().Be("+");
        input.Prefix.Should().Be("@");
        input.Suffix.Should().Be(".com");
        input.Status.Should().Be(AntdStatus.Success);
        input.Size.Should().Be(AntdControlSize.Small);
        input.Variant.Should().Be(AntdInputVariant.Filled);
        tag.Color.Should().Be(AntdTagColor.Success);
        tag.IsClosable.Should().BeTrue();
        tag.IsCheckable.Should().BeTrue();
        tag.IsChecked.Should().BeTrue();
        tag.Borderless.Should().BeTrue();
    }

    [TestMethod]
    public void InputNumberAndPagination_ShouldCoerceValues()
    {
        var inputNumber = new InputNumber
        {
            Minimum = 0,
            Maximum = 10,
            Precision = 2,
            Value = 12.345d,
        };

        var pagination = new Pagination
        {
            Total = 500,
            PageSize = 10,
            CurrentPage = 999,
        };

        inputNumber.Value.Should().Be(10d);

        inputNumber.Value = 3.333d;

        inputNumber.Value.Should().Be(3.33d);
        pagination.PageCount.Should().Be(50);
        pagination.CurrentPage.Should().Be(50);
    }

    [TestMethod]
    public void Tabs_ShouldSelectFirstEnabledPaneAndSwitchByCommand()
    {
        var designPane = new TabPane { Key = "design", Header = "Design" };
        var codePane = new TabPane { Key = "code", Header = "Code" };
        var tabs = new Tabs
        {
            Items = new ObservableCollection<TabPane> { designPane, codePane },
        };

        tabs.SelectedIndex.Should().Be(0);
        tabs.SelectedKey.Should().Be("design");
        tabs.SelectedItem.Should().BeSameAs(designPane);
        designPane.IsSelected.Should().BeTrue();

        tabs.SelectTabCommand.Execute(codePane);

        tabs.SelectedIndex.Should().Be(1);
        tabs.SelectedKey.Should().Be("code");
        tabs.SelectedItem.Should().BeSameAs(codePane);
        codePane.IsSelected.Should().BeTrue();
        designPane.IsSelected.Should().BeFalse();
    }

    [TestMethod]
    public void ThemeManager_ShouldUpdateExistingThemeResources()
    {
        var application = new Application();
        var themeResources = new AntdThemeResources();
        application.Resources.MergedDictionaries.Add(themeResources);

        AntdThemeManager.Current.Apply(application, AntdThemeMode.Dark, new AntdSeedToken { PrimaryColor = AntdColor.Parse("#FF69B4") });

        themeResources.Theme.Should().Be(AntdThemeMode.Dark);
        var brush = themeResources[AntdResourceKeys.BrushPrimary].Should().BeOfType<SolidColorBrush>().Subject;
        brush.Color.Should().Be(Color.FromRgb(255, 105, 180));
    }

    [TestMethod]
    public void OverlayServices_ShouldAttachHostAndCompleteModalTasks()
    {
        var root = new global::Avalonia.Controls.Grid();
        var messageService = new MessageService();
        var notificationService = new NotificationService();
        var modalService = new ModalService();

        messageService.Show(root, "Saved", MessageKind.Success);
        notificationService.Show(root, new NotificationRequest { Title = "Uploaded", Description = "File complete" });
        var modalTask = modalService.ShowAsync(root, new ModalRequest { Title = "Confirm", Content = "Continue?" });

        var host = OverlayHost.Get(root);

        host.Should().NotBeNull();
        host!.Items.Count.Should().Be(3);
        modalTask.IsCompleted.Should().BeFalse();

        host.CloseLatestModal(true);

        modalTask.IsCompletedSuccessfully.Should().BeTrue();
        modalTask.Result.Should().BeTrue();
    }
}
