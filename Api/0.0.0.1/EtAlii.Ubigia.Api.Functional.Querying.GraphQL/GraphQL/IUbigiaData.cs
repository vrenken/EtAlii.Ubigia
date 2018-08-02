namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUbigiaData
    {
        IEnumerable<StarWarsCharacter> GetFriends(StarWarsCharacter character);
        Task<Human> GetHumanByIdAsync(string id);
        Task<Droid> GetDroidByIdAsync(string id);
        Human AddHuman(Human human);
    }
}