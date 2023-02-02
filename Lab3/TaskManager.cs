using System.Threading.Channels;

namespace Lab3
{
    internal class TaskManager
    {
        public static async Task TokenRing(int tasksCount, Token token)
        {
            var tasks = new List<Task>();
            var tokenChannels = new List<Channel<Token>>()
                ;
            tokenChannels.Add(Channel.CreateBounded<Token>(new BoundedChannelOptions(1)));
            await tokenChannels[0].Writer.WriteAsync(token);
            try
            {
                for (var taskIndex = 1; taskIndex < tasksCount; taskIndex++)
                {
                    tokenChannels.Add(Channel.CreateBounded<Token>(new BoundedChannelOptions(1)));
                    tasks.Add(WorkCollector.TokenWorkInTasks(taskIndex, tokenChannels[taskIndex - 1], tokenChannels[taskIndex]));
                }

                await Task.WhenAll(tasks);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}
