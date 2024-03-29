using Microsoft.AspNetCore.Components;

namespace RazorString.Templates {
    internal class WithType<T> : Template where T : IComponent {

        private TemplateFactory Factory { get; }
        internal WithType(TemplateFactory Factory) : base() { 
            this.Factory = Factory;
        }

        internal override async Task RenderToAsync(TextWriter Target, IDictionary<string, object?> Parameters) {
            await Factory.Renderer.Dispatcher.InvokeAsync(async () => {

                var parameters = ParameterView.FromDictionary(Parameters);
                var output = await Factory.Renderer.RenderComponentAsync<T>(parameters);

                output.WriteHtmlTo(Target);
            });
        }

    }

}
