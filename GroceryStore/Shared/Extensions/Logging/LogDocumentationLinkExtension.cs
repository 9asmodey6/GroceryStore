namespace GroceryStore.Shared.Extensions;

public static class LogDocumentationLinkExtension
{
    public static IApplicationBuilder LogDocumentationLink(this WebApplication app)
    {
        app.Lifetime.ApplicationStarted.Register(() =>
        {
            var addresses = app.Urls;
            var isDocker = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";

            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("🚀 GROCERY STORE API IS ONLINE");

            foreach (var url in addresses)
            {
                Console.WriteLine($"🏠 Scalar: http://localhost:5256/scalar");
            }

            if (isDocker)
            {
                Console.WriteLine("🔗 External: http://localhost:8080");
                Console.WriteLine("📚 Scalar:   http://localhost:8080/scalar/");
                Console.WriteLine("🔍 Seq UI:   http://localhost:5341 (Structured Logging)");
            }
            else
            {
                Console.WriteLine("🔍 Seq UI:   http://localhost:5341");
            }

            Console.WriteLine(new string('=', 60) + "\n");
        });

        return app;
    }
}