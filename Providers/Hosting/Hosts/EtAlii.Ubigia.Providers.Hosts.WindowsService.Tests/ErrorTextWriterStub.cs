namespace EtAlii.Ubigia.Provisioning.Hosting
{
    using System.IO;

    public class ErrorTextWriterStub : TextWriter
    {
        public ErrorTextWriterStub()
            : base()
        {
        }

        public override System.Text.Encoding Encoding => System.Text.Encoding.Unicode;
    }
}
