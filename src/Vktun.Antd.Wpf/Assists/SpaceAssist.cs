using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Vktun.Antd.Wpf;

/// <summary>
/// Applies simple spacing to the direct children of a panel.
/// </summary>
public static class SpaceAssist
{
    /// <summary>
    /// Identifies the <see cref="Gap"/> attached property.
    /// </summary>
    public static readonly DependencyProperty GapProperty =
        DependencyProperty.RegisterAttached(
            "Gap",
            typeof(double),
            typeof(SpaceAssist),
            new PropertyMetadata(0d, OnGapChanged));

    /// <summary>
    /// Gets the configured spacing value.
    /// </summary>
    public static double GetGap(DependencyObject element)
    {
        ArgumentNullException.ThrowIfNull(element);
        return (double)element.GetValue(GapProperty);
    }

    /// <summary>
    /// Sets the configured spacing value.
    /// </summary>
    public static void SetGap(DependencyObject element, double value)
    {
        ArgumentNullException.ThrowIfNull(element);
        element.SetValue(GapProperty, value);
    }

    private static void OnGapChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
    {
        if (dependencyObject is not Panel panel)
        {
            return;
        }

        panel.Loaded -= PanelOnLoaded;
        panel.Loaded += PanelOnLoaded;
        UpdateSpacing(panel);
    }

    private static void PanelOnLoaded(object sender, RoutedEventArgs eventArgs)
    {
        if (sender is Panel panel)
        {
            UpdateSpacing(panel);
        }
    }

    private static void UpdateSpacing(Panel panel)
    {
        var gap = GetGap(panel);
        if (gap <= 0d)
        {
            return;
        }

        if (panel is StackPanel stackPanel)
        {
            for (var index = 0; index < stackPanel.Children.Count; index++)
            {
                if (stackPanel.Children[index] is not FrameworkElement child)
                {
                    continue;
                }

                child.Margin = stackPanel.Orientation == Orientation.Horizontal
                    ? new Thickness(0d, 0d, index == stackPanel.Children.Count - 1 ? 0d : gap, 0d)
                    : new Thickness(0d, 0d, 0d, index == stackPanel.Children.Count - 1 ? 0d : gap);
            }

            return;
        }

        foreach (var child in panel.Children.OfType<FrameworkElement>())
        {
            child.Margin = new Thickness(0d, 0d, gap, gap);
        }
    }
}
