namespace EtAlii.xTechnology.Hosting.Service.Rest
{
    using System.Reflection;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Controllers;

    public class TypedControllerFeatureProvider<TController> : ControllerFeatureProvider, ITypedControllerFeatureProvider 
        where TController : ControllerBase
    {
        protected override bool IsController(TypeInfo typeInfo)
        {
            return typeof(TController).GetTypeInfo().IsAssignableFrom(typeInfo) && base.IsController(typeInfo);
        }
    }
}
