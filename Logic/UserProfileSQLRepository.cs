using System.Collections.Generic;
using System.Linq;
using SimpleBot.Interface;
using System.Data.Entity;
using SimpleBot.Model; 

namespace SimpleBot.Logic
{
    public class UserProfileSqlRepository : IUserProfileRepository
    {
        private readonly Contexto _context;
        // Nao armazenar em memoria
        // private readonly List<Profile> _collection;
       

        public UserProfileSqlRepository(Contexto context)
        {
            _context = context;
            // Nao pode fazer isso!!!
            _collection = context.Profile.ToList();
        }
        
        public UserProfile GetProfile(string id)
        {
            
            var profile = _collection.FirstOrDefault(x => x.MessageId==id) ?? new Profile();

            return new UserProfile
            {
                Id = id,                
                Visitas = profile.Visitas
            };
            
        }

        public void SetProfile(string id, UserProfile profile)
        {
            var prof = _collection.FirstOrDefault(x => x.MessageId == id);

            if (prof == null)
            {
                prof = new Profile
                {
                    MessageId = profile.Id,
                    Visitas = profile.Visitas,
                    Nome = profile.Id
                };

                _context.Profile.Add(prof);
                _context.SaveChanges();

                return;
            }

            prof.MessageId = profile.Id;
            prof.Visitas = profile.Visitas;

            // Essa linha indica que algo esta errado na utilizacao do EF
            _context.Entry(prof).State = EntityState.Modified;
            
            _context.SaveChanges();
        }
    }
}
