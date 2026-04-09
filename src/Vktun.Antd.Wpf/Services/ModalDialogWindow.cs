using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Vktun.Antd.Wpf;

internal sealed class ModalDialogWindow : Window
{
    private readonly ModalRequest _request;

    public ModalDialogWindow(Window owner, ModalRequest request)
    {
        ArgumentNullException.ThrowIfNull(owner);
        ArgumentNullException.ThrowIfNull(request);

        _request = request;
        Owner = owner;
        Title = request.Title;
        ShowInTaskbar = false;
        ResizeMode = ResizeMode.NoResize;
        SizeToContent = SizeToContent.WidthAndHeight;
        WindowStartupLocation = WindowStartupLocation.CenterOwner;
        WindowStyle = WindowStyle.None;
        AllowsTransparency = true;
        Background = Brushes.Transparent;
        Content = BuildContent(owner, request);
        PreviewKeyDown += OnPreviewKeyDown;
    }

    private UIElement BuildContent(Window owner, ModalRequest request)
    {
        var titleBlock = new TextBlock
        {
            Text = request.Title,
            FontSize = owner.TryFindResource(AntdResourceKeys.FontSizeLarge) is double fontSize ? fontSize : 16d,
            FontWeight = FontWeights.SemiBold,
            VerticalAlignment = VerticalAlignment.Center,
        };

        var closeButton = new Button
        {
            Content = "×",
            Width = 36d,
            Height = 36d,
            HorizontalAlignment = HorizontalAlignment.Right,
            Type = AntdButtonType.Text,
        };
        closeButton.Click += (_, _) => CloseWith(null);

        var header = new Grid();
        header.ColumnDefinitions.Add(new ColumnDefinition());
        header.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
        header.Children.Add(titleBlock);
        Grid.SetColumn(closeButton, 1);
        header.Children.Add(closeButton);

        var bodyContent = request.Content as UIElement ?? new TextBlock
        {
            Text = request.Content?.ToString() ?? string.Empty,
            TextWrapping = TextWrapping.Wrap,
        };

        var footer = new Space
        {
            Orientation = Orientation.Horizontal,
            HorizontalAlignment = HorizontalAlignment.Right,
            Gap = 8d,
        };

        if (request.ShowCancel)
        {
            var cancelButton = new Button
            {
                Content = request.CancelText,
                MinWidth = 88d,
            };
            cancelButton.Click += (_, _) => CloseWith(false);
            footer.Children.Add(cancelButton);
        }

        var okButton = new Button
        {
            Content = request.OkText,
            MinWidth = 88d,
            Type = AntdButtonType.Primary,
        };
        okButton.Click += (_, _) => CloseWith(true);
        footer.Children.Add(okButton);

        var stack = new Space
        {
            Gap = 18d,
        };
        stack.Children.Add(header);
        stack.Children.Add(bodyContent);
        stack.Children.Add(footer);

        return new Border
        {
            Margin = new Thickness(24d),
            Width = request.Width,
            MaxWidth = request.MaxWidth,
            Padding = new Thickness(24d),
            Background = owner.TryFindResource(AntdResourceKeys.BrushBgElevated) as Brush ?? Brushes.White,
            BorderBrush = owner.TryFindResource(AntdResourceKeys.BrushBorderSecondary) as Brush ?? Brushes.LightGray,
            BorderThickness = new Thickness(1d),
            CornerRadius = new CornerRadius(12d),
            Effect = owner.TryFindResource(AntdResourceKeys.ShadowModal) as Effect,
            Child = stack,
        };
    }

    private void CloseWith(bool? result)
    {
        if (result.HasValue)
        {
            DialogResult = result.Value;
            return;
        }

        Close();
    }

    private void OnPreviewKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key != Key.Escape)
        {
            return;
        }

        CloseWith(_request.ShowCancel ? false : null);
        e.Handled = true;
    }
}
