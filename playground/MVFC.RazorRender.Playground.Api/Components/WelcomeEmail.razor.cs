namespace MVFC.RazorRender.Playground.Api.Components;

public partial class WelcomeEmail : ComponentBase
{
    [Parameter]
    public required WelcomeEmailModel Model { get; set; }
}
