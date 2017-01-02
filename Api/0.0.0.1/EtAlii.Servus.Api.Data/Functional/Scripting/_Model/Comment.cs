namespace EtAlii.Servus.Api.Data
{
    using System;
    using System.Collections.Generic;

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
