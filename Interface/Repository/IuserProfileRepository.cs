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
		
	// Por que parametro ref?
        void SetProfile(string id, ref UserProfile profile);
		
	// Cuidado com a nomenclatura: letra minuscula?
        void update(UserProfile profile);
        void insert(UserProfile profile);
        void RemoveUserProfile(UserProfile profile);
        
	// Evitar Dispose: seria possível refatorar o código e evitar?
	void Dispose();
    }
}
