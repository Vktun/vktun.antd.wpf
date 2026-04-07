using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Vktun.Antd.Wpf;

/// <summary>
/// Represents a table column definition.
/// </summary>
public class TableColumn : DependencyObject
{
    /// <summary>
    /// Gets or sets the column title.
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
        DependencyProperty.Register(nameof(Title), typeof(string), typeof(TableColumn),
            new PropertyMetadata(string.Empty));

    /// <summary>
    /// Gets or sets the data field name.
    /// </summary>
    public string DataIndex
    {
        get => (string)GetValue(DataIndexProperty);
        set => SetValue(DataIndexProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="DataIndex"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty DataIndexProperty =
        DependencyProperty.Register(nameof(DataIndex), typeof(string), typeof(TableColumn),
            new PropertyMetadata(string.Empty));

    /// <summary>
    /// Gets or sets the column width.
    /// </summary>
    public DataGridLength Width
    {
        get => (DataGridLength)GetValue(WidthProperty);
        set => SetValue(WidthProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="Width"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty WidthProperty =
        DependencyProperty.Register(nameof(Width), typeof(DataGridLength), typeof(TableColumn),
            new PropertyMetadata(DataGridLength.Auto));

    /// <summary>
    /// Gets or sets whether the column is sortable.
    /// </summary>
    public bool Sortable
    {
        get => (bool)GetValue(SortableProperty);
        set => SetValue(SortableProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="Sortable"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SortableProperty =
        DependencyProperty.Register(nameof(Sortable), typeof(bool), typeof(TableColumn),
            new PropertyMetadata(false));
}

/// <summary>
/// Represents a table component with Ant Design styling.
/// </summary>
public class Table : DataGrid
{
    static Table()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Table), new FrameworkPropertyMetadata(typeof(Table)));
    }

    public Table()
    {
        AutoGenerateColumns = false;
        IsReadOnly = true;
        HeadersVisibility = DataGridHeadersVisibility.Column;
        GridLinesVisibility = DataGridGridLinesVisibility.None;
        BorderThickness = new Thickness(0);
    }

    /// <summary>
    /// Gets or sets the table size.
    /// </summary>
    public AntdTableSize TableSize
    {
        get => (AntdTableSize)GetValue(TableSizeProperty);
        set => SetValue(TableSizeProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="TableSize"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty TableSizeProperty =
        DependencyProperty.Register(nameof(TableSize), typeof(AntdTableSize), typeof(Table),
            new PropertyMetadata(AntdTableSize.Middle, OnTableSizeChanged));

    private static void OnTableSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is Table table)
        {
            table.UpdateRowHeight();
        }
    }

    /// <summary>
    /// Gets or sets whether the table has borders.
    /// </summary>
    public bool Bordered
    {
        get => (bool)GetValue(BorderedProperty);
        set => SetValue(BorderedProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="Bordered"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty BorderedProperty =
        DependencyProperty.Register(nameof(Bordered), typeof(bool), typeof(Table),
            new PropertyMetadata(false));

    /// <summary>
    /// Gets or sets the loading state.
    /// </summary>
    public bool Loading
    {
        get => (bool)GetValue(LoadingProperty);
        set => SetValue(LoadingProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="Loading"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty LoadingProperty =
        DependencyProperty.Register(nameof(Loading), typeof(bool), typeof(Table),
            new PropertyMetadata(false));

    private void UpdateRowHeight()
    {
        switch (TableSize)
        {
            case AntdTableSize.Small:
                RowHeight = 39;
                break;
            case AntdTableSize.Middle:
                RowHeight = 55;
                break;
            case AntdTableSize.Large:
                RowHeight = 71;
                break;
        }
    }
}