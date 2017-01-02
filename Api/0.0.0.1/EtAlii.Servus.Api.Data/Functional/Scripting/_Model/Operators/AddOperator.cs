namespace EtAlii.Servus.Api.Data
{
    using EtAlii.Servus.Api.Data.Model;
    using Moppet.Lapa;
    using System.Collections.Generic;

    public class AddOperator : Operator
    {
        public AddOperator()
        {
        }

        public override string ToString()
        {
            return " += ";
        }
    }
}
