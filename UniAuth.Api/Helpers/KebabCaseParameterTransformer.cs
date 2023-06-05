namespace Api.Helpers
{
    public class KebabCaseParameterTransformer : IOutboundParameterTransformer
    {
        public string? TransformOutbound(object? value) =>
            StringUtils.ToKebabCase(value?.ToString());
    }
}
