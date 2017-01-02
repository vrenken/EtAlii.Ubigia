namespace EtAlii.Servus.Api.Data
{
    using EtAlii.Servus.Api.Data.Model;
    using Moppet.Lapa;
    using System.Collections.Generic;

    public class AssignOperator : Operator
    {
        public AssignOperator()
        {
        }

        public override string ToString()
        {
            return " <= ";
        }
    }
}
