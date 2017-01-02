namespace EtAlii.Servus.Api.Data
{
    using System;

    public class IdentifierOutputHandler : ActionHandlerBase<IdentifierOutput>
    {
        private readonly PathHelper _pathHelper;

        public IdentifierOutputHandler(PathHelper pathHelper)
        {
            _pathHelper = pathHelper;
        }

        public override void Handle(IdentifierOutput action, ScriptScope scope, IDataConnection connection)
        {
            throw new NotImplementedException();
            //var node = _pathHelper.Get(action.Path, scope, connection);
            //if (node != null)
            //{
            //    scope.Output(node);
            //}
        }
    }
}
