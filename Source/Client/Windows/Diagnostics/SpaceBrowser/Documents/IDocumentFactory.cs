namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    public interface IDocumentFactory
    {
        IDocumentViewModel Create(IDocumentContext documentContext);
    }
}
