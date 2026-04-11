using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Threading;

namespace Vktun.Antd.Wpf;

/// <summary>
/// Represents a popover confirm dialog with Ant Design styling.
/// </summary>
[TemplatePart(Name = "PART_ConfirmButton", Type = typeof(Button))]
[TemplatePart(Name = "PART_CancelButton", Type = typeof(Button))]
[TemplatePart(Name = "PART_Popup", Type = typeof(Popup))]
public class Popconfirm : ContentControl
{
    static Popconfirm()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Popconfirm), new FrameworkPropertyMetadata(typeof(Popconfirm)));
    }

    private Button? _confirmButton;
    private Button? _cancelButton;
    private Popup? _popup;
    private bool _isPopupClosing;

    /// <summary>
    /// Gets or sets the title text.
    /// </summary>
    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="Title"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register(nameof(Title), typeof(string), typeof(Popconfirm),
            new PropertyMetadata(string.Empty));

    /// <summary>
    /// Gets or sets the confirm button text.
    /// </summary>
    public string OkText
    {
        get => (string)GetValue(OkTextProperty);
        set => SetValue(OkTextProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="OkText"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty OkTextProperty =
        DependencyProperty.Register(nameof(OkText), typeof(string), typeof(Popconfirm),
            new PropertyMetadata("确定"));

    /// <summary>
    /// Gets or sets the cancel button text.
    /// </summary>
    public string CancelText
    {
        get => (string)GetValue(CancelTextProperty);
        set => SetValue(CancelTextProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="CancelText"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty CancelTextProperty =
        DependencyProperty.Register(nameof(CancelText), typeof(string), typeof(Popconfirm),
            new PropertyMetadata("取消"));

    /// <summary>
    /// Gets or sets the confirm button type.
    /// </summary>
    public AntdButtonType OkType
    {
        get => (AntdButtonType)GetValue(OkTypeProperty);
        set => SetValue(OkTypeProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="OkType"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty OkTypeProperty =
        DependencyProperty.Register(nameof(OkType), typeof(AntdButtonType), typeof(Popconfirm),
            new PropertyMetadata(AntdButtonType.Primary));

    /// <summary>
    /// Gets or sets the placement of the popover.
    /// </summary>
    public PlacementMode Placement
    {
        get => (PlacementMode)GetValue(PlacementProperty);
        set => SetValue(PlacementProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="Placement"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty PlacementProperty =
        DependencyProperty.Register(nameof(Placement), typeof(PlacementMode), typeof(Popconfirm),
            new PropertyMetadata(PlacementMode.Top));

    /// <summary>
    /// Gets or sets whether the popover is visible.
    /// </summary>
    public bool IsOpen
    {
        get => (bool)GetValue(IsOpenProperty);
        set => SetValue(IsOpenProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="IsOpen"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsOpenProperty =
        DependencyProperty.Register(nameof(IsOpen), typeof(bool), typeof(Popconfirm),
            new PropertyMetadata(false));

    /// <summary>
    /// Gets or sets the icon visibility.
    /// </summary>
    public bool ShowIcon
    {
        get => (bool)GetValue(ShowIconProperty);
        set => SetValue(ShowIconProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="ShowIcon"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ShowIconProperty =
        DependencyProperty.Register(nameof(ShowIcon), typeof(bool), typeof(Popconfirm),
            new PropertyMetadata(true));

    /// <summary>
    /// Raised when the confirm button is clicked.
    /// </summary>
    public static readonly RoutedEvent ConfirmEvent =
        EventManager.RegisterRoutedEvent("Confirm", RoutingStrategy.Direct,
            typeof(RoutedEventHandler), typeof(Popconfirm));

    /// <summary>
    /// Raised when the cancel button is clicked.
    /// </summary>
    public static readonly RoutedEvent CancelEvent =
        EventManager.RegisterRoutedEvent("Cancel", RoutingStrategy.Direct,
            typeof(RoutedEventHandler), typeof(Popconfirm));

    public event RoutedEventHandler Confirm
    {
        add => AddHandler(ConfirmEvent, value);
        remove => RemoveHandler(ConfirmEvent, value);
    }

    public event RoutedEventHandler Cancel
    {
        add => AddHandler(CancelEvent, value);
        remove => RemoveHandler(CancelEvent, value);
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        if (_popup != null)
        {
            _popup.Closed -= OnPopupClosed;
        }

        if (_confirmButton != null)
        {
            _confirmButton.Click -= OnConfirmClick;
        }

        if (_cancelButton != null)
        {
            _cancelButton.Click -= OnCancelClick;
        }

        _popup = GetTemplateChild("PART_Popup") as Popup;
        _confirmButton = GetTemplateChild("PART_ConfirmButton") as Button;
        _cancelButton = GetTemplateChild("PART_CancelButton") as Button;

        if (_popup != null)
        {
            _popup.Closed += OnPopupClosed;
        }

        if (_confirmButton != null)
        {
            _confirmButton.Click += OnConfirmClick;
        }

        if (_cancelButton != null)
        {
            _cancelButton.Click += OnCancelClick;
        }
    }

    protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        base.OnPreviewMouseLeftButtonDown(e);

        if (e.Handled || _isPopupClosing)
        {
            return;
        }

        e.Handled = true;
        var targetIsOpen = !IsOpen;
        Dispatcher.BeginInvoke(() =>
        {
            IsOpen = targetIsOpen;
        }, DispatcherPriority.Input);
    }

    private void OnConfirmClick(object sender, RoutedEventArgs e)
    {
        IsOpen = false;
        RaiseEvent(new RoutedEventArgs(ConfirmEvent, this));
    }

    private void OnCancelClick(object sender, RoutedEventArgs e)
    {
        IsOpen = false;
        RaiseEvent(new RoutedEventArgs(CancelEvent, this));
    }

    private void OnPopupClosed(object? sender, EventArgs e)
    {
        SetCurrentValue(IsOpenProperty, false);
        _isPopupClosing = true;
        Dispatcher.BeginInvoke(() =>
        {
            _isPopupClosing = false;
        }, DispatcherPriority.Background);
    }

    /// <summary>
    /// Shows the popconfirm.
    /// </summary>
    public void Show()
    {
        IsOpen = true;
    }

    /// <summary>
    /// Hides the popconfirm.
    /// </summary>
    public void Hide()
    {
        IsOpen = false;
    }
}
