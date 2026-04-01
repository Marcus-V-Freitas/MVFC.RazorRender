namespace MVFC.RazorRender.Tests.Partials;

public partial class CommentPartial : ComponentBase
{
    [Parameter]
    public required Comment Model { get; set; }
}
