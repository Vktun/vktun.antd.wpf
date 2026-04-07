using System;
using System.Windows;

namespace Vktun.Antd.Wpf;

/// <summary>
/// Provides visual variant metadata for <see cref="System.Windows.Controls.ComboBox"/>.
/// </summary>
[Obsolete("Use antd:ComboBox.Variant property instead. Will be removed in v2.0.")]
public static class ComboBoxAssist
{
    /// <summary>
    /// Identifies the <see cref="Variant"/> attached property.
    /// </summary>
    public static readonly DependencyProperty VariantProperty =
        DependencyProperty.RegisterAttached(
            "Variant",
            typeof(AntdComboBoxVariant),
            typeof(ComboBoxAssist),
            new FrameworkPropertyMetadata(AntdComboBoxVariant.Outlined, FrameworkPropertyMetadataOptions.Inherits));

    /// <summary>
    /// Gets the ComboBox variant.
    /// </summary>
    /// <param name="element">The target element.</param>
    /// <returns>The configured variant.</returns>
    public static AntdComboBoxVariant GetVariant(DependencyObject element)
    {
        ArgumentNullException.ThrowIfNull(element);
        return (AntdComboBoxVariant)element.GetValue(VariantProperty);
    }

    /// <summary>
    /// Sets the ComboBox variant.
    /// </summary>
    /// <param name="element">The target element.</param>
    /// <param name="value">The variant to apply.</param>
    public static void SetVariant(DependencyObject element, AntdComboBoxVariant value)
    {
        ArgumentNullException.ThrowIfNull(element);
        element.SetValue(VariantProperty, value);
    }
}
