namespace EtAlii.Servus.Infrastructure.Transport
{
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Transport;

    internal partial class SystemAuthenticationDataClient : IAuthenticationDataClient
    {
        public async Task<Space> GetSpace(ISpaceConnection connection)
        {
            if (connection.Space != null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.SpaceAlreadyOpen);
            }

            var space = await GetSpace(connection.Account, connection.Configuration.Space);
            if (space == null)
            {
                throw new UnauthorizedInfrastructureOperationException(InvalidInfrastructureOperation.UnableToConnectToSpace);
            }

            return space;
        }

        private async Task<Space> GetSpace(Account currentAccount, string spaceName)
        {
            var spaces = _infrastructure.Spaces.GetAll(currentAccount.Id);
            return await Task.FromResult(spaces.FirstOrDefault(s => s.Name == spaceName));
        }
    }
}
