namespace MVFC.RazorRender.Tests.Partial;

public partial class CommentPartial : ComponentBase
{
    [Parameter]
    public required Comment Model { get; set; }
}