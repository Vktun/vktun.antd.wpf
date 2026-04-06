using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Vktun.Antd.Wpf;

/// <summary>
/// Represents an Ant Design inspired pagination control.
/// </summary>
public class Pagination : Control
{
    private static readonly IReadOnlyList<int> DefaultPageSizeOptions = [10, 20, 50, 100];
    private readonly ObservableCollection<PaginationItemInfo> _pageItems = [];
    private TextBox? _quickJumperTextBox;

    static Pagination()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Pagination), new FrameworkPropertyMetadata(typeof(Pagination)));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Pagination"/> class.
    /// </summary>
    public Pagination()
    {
        PreviousPageCommand = new RelayCommand(_ => ChangePage(CurrentPage - 1), _ => CurrentPage > 1);
        NextPageCommand = new RelayCommand(_ => ChangePage(CurrentPage + 1), _ => CurrentPage < PageCount);
        SelectPageCommand = new RelayCommand(parameter => ChangePage(ToPage(parameter)), parameter => CanSelectPage(parameter));

        if (PageSizeOptions is null)
        {
            PageSizeOptions = DefaultPageSizeOptions;
        }

        RefreshPageItems();
    }

    /// <summary>
    /// Identifies the <see cref="Total"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty TotalProperty =
        DependencyProperty.Register(
            nameof(Total),
            typeof(int),
            typeof(Pagination),
            new FrameworkPropertyMetadata(0, OnPaginationPropertyChanged, CoerceNonNegative));

    /// <summary>
    /// Identifies the <see cref="CurrentPage"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty CurrentPageProperty =
        DependencyProperty.Register(
            nameof(CurrentPage),
            typeof(int),
            typeof(Pagination),
            new FrameworkPropertyMetadata(1, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnCurrentPageChanged, CoerceCurrentPage));

    /// <summary>
    /// Identifies the <see cref="PageSize"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty PageSizeProperty =
        DependencyProperty.Register(
            nameof(PageSize),
            typeof(int),
            typeof(Pagination),
            new FrameworkPropertyMetadata(10, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnPageSizeChanged, CoercePositive));

    /// <summary>
    /// Identifies the <see cref="ShowSizeChanger"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ShowSizeChangerProperty =
        DependencyProperty.Register(
            nameof(ShowSizeChanger),
            typeof(bool),
            typeof(Pagination),
            new FrameworkPropertyMetadata(false));

    /// <summary>
    /// Identifies the <see cref="ShowQuickJumper"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ShowQuickJumperProperty =
        DependencyProperty.Register(
            nameof(ShowQuickJumper),
            typeof(bool),
            typeof(Pagination),
            new FrameworkPropertyMetadata(false));

    /// <summary>
    /// Identifies the <see cref="PageSizeOptions"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty PageSizeOptionsProperty =
        DependencyProperty.Register(
            nameof(PageSizeOptions),
            typeof(IEnumerable<int>),
            typeof(Pagination),
            new FrameworkPropertyMetadata(DefaultPageSizeOptions, OnPaginationPropertyChanged));

    /// <summary>
    /// Identifies the <see cref="QuickJumpText"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty QuickJumpTextProperty =
        DependencyProperty.Register(
            nameof(QuickJumpText),
            typeof(string),
            typeof(Pagination),
            new FrameworkPropertyMetadata(string.Empty));

    /// <summary>
    /// Gets or sets the total item count.
    /// </summary>
    public int Total
    {
        get => (int)GetValue(TotalProperty);
        set => SetValue(TotalProperty, value);
    }

    /// <summary>
    /// Gets or sets the selected page number.
    /// </summary>
    public int CurrentPage
    {
        get => (int)GetValue(CurrentPageProperty);
        set => SetValue(CurrentPageProperty, value);
    }

    /// <summary>
    /// Gets or sets the number of items shown on each page.
    /// </summary>
    public int PageSize
    {
        get => (int)GetValue(PageSizeProperty);
        set => SetValue(PageSizeProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the page size selector is visible.
    /// </summary>
    public bool ShowSizeChanger
    {
        get => (bool)GetValue(ShowSizeChangerProperty);
        set => SetValue(ShowSizeChangerProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the quick jumper input is visible.
    /// </summary>
    public bool ShowQuickJumper
    {
        get => (bool)GetValue(ShowQuickJumperProperty);
        set => SetValue(ShowQuickJumperProperty, value);
    }

    /// <summary>
    /// Gets or sets the available page size options.
    /// </summary>
    public IEnumerable<int> PageSizeOptions
    {
        get => (IEnumerable<int>)GetValue(PageSizeOptionsProperty);
        set => SetValue(PageSizeOptionsProperty, value);
    }

    /// <summary>
    /// Gets or sets the text entered in the quick jumper.
    /// </summary>
    public string QuickJumpText
    {
        get => (string)GetValue(QuickJumpTextProperty);
        set => SetValue(QuickJumpTextProperty, value);
    }

    /// <summary>
    /// Gets the rendered pagination items.
    /// </summary>
    public IEnumerable PageItems => _pageItems;

    /// <summary>
    /// Gets the command that navigates to the previous page.
    /// </summary>
    public ICommand PreviousPageCommand { get; }

    /// <summary>
    /// Gets the command that navigates to the next page.
    /// </summary>
    public ICommand NextPageCommand { get; }

    /// <summary>
    /// Gets the command that selects a page number.
    /// </summary>
    public ICommand SelectPageCommand { get; }

    /// <inheritdoc />
    public override void OnApplyTemplate()
    {
        if (_quickJumperTextBox is not null)
        {
            _quickJumperTextBox.KeyDown -= QuickJumperTextBox_OnKeyDown;
        }

        base.OnApplyTemplate();

        _quickJumperTextBox = GetTemplateChild("PART_QuickJumperTextBox") as TextBox;
        if (_quickJumperTextBox is not null)
        {
            _quickJumperTextBox.KeyDown += QuickJumperTextBox_OnKeyDown;
        }
    }

    private int PageCount => Math.Max(1, (int)Math.Ceiling(Math.Max(0, Total) / (double)Math.Max(1, PageSize)));

    private static void OnPaginationPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
    {
        var pagination = (Pagination)dependencyObject;
        pagination.CoerceValue(CurrentPageProperty);
        pagination.RefreshPageItems();
    }

    private static void OnCurrentPageChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
    {
        var pagination = (Pagination)dependencyObject;
        pagination.RefreshPageItems();
    }

    private static void OnPageSizeChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
    {
        var pagination = (Pagination)dependencyObject;
        pagination.CoerceValue(CurrentPageProperty);
        pagination.RefreshPageItems();
    }

    private static object CoerceNonNegative(DependencyObject dependencyObject, object baseValue)
    {
        return Math.Max(0, (int)baseValue);
    }

    private static object CoercePositive(DependencyObject dependencyObject, object baseValue)
    {
        return Math.Max(1, (int)baseValue);
    }

    private static object CoerceCurrentPage(DependencyObject dependencyObject, object baseValue)
    {
        var pagination = (Pagination)dependencyObject;
        var page = Math.Max(1, (int)baseValue);
        return Math.Min(page, pagination.PageCount);
    }

    private void QuickJumperTextBox_OnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key != Key.Enter)
        {
            return;
        }

        if (!int.TryParse(QuickJumpText, out var page))
        {
            QuickJumpText = string.Empty;
            return;
        }

        ChangePage(page);
        QuickJumpText = string.Empty;
    }

    private void ChangePage(int page)
    {
        CurrentPage = Math.Max(1, Math.Min(PageCount, page));
    }

    private bool CanSelectPage(object? parameter)
    {
        var page = ToPage(parameter);
        return page > 0 && page != CurrentPage;
    }

    private static int ToPage(object? parameter)
    {
        return parameter switch
        {
            int page => page,
            string text when int.TryParse(text, out var page) => page,
            PaginationItemInfo item => item.PageNumber,
            _ => 0,
        };
    }

    private void RefreshPageItems()
    {
        _pageItems.Clear();

        foreach (var item in BuildItems())
        {
            _pageItems.Add(item);
        }

        CommandManager.InvalidateRequerySuggested();
    }

    private IEnumerable<PaginationItemInfo> BuildItems()
    {
        var pageCount = PageCount;
        if (pageCount <= 7)
        {
            return Enumerable.Range(1, pageCount).Select(CreatePageItem);
        }

        var pageNumbers = new List<int> { 1 };

        if (CurrentPage <= 4)
        {
            pageNumbers.AddRange([2, 3, 4, 5]);
            pageNumbers.Add(-1);
        }
        else if (CurrentPage >= pageCount - 3)
        {
            pageNumbers.Add(-1);
            pageNumbers.AddRange(Enumerable.Range(pageCount - 4, 4));
        }
        else
        {
            pageNumbers.Add(-1);
            pageNumbers.AddRange([CurrentPage - 1, CurrentPage, CurrentPage + 1]);
            pageNumbers.Add(-1);
        }

        pageNumbers.Add(pageCount);

        return pageNumbers
            .Select(page => page < 0 ? PaginationItemInfo.CreateEllipsis() : CreatePageItem(page));
    }

    private PaginationItemInfo CreatePageItem(int page)
    {
        return new PaginationItemInfo(page.ToString(), page, page == CurrentPage, IsEllipsis: false);
    }

    private sealed class RelayCommand(Action<object?> execute, Predicate<object?> canExecute) : ICommand
    {
        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object? parameter)
        {
            return canExecute(parameter);
        }

        public void Execute(object? parameter)
        {
            execute(parameter);
        }
    }

    private sealed record PaginationItemInfo(string Text, int PageNumber, bool IsCurrent, bool IsEllipsis)
    {
        public static PaginationItemInfo CreateEllipsis()
        {
            return new PaginationItemInfo("...", 0, false, true);
        }
    }
}

