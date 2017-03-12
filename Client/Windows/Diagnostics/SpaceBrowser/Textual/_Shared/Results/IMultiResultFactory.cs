namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    public interface IMultiResultFactory
    {
        Result[] Convert(object o, object group);
    }
}