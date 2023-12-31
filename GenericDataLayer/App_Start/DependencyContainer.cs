﻿using GenericDataLayer.Controllers;
using Api.Core.Controllers.Base;
using Autofac;
using Autofac.Integration.WebApi;
using System.Net;
using System.Reflection;
using System.Web.Http;

namespace Api.App_Start
{
    public class DependencyContainer
    {
        public static IContainer BuildContainer(HttpConfiguration configuration)
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(typeof(NotFoundController).GetTypeInfo().Assembly);
            builder.RegisterApiControllers(typeof(BaseController).GetTypeInfo().Assembly);
            builder.RegisterApiControllers(typeof(GroupsController).GetTypeInfo().Assembly);
            builder.RegisterApiControllers(typeof(PermisosController).GetTypeInfo().Assembly);
            builder.RegisterApiControllers(typeof(PlanosController).GetTypeInfo().Assembly);
            builder.RegisterApiControllers(typeof(RolesController).GetTypeInfo().Assembly);
            builder.RegisterApiControllers(typeof(UsersController).GetTypeInfo().Assembly);


            builder.RegisterWebApiFilterProvider(configuration);

            // Register all services.

            var container = builder.Build();
            return container;
        }
    }
}