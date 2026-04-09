using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
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

    #pragma warning disable CS0618
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
    #pragma warning restore CS0618

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
                var border = comboBox.Template.FindName("Border", comboBox).Should().BeOfType<Border>().Subject;

                // HandyControl-inspired design: Popup positions relative to Border (not entire ComboBox)
                popup.PlacementTarget.Should().BeSameAs(border);
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
    public void ComboBoxToggleSurface_ShouldProvideHitTestAreaAndToggleDropDown()
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
                WpfTestHost.Pump();

                var toggle = comboBox.Template.FindName("PART_DropDownToggle", comboBox).Should().BeOfType<ToggleBlock>().Subject;
                toggle.ApplyTemplate();
                WpfTestHost.Pump();

                VisualTreeHelper.GetChild(toggle, 0).Should().BeOfType<Border>();
                comboBox.IsDropDownOpen.Should().BeFalse();

                InvokeToggleBlockMouseDown(toggle);
                WpfTestHost.Pump();

                comboBox.IsDropDownOpen.Should().BeTrue();
                toggle.IsChecked.Should().BeTrue();

                InvokeToggleBlockMouseDown(toggle);
                WpfTestHost.Pump();

                comboBox.IsDropDownOpen.Should().BeFalse();
                toggle.IsChecked.Should().BeFalse();
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

                var headerButton = calendarItem.Template.FindName("PART_HeaderButton", calendarItem) as Button
                    ?? FindVisualChildren<Button>(calendarItem).FirstOrDefault(button => button.Name == "PART_HeaderButton");
                var dayButton = FindVisualChildren<CalendarDayButton>(calendarItem).FirstOrDefault();
                var selectedDay = FindVisualChildren<CalendarDayButton>(calendarItem).FirstOrDefault(button => button.IsSelected);

                popupBorder.Background.Should().BeOfType<SolidColorBrush>();

                if (headerButton is not null)
                {
                    headerButton.Background.Should().BeOfType<SolidColorBrush>();
                    ((SolidColorBrush)headerButton.Background).Color.Should().Be(((SolidColorBrush)Application.Current.Resources[AntdResourceKeys.BrushFillQuaternary]).Color);
                }

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

    [TestMethod]
    public void InputNumber_ShouldClampStepPrecisionAndSynchronizeText()
    {
        WpfTestHost.Run(() =>
        {
            InitializeThemeResources();

            var inputNumber = new InputNumber
            {
                Width = 180,
                Minimum = 0,
                Maximum = 10,
                Step = 0.25,
                Precision = 2,
                Value = 12.345,
                Prefix = "RMB",
                Suffix = "CNY",
            };

            var window = CreateWindow(inputNumber);

            try
            {
                window.Show();
                inputNumber.ApplyTemplate();
                WpfTestHost.Pump();

                inputNumber.Value.Should().Be(10d);

                var textBox = inputNumber.Template.FindName("PART_TextBox", inputNumber).Should().BeOfType<TextBox>().Subject;
                textBox.Text.Should().Be("10.00");

                inputNumber.Decrement();
                inputNumber.Value.Should().Be(9.75d);
                textBox.Text.Should().Be("9.75");

                textBox.Text = "3.333";
                CommitInputNumberText(inputNumber);

                inputNumber.Value.Should().Be(3.33d);
                textBox.Text.Should().Be("3.33");

                inputNumber.Value = -2d;
                inputNumber.Value.Should().Be(0d);
            }
            finally
            {
                window.Close();
            }
        });
    }

    [TestMethod]
    public void Segmented_ShouldSwitchSelectionAndPreserveDisabledItems()
    {
        WpfTestHost.Run(() =>
        {
            InitializeThemeResources();

            var segmented = new Segmented
            {
                Block = true,
                SelectedIndex = 0,
            };
            segmented.Items.Add(new ListBoxItem { Content = "Daily" });
            segmented.Items.Add(new ListBoxItem { Content = "Weekly", IsEnabled = false });
            segmented.Items.Add(new ListBoxItem { Content = "Monthly" });

            var window = CreateWindow(segmented);

            try
            {
                window.Show();
                segmented.ApplyTemplate();
                WpfTestHost.Pump();

                segmented.Block.Should().BeTrue();
                segmented.ItemContainerStyle.Should().NotBeNull();
                ((ListBoxItem)segmented.Items[1]).IsEnabled.Should().BeFalse();

                segmented.SelectedIndex = 2;

                segmented.SelectedIndex.Should().Be(2);
                segmented.SelectedItem.Should().BeSameAs(segmented.Items[2]);
            }
            finally
            {
                window.Close();
            }
        });
    }

    [TestMethod]
    public void Statistic_ShouldFormatValueAndRenderPrefixSuffix()
    {
        WpfTestHost.Run(() =>
        {
            InitializeThemeResources();

            var statistic = new Statistic
            {
                Title = "Conversion",
                Value = 0.2734,
                ValueFormat = "{0:P1}",
                Prefix = "+",
                Suffix = "pts",
            };

            var window = CreateWindow(statistic);

            try
            {
                window.Show();
                statistic.ApplyTemplate();
                WpfTestHost.Pump();

                statistic.FormattedValue.Should().Be(string.Format(CultureInfo.InvariantCulture, "{0:P1}", 0.2734));

                var texts = FindVisualChildren<TextBlock>(statistic).Select(static textBlock => textBlock.Text).ToList();
                texts.Should().Contain("Conversion");
                texts.Should().Contain(statistic.FormattedValue);
                texts.Should().Contain("+");
                texts.Should().Contain("pts");
            }
            finally
            {
                window.Close();
            }
        });
    }

    [TestMethod]
    public void Watermark_ShouldLoadTemplateAndCreateBrush()
    {
        WpfTestHost.Run(() =>
        {
            InitializeThemeResources();

            var watermark = new Watermark
            {
                Text = "Ant Design WPF",
                Content = new Border
                {
                    Width = 200,
                    Height = 80,
                    Background = Brushes.White,
                },
            };

            var window = CreateWindow(watermark);

            try
            {
                window.Show();
                watermark.ApplyTemplate();
                WpfTestHost.Pump();

                watermark.WatermarkBrush.Should().NotBeSameAs(Brushes.Transparent);

                var overlay = FindVisualChildren<Rectangle>(watermark).FirstOrDefault();
                overlay.Should().NotBeNull();
                overlay!.Fill.Should().BeSameAs(watermark.WatermarkBrush);
            }
            finally
            {
                window.Close();
            }
        });
    }

    [TestMethod]
    public void Calendar_ShouldApplyStandaloneCalendarStyles()
    {
        WpfTestHost.Run(() =>
        {
            InitializeThemeResources();

            var calendar = new Vktun.Antd.Wpf.Calendar
            {
                SelectedDate = new DateTime(2026, 4, 9),
                DisplayDate = new DateTime(2026, 4, 9),
            };

            var window = CreateWindow(calendar, height: 360);

            try
            {
                window.Show();
                calendar.ApplyTemplate();
                WpfTestHost.Pump();

                calendar.CalendarItemStyle.Should().NotBeNull();
                calendar.CalendarButtonStyle.Should().NotBeNull();
                calendar.CalendarDayButtonStyle.Should().NotBeNull();
                calendar.Template.FindName("PART_CalendarItem", calendar).Should().BeOfType<CalendarItem>();
            }
            finally
            {
                window.Close();
            }
        });
    }

    [TestMethod]
    public void Slider_ShouldExposeTrackFromTemplate()
    {
        WpfTestHost.Run(() =>
        {
            InitializeThemeResources();

            var slider = new Vktun.Antd.Wpf.Slider
            {
                Minimum = 0,
                Maximum = 100,
                Value = 64,
            };

            var window = CreateWindow(slider);

            try
            {
                window.Show();
                slider.ApplyTemplate();
                WpfTestHost.Pump();

                slider.Template.FindName("PART_Track", slider).Should().BeOfType<Track>();
            }
            finally
            {
                window.Close();
            }
        });
    }

    [TestMethod]
    public void Splitter_ShouldApplyTemplateAndRenderGrip()
    {
        WpfTestHost.Run(() =>
        {
            InitializeThemeResources();

            var grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            grid.ColumnDefinitions.Add(new ColumnDefinition());

            var splitter = new Splitter
            {
                ResizeDirection = GridResizeDirection.Columns,
                ResizeBehavior = GridResizeBehavior.PreviousAndNext,
            };
            Grid.SetColumn(splitter, 1);
            grid.Children.Add(new Border());
            grid.Children.Add(splitter);
            grid.Children.Add(new Border { });
            Grid.SetColumn(grid.Children[2], 2);

            var window = CreateWindow(grid);

            try
            {
                window.Show();
                splitter.ApplyTemplate();
                WpfTestHost.Pump();

                splitter.Template.Should().NotBeNull();
                VisualTreeHelper.GetChildrenCount(splitter).Should().BeGreaterThan(0);
            }
            finally
            {
                window.Close();
            }
        });
    }

    private static Window CreateWindow(object content, double width = 420, double height = 280)
    {
        return new Window
        {
            Content = content,
            Width = width,
            Height = height,
        };
    }

    private static void InitializeThemeResources()
    {
        Application.Current!.Resources = new ResourceDictionary();
        Application.Current.Resources.MergedDictionaries.Add(new AntdThemeResources());
    }

    private static void CommitInputNumberText(InputNumber inputNumber)
    {
        var commitTextMethod = typeof(InputNumber).GetMethod("CommitText", BindingFlags.Instance | BindingFlags.NonPublic);
        commitTextMethod.Should().NotBeNull();
        commitTextMethod!.Invoke(inputNumber, null);

        WpfTestHost.Pump();
    }

    private static void InvokeToggleBlockMouseDown(ToggleBlock toggle)
    {
        var onMouseLeftButtonDown = typeof(ToggleBlock).GetMethod("OnMouseLeftButtonDown", BindingFlags.Instance | BindingFlags.NonPublic);
        onMouseLeftButtonDown.Should().NotBeNull();

        var args = new MouseButtonEventArgs(Mouse.PrimaryDevice, Environment.TickCount, MouseButton.Left)
        {
            RoutedEvent = UIElement.MouseLeftButtonDownEvent,
            Source = toggle,
        };

        onMouseLeftButtonDown!.Invoke(toggle, new object[] { args });
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


