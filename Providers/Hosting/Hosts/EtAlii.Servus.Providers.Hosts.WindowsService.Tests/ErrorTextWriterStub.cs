namespace EtAlii.Servus.Provisioning.Hosting
{
    using System.IO;

    public class ErrorTextWriterStub : TextWriter
    {
        public ErrorTextWriterStub()
            : base()
        {
        }

        public override System.Text.Encoding Encoding
        {
            get { return System.Text.Encoding.Unicode; }
        }
    }
}
