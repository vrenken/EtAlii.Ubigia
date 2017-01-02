namespace EtAlii.Servus.Api.Data
{
    using EtAlii.Servus.Api.Data.Model;
    using Moppet.Lapa;
    using System.Collections.Generic;

    public class RemoveOperator : Operator
    {
        public RemoveOperator()
        {
        }

        public override string ToString()
        {
            return " -= ";
        }
    }
}
