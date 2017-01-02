namespace EtAlii.Servus.Api.Data
{
    using EtAlii.Servus.Api.Scripting;

    public class __CommandData
    {
        public __CommandData(string statement, __NamedParameter[] namedParameters)
        {
            Statement = statement;
            NamedParameters = namedParameters;
        }

        public string Statement { get; private set; }
        public __NamedParameter[] NamedParameters { get; private set; }

        public Script CreateScript(IDataConnection connection)
        {
            // TODO: PGJ
            //var query = connection.CreateScript(Statement);

            //foreach (var parameter in NamedParameters)
            //    query.SetParameter(parameter.Name, parameter.Value);

            return null;// query;
        }
    }
}