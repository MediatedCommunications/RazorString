using Example.Templates;
using RazorString;

namespace Example {
    internal class Program {
        static async Task Main(string[] args) {

            var Template1 = TemplateFactory.Default.FromComponent<HelloWorld>();
            
            
            var Template2 = TemplateFactory.Default.FromComponent<Hello>()
                .WithParameters([("Name", "John Smith")])
                ;

            var Template3 = TemplateFactory.Default.FromComponent<Message>()
                .WithContext(new MessageContext() {
                    Name = "John Smith",
                    Date_Of_Birth = new DateTime(1901, 01, 01),
                });

            var Template4 = TemplateFactory.Default.FromComponent<Composite>()
                .WithContext(new CompositeContext() {
                    Name = "John Smith",
                    Date_Of_Birth = new DateTime(1901, 01, 01),
                })
                .WithParameters([
                    ("Breakfast", "Eggs"),
                    ("Lunch", "Sandwich"),
                ])
                .WithParameters([
                    ("Dinner", "Pizza"),
                ])
                ;



            var Html1 = await Template1.RenderAsync();
            var Html2 = await Template2.RenderAsync();
            var Html3 = await Template3.RenderAsync();
            var Html4 = await Template4.RenderAsync();


            Console.WriteLine(Html1);
            Console.WriteLine(Html2);
            Console.WriteLine(Html3);
            Console.WriteLine(Html4);


        }
    }
}
