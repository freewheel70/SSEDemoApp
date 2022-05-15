using RandomDataGenerator.FieldOptions;
using RandomDataGenerator.Randomizers;

namespace SSEDemoApp.Controllers
{
    public static class SSEHttpContextExtensions
    {
        private static readonly IRandomizerString _randomizerFirstName = RandomizerFactory.GetRandomizer(new FieldOptionsCity());
        public static async Task SSEInitAsync(this HttpContext ctx)
        {
            ctx.Response.Headers.Add("Cache-Control", "no-cache");
            ctx.Response.Headers.Add("Content-Type", "text/event-stream");
            await ctx.Response.Body.FlushAsync();
        }

        public static async Task SSESendDataAsync(this HttpContext ctx)
        {
            
            await ctx.Response.WriteAsync("event: data\n");
            await ctx.Response.WriteAsync("data: " + _randomizerFirstName.Generate() + "\n");

            await ctx.Response.WriteAsync("\n");
            await ctx.Response.Body.FlushAsync();
            Thread.Sleep(new Random().Next(500, 2000));
        }


        public static async Task SSESendCloseEventAsync(this HttpContext ctx)
        {

            await ctx.Response.WriteAsync("event: close\n");
            await ctx.Response.WriteAsync("data: bye\n");

            await ctx.Response.WriteAsync("\n");
            await ctx.Response.Body.FlushAsync();
        }
    }
}
