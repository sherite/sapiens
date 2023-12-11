namespace GenericDataLayer
{
    using Autofac;
    using GenericDataLayer.Managers;

    /// <summary>
    /// Register class
    /// </summary>
    public static class Register
    {
        /// <summary>
        /// Gets or sets the builder.
        /// </summary>
        /// <value>
        /// The builder.
        /// </value>
        public static ContainerBuilder Builder { get; set; }

        /// <summary>
        /// Registers the types.
        /// </summary>
        /// <returns></returns>
        public static ContainerBuilder RegisterTypes()
        {
            Builder = new ContainerBuilder();
            RegisterServices();
            return Builder;
        }

        /// <summary>
        /// Registers the services.
        /// </summary>
        private static void RegisterServices()
        {
            // Other
            Builder.RegisterType<UsersManager>().As<UsersManager>().InstancePerRequest();
        }
    }
}