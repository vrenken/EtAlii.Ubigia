namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    public interface IResultFactory
    {
        Result Convert(object o, object group);
    }
}