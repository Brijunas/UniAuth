namespace Api.Helpers
{
    public class KebabCaseParameterTransformer : IOutboundParameterTransformer
    {
        public string? TransformOutbound(object? value)
        {
            var stringValue = value?.ToString();
            return StringUtils.ToKebabCase(stringValue);
        }
    }
}
