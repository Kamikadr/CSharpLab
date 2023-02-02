using Lab3;
using System.Threading.Channels;

internal static class WorkCollector
{
    public static async Task TokenWorkInTasks(int taskIndex, Channel<Token, Token> input, Channel<Token> output)
    {
        await input.Reader.WaitToReadAsync();
        var token = await input.Reader.ReadAsync();
        token.LifeTime--;
        if (token.Recipient == taskIndex && token.LifeTime > 0)
        {
            Console.WriteLine($"I'm {taskIndex} and I've got message: {token.Data}");
        }
        else
        {
            if (token.LifeTime <= 0)
            {
                Console.WriteLine($"Time is over, time of life = {token.LifeTime}");
                return;
            }
            Console.WriteLine($"I'm {taskIndex}, not my time of life = {token.LifeTime}");
            await output.Writer.WriteAsync(token);
        }
    }
}