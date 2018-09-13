namespace SimpleBot.Interfaces.Repository
{
	/// <summary>
	/// Classe: userProfile
	/// </summary>
	/// Autor: Gerado automaticamento pelo sistema ClassGenerator por Wagner Sereia dos Santos
	/// Revisao da classe: 1
	public interface IUserProfileRepository
	{
        UserProfile GetProfile(string id);
        void SetProfile(string id, ref UserProfile profile);
        void update(UserProfile profile);
        void insert(UserProfile profile);
        void RemoveUserProfile(UserProfile profile);
        void Dispose();
    }
}
