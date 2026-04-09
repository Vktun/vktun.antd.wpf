using System.Windows;
using System.Windows.Controls;

namespace Vktun.Antd.Wpf;

/// <summary>
/// Represents an Ant Design styled password input field with prefix, suffix, and status support.
/// Note: Since PasswordBox is sealed, this control wraps a PasswordBox internally via template.
/// </summary>
public class PasswordInput : Control
{
    private PasswordBox? _passwordBox;
    private bool _isUpdatingFromControl;

    static PasswordInput()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(PasswordInput),
            new FrameworkPropertyMetadata(typeof(PasswordInput)));
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        // Unsubscribe from previous instance if any
        if (_passwordBox != null)
        {
            _passwordBox.PasswordChanged -= OnPasswordBoxPasswordChanged;
        }

        // Get the PasswordBox from template
        _passwordBox = GetTemplateChild("PART_PasswordHost") as PasswordBox;

        if (_passwordBox != null)
        {
            // Set initial password value
            _passwordBox.Password = Password;
            
            // Subscribe to password changes
            _passwordBox.PasswordChanged += OnPasswordBoxPasswordChanged;
        }
    }

    private void OnPasswordBoxPasswordChanged(object sender, RoutedEventArgs e)
    {
        if (_isUpdatingFromControl) return;

        if (_passwordBox != null)
        {
            _isUpdatingFromControl = true;
            try
            {
                Password = _passwordBox.Password;
            }
            finally
            {
                _isUpdatingFromControl = false;
            }
        }
    }

    /// <summary>
    /// Identifies the <see cref="Size"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SizeProperty =
        DependencyProperty.Register(nameof(Size), typeof(AntdControlSize), typeof(PasswordInput),
            new PropertyMetadata(AntdControlSize.Middle));

    /// <summary>
    /// Identifies the <see cref="Status"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty StatusProperty =
        DependencyProperty.Register(nameof(Status), typeof(AntdStatus), typeof(PasswordInput),
            new PropertyMetadata(AntdStatus.None));

    /// <summary>
    /// Identifies the <see cref="Variant"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty VariantProperty =
        DependencyProperty.Register(nameof(Variant), typeof(AntdInputVariant), typeof(PasswordInput),
            new PropertyMetadata(AntdInputVariant.Outlined));

    /// <summary>
    /// Identifies the <see cref="Prefix"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty PrefixProperty =
        DependencyProperty.Register(nameof(Prefix), typeof(object), typeof(PasswordInput),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="Suffix"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SuffixProperty =
        DependencyProperty.Register(nameof(Suffix), typeof(object), typeof(PasswordInput),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="Password"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty PasswordProperty =
        DependencyProperty.Register(nameof(Password), typeof(string), typeof(PasswordInput),
            new PropertyMetadata(string.Empty, OnPasswordChanged));

    private static void OnPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var control = (PasswordInput)d;
        if (control._passwordBox != null && !control._isUpdatingFromControl)
        {
            control._passwordBox.Password = (string)e.NewValue;
        }
    }

    /// <summary>
    /// Identifies the <see cref="PasswordChar"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty PasswordCharProperty =
        DependencyProperty.Register(nameof(PasswordChar), typeof(char), typeof(PasswordInput),
            new PropertyMetadata('●'));

    /// <summary>
    /// Gets or sets the password input size.
    /// </summary>
    public AntdControlSize Size
    {
        get => (AntdControlSize)GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    /// <summary>
    /// Gets or sets the password input status.
    /// </summary>
    public AntdStatus Status
    {
        get => (AntdStatus)GetValue(StatusProperty);
        set => SetValue(StatusProperty, value);
    }

    /// <summary>
    /// Gets or sets the password input variant style.
    /// </summary>
    public AntdInputVariant Variant
    {
        get => (AntdInputVariant)GetValue(VariantProperty);
        set => SetValue(VariantProperty, value);
    }

    /// <summary>
    /// Gets or sets the prefix content.
    /// </summary>
    public object? Prefix
    {
        get => GetValue(PrefixProperty);
        set => SetValue(PrefixProperty, value);
    }

    /// <summary>
    /// Gets or sets the suffix content.
    /// </summary>
    public object? Suffix
    {
        get => GetValue(SuffixProperty);
        set => SetValue(SuffixProperty, value);
    }

    /// <summary>
    /// Gets or sets the password value.
    /// </summary>
    public string Password
    {
        get => (string)GetValue(PasswordProperty);
        set => SetValue(PasswordProperty, value);
    }

    /// <summary>
    /// Gets or sets the password mask character.
    /// </summary>
    public char PasswordChar
    {
        get => (char)GetValue(PasswordCharProperty);
        set => SetValue(PasswordCharProperty, value);
    }
}