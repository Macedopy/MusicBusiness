using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MusicBusiness.src.Application.Dtos;
using MusicBusiness.src.Configuration.Models;
using SpotifyAPI.Web;

namespace MusicBusiness.src.Infrastructure
{
    public interface IUserAcessRepositorie
    {
        Task<PrincipalUser> GetUserById(Guid id);
        Task<PrincipalUser> PostUser(PrincipalUser principalUser);
        Task<PrincipalUser> UpdateUser(Guid id ,PrincipalUser principalUser);
    }
    public sealed class UserAcessRepositorie
    {
        private readonly IMongoCollection<PrincipalUser> _mongodb;
        private readonly SpotifyClient _spotify;

        public UserAcessRepositorie(IOptions<DatabaseSettings> databaseConfiguration, IOptions<SpotifySettings> spotifyConfiguration)
        {
            var client = new MongoClient(databaseConfiguration.Value.ConnectionString);
            var database = client.GetDatabase(databaseConfiguration.Value.DatabaseName);
            _mongodb = database.GetCollection<PrincipalUser>(databaseConfiguration.Value.UserCollection);

            var spotifyAPI = SpotifyClientConfig.CreateDefault();
            _spotify = new SpotifyClient(spotifyAPI.WithToken(spotifyConfiguration.Value.ClientID));
        }

        
    }
}