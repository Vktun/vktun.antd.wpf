using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Vktun.Antd.Wpf;

/// <summary>
/// Represents a single step in a step process.
/// </summary>
public class Step : Control
{
    static Step()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Step),
            new FrameworkPropertyMetadata(typeof(Step)));
    }

    /// <summary>
    /// Identifies the <see cref="Title"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register(nameof(Title), typeof(object), typeof(Step),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="Description"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty DescriptionProperty =
        DependencyProperty.Register(nameof(Description), typeof(object), typeof(Step),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="Icon"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register(nameof(Icon), typeof(object), typeof(Step),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="SubTitle"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SubTitleProperty =
        DependencyProperty.Register(nameof(SubTitle), typeof(object), typeof(Step),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="Status"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty StatusProperty =
        DependencyProperty.Register(nameof(Status), typeof(AntdStepStatus), typeof(Step),
            new PropertyMetadata(AntdStepStatus.Wait));

    /// <summary>
    /// Identifies the <see cref="Index"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IndexProperty =
        DependencyProperty.Register(nameof(Index), typeof(int), typeof(Step),
            new PropertyMetadata(0));

    /// <summary>
    /// Identifies the <see cref="IsCurrent"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsCurrentProperty =
        DependencyProperty.Register(nameof(IsCurrent), typeof(bool), typeof(Step),
            new PropertyMetadata(false));

    /// <summary>
    /// Identifies the <see cref="IsLast"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsLastProperty =
        DependencyProperty.Register(nameof(IsLast), typeof(bool), typeof(Step),
            new PropertyMetadata(false));

    /// <summary>
    /// Identifies the <see cref="Disabled"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty DisabledProperty =
        DependencyProperty.Register(nameof(Disabled), typeof(bool), typeof(Step),
            new PropertyMetadata(false));

    /// <summary>
    /// Identifies the <see cref="Clickable"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ClickableProperty =
        DependencyProperty.Register(nameof(Clickable), typeof(bool), typeof(Step),
            new PropertyMetadata(true));

    /// <summary>
    /// Gets or sets the title content.
    /// </summary>
    public object? Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    /// <summary>
    /// Gets or sets the description content.
    /// </summary>
    public object? Description
    {
        get => GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    /// <summary>
    /// Gets or sets the icon content.
    /// </summary>
    public object? Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    /// <summary>
    /// Gets or sets the subtitle content.
    /// </summary>
    public object? SubTitle
    {
        get => GetValue(SubTitleProperty);
        set => SetValue(SubTitleProperty, value);
    }

    /// <summary>
    /// Gets or sets the step status.
    /// </summary>
    public AntdStepStatus Status
    {
        get => (AntdStepStatus)GetValue(StatusProperty);
        set => SetValue(StatusProperty, value);
    }

    /// <summary>
    /// Gets or sets the step index.
    /// </summary>
    public int Index
    {
        get => (int)GetValue(IndexProperty);
        set => SetValue(IndexProperty, value);
    }

    /// <summary>
    /// Gets or sets whether this is the current step.
    /// </summary>
    public bool IsCurrent
    {
        get => (bool)GetValue(IsCurrentProperty);
        set => SetValue(IsCurrentProperty, value);
    }

    /// <summary>
    /// Gets or sets whether this is the last step.
    /// </summary>
    public bool IsLast
    {
        get => (bool)GetValue(IsLastProperty);
        set => SetValue(IsLastProperty, value);
    }

    /// <summary>
    /// Gets or sets whether this step is disabled.
    /// </summary>
    public bool Disabled
    {
        get => (bool)GetValue(DisabledProperty);
        set => SetValue(DisabledProperty, value);
    }

    /// <summary>
    /// Gets or sets whether this step is clickable.
    /// </summary>
    public bool Clickable
    {
        get => (bool)GetValue(ClickableProperty);
        set => SetValue(ClickableProperty, value);
    }
}

/// <summary>
/// Represents a step navigation bar that guides users through the steps of a task.
/// </summary>
public class Steps : ItemsControl
{
    static Steps()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Steps),
            new FrameworkPropertyMetadata(typeof(Steps)));
    }

    /// <summary>
    /// Identifies the <see cref="Current"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty CurrentProperty =
        DependencyProperty.Register(nameof(Current), typeof(int), typeof(Steps),
            new PropertyMetadata(0, OnCurrentChanged));

    /// <summary>
    /// Identifies the <see cref="Status"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty StatusProperty =
        DependencyProperty.Register(nameof(Status), typeof(AntdStepStatus), typeof(Steps),
            new PropertyMetadata(AntdStepStatus.Process));

    /// <summary>
    /// Identifies the <see cref="Size"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SizeProperty =
        DependencyProperty.Register(nameof(Size), typeof(AntdControlSize), typeof(Steps),
            new PropertyMetadata(AntdControlSize.Middle));

    /// <summary>
    /// Identifies the <see cref="Direction"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty DirectionProperty =
        DependencyProperty.Register(nameof(Direction), typeof(AntdStepsDirection), typeof(Steps),
            new PropertyMetadata(AntdStepsDirection.Horizontal));

    /// <summary>
    /// Identifies the <see cref="LabelPlacement"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty LabelPlacementProperty =
        DependencyProperty.Register(nameof(LabelPlacement), typeof(AntdStepsLabelPlacement), typeof(Steps),
            new PropertyMetadata(AntdStepsLabelPlacement.Horizontal));

    /// <summary>
    /// Identifies the <see cref="Type"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty TypeProperty =
        DependencyProperty.Register(nameof(Type), typeof(AntdStepsType), typeof(Steps),
            new PropertyMetadata(AntdStepsType.Default));

    /// <summary>
    /// Identifies the <see cref="ProgressDot"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ProgressDotProperty =
        DependencyProperty.Register(nameof(ProgressDot), typeof(bool), typeof(Steps),
            new PropertyMetadata(false));

    /// <summary>
    /// Identifies the <see cref="Initial"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty InitialProperty =
        DependencyProperty.Register(nameof(Initial), typeof(int), typeof(Steps),
            new PropertyMetadata(0));

    /// <summary>
    /// Identifies the <see cref="Icons"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IconsProperty =
        DependencyProperty.Register(nameof(Icons), typeof(StepsIcons), typeof(Steps),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="ContainerWidth"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ContainerWidthProperty =
        DependencyProperty.Register(nameof(ContainerWidth), typeof(double), typeof(Steps),
            new PropertyMetadata(1000.0));

    /// <summary>
    /// Routed event for when a step is clicked.
    /// </summary>
    public static readonly RoutedEvent StepClickEvent =
        EventManager.RegisterRoutedEvent("StepClick", RoutingStrategy.Bubble,
            typeof(StepClickRoutedEventHandler), typeof(Steps));

    /// <summary>
    /// Routed event for when the current step changes.
    /// </summary>
    public static readonly RoutedEvent CurrentChangedEvent =
        EventManager.RegisterRoutedEvent("CurrentChanged", RoutingStrategy.Bubble,
            typeof(RoutedEventHandler), typeof(Steps));

    /// <summary>
    /// Gets or sets the current step index.
    /// </summary>
    public int Current
    {
        get => (int)GetValue(CurrentProperty);
        set => SetValue(CurrentProperty, value);
    }

    /// <summary>
    /// Gets or sets the overall status of steps.
    /// </summary>
    public AntdStepStatus Status
    {
        get => (AntdStepStatus)GetValue(StatusProperty);
        set => SetValue(StatusProperty, value);
    }

    /// <summary>
    /// Gets or sets the steps size.
    /// </summary>
    public AntdControlSize Size
    {
        get => (AntdControlSize)GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    /// <summary>
    /// Gets or sets the direction of steps.
    /// </summary>
    public AntdStepsDirection Direction
    {
        get => (AntdStepsDirection)GetValue(DirectionProperty);
        set => SetValue(DirectionProperty, value);
    }

    /// <summary>
    /// Gets or sets the label placement.
    /// </summary>
    public AntdStepsLabelPlacement LabelPlacement
    {
        get => (AntdStepsLabelPlacement)GetValue(LabelPlacementProperty);
        set => SetValue(LabelPlacementProperty, value);
    }

    /// <summary>
    /// Gets or sets the steps type.
    /// </summary>
    public AntdStepsType Type
    {
        get => (AntdStepsType)GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }

    /// <summary>
    /// Gets or sets whether to use progress dots instead of icons.
    /// </summary>
    public bool ProgressDot
    {
        get => (bool)GetValue(ProgressDotProperty);
        set => SetValue(ProgressDotProperty, value);
    }

    /// <summary>
    /// Gets or sets the starting step index.
    /// </summary>
    public int Initial
    {
        get => (int)GetValue(InitialProperty);
        set => SetValue(InitialProperty, value);
    }

    /// <summary>
    /// Gets or sets custom icons for different states.
    /// </summary>
    public StepsIcons? Icons
    {
        get => (StepsIcons?)GetValue(IconsProperty);
        set => SetValue(IconsProperty, value);
    }

    /// <summary>
    /// Gets or sets the container width for percentage width steps.
    /// </summary>
    public double ContainerWidth
    {
        get => (double)GetValue(ContainerWidthProperty);
        set => SetValue(ContainerWidthProperty, value);
    }

    /// <summary>
    /// Occurs when a step is clicked.
    /// </summary>
    public event StepClickRoutedEventHandler StepClick
    {
        add => AddHandler(StepClickEvent, value);
        remove => RemoveHandler(StepClickEvent, value);
    }

    /// <summary>
    /// Occurs when the current step changes.
    /// </summary>
    public event RoutedEventHandler CurrentChanged
    {
        add => AddHandler(CurrentChangedEvent, value);
        remove => RemoveHandler(CurrentChangedEvent, value);
    }

    private static void OnCurrentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var steps = (Steps)d;
        steps.UpdateStepStatuses();
        steps.RaiseEvent(new RoutedEventArgs(CurrentChangedEvent, steps));
    }

    /// <summary>
    /// Updates the indices of all steps.
    /// </summary>
    public void UpdateStepIndices()
    {
        if (Items == null) return;

        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i] is Step step)
            {
                step.Index = Initial + i;
                step.IsLast = i == Items.Count - 1;
            }
        }
    }

    /// <summary>
    /// Updates the statuses of all steps based on current.
    /// </summary>
    public void UpdateStepStatuses()
    {
        if (Items == null) return;

        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i] is Step step)
            {
                step.IsCurrent = i == Current;
                if (step.Status == AntdStepStatus.Wait)
                {
                    step.Status = i < Current ? AntdStepStatus.Finish :
                        i == Current ? AntdStepStatus.Process :
                        AntdStepStatus.Wait;
                }
            }
        }
    }

    /// <summary>
    /// Moves to the next step.
    /// </summary>
    public void Next()
    {
        if (Current < Items.Count - 1)
        {
            Current++;
        }
    }

    /// <summary>
    /// Moves to the previous step.
    /// </summary>
    public void Previous()
    {
        if (Current > 0)
        {
            Current--;
        }
    }

    protected override void OnItemsChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        base.OnItemsChanged(e);
        UpdateStepIndices();
        UpdateStepStatuses();
    }
}

/// <summary>
/// Delegate for handling step click events.
/// </summary>
public delegate void StepClickRoutedEventHandler(object sender, StepClickRoutedEventArgs e);

/// <summary>
/// Routed event arguments for step click events.
/// </summary>
public class StepClickRoutedEventArgs : RoutedEventArgs
{
    /// <summary>
    /// Gets the clicked step.
    /// </summary>
    public Step? ClickedStep { get; }

    /// <summary>
    /// Gets the index of the clicked step.
    /// </summary>
    public int ClickedIndex { get; }

    public StepClickRoutedEventArgs(RoutedEvent routedEvent, object source, Step? step, int index)
        : base(routedEvent, source)
    {
        ClickedStep = step;
        ClickedIndex = index;
    }
}

/// <summary>
/// Represents custom icons for different step states.
/// </summary>
public class StepsIcons : DependencyObject
{
    /// <summary>
    /// Identifies the <see cref="Wait"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty WaitProperty =
        DependencyProperty.Register(nameof(Wait), typeof(object), typeof(StepsIcons),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="Process"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ProcessProperty =
        DependencyProperty.Register(nameof(Process), typeof(object), typeof(StepsIcons),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="Finish"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty FinishProperty =
        DependencyProperty.Register(nameof(Finish), typeof(object), typeof(StepsIcons),
            new PropertyMetadata(null));

    /// <summary>
    /// Identifies the <see cref="Error"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ErrorProperty =
        DependencyProperty.Register(nameof(Error), typeof(object), typeof(StepsIcons),
            new PropertyMetadata(null));

    /// <summary>
    /// Gets or sets the waiting icon.
    /// </summary>
    public object? Wait
    {
        get => GetValue(WaitProperty);
        set => SetValue(WaitProperty, value);
    }

    /// <summary>
    /// Gets or sets the processing icon.
    /// </summary>
    public object? Process
    {
        get => GetValue(ProcessProperty);
        set => SetValue(ProcessProperty, value);
    }

    /// <summary>
    /// Gets or sets the finished icon.
    /// </summary>
    public object? Finish
    {
        get => GetValue(FinishProperty);
        set => SetValue(FinishProperty, value);
    }

    /// <summary>
    /// Gets or sets the error icon.
    /// </summary>
    public object? Error
    {
        get => GetValue(ErrorProperty);
        set => SetValue(ErrorProperty, value);
    }
}

/// <summary>
/// Attached properties for Steps component.
/// </summary>
public static class StepsAssist
{
    /// <summary>
    /// Identifies the <see cref="GetLineSize"/> attached property.
    /// </summary>
    public static readonly DependencyProperty LineSizeProperty =
        DependencyProperty.RegisterAttached("LineSize", typeof(double), typeof(StepsAssist),
            new PropertyMetadata(1.0));

    /// <summary>
    /// Gets the line size.
    /// </summary>
    public static double GetLineSize(DependencyObject obj)
    {
        return (double)obj.GetValue(LineSizeProperty);
    }

    /// <summary>
    /// Sets the line size.
    /// </summary>
    public static void SetLineSize(DependencyObject obj, double value)
    {
        obj.SetValue(LineSizeProperty, value);
    }
}