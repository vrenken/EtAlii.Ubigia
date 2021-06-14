namespace EtAlii.xTechnology.Hosting.Service.Rest
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.Extensions.DependencyInjection;

    public static class MvcBuilderAddTypedControllersExtension
    {
        public static IMvcBuilder AddTypedControllers<TController>(this IMvcBuilder mvcBuilder)
            where TController : ControllerBase
        {
            return mvcBuilder
                .AddApplicationPart(typeof(TController).Assembly)
                .ConfigureApplicationPartManager(manager =>
                {
                    var originalControllerFeatureProvider = manager.FeatureProviders
                        .OfType<ControllerFeatureProvider>()
                        .SingleOrDefault(c => c is not ITypedControllerFeatureProvider);
                    if (originalControllerFeatureProvider != null)
                    {
                        manager.FeatureProviders.Remove(originalControllerFeatureProvider);
                    }
                    manager.FeatureProviders.Add(new TypedControllerFeatureProvider<TController>());
                });
        }
    }
}
