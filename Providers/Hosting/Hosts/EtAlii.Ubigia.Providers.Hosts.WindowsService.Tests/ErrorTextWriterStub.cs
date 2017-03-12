namespace EtAlii.Ubigia.Provisioning.Hosting
{
    using System.IO;

    public class ErrorTextWriterStub : TextWriter
    {
        public override System.Text.Encoding Encoding => System.Text.Encoding.Unicode;
    }
}
