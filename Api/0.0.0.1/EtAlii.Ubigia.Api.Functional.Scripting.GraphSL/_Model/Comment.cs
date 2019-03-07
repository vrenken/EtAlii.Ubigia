﻿namespace EtAlii.Ubigia.Api.Functional
{
    public class Comment : SequencePart
    {
        private readonly string _text;

        public Comment(string text)
        {
            _text = text;
        }

        public override string ToString()
        {
            return $"--{_text}";
        }
    }
}
