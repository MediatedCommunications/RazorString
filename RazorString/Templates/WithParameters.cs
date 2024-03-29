namespace RazorString.Templates {

    internal class WithParameters : DerivedTemplate {
        private IDictionary<string, object?> Parameters { get; }

        internal WithParameters(Template BaseTemplate, IDictionary<string, object?> Parameters) : base(BaseTemplate) {
            this.Parameters = Parameters;
        }

        internal override void FillParameters(IDictionary<string, object?> Target) {
            base.FillParameters(Target);

            foreach (var (Name, Value) in Parameters) {
                Target[Name] = Value;
            }
        }


    }

}
