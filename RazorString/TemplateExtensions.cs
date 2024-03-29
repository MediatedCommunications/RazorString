using RazorString.Templates;
using System.Collections.Frozen;

namespace RazorString {
    public static class  TemplateExtensions {

        /// <summary>
        /// Pass a parameter named "Context" into the <paramref name="This"/> <see cref="Template"/>./>
        /// </summary>
        /// <typeparam name="TContextParameter">The type of <paramref name="Context"/>. </typeparam>
        /// <param name="This"></param>
        /// <param name="Context"></param>
        /// <returns></returns>
        public static Template WithContext<TContextParameter>(this Template This, TContextParameter Context) { 
            return new WithContext<TContextParameter>(This, Context);
        }


        /// <summary>
        /// Pass a Name/Value collection of parameter into the <paramref name="This"/> <see cref="Template"/>./>
        /// </summary>
        /// <param name="This"></param>
        /// <param name="Parameters"></param>
        /// <returns></returns>
        public static Template WithParameters(this Template This, IDictionary<string, object?> Parameters) {
            return new WithParameters(This, Parameters);
        }

        /// Pass a Name/Value collection of parameter into the <paramref name="This"/> <see cref="Template"/>./>
        public static Template WithParameters(this Template This, IEnumerable<(string, object?)> Parameters) {
            var Params = Parameters.ToDictionary(x => x.Item1, x => x.Item2);

            return WithParameters(This, Params);
        }

    }

}
