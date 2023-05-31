using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Api.Helpers
{
    public class NamespaceRoutingConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            // If the controller has already been configured with a route, don't override it.
            if (controller.Selectors.Any(selector => selector.AttributeRouteModel != null))
                return;

            var namespc = controller.ControllerType.Namespace;
            if (namespc is null)
                return;

            var lastNamespcPart = namespc.Split('.').Last();
            var kebabCaseNamespc = StringUtils.ToKebabCase(lastNamespcPart);

            foreach (var selector in controller.Selectors)
            {
                selector.AttributeRouteModel = new AttributeRouteModel()
                {
                    Template = $"api/{kebabCaseNamespc}"
                };
            }
        }
    }
}
