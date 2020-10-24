namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    public interface IMultiResultFactory
    {
        Result[] Convert(object o, object group);
    }
}