namespace RazorString {
    public abstract class Template {

        internal Template() { }

        internal virtual void FillParameters(IDictionary<string, object?> Target) {

        }

        /// <summary>
        /// Render this <see cref="Template"/> to a <see cref="string"/>
        /// </summary>
        /// <returns>The generated text.</returns>
        public async Task<string> RenderAsync() {
            using var Target = new StringWriter();
            await RenderAsync(Target);
            return Target.ToString();
        }

        /// <summary>
        /// Render this <see cref="Template"/> to a <see cref="TextWriter"/>.
        /// </summary>
        /// <param name="Target">The <see cref="TextWriter"/> that content should be output to.</param>
        /// <returns></returns>
        public Task RenderAsync(TextWriter Target) {

            var Parameters = new Dictionary<string, object?>();
            FillParameters(Parameters);

            return RenderToAsync(Target, Parameters);
        }

        internal abstract Task RenderToAsync(TextWriter Target, IDictionary<string, object?> Parameters);

    }

}
