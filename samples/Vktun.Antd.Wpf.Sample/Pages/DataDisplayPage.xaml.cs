using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace Vktun.Antd.Wpf.Sample.Pages;

public partial class DataDisplayPage : UserControl
{
    public DataDisplayPage()
    {
        InitializeComponent();
        DataContext = this;
    }

    public ObservableCollection<ReleaseRow> Releases { get; } =
    [
        new("v1.0.0", "Released", "2026-04-09"),
        new("v0.9.0", "Beta", "2026-03-28"),
        new("v0.8.1", "Archived", "2026-03-12"),
    ];

    public ObservableCollection<string> ReportingPeriods { get; } =
    [
        "日报",
        "周报",
        "月报",
    ];

    public sealed record ReleaseRow(string Version, string Status, string Date);
}
