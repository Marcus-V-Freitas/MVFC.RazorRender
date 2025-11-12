namespace MVFC.RazorRender.Tests.Dtos;

public sealed record CommentParameterDto(Post Model, string CacheKey) : IRazorCacheParameter;