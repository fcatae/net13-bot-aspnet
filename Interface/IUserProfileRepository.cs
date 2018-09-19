using SimpleBot.Logic;

namespace SimpleBot.Interface
{
    internal interface IUserProfileRepository
    {
        UserProfile GetProfile(string id);

        void SetProfile(string id, UserProfile profile);
    }
}
