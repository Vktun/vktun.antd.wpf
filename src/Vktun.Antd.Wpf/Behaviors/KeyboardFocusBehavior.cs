using System.Windows;

namespace Vktun.Antd.Wpf;

internal static class KeyboardFocusBehavior
{
    internal static readonly DependencyProperty IsFocusWithinProperty =
        DependencyProperty.RegisterAttached(
            "IsFocusWithin",
            typeof(bool),
            typeof(KeyboardFocusBehavior),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));

    internal static void SetIsFocusWithin(DependencyObject element, bool value)
    {
        element.SetValue(IsFocusWithinProperty, value);
    }
}
