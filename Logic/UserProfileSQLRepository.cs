using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleBot.Interface;
using System.Data.Entity;
using SimpleBot.Model;

namespace SimpleBot.Logic
{
    public class UserProfileSQLRepository : IUserProfileRepository
    {
        private Contexto context;
        private List<Profile> _collection;
       

        public UserProfileSQLRepository(Contexto context)
        {
            this.context = context;
            this._collection = context.Profile.ToList();
        }
        
        public UserProfile GetProfile(string id)
        {
            
            var profile = _collection.Where(x=>x.MessageId==id).FirstOrDefault();

            if (profile == null) profile = new Profile();

            return new UserProfile
            {
                Id = id,                
                Visitas = profile.Visitas
            };
            
        }

        public void SetProfile(string id, UserProfile profile)
        {
            var prof = _collection.Where(x => x.MessageId == id).FirstOrDefault();

            if (prof == null)
            {
                prof = new Profile();

                prof.MessageId = profile.Id;
                prof.Visitas = profile.Visitas;
                prof.Nome = profile.Id;

                context.Profile.Add(prof);
                context.SaveChanges();

                return;
            }

            prof.MessageId = profile.Id;
            prof.Visitas = profile.Visitas;

            context.Entry(prof).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}