using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MusicBusiness.src.Application.Dtos;
using MusicBusiness.src.Configuration.Models;
using SpotifyAPI.Web;

namespace MusicBusiness.src.Infrastructure
{
    public interface IUserAcessRepositorie
    {
        Task<PrincipalUser> GetUserById(Guid id, CancellationToken cancellationToken);
        Task PostUser(PrincipalUser principalUser, CancellationToken cancellationToken);
        Task<bool> UpdateUser(Guid id ,PrincipalUser principalUser, CancellationToken cancellationToken);
        Task<bool> DeleteUser(Guid id , CancellationToken cancellationToken);
    }
    public sealed class UserAcessRepositorie : IUserAcessRepositorie
    {
        private readonly IMongoCollection<PrincipalUser> _mongodb;
        private readonly SpotifyClient _spotify;

        public UserAcessRepositorie(IOptions<DatabaseSettings> databaseConfiguration, IOptions<SpotifySettings> spotifyConfiguration)
        {
            var client = new MongoClient(databaseConfiguration.Value.ConnectionString);
            var database = client.GetDatabase(databaseConfiguration.Value.DatabaseName);
            _mongodb = database.GetCollection<PrincipalUser>(databaseConfiguration.Value.UserCollection);

            // var spotifyAPI = SpotifyClientConfig.CreateDefault();
            // _spotify = new SpotifyClient(spotifyAPI.WithToken(spotifyConfiguration.Value.ClientID));
        }

        public async Task<bool> DeleteUser(Guid id, CancellationToken cancellationToken)
        {
            var existingDriver = await GetUserById(id, cancellationToken);

            try
            {
                await _mongodb.DeleteOneAsync(x => x.Id == id, cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<PrincipalUser> GetUserById(Guid id, CancellationToken cancellationToken)
        {
            return await _mongodb.Find(d => d.Id == id).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task PostUser(PrincipalUser principalUser, CancellationToken cancellationToken)
        {
            var guid = Guid.NewGuid();
            principalUser.Id = guid;

            var existinUsers = await GetUserById(guid, cancellationToken);
            if (existinUsers != null)
            {
                throw new Exception("User already exists!");
            }
            await _mongodb.InsertOneAsync(principalUser);
        }

        public async Task<bool> UpdateUser(Guid id, PrincipalUser principalUser, CancellationToken cancellationToken)
        {
            var existingUser = await GetUserById(principalUser.Id, cancellationToken);
            try
            {
                var updateUser = new PrincipalUser
                {
                    Id = principalUser.Id,
                    Username = principalUser.Username,
                    Password = principalUser.Password,
                    Email = principalUser.Email
                };
                var updateResult = await _mongodb.ReplaceOneAsync(x => x.Id == id, principalUser, cancellationToken: cancellationToken);
            }catch(Exception e)
            {
                throw new Exception($"Can't update the user ->{e}");
            }
            return true;
        }
    }
}