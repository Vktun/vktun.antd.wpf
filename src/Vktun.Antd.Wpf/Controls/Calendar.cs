using System.Windows;

namespace Vktun.Antd.Wpf;

/// <summary>
/// Represents a standalone themed calendar control.
/// </summary>
public class Calendar : System.Windows.Controls.Calendar
{
    static Calendar()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Calendar), new FrameworkPropertyMetadata(typeof(Calendar)));
    }
}
