using RandomDataGenerator.FieldOptions;
using RandomDataGenerator.Randomizers;

namespace SSEDemoApp.Controllers
{
    public static class SSEHttpContextExtensions
    {
        private static readonly IRandomizerString _cityNameRandomizer = RandomizerFactory.GetRandomizer(new FieldOptionsCity());
        private static readonly IRandomizerGuid _idRandomizer = RandomizerFactory.GetRandomizer(new FieldOptionsGuid());
        public static async Task SSEInitAsync(this HttpContext ctx)
        {
            ctx.Response.Headers.Add("Content-Type", "text/event-stream");
            await ctx.Response.Body.FlushAsync();
        }

        public static async Task SSESendDataAsync(this HttpContext ctx)
        {
            await ctx.Response.WriteAsync("id: " + _idRandomizer.GenerateAsString() + "\n");
            await ctx.Response.WriteAsync("event: city-notification\n");
            await ctx.Response.WriteAsync("retry: 3\n");
            await ctx.Response.WriteAsync("data: " + _cityNameRandomizer.Generate() + "\n\n");

            await ctx.Response.Body.FlushAsync();

            Thread.Sleep(new Random().Next(300, 1000));
        }


        public static async Task SSESendCloseEventAsync(this HttpContext ctx)
        {
            await ctx.Response.WriteAsync("event: close\n");
            await ctx.Response.WriteAsync("retry: 3\n");
            await ctx.Response.WriteAsync("data: bye\n");

            await ctx.Response.WriteAsync("\n");
            await ctx.Response.Body.FlushAsync();
        }
    }
}
