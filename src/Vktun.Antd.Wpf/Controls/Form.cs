using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Vktun.Antd.Wpf;

/// <summary>
/// Represents a form validation rule.
/// </summary>
public abstract class FormRule : DependencyObject
{
    /// <summary>
    /// Gets or sets the validation message.
    /// </summary>
    public string Message
    {
        get => (string)GetValue(MessageProperty);
        set => SetValue(MessageProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="Message"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty MessageProperty =
        DependencyProperty.Register(nameof(Message), typeof(string), typeof(FormRule),
            new PropertyMetadata(string.Empty));

    /// <summary>
    /// Validates the value.
    /// </summary>
    public abstract bool Validate(object? value);
}

/// <summary>
/// Represents a required field validation rule.
/// </summary>
public class RequiredRule : FormRule
{
    /// <summary>
    /// Validates that the value is not null or empty.
    /// </summary>
    public override bool Validate(object? value)
    {
        if (value == null) return false;
        if (value is string str) return !string.IsNullOrWhiteSpace(str);
        if (value is ICollection col) return col.Count > 0;
        return true;
    }
}

/// <summary>
/// Represents a string length validation rule.
/// </summary>
public class LengthRule : FormRule
{
    /// <summary>
    /// Gets or sets the minimum length.
    /// </summary>
    public int Min
    {
        get => (int)GetValue(MinProperty);
        set => SetValue(MinProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="Min"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty MinProperty =
        DependencyProperty.Register(nameof(Min), typeof(int), typeof(LengthRule),
            new PropertyMetadata(0));

    /// <summary>
    /// Gets or sets the maximum length.
    /// </summary>
    public int Max
    {
        get => (int)GetValue(MaxProperty);
        set => SetValue(MaxProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="Max"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty MaxProperty =
        DependencyProperty.Register(nameof(Max), typeof(int), typeof(LengthRule),
            new PropertyMetadata(int.MaxValue));

    /// <summary>
    /// Validates the string length.
    /// </summary>
    public override bool Validate(object? value)
    {
        if (value is string str)
        {
            return str.Length >= Min && str.Length <= Max;
        }
        return true;
    }
}

/// <summary>
/// Represents a pattern validation rule.
/// </summary>
public class PatternRule : FormRule
{
    /// <summary>
    /// Gets or sets the regex pattern.
    /// </summary>
    public string Pattern
    {
        get => (string)GetValue(PatternProperty);
        set => SetValue(PatternProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="Pattern"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty PatternProperty =
        DependencyProperty.Register(nameof(Pattern), typeof(string), typeof(PatternRule),
            new PropertyMetadata(string.Empty));

    /// <summary>
    /// Validates against the pattern.
    /// </summary>
    public override bool Validate(object? value)
    {
        if (value is string str && !string.IsNullOrEmpty(Pattern))
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str, Pattern);
        }
        return true;
    }
}

/// <summary>
/// Represents a form item component with Ant Design styling.
/// </summary>
public class FormItem : ContentControl
{
    static FormItem()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(FormItem), new FrameworkPropertyMetadata(typeof(FormItem)));
    }

    /// <summary>
    /// Gets or sets the label text.
    /// </summary>
    public string Label
    {
        get => (string)GetValue(LabelProperty);
        set => SetValue(LabelProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="Label"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty LabelProperty =
        DependencyProperty.Register(nameof(Label), typeof(string), typeof(FormItem),
            new PropertyMetadata(string.Empty));

    /// <summary>
    /// Gets or sets the field name.
    /// </summary>
    public new string Name
    {
        get => (string)GetValue(NameProperty);
        set => SetValue(NameProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="Name"/> dependency property.
    /// </summary>
    public new static readonly DependencyProperty NameProperty =
        DependencyProperty.Register(nameof(Name), typeof(string), typeof(FormItem),
            new PropertyMetadata(string.Empty));

    /// <summary>
    /// Gets or sets whether the field is required.
    /// </summary>
    public bool Required
    {
        get => (bool)GetValue(RequiredProperty);
        set => SetValue(RequiredProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="Required"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty RequiredProperty =
        DependencyProperty.Register(nameof(Required), typeof(bool), typeof(FormItem),
            new PropertyMetadata(false));

    /// <summary>
    /// Gets or sets the help text.
    /// </summary>
    public string Help
    {
        get => (string)GetValue(HelpProperty);
        set => SetValue(HelpProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="Help"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty HelpProperty =
        DependencyProperty.Register(nameof(Help), typeof(string), typeof(FormItem),
            new PropertyMetadata(string.Empty));

    /// <summary>
    /// Gets or sets the validation status.
    /// </summary>
    public AntdFormValidateStatus ValidateStatus
    {
        get => (AntdFormValidateStatus)GetValue(ValidateStatusProperty);
        set => SetValue(ValidateStatusProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="ValidateStatus"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ValidateStatusProperty =
        DependencyProperty.Register(nameof(ValidateStatus), typeof(AntdFormValidateStatus), typeof(FormItem),
            new PropertyMetadata(AntdFormValidateStatus.Default));

    /// <summary>
    /// Gets or sets whether to show feedback icon.
    /// </summary>
    public bool HasFeedback
    {
        get => (bool)GetValue(HasFeedbackProperty);
        set => SetValue(HasFeedbackProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="HasFeedback"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty HasFeedbackProperty =
        DependencyProperty.Register(nameof(HasFeedback), typeof(bool), typeof(FormItem),
            new PropertyMetadata(false));

    /// <summary>
    /// Gets or sets the label column span.
    /// </summary>
    public int LabelCol
    {
        get => (int)GetValue(LabelColProperty);
        set => SetValue(LabelColProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="LabelCol"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty LabelColProperty =
        DependencyProperty.Register(nameof(LabelCol), typeof(int), typeof(FormItem),
            new PropertyMetadata(6));

    /// <summary>
    /// Gets or sets the wrapper column span.
    /// </summary>
    public int WrapperCol
    {
        get => (int)GetValue(WrapperColProperty);
        set => SetValue(WrapperColProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="WrapperCol"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty WrapperColProperty =
        DependencyProperty.Register(nameof(WrapperCol), typeof(int), typeof(FormItem),
            new PropertyMetadata(18));

    /// <summary>
    /// Gets or sets the validation rules.
    /// </summary>
    public List<FormRule> Rules
    {
        get => (List<FormRule>)GetValue(RulesProperty);
        set => SetValue(RulesProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="Rules"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty RulesProperty =
        DependencyProperty.Register(nameof(Rules), typeof(List<FormRule>), typeof(FormItem),
            new PropertyMetadata(new List<FormRule>()));

    /// <summary>
    /// Gets or sets the error message.
    /// </summary>
    public string ErrorMessage
    {
        get => (string)GetValue(ErrorMessageProperty);
        set => SetValue(ErrorMessageProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="ErrorMessage"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ErrorMessageProperty =
        DependencyProperty.Register(nameof(ErrorMessage), typeof(string), typeof(FormItem),
            new PropertyMetadata(string.Empty));

    /// <summary>
    /// Validates the form item.
    /// </summary>
    public bool Validate(object? value)
    {
        foreach (var rule in Rules)
        {
            if (!rule.Validate(value))
            {
                ValidateStatus = AntdFormValidateStatus.Error;
                ErrorMessage = rule.Message;
                return false;
            }
        }

        ValidateStatus = AntdFormValidateStatus.Success;
        ErrorMessage = string.Empty;
        return true;
    }

    /// <summary>
    /// Resets the validation status.
    /// </summary>
    public void ResetValidation()
    {
        ValidateStatus = AntdFormValidateStatus.Default;
        ErrorMessage = string.Empty;
    }
}

/// <summary>
/// Represents a form container component with Ant Design styling.
/// </summary>
public class Form : ItemsControl
{
    static Form()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Form), new FrameworkPropertyMetadata(typeof(Form)));
    }

    /// <summary>
    /// Gets or sets the form layout.
    /// </summary>
    public AntdFormLayout Layout
    {
        get => (AntdFormLayout)GetValue(LayoutProperty);
        set => SetValue(LayoutProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="Layout"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty LayoutProperty =
        DependencyProperty.Register(nameof(Layout), typeof(AntdFormLayout), typeof(Form),
            new PropertyMetadata(AntdFormLayout.Horizontal));

    /// <summary>
    /// Gets or sets the label column span.
    /// </summary>
    public int LabelCol
    {
        get => (int)GetValue(LabelColProperty);
        set => SetValue(LabelColProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="LabelCol"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty LabelColProperty =
        DependencyProperty.Register(nameof(LabelCol), typeof(int), typeof(Form),
            new PropertyMetadata(6));

    /// <summary>
    /// Gets or sets the wrapper column span.
    /// </summary>
    public int WrapperCol
    {
        get => (int)GetValue(WrapperColProperty);
        set => SetValue(WrapperColProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="WrapperCol"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty WrapperColProperty =
        DependencyProperty.Register(nameof(WrapperCol), typeof(int), typeof(Form),
            new PropertyMetadata(18));

    /// <summary>
    /// Gets or sets the label alignment.
    /// </summary>
    public HorizontalAlignment LabelAlign
    {
        get => (HorizontalAlignment)GetValue(LabelAlignProperty);
        set => SetValue(LabelAlignProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="LabelAlign"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty LabelAlignProperty =
        DependencyProperty.Register(nameof(LabelAlign), typeof(HorizontalAlignment), typeof(Form),
            new PropertyMetadata(HorizontalAlignment.Right));

    /// <summary>
    /// Gets or sets whether to validate on blur.
    /// </summary>
    public bool ValidateOnBlur
    {
        get => (bool)GetValue(ValidateOnBlurProperty);
        set => SetValue(ValidateOnBlurProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="ValidateOnBlur"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ValidateOnBlurProperty =
        DependencyProperty.Register(nameof(ValidateOnBlur), typeof(bool), typeof(Form),
            new PropertyMetadata(true));

    /// <summary>
    /// Gets or sets the form data.
    /// </summary>
    public IDictionary<string, object?> Model
    {
        get => (IDictionary<string, object?>)GetValue(ModelProperty);
        set => SetValue(ModelProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="Model"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ModelProperty =
        DependencyProperty.Register(nameof(Model), typeof(IDictionary<string, object?>), typeof(Form),
            new PropertyMetadata(null));

    /// <summary>
    /// Validates all form items.
    /// </summary>
    public bool Validate()
    {
        bool isValid = true;

        foreach (var item in Items)
        {
            if (item is FormItem formItem)
            {
                var value = GetFieldValue(formItem.Name);
                if (!formItem.Validate(value))
                {
                    isValid = false;
                }
            }
        }

        return isValid;
    }

    /// <summary>
    /// Resets all form validations.
    /// </summary>
    public void ResetValidation()
    {
        foreach (var item in Items)
        {
            if (item is FormItem formItem)
            {
                formItem.ResetValidation();
            }
        }
    }

    /// <summary>
    /// Submits the form.
    /// </summary>
    public void SubmitForm()
    {
        if (Validate())
        {
            RaiseFormSubmitEvent();
        }
    }

    private object? GetFieldValue(string fieldName)
    {
        if (Model != null && Model.TryGetValue(fieldName, out var value))
        {
            return value;
        }
        return null;
    }

    /// <summary>
    /// Raised when the form is submitted successfully.
    /// </summary>
    public static readonly RoutedEvent FormSubmitEvent =
        EventManager.RegisterRoutedEvent("FormSubmit", RoutingStrategy.Direct,
            typeof(RoutedEventHandler), typeof(Form));

    public event RoutedEventHandler FormSubmit
    {
        add => AddHandler(FormSubmitEvent, value);
        remove => RemoveHandler(FormSubmitEvent, value);
    }

    private void RaiseFormSubmitEvent()
    {
        RaiseEvent(new RoutedEventArgs(FormSubmitEvent, this));
    }
}
