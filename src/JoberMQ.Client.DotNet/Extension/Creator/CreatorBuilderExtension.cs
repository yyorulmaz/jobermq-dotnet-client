using JoberMQ.Client.DotNet.Abs;

public static class CreatorBuilderExtension
{
    public static CreatorBuilderModel Creator(this IClient client)
        => new CreatorBuilderModel(client);
}

public class CreatorBuilderModel
{
    internal IClient client { get; set; }
    internal CreatorBuilderModel(IClient client)
        => this.client = client;
}