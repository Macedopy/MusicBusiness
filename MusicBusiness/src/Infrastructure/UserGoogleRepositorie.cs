using Google.Apis.Auth;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MusicBusiness.src.Application.Dtos;
using MusicBusiness.src.Configuration.Models;
using SpotifyAPI.Web;

namespace MusicBusiness.src.Infrastructure
{
    public interface IUserGoogleRepositorie
    {
        Task<PrincipalUserGoogle> GetUserByToken(Guid id);
        Task<PrincipalUserGoogle> PostUserGoogle(PrincipalUserGoogle google);
    }
    public sealed class UserGoogleRepositorie : IUserGoogleRepositorie
    {
        private readonly IMongoCollection<PrincipalUserGoogle> _userGoogle;
        private readonly SpotifyClient _spotify;

        public UserGoogleRepositorie(IOptions<DatabaseSettings> databaseConfiguration, IOptions<SpotifySettings> spotifyConfiguration)
        {
            var client = new MongoClient(databaseConfiguration.Value.ConnectionString);
            var database = client.GetDatabase(databaseConfiguration.Value.DatabaseName);
            _userGoogle = database.GetCollection<PrincipalUserGoogle>(databaseConfiguration.Value.UserCollection);

            var spotifyAPI = SpotifyClientConfig.CreateDefault();
            _spotify = new SpotifyClient(spotifyAPI.WithToken(spotifyConfiguration.Value.ClientID));
        }

        public  Task<PrincipalUserGoogle> GetUserByToken(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<PrincipalUserGoogle> PostUserGoogle(PrincipalUserGoogle google)
        {
            GoogleJsonWebSignature.Payload payload;
            
            payload = await GoogleJsonWebSignature.ValidateAsync(google.IdToken);
            var existingUser = await _userGoogle.Find(u => u.Email == payload.Email).FirstOrDefaultAsync();
            if(existingUser == null)
            existingUser = new PrincipalUserGoogle
            {
                Email = payload.Email,
                Password = payload.Name,
            };
            await _userGoogle.InsertOneAsync(existingUser);
            return existingUser;
        }
    }
}