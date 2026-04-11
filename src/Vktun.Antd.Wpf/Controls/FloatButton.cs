using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Vktun.Antd.Wpf;

/// <summary>
/// Represents an Ant Design inspired floating action button.
/// </summary>
public class FloatButton : Button
{
    private IParentSite? _originalSite;
    private Window? _overlayOwner;
    private bool _isInternalMove;

    static FloatButton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(FloatButton), new FrameworkPropertyMetadata(typeof(FloatButton)));
    }

    public FloatButton()
    {
        Loaded += OnLoaded;
        Unloaded += OnUnloaded;
    }

    /// <summary>
    /// Identifies the <see cref="Icon"/> dependency property.
    /// </summary>
    public new static readonly DependencyProperty IconProperty =
        DependencyProperty.Register(nameof(Icon), typeof(object), typeof(FloatButton), new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="Description"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty DescriptionProperty =
        DependencyProperty.Register(nameof(Description), typeof(object), typeof(FloatButton), new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="Shape"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ShapeProperty =
        DependencyProperty.Register(nameof(Shape), typeof(AntdFloatButtonShape), typeof(FloatButton), new PropertyMetadata(AntdFloatButtonShape.Circle));

    /// <summary>
    /// Identifies the <see cref="IsGlobal"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsGlobalProperty =
        DependencyProperty.Register(
            nameof(IsGlobal),
            typeof(bool),
            typeof(FloatButton),
            new PropertyMetadata(false, OnGlobalPresentationPropertyChanged));

    /// <summary>
    /// Identifies the <see cref="Placement"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty PlacementProperty =
        DependencyProperty.Register(
            nameof(Placement),
            typeof(AntdFloatButtonPlacement),
            typeof(FloatButton),
            new PropertyMetadata(AntdFloatButtonPlacement.BottomRight, OnGlobalPresentationPropertyChanged));

    /// <summary>
    /// Identifies the <see cref="GlobalMargin"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty GlobalMarginProperty =
        DependencyProperty.Register(
            nameof(GlobalMargin),
            typeof(Thickness),
            typeof(FloatButton),
            new PropertyMetadata(new Thickness(24d), OnGlobalPresentationPropertyChanged));

    /// <summary>
    /// Gets or sets the icon content.
    /// </summary>
    public new object? Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    /// <summary>
    /// Gets or sets the secondary description displayed below the main content.
    /// </summary>
    public object? Description
    {
        get => GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    /// <summary>
    /// Gets or sets the rendered button shape.
    /// </summary>
    public AntdFloatButtonShape Shape
    {
        get => (AntdFloatButtonShape)GetValue(ShapeProperty);
        set => SetValue(ShapeProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the button should be hosted in the window overlay layer.
    /// </summary>
    public bool IsGlobal
    {
        get => (bool)GetValue(IsGlobalProperty);
        set => SetValue(IsGlobalProperty, value);
    }

    /// <summary>
    /// Gets or sets the placement used when the button is hosted as a window-level floating action button.
    /// </summary>
    public AntdFloatButtonPlacement Placement
    {
        get => (AntdFloatButtonPlacement)GetValue(PlacementProperty);
        set => SetValue(PlacementProperty, value);
    }

    /// <summary>
    /// Gets or sets the margin applied around the window edge when the button is hosted globally.
    /// </summary>
    public Thickness GlobalMargin
    {
        get => (Thickness)GetValue(GlobalMarginProperty);
        set => SetValue(GlobalMarginProperty, value);
    }

    private static void OnGlobalPresentationPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var button = (FloatButton)d;

        if (button.IsGlobal)
        {
            if (!button.IsLoaded && button._overlayOwner is null)
            {
                return;
            }

            button.AttachToOverlay();
            button.UpdateOverlayPresentation();
            return;
        }

        button.RestoreToOriginalParent();
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        if (_isInternalMove || !IsGlobal)
        {
            return;
        }

        AttachToOverlay();
        UpdateOverlayPresentation();
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        if (_isInternalMove)
        {
            return;
        }

        if (!IsGlobal)
        {
            _overlayOwner = null;
        }
    }

    private void AttachToOverlay()
    {
        if (_overlayOwner is not null)
        {
            return;
        }

        var window = Window.GetWindow(this);
        if (window is null)
        {
            return;
        }

        if (_originalSite is null)
        {
            if (Parent is null && ReferenceEquals(window.Content, this))
            {
                _ = OverlayHost.Attach(window);
            }

            _originalSite = ParentSite.Create(this);
            if (_originalSite is null)
            {
                return;
            }
        }

        var overlayHost = OverlayHost.Attach(window);

        PerformInternalMove(() =>
        {
            _originalSite.Remove(this);
            overlayHost.AddFloatButton(this);
        });

        _overlayOwner = window;
    }

    private void RestoreToOriginalParent()
    {
        if (_overlayOwner is null || _originalSite is null)
        {
            return;
        }

        var overlayOwner = _overlayOwner;
        _overlayOwner = null;

        PerformInternalMove(() =>
        {
            OverlayHost.Get(overlayOwner)?.RemoveFloatButton(this);
            _originalSite.Restore(this);
        });
    }

    private void UpdateOverlayPresentation()
    {
        if (_overlayOwner is null)
        {
            return;
        }

        OverlayHost.Get(_overlayOwner)?.UpdateFloatButton(this);
    }

    private void PerformInternalMove(Action action)
    {
        _isInternalMove = true;
        try
        {
            action();
        }
        finally
        {
            _isInternalMove = false;
        }
    }

    private interface IParentSite
    {
        void Remove(FloatButton button);

        void Restore(FloatButton button);
    }

    private static class ParentSite
    {
        public static IParentSite? Create(FloatButton button)
        {
            if (TryCreate(button.Parent, button, out var site))
            {
                return site;
            }

            var visualParent = VisualTreeHelper.GetParent(button);
            if (!ReferenceEquals(visualParent, button.Parent) && TryCreate(visualParent, button, out site))
            {
                return site;
            }

            return null;
        }

        private static bool TryCreate(DependencyObject? parent, FloatButton button, out IParentSite? site)
        {
            site = parent switch
            {
                Panel panel when panel.Children.Contains(button) => new PanelSite(panel, panel.Children.IndexOf(button)),
                Decorator decorator when ReferenceEquals(decorator.Child, button) => new DecoratorSite(decorator),
                ContentControl contentControl when ReferenceEquals(contentControl.Content, button) => new ContentControlSite(contentControl),
                ContentPresenter contentPresenter when ReferenceEquals(contentPresenter.Content, button) => new ContentPresenterSite(contentPresenter),
                _ => null,
            };

            return site is not null;
        }
    }

    private sealed class PanelSite(Panel panel, int index) : IParentSite
    {
        public void Remove(FloatButton button)
        {
            panel.Children.Remove(button);
        }

        public void Restore(FloatButton button)
        {
            if (panel.Children.Contains(button))
            {
                return;
            }

            if (index >= 0 && index <= panel.Children.Count)
            {
                panel.Children.Insert(index, button);
                return;
            }

            panel.Children.Add(button);
        }
    }

    private sealed class DecoratorSite(Decorator decorator) : IParentSite
    {
        public void Remove(FloatButton button)
        {
            if (ReferenceEquals(decorator.Child, button))
            {
                decorator.Child = null;
            }
        }

        public void Restore(FloatButton button)
        {
            decorator.Child = button;
        }
    }

    private sealed class ContentControlSite(ContentControl contentControl) : IParentSite
    {
        public void Remove(FloatButton button)
        {
            if (ReferenceEquals(contentControl.Content, button))
            {
                contentControl.Content = null;
            }
        }

        public void Restore(FloatButton button)
        {
            contentControl.Content = button;
        }
    }

    private sealed class ContentPresenterSite(ContentPresenter contentPresenter) : IParentSite
    {
        public void Remove(FloatButton button)
        {
            if (ReferenceEquals(contentPresenter.Content, button))
            {
                contentPresenter.Content = null;
            }
        }

        public void Restore(FloatButton button)
        {
            contentPresenter.Content = button;
        }
    }
}
