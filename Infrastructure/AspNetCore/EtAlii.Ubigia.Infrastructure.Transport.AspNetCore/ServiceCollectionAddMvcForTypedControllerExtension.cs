﻿namespace EtAlii.Ubigia.Infrastructure.Transport.AspNetCore
{
    using System;
    using System.Linq;
    using EtAlii.xTechnology.Hosting.AspNetCore;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionAddMvcForTypedControllerExtension
    {
        public static IMvcBuilder AddMvcForTypedController<TController>(this IServiceCollection services)
            where TController : ControllerBase
        {
            return AddMvcForTypedController<TController>(services, options => { });
        }

        public static IMvcBuilder AddMvcForTypedController<TController>(this IServiceCollection services, Action<MvcOptions> configureMvcOptions)
            where TController : ControllerBase
        {
            return services
                .AddMvc(configureMvcOptions)
                .AddApplicationPart(typeof(TController).Assembly)
                .ConfigureApplicationPartManager(manager =>
                {
                    manager.FeatureProviders.Remove(manager.FeatureProviders.OfType<ControllerFeatureProvider>().Single());
                    manager.FeatureProviders.Add(new TypedControllerFeatureProvider<TController>());
                });

        }

    }
}
