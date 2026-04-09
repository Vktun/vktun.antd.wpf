using System.Windows;
using System.Windows.Controls;

namespace Vktun.Antd.Wpf;

/// <summary>
/// Represents an Ant Design style application layout container.
/// </summary>
public class Layout : DockPanel
{
}

/// <summary>
/// Represents a layout header region.
/// </summary>
public class LayoutHeader : ContentControl
{
    static LayoutHeader()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(LayoutHeader), new FrameworkPropertyMetadata(typeof(LayoutHeader)));
    }

    public LayoutHeader()
    {
        DockPanel.SetDock(this, Dock.Top);
    }
}

/// <summary>
/// Represents a layout footer region.
/// </summary>
public class LayoutFooter : ContentControl
{
    static LayoutFooter()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(LayoutFooter), new FrameworkPropertyMetadata(typeof(LayoutFooter)));
    }

    public LayoutFooter()
    {
        DockPanel.SetDock(this, Dock.Bottom);
    }
}

/// <summary>
/// Represents a layout sider region.
/// </summary>
public class LayoutSider : ContentControl
{
    /// <summary>
    /// Identifies the <see cref="Placement"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty PlacementProperty =
        DependencyProperty.Register(nameof(Placement), typeof(Dock), typeof(LayoutSider),
            new PropertyMetadata(Dock.Left, OnPlacementChanged));

    static LayoutSider()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(LayoutSider), new FrameworkPropertyMetadata(typeof(LayoutSider)));
    }

    public LayoutSider()
    {
        DockPanel.SetDock(this, Placement);
    }

    /// <summary>
    /// Gets or sets the docking placement for the sider.
    /// </summary>
    public Dock Placement
    {
        get => (Dock)GetValue(PlacementProperty);
        set => SetValue(PlacementProperty, value);
    }

    private static void OnPlacementChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is LayoutSider sider)
        {
            DockPanel.SetDock(sider, sider.Placement);
        }
    }
}

/// <summary>
/// Represents the primary layout content region.
/// </summary>
public class LayoutContent : ContentControl
{
    static LayoutContent()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(LayoutContent), new FrameworkPropertyMetadata(typeof(LayoutContent)));
    }
}
