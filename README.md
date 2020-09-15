# Dependency Injection Unity Container MVC5

## What is the Unity Container?

The Dependency Injection Design Pattern allows us to inject the dependency objects into a class that depends on it. Unity is a dependency injection container that can be used for creating and injecting the dependency object using either constructor, method, or property injections.

## Installing Unity.Mvc5 Library from NuGet

In order to install Unity.Mvc5 Library using the NuGet, we need to go to the Package Manager Console and execute the Install-Package Unity.Mvc5 command.

## Configuring the Components with the Unity Container

    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IUnitOfWork, UnitOfWork>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
