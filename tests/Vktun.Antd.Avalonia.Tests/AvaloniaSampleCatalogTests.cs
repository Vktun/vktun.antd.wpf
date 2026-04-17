using Avalonia.Controls;
using Avalonia.VisualTree;
using FluentAssertions;
using Vktun.Antd.Avalonia.Sample.Catalog;

namespace Vktun.Antd.Avalonia.Tests;

[TestClass]
public sealed class AvaloniaSampleCatalogTests
{
    [TestMethod]
    public void Catalog_ShouldListEveryControlShownByWpfSample()
    {
        var expectedNames = new[]
        {
            "Button",
            "FloatButton",
            "TypographyTitle",
            "TypographyText",
            "TypographyParagraph",
            "Divider",
            "Row",
            "Col",
            "Space",
            "Flex",
            "Layout",
            "LayoutHeader",
            "LayoutSider",
            "LayoutContent",
            "LayoutFooter",
            "Splitter",
            "Breadcrumb",
            "BreadcrumbItem",
            "Dropdown",
            "DropdownMenu",
            "DropdownItem",
            "DropdownButton",
            "Menu",
            "MenuItem",
            "Submenu",
            "Pagination",
            "Steps",
            "Step",
            "Tabs",
            "Checkbox",
            "CheckboxGroup",
            "Input",
            "PasswordInput",
            "ComboBox",
            "Select",
            "Form",
            "FormItem",
            "Radio",
            "RadioGroup",
            "DatePicker",
            "RangePicker",
            "Switch",
            "InputNumber",
            "Slider",
            "Avatar",
            "Badge",
            "Card",
            "Collapse",
            "CollapsePanel",
            "Descriptions",
            "DescriptionItem",
            "Empty",
            "List",
            "ListItem",
            "Tooltip",
            "Popover",
            "Table",
            "Tag",
            "Timeline",
            "TimelineItem",
            "Calendar",
            "Segmented",
            "Statistic",
            "Alert",
            "Drawer",
            "Message",
            "Modal",
            "Notification",
            "Popconfirm",
            "Progress",
            "Result",
            "Skeleton",
            "Spin",
            "Watermark",
        };

        CatalogControlCatalog.Items.Select(static item => item.Name)
            .Should()
            .Contain(expectedNames);
    }

    [TestMethod]
    public void Catalog_ShouldAdaptUnsupportedWpfOnlyControlsWithNativeAvaloniaControls()
    {
        var adaptedNames = new[]
        {
            "TypographyTitle",
            "TypographyText",
            "TypographyParagraph",
            "Row",
            "Col",
            "LayoutHeader",
            "LayoutSider",
            "LayoutContent",
            "LayoutFooter",
            "BreadcrumbItem",
            "DropdownMenu",
            "DropdownItem",
            "DropdownButton",
            "MenuItem",
            "Submenu",
            "Step",
            "CheckboxGroup",
            "FormItem",
            "RadioGroup",
            "RangePicker",
            "CollapsePanel",
            "DescriptionItem",
            "ListItem",
            "Popover",
            "TimelineItem",
            "Message",
            "Modal",
            "Notification",
        };

        var adaptedItems = CatalogControlCatalog.Items
            .Where(item => adaptedNames.Contains(item.Name))
            .ToArray();

        adaptedItems
            .Should()
            .HaveCount(adaptedNames.Length)
            .And
            .OnlyContain(static item => item.Status == CatalogSupportStatus.NativeAdaptation);

        adaptedItems.Should().OnlyContain(static item => item.PreviewFactory != null);
        adaptedItems.Select(static item => item.PreviewFactory!.Invoke())
            .Should()
            .OnlyContain(static preview => preview != null);
        CatalogControlCatalog.Items.Should().NotContain(static item => item.Status == CatalogSupportStatus.Incompatible);
    }

    [TestMethod]
    public void CatalogPage_ShouldUseScrollableContentWithoutFixedPreviewHeights()
    {
        var page = CatalogControlCatalog.CreatePage("data-display");
        var wrapPanels = FindControlDescendants<WrapPanel>(page).ToArray();

        page.Should().BeOfType<ScrollViewer>();
        wrapPanels.Should().NotBeEmpty();
        wrapPanels.Should().OnlyContain(static panel => double.IsNaN(panel.ItemHeight));
    }

    private static IEnumerable<T> FindControlDescendants<T>(Control root)
        where T : Control
    {
        if (root is T matched)
        {
            yield return matched;
        }

        foreach (var child in GetControlChildren(root))
        {
            foreach (var descendant in FindControlDescendants<T>(child))
            {
                yield return descendant;
            }
        }
    }

    private static IEnumerable<Control> GetControlChildren(Control root)
    {
        if (root is Panel panel)
        {
            return panel.Children.OfType<Control>();
        }

        if (root is Border { Child: Control borderChild })
        {
            return [borderChild];
        }

        if (root is ContentControl { Content: Control contentChild })
        {
            return [contentChild];
        }

        return root.GetVisualChildren().OfType<Control>();
    }
}
