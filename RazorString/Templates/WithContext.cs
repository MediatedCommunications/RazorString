namespace RazorString.Templates {
    internal class WithContext<T> : DerivedTemplate {
        private T Context { get; }

        internal WithContext(Template BaseTemplate, T Context) : base(BaseTemplate) {
            this.Context = Context;
        }

        internal override void FillParameters(IDictionary<string, object?> Target) {
            base.FillParameters(Target);

            Target[nameof(Context)] = Context;
        }

    }

}
