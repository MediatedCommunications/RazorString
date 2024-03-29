namespace RazorString.Templates {
    internal abstract class DerivedTemplate : Template {
        protected Template BaseTemplate { get; }
        
        internal DerivedTemplate(Template BaseTemplate) {
            this.BaseTemplate = BaseTemplate;
        }

        internal override void FillParameters(IDictionary<string, object?> Target) {
            base.FillParameters(Target);
            BaseTemplate.FillParameters(Target);
        }

        internal override Task RenderToAsync(TextWriter Target, IDictionary<string, object?> Parameters) {
            return BaseTemplate.RenderToAsync(Target, Parameters);
        }
    }

}
