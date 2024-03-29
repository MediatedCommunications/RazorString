using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Immutable;
using RazorString.Templates;

namespace RazorString {


    public class TemplateFactory {

        internal HtmlRenderer Renderer { get; }

        internal TemplateFactory(HtmlRenderer Renderer) {
            this.Renderer = Renderer;
        }

        /// <summary>
        /// Create a <see cref="Template"/> from <typeparamref name="TComponent"/>.
        /// </summary>
        /// <typeparam name="TComponent">The type of a Razor component.</typeparam>
        /// <returns></returns>
        public Template FromComponent<TComponent>() where TComponent : IComponent {
            return new WithType<TComponent>(this);
        }

        /// <summary>
        /// A default <see cref="TemplateFactory"/> created by <see cref="Create()"/>.
        /// </summary>
        public static TemplateFactory Default { get; }

        /// <summary>
        /// Create a default <see cref="TemplateFactory" /> using a basic <see cref=" IServiceProvider"/>.
        /// </summary>
        /// <returns></returns>
        public static TemplateFactory Create() {
            var services = new ServiceCollection();
            services.AddLogging();
            var serviceProvider = services.BuildServiceProvider();

            return Create(serviceProvider);
        }

        /// <summary>
        /// Create a default <see cref="TemplateFactory" /> using the <paramref name="ServiceProvider"/> <see cref=" IServiceProvider"/>.
        /// </summary>
        /// <param name="ServiceProvider"></param>
        /// <returns></returns>
        public static TemplateFactory Create(IServiceProvider ServiceProvider) {
            var loggerFactory = ServiceProvider.GetRequiredService<ILoggerFactory>();

            var Renderer = new HtmlRenderer(ServiceProvider, loggerFactory);

            var ret = new TemplateFactory(Renderer);

            return ret;
        }

        static TemplateFactory() {
            Default = Create();
        }

    }

}
