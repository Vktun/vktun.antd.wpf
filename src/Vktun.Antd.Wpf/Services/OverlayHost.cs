using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Threading;

namespace Vktun.Antd.Wpf;

internal sealed class OverlayHost
{
    private readonly StackPanel _messagePanel;
    private readonly StackPanel _notificationRightPanel;
    private readonly StackPanel _notificationLeftPanel;
    private readonly Grid _floatButtonLayer;
    private readonly Grid _modalLayer;
    private readonly Dictionary<FloatButton, ContentControl> _floatButtonHosts = [];

    private OverlayHost(
        StackPanel messagePanel,
        StackPanel notificationRightPanel,
        StackPanel notificationLeftPanel,
        Grid floatButtonLayer,
        Grid modalLayer)
    {
        _messagePanel = messagePanel;
        _notificationRightPanel = notificationRightPanel;
        _notificationLeftPanel = notificationLeftPanel;
        _floatButtonLayer = floatButtonLayer;
        _modalLayer = modalLayer;
    }

    public static OverlayHost Attach(Window owner)
    {
        ArgumentNullException.ThrowIfNull(owner);

        if (owner.Content is Grid existingGrid && existingGrid.Tag is OverlayHost existingHost)
        {
            return existingHost;
        }

        var originalContent = owner.Content as UIElement ?? new Grid();
        var root = owner.Content as Grid;
        if (root is null || root.Tag is not OverlayHost)
        {
            root = new Grid();
            owner.Content = null;
            root.Children.Add(originalContent);
            owner.Content = root;
        }

        var passiveOverlay = new Grid
        {
            IsHitTestVisible = false,
        };

        var messagePanel = new StackPanel
        {
            VerticalAlignment = VerticalAlignment.Top,
            HorizontalAlignment = HorizontalAlignment.Center,
            Margin = new Thickness(0d, 24d, 0d, 0d),
        };

        var notificationRightPanel = new StackPanel
        {
            VerticalAlignment = VerticalAlignment.Top,
            HorizontalAlignment = HorizontalAlignment.Right,
            Margin = new Thickness(0d, 24d, 24d, 0d),
        };

        var notificationLeftPanel = new StackPanel
        {
            VerticalAlignment = VerticalAlignment.Top,
            HorizontalAlignment = HorizontalAlignment.Left,
            Margin = new Thickness(24d, 24d, 0d, 0d),
        };

        var interactiveOverlay = new Grid();

        var floatButtonLayer = new Grid();

        var modalLayer = new Grid
        {
            Visibility = Visibility.Collapsed,
            IsHitTestVisible = false,
        };

        passiveOverlay.Children.Add(messagePanel);
        passiveOverlay.Children.Add(notificationRightPanel);
        passiveOverlay.Children.Add(notificationLeftPanel);

        interactiveOverlay.Children.Add(floatButtonLayer);
        interactiveOverlay.Children.Add(modalLayer);

        root.Children.Add(passiveOverlay);
        root.Children.Add(interactiveOverlay);

        var host = new OverlayHost(messagePanel, notificationRightPanel, notificationLeftPanel, floatButtonLayer, modalLayer);
        root.Tag = host;
        return host;
    }

    public void AddFloatButton(FloatButton button)
    {
        ArgumentNullException.ThrowIfNull(button);

        if (_floatButtonHosts.TryGetValue(button, out var existingHost))
        {
            UpdateFloatButton(button);
            existingHost.Visibility = button.Visibility;
            return;
        }

        var host = new ContentControl
        {
            Content = button,
            Focusable = false,
        };

        _floatButtonHosts.Add(button, host);
        _floatButtonLayer.Children.Add(host);
        UpdateFloatButton(button);
    }

    public void UpdateFloatButton(FloatButton button)
    {
        ArgumentNullException.ThrowIfNull(button);

        if (!_floatButtonHosts.TryGetValue(button, out var host))
        {
            return;
        }

        host.Visibility = button.Visibility;
        host.Margin = button.GlobalMargin;

        switch (button.Placement)
        {
            case AntdFloatButtonPlacement.BottomLeft:
                host.HorizontalAlignment = HorizontalAlignment.Left;
                host.VerticalAlignment = VerticalAlignment.Bottom;
                break;
            case AntdFloatButtonPlacement.TopRight:
                host.HorizontalAlignment = HorizontalAlignment.Right;
                host.VerticalAlignment = VerticalAlignment.Top;
                break;
            case AntdFloatButtonPlacement.TopLeft:
                host.HorizontalAlignment = HorizontalAlignment.Left;
                host.VerticalAlignment = VerticalAlignment.Top;
                break;
            default:
                host.HorizontalAlignment = HorizontalAlignment.Right;
                host.VerticalAlignment = VerticalAlignment.Bottom;
                break;
        }
    }

    public void RemoveFloatButton(FloatButton button)
    {
        ArgumentNullException.ThrowIfNull(button);

        if (!_floatButtonHosts.Remove(button, out var host))
        {
            return;
        }

        _floatButtonLayer.Children.Remove(host);
        host.Content = null;
    }

    public Border ShowMessage(Window owner, string content, MessageKind kind, TimeSpan duration)
    {
        var item = CreateToast(owner, content, kind, width: 320d, showTitle: false);
        _messagePanel.Children.Add(item);
        ScheduleRemoval(item, _messagePanel, duration);
        return item;
    }

    public Border ShowNotification(Window owner, NotificationRequest request)
    {
        var text = string.IsNullOrWhiteSpace(request.Description)
            ? request.Title
            : $"{request.Title}{Environment.NewLine}{request.Description}";
        var item = CreateToast(owner, text, request.Kind, width: 360d, showTitle: true);
        var panel = request.Placement == NotificationPlacement.TopLeft ? _notificationLeftPanel : _notificationRightPanel;
        panel.Children.Add(item);
        ScheduleRemoval(item, panel, request.Duration);
        return item;
    }

    public Task<bool?> ShowModalAsync(Window owner, ModalRequest request, CancellationToken cancellationToken)
    {
        var completionSource = new TaskCompletionSource<bool?>();
        Grid? layer = null;
        var mask = new Border
        {
            Background = owner.TryFindResource(AntdResourceKeys.BrushMask) as Brush ?? new SolidColorBrush(Color.FromArgb(120, 0, 0, 0)),
        };

        var titleText = new TextBlock
        {
            Text = request.Title,
            FontSize = owner.TryFindResource(AntdResourceKeys.FontSizeLarge) is double fontSize ? fontSize : 16d,
            FontWeight = FontWeights.SemiBold,
            Margin = new Thickness(0d, 0d, 0d, 12d),
        };

        var content = request.Content as UIElement ?? new TextBlock
        {
            Text = request.Content?.ToString() ?? string.Empty,
            TextWrapping = TextWrapping.Wrap,
        };

        var footer = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            HorizontalAlignment = HorizontalAlignment.Right,
        };
        SpaceAssist.SetGap(footer, 8d);

        void Close(bool? result)
        {
            if (completionSource.Task.IsCompleted)
            {
                return;
            }

            if (layer is null)
            {
                completionSource.TrySetResult(result);
                return;
            }

            _modalLayer.Children.Remove(layer);
            _modalLayer.Visibility = _modalLayer.Children.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
            _modalLayer.IsHitTestVisible = _modalLayer.Children.Count > 0;
            completionSource.TrySetResult(result);
        }

        var cancelButton = new Button
        {
            Content = request.CancelText,
            MinWidth = 88d,
        };
        cancelButton.Click += (_, _) => Close(false);

        var okButton = new Button
        {
            Content = request.OkText,
            MinWidth = 88d,
        };
        ButtonAssist.SetType(okButton, AntdButtonType.Primary);
        okButton.Click += (_, _) => Close(true);

        if (request.ShowCancel)
        {
            footer.Children.Add(cancelButton);
        }

        footer.Children.Add(okButton);

        var stack = new StackPanel();
        stack.Children.Add(titleText);
        stack.Children.Add(content);
        stack.Children.Add(new Border { Height = 20d, Background = Brushes.Transparent });
        stack.Children.Add(footer);

        var card = new Border
        {
            Width = 420d,
            MaxWidth = 560d,
            Background = owner.TryFindResource(AntdResourceKeys.BrushBgElevated) as Brush ?? Brushes.White,
            BorderBrush = owner.TryFindResource(AntdResourceKeys.BrushBorderSecondary) as Brush ?? Brushes.LightGray,
            BorderThickness = new Thickness(1d),
            CornerRadius = new CornerRadius(12d),
            Padding = new Thickness(24d),
            Effect = owner.TryFindResource(AntdResourceKeys.ShadowModal) as Effect,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            Child = stack,
        };

        layer = new Grid
        {
            IsHitTestVisible = true,
        };
        layer.Children.Add(mask);
        layer.Children.Add(card);

        if (request.MaskClosable)
        {
            mask.MouseLeftButtonDown += (_, _) => Close(null);
        }

        _modalLayer.Children.Add(layer);
        _modalLayer.Visibility = Visibility.Visible;
        _modalLayer.IsHitTestVisible = true;

        cancellationToken.Register(() => owner.Dispatcher.Invoke(() => Close(null)));
        return completionSource.Task;
    }

    public static OverlayHost? Get(Window owner)
    {
        return owner.Content is Grid grid ? grid.Tag as OverlayHost : null;
    }

    private static void ScheduleRemoval(Border item, Panel panel, TimeSpan duration)
    {
        var timer = new DispatcherTimer { Interval = duration };
        timer.Tick += (_, _) =>
        {
            timer.Stop();
            panel.Children.Remove(item);
        };

        timer.Start();
    }

    private static Border CreateToast(Window owner, string content, MessageKind kind, double width, bool showTitle)
    {
        var accent = kind switch
        {
            MessageKind.Success => owner.TryFindResource(AntdResourceKeys.BrushSuccess) as Brush,
            MessageKind.Warning => owner.TryFindResource(AntdResourceKeys.BrushWarning) as Brush,
            MessageKind.Error => owner.TryFindResource(AntdResourceKeys.BrushError) as Brush,
            _ => owner.TryFindResource(AntdResourceKeys.BrushPrimary) as Brush,
        } ?? Brushes.DodgerBlue;

        var parts = content.Split(Environment.NewLine, StringSplitOptions.None);
        var panel = new StackPanel();

        if (showTitle && parts.Length > 1)
        {
            panel.Children.Add(new TextBlock
            {
                Text = parts[0],
                FontWeight = FontWeights.SemiBold,
                Margin = new Thickness(0d, 0d, 0d, 4d),
            });

            panel.Children.Add(new TextBlock
            {
                Text = string.Join(Environment.NewLine, parts.Skip(1)),
                Opacity = 0.86d,
                TextWrapping = TextWrapping.Wrap,
            });
        }
        else
        {
            panel.Children.Add(new TextBlock
            {
                Text = content,
                TextWrapping = TextWrapping.Wrap,
            });
        }

        var body = new Grid();
        body.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
        body.ColumnDefinitions.Add(new ColumnDefinition());

        body.Children.Add(new Border
        {
            Width = 10d,
            Height = 10d,
            CornerRadius = new CornerRadius(5d),
            Background = accent,
            Margin = new Thickness(0d, 6d, 12d, 0d),
            VerticalAlignment = VerticalAlignment.Top,
        });

        Grid.SetColumn(panel, 1);
        body.Children.Add(panel);

        return new Border
        {
            Width = width,
            Margin = new Thickness(0d, 0d, 0d, 12d),
            Padding = new Thickness(16d, 14d, 16d, 14d),
            Background = owner.TryFindResource(AntdResourceKeys.BrushBgElevated) as Brush ?? Brushes.White,
            BorderBrush = owner.TryFindResource(AntdResourceKeys.BrushBorderSecondary) as Brush ?? Brushes.LightGray,
            BorderThickness = new Thickness(1d),
            CornerRadius = new CornerRadius(12d),
            Effect = owner.TryFindResource(AntdResourceKeys.ShadowPopup) as Effect,
            Child = body,
        };
    }
}
