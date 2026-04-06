using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Vktun.Antd.Wpf;

/// <summary>
/// Represents a drawer panel that slides in from the edge of the screen.
/// </summary>
public class Drawer : ContentControl
{
    static Drawer()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Drawer),
            new FrameworkPropertyMetadata(typeof(Drawer)));
    }

    /// <summary>
    /// Identifies the <see cref="Placement"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty PlacementProperty =
        DependencyProperty.Register(nameof(Placement), typeof(AntdDrawerPlacement), typeof(Drawer),
            new PropertyMetadata(AntdDrawerPlacement.Right, OnPlacementChanged));

    /// <summary>
    /// Identifies the <see cref="IsOpen"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsOpenProperty =
        DependencyProperty.Register(nameof(IsOpen), typeof(bool), typeof(Drawer),
            new PropertyMetadata(false, OnIsOpenChanged));

    /// <summary>
    /// Identifies the <see cref="Title"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register(nameof(Title), typeof(object), typeof(Drawer),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="Width"/> dependency property.
    /// </summary>
    public new static readonly DependencyProperty WidthProperty =
        DependencyProperty.Register(nameof(Width), typeof(double), typeof(Drawer),
            new PropertyMetadata(300.0));

    /// <summary>
    /// Identifies the <see cref="Height"/> dependency property.
    /// </summary>
    public new static readonly DependencyProperty HeightProperty =
        DependencyProperty.Register(nameof(Height), typeof(double), typeof(Drawer),
            new PropertyMetadata(300.0));

    /// <summary>
    /// Identifies the <see cref="ShowMask"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ShowMaskProperty =
        DependencyProperty.Register(nameof(ShowMask), typeof(bool), typeof(Drawer),
            new PropertyMetadata(true));

    /// <summary>
    /// Identifies the <see cref="MaskClosable"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty MaskClosableProperty =
        DependencyProperty.Register(nameof(MaskClosable), typeof(bool), typeof(Drawer),
            new PropertyMetadata(true));

    /// <summary>
    /// Identifies the <see cref="MaskColor"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty MaskColorProperty =
        DependencyProperty.Register(nameof(MaskColor), typeof(Color), typeof(Drawer),
            new PropertyMetadata(Color.FromArgb(0, 0, 0, 0)));

    /// <summary>
    /// Identifies the <see cref="MaskOpacity"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty MaskOpacityProperty =
        DependencyProperty.Register(nameof(MaskOpacity), typeof(double), typeof(Drawer),
            new PropertyMetadata(0.45));

    /// <summary>
    /// Identifies the <see cref="Closable"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ClosableProperty =
        DependencyProperty.Register(nameof(Closable), typeof(bool), typeof(Drawer),
            new PropertyMetadata(true));

    /// <summary>
    /// Identifies the <see cref="CloseIcon"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty CloseIconProperty =
        DependencyProperty.Register(nameof(CloseIcon), typeof(object), typeof(Drawer),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="Footer"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty FooterProperty =
        DependencyProperty.Register(nameof(Footer), typeof(object), typeof(Drawer),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="Extra"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ExtraProperty =
        DependencyProperty.Register(nameof(Extra), typeof(object), typeof(Drawer),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="Keyboard"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty KeyboardProperty =
        DependencyProperty.Register(nameof(Keyboard), typeof(bool), typeof(Drawer),
            new PropertyMetadata(true));

    /// <summary>
    /// Identifies the <see cref="PushDistance"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty PushDistanceProperty =
        DependencyProperty.Register(nameof(PushDistance), typeof(double), typeof(Drawer),
            new PropertyMetadata(180.0));

    /// <summary>
    /// Identifies the <see cref="Animated"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty AnimatedProperty =
        DependencyProperty.Register(nameof(Animated), typeof(bool), typeof(Drawer),
            new PropertyMetadata(true));

    /// <summary>
    /// Identifies the <see cref="AnimationDuration"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty AnimationDurationProperty =
        DependencyProperty.Register(nameof(AnimationDuration), typeof(int), typeof(Drawer),
            new PropertyMetadata(300));

    /// <summary>
    /// Identifies the <see cref="AutoSize"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty AutoSizeProperty =
        DependencyProperty.Register(nameof(AutoSize), typeof(bool), typeof(Drawer),
            new PropertyMetadata(false));

    /// <summary>
    /// Identifies the <see cref="ZIndex"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ZIndexProperty =
        DependencyProperty.Register(nameof(ZIndex), typeof(int), typeof(Drawer),
            new PropertyMetadata(1000));

    /// <summary>
    /// Routed event for when the drawer opens.
    /// </summary>
    public static readonly RoutedEvent DrawerOpenedEvent =
        EventManager.RegisterRoutedEvent("DrawerOpened", RoutingStrategy.Bubble,
            typeof(RoutedEventHandler), typeof(Drawer));

    /// <summary>
    /// Routed event for when the drawer closes.
    /// </summary>
    public static readonly RoutedEvent DrawerClosedEvent =
        EventManager.RegisterRoutedEvent("DrawerClosed", RoutingStrategy.Bubble,
            typeof(RoutedEventHandler), typeof(Drawer));

    /// <summary>
    /// Gets or sets the placement position of the drawer.
    /// </summary>
    public AntdDrawerPlacement Placement
    {
        get => (AntdDrawerPlacement)GetValue(PlacementProperty);
        set => SetValue(PlacementProperty, value);
    }

    /// <summary>
    /// Gets or sets whether the drawer is visible.
    /// </summary>
    public bool IsOpen
    {
        get => (bool)GetValue(IsOpenProperty);
        set => SetValue(IsOpenProperty, value);
    }

    /// <summary>
    /// Gets or sets the title content.
    /// </summary>
    public object? Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    /// <summary>
    /// Gets or sets the width of the drawer.
    /// </summary>
    public new double Width
    {
        get => (double)GetValue(WidthProperty);
        set => SetValue(WidthProperty, value);
    }

    /// <summary>
    /// Gets or sets the height of the drawer.
    /// </summary>
    public new double Height
    {
        get => (double)GetValue(HeightProperty);
        set => SetValue(HeightProperty, value);
    }

    /// <summary>
    /// Gets or sets whether a mask overlay is shown.
    /// </summary>
    public bool ShowMask
    {
        get => (bool)GetValue(ShowMaskProperty);
        set => SetValue(ShowMaskProperty, value);
    }

    /// <summary>
    /// Gets or sets whether clicking the mask closes the drawer.
    /// </summary>
    public bool MaskClosable
    {
        get => (bool)GetValue(MaskClosableProperty);
        set => SetValue(MaskClosableProperty, value);
    }

    /// <summary>
    /// Gets or sets the mask overlay color.
    /// </summary>
    public Color MaskColor
    {
        get => (Color)GetValue(MaskColorProperty);
        set => SetValue(MaskColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the mask opacity.
    /// </summary>
    public double MaskOpacity
    {
        get => (double)GetValue(MaskOpacityProperty);
        set => SetValue(MaskOpacityProperty, value);
    }

    /// <summary>
    /// Gets or sets whether the close button is shown.
    /// </summary>
    public bool Closable
    {
        get => (bool)GetValue(ClosableProperty);
        set => SetValue(ClosableProperty, value);
    }

    /// <summary>
    /// Gets or sets the close icon content.
    /// </summary>
    public object? CloseIcon
    {
        get => GetValue(CloseIconProperty);
        set => SetValue(CloseIconProperty, value);
    }

    /// <summary>
    /// Gets or sets the footer content.
    /// </summary>
    public object? Footer
    {
        get => GetValue(FooterProperty);
        set => SetValue(FooterProperty, value);
    }

    /// <summary>
    /// Gets or sets extra content in the header.
    /// </summary>
    public object? Extra
    {
        get => GetValue(ExtraProperty);
        set => SetValue(ExtraProperty, value);
    }

    /// <summary>
    /// Gets or sets whether keyboard close (ESC) is enabled.
    /// </summary>
    public bool Keyboard
    {
        get => (bool)GetValue(KeyboardProperty);
        set => SetValue(KeyboardProperty, value);
    }

    /// <summary>
    /// Gets or sets the push distance when drawer opens.
    /// </summary>
    public double PushDistance
    {
        get => (double)GetValue(PushDistanceProperty);
        set => SetValue(PushDistanceProperty, value);
    }

    /// <summary>
    /// Gets or sets whether animation is enabled.
    /// </summary>
    public bool Animated
    {
        get => (bool)GetValue(AnimatedProperty);
        set => SetValue(AnimatedProperty, value);
    }

    /// <summary>
    /// Gets or sets the animation duration in milliseconds.
    /// </summary>
    public int AnimationDuration
    {
        get => (int)GetValue(AnimationDurationProperty);
        set => SetValue(AnimationDurationProperty, value);
    }

    /// <summary>
    /// Gets or sets whether the drawer auto-sizes to content.
    /// </summary>
    public bool AutoSize
    {
        get => (bool)GetValue(AutoSizeProperty);
        set => SetValue(AutoSizeProperty, value);
    }

    /// <summary>
    /// Gets or sets the z-index of the drawer.
    /// </summary>
    public int ZIndex
    {
        get => (int)GetValue(ZIndexProperty);
        set => SetValue(ZIndexProperty, value);
    }

    /// <summary>
    /// Occurs when the drawer opens.
    /// </summary>
    public event RoutedEventHandler DrawerOpened
    {
        add => AddHandler(DrawerOpenedEvent, value);
        remove => RemoveHandler(DrawerOpenedEvent, value);
    }

    /// <summary>
    /// Occurs when the drawer closes.
    /// </summary>
    public event RoutedEventHandler DrawerClosed
    {
        add => AddHandler(DrawerClosedEvent, value);
        remove => RemoveHandler(DrawerClosedEvent, value);
    }

    private static void OnPlacementChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var drawer = (Drawer)d;
        drawer.UpdatePlacement();
    }

    private static void OnIsOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var drawer = (Drawer)d;
        if ((bool)e.NewValue)
        {
            drawer.RaiseEvent(new RoutedEventArgs(DrawerOpenedEvent, drawer));
        }
        else
        {
            drawer.RaiseEvent(new RoutedEventArgs(DrawerClosedEvent, drawer));
        }
    }

    /// <summary>
    /// Updates the drawer layout based on placement.
    /// </summary>
    private void UpdatePlacement()
    {
        // The placement is handled in the style template
    }

    /// <summary>
    /// Closes the drawer.
    /// </summary>
    public void Close()
    {
        IsOpen = false;
    }

    /// <summary>
    /// Opens the drawer.
    /// </summary>
    public void Open()
    {
        IsOpen = true;
    }
}

/// <summary>
/// Attached properties for Drawer component.
/// </summary>
public static class DrawerAssist
{
    /// <summary>
    /// Identifies the <see cref="GetOwner"/> attached property.
    /// </summary>
    public static readonly DependencyProperty OwnerProperty =
        DependencyProperty.RegisterAttached("Owner", typeof(FrameworkElement), typeof(DrawerAssist),
            new PropertyMetadata(null));

    /// <summary>
    /// Gets the owner element.
    /// </summary>
    public static FrameworkElement? GetOwner(DependencyObject obj)
    {
        return (FrameworkElement?)obj.GetValue(OwnerProperty);
    }

    /// <summary>
    /// Sets the owner element.
    /// </summary>
    public static void SetOwner(DependencyObject obj, FrameworkElement? value)
    {
        obj.SetValue(OwnerProperty, value);
    }
}