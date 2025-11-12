namespace MVFC.RazorRender.Tests.Models;

public sealed record Post(
    int Id,
    string Title,
    string Content,
    IReadOnlyList<Comment> Comments);