public enum FusionSessionMode
{
    Host,
    Client,
    AutoHostOrClient,
    Single
}

public static class NetworkModeSelector
{
    public static FusionSessionMode Mode = FusionSessionMode.Host;
}