namespace MVFC.RazorRender.Tests.Views;

public partial class BlogView : ComponentBase
{
    [Parameter]
    public required Post Model { get; set; }
}