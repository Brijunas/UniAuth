using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Api.Helpers
{
    public class NamespaceRoutingConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            // If the controller has already been configured with a route, don't override it.
            if (controller.Selectors.Any(selector => selector.AttributeRouteModel is not null))
                return;

            // If the controller is not in a namespace, don't override it.
            var namespc = controller.ControllerType.Namespace;
            if (namespc is null)
                return;

            // Get the last part of the namespace.
            var lastNamespcPart = namespc.Split('.').Last();

            // Convert PascalCase to kebab-case.
            var kebabCaseNamespc = StringUtils.ToKebabCase(lastNamespcPart);

            // Add the route.
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
