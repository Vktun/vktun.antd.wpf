namespace Vktun.Antd.Wpf.Sample.Snippets;

public static class GeneralSnippets
{
    public static string Button => """
<antd:Space Orientation="Horizontal" Gap="12">
    <antd:Button Content="Default" />
    <antd:Button Type="Primary" Content="Primary" />
    <antd:Button Type="Dashed" Content="Dashed" />
    <antd:Button Type="Text" Content="Text" />
</antd:Space>
""";

    public static string FloatButton => """
<antd:FloatButton Icon="+"
                  Content="反馈"
                  Description="全局入口"
                  IsGlobal="True"
                  Placement="BottomRight" />
""";

    public static string Typography => """
<StackPanel>
    <antd:TypographyTitle Level="2" Text="目录页标题" />
    <antd:TypographyText Text="映射到 Ant Design Typography 系列。" />
    <antd:TypographyParagraph
        Text="正文支持强调、复制、截断等语义，用来承载组件说明。"
        Strong="True" />
</StackPanel>
""";
}
