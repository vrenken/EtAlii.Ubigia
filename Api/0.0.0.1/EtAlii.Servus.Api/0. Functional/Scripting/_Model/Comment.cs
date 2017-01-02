namespace EtAlii.Servus.Api.Functional
{
    using System;

    public class Comment : SequencePart
    {
        public readonly string Text;

        public Comment(string text)
        {
            Text = text;
        }

        public override string ToString()
        {
            return String.Format("#{0}", Text);
        }
    }
}
