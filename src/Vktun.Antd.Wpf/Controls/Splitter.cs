using System.Windows;
using System.Windows.Controls;

namespace Vktun.Antd.Wpf;

/// <summary>
/// Represents a themed grid splitter.
/// </summary>
public class Splitter : GridSplitter
{
    static Splitter()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Splitter), new FrameworkPropertyMetadata(typeof(Splitter)));
    }
}
