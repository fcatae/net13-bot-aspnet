using SimpleBot.Logic;


namespace SimpleBot.Repository.Interfaces
{
    interface IUserProfileRepository
    {
        UserProfile GetProfile(string id);

        long SetProfile(UserProfile profile);
    }
}
