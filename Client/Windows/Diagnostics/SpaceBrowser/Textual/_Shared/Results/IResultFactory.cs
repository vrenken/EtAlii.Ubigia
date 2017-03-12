namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    public interface IResultFactory
    {
        Result Convert(object o, object group);
    }
}