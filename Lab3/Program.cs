using Lab3;

Token token = new Token();
Console.WriteLine("Enter message:");
token.Data = Convert.ToString(Console.ReadLine());
Console.WriteLine("Enter number of stream:");
token.Recipient = Convert.ToInt32(Console.ReadLine());
Console.WriteLine("Enter TTL:");
token.LifeTime = Convert.ToInt32(Console.ReadLine());

await TaskManager.TokenRing(token.Recipient, token);
