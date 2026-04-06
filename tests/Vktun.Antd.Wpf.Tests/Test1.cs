using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;
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
            ((SolidColorBrush)themeResources[AntdResourceKeys.BrushPrimary]).Color.Should().Be(Colors.HotPink);
        });
    }

    [TestMethod]
    public void ThemeResources_ShouldLoadFromXaml()
    {
        WpfTestHost.Run(() =>
        {
            const string xaml = """
<ResourceDictionary xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                    xmlns:antd="clr-namespace:Vktun.Antd.Wpf;assembly=Vktun.Antd.Wpf">
    <ResourceDictionary.MergedDictionaries>
        <antd:AntdThemeResources Theme="Light" />
    </ResourceDictionary.MergedDictionaries>
</ResourceDictionary>
""";

            var resources = XamlReader.Parse(xaml).Should().BeOfType<ResourceDictionary>().Subject;
            resources.MergedDictionaries.Should().ContainSingle();
            resources.MergedDictionaries[0].Should().BeOfType<AntdThemeResources>();
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
    public void ComboBoxTemplate_ShouldExposePopupPartsAndReactToThemeSwitch()
    {
        WpfTestHost.Run(() =>
        {
            Application.Current!.Resources = new ResourceDictionary();
            Application.Current.Resources.MergedDictionaries.Add(new AntdThemeResources());

            var comboBox = new ComboBox
            {
                Width = 220,
                ItemsSource = new[] { "Ocean", "Aurora", "Nebula" },
                SelectedIndex = 0,
            };

            var window = new Window
            {
                Content = comboBox,
                Width = 420,
                Height = 240,
            };

            try
            {
                window.Show();
                comboBox.ApplyTemplate();
                comboBox.IsDropDownOpen = true;
                WpfTestHost.Pump();

                var popup = comboBox.Template.FindName("PART_Popup", comboBox).Should().BeOfType<Popup>().Subject;
                var popupBorder = comboBox.Template.FindName("PART_PopupBorder", comboBox).Should().BeOfType<Border>().Subject;
                var arrow = comboBox.Template.FindName("PART_Arrow", comboBox).Should().BeOfType<Path>().Subject;

                popup.PlacementTarget.Should().BeSameAs(comboBox);
                ((SolidColorBrush)popupBorder.Background).Color.Should().Be(((SolidColorBrush)Application.Current.Resources[AntdResourceKeys.BrushBgElevated]).Color);
                ((SolidColorBrush)arrow.Stroke).Color.Should().Be(((SolidColorBrush)Application.Current.Resources[AntdResourceKeys.BrushPrimary]).Color);

                AntdThemeManager.Current.Apply(Application.Current, AntdThemeMode.Dark);
                comboBox.IsDropDownOpen = false;
                comboBox.ApplyTemplate();
                comboBox.IsDropDownOpen = true;
                WpfTestHost.Pump();

                popupBorder = comboBox.Template.FindName("PART_PopupBorder", comboBox).Should().BeOfType<Border>().Subject;
                popupBorder.Background.Should().BeOfType<SolidColorBrush>();
                popupBorder.BorderBrush.Should().BeOfType<SolidColorBrush>();
            }
            finally
            {
                window.Close();
            }
        });
    }

    [TestMethod]
    public void DatePickerTemplate_ShouldExposePopupAndCalendarPartsAndReactToThemeSwitch()
    {
        WpfTestHost.Run(() =>
        {
            Application.Current!.Resources = new ResourceDictionary();
            Application.Current.Resources.MergedDictionaries.Add(new AntdThemeResources());

            var datePicker = new DatePicker
            {
                Width = 220,
                SelectedDate = new DateTime(2026, 4, 3),
                DisplayDate = new DateTime(2026, 4, 3),
            };

            var window = new Window
            {
                Content = datePicker,
                Width = 420,
                Height = 280,
            };

            try
            {
                window.Show();
                datePicker.ApplyTemplate();
                datePicker.IsDropDownOpen = true;
                WpfTestHost.Pump();

                var popup = datePicker.Template.FindName("PART_Popup", datePicker).Should().BeOfType<Popup>().Subject;
                var popupBorder = datePicker.Template.FindName("PART_PopupBorder", datePicker).Should().BeOfType<Border>().Subject;
                var calendar = datePicker.Template.FindName("PART_Calendar", datePicker).Should().BeOfType<Calendar>().Subject;

                popup.PlacementTarget.Should().BeSameAs(datePicker);
                datePicker.IsDropDownOpen.Should().BeTrue();
                calendar.ApplyTemplate();

                var calendarItem = calendar.Template.FindName("PART_CalendarItem", calendar).Should().BeOfType<CalendarItem>().Subject;
                calendarItem.ApplyTemplate();

                var headerButton = FindVisualChildren<Button>(calendarItem).FirstOrDefault(button => button.Name == "PART_HeaderButton");
                var dayButton = FindVisualChildren<CalendarDayButton>(calendarItem).FirstOrDefault();
                var selectedDay = FindVisualChildren<CalendarDayButton>(calendarItem).FirstOrDefault(button => button.IsSelected);

                popupBorder.Background.Should().BeOfType<SolidColorBrush>();
                headerButton.Should().NotBeNull();
                headerButton!.Background.Should().BeOfType<SolidColorBrush>();
                ((SolidColorBrush)headerButton.Background).Color.Should().Be(((SolidColorBrush)Application.Current.Resources[AntdResourceKeys.BrushFillQuaternary]).Color);

                dayButton.Should().NotBeNull();
                calendar.CalendarDayButtonStyle.Should().NotBeNull();
                dayButton!.Style.Should().BeSameAs(calendar.CalendarDayButtonStyle);
                dayButton.FontSize.Should().Be((double)Application.Current.Resources[AntdResourceKeys.FontSizeSmall]);

                if (selectedDay is not null)
                {
                    selectedDay.Style.Should().BeSameAs(calendar.CalendarDayButtonStyle);
                }

                AntdThemeManager.Current.Apply(Application.Current, AntdThemeMode.Dark);
                datePicker.IsDropDownOpen = false;
                datePicker.ApplyTemplate();
                datePicker.IsDropDownOpen = true;
                WpfTestHost.Pump();

                popupBorder = datePicker.Template.FindName("PART_PopupBorder", datePicker).Should().BeOfType<Border>().Subject;
                popupBorder.Background.Should().BeOfType<SolidColorBrush>();
                popupBorder.BorderBrush.Should().BeOfType<SolidColorBrush>();
            }
            finally
            {
                window.Close();
            }
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

    [TestMethod]
    public void FloatButton_ShouldAttachToOverlayHostWhenGlobal()
    {
        WpfTestHost.Run(() =>
        {
            Application.Current!.Resources = new ResourceDictionary();
            Application.Current.Resources.MergedDictionaries.Add(new AntdThemeResources());

            var container = new StackPanel();
            var floatButton = new FloatButton
            {
                IsGlobal = true,
            };
            container.Children.Add(floatButton);

            var window = new Window
            {
                Content = new Grid
                {
                    Children =
                    {
                        container,
                    },
                },
                Width = 600,
                Height = 400,
            };

            window.Show();
            WpfTestHost.Pump();

            OverlayHost.Get(window).Should().NotBeNull();
            container.Children.Count.Should().Be(0);
            ((Grid)window.Content).Children.Count.Should().BeGreaterThan(1);

            floatButton.IsGlobal = false;
            WpfTestHost.Pump();

            container.Children.Count.Should().Be(1);
            container.Children[0].Should().BeSameAs(floatButton);
            window.Close();
        });
    }

    [TestMethod]
    public void Pagination_ShouldClampCurrentPageToPageCount()
    {
        WpfTestHost.Run(() =>
        {
            var pagination = new Pagination
            {
                Total = 500,
                PageSize = 10,
                CurrentPage = 999,
            };

            pagination.CurrentPage.Should().Be(50);
        });
    }

    private static IEnumerable<T> FindVisualChildren<T>(DependencyObject root)
        where T : DependencyObject
    {
        var count = VisualTreeHelper.GetChildrenCount(root);
        for (var index = 0; index < count; index++)
        {
            var child = VisualTreeHelper.GetChild(root, index);
            if (child is T typedChild)
            {
                yield return typedChild;
            }

            foreach (var nestedChild in FindVisualChildren<T>(child))
            {
                yield return nestedChild;
            }
        }
    }
}



