﻿namespace EtAlii.Servus.Storage
{
    public class File : Item
    {
        public byte[] Content { get; set; }

        public File(string name)
            : base(name)
        {
        }

        public override string ToString()
        {
            return Name ?? "[Empty]";
        }
    }
}
