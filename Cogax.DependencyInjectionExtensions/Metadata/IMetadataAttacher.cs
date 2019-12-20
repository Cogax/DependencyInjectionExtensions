namespace Cogax.DependencyInjectionExtensions.Metadata
{
    public interface IMetadataAttacher
    {
        IMetadataAttacher WithMetadata(string key, object value);
    }
}