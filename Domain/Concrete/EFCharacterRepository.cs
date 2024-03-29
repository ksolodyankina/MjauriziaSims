﻿using Domain.Abstract;
using Domain.Entities;

namespace Domain.Concrete
{
    public class EFCharacterRepository : ICharacterRepository
    {
        EFDbContext context;

        public EFCharacterRepository(EFDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Character> Characters
        {
            get { return context.Characters; }
        }


        public void SaveCharacter(Character character)
        {
            if (character.CharacterId == 0)
            {
                context.Characters.Add(character);
            }
            else
            {
                Character dbEntry = context.Characters.Find(character.CharacterId);
                if (dbEntry != null)
                {
                    dbEntry.Name = character.Name;
                    dbEntry.Family = character.Family;
                    dbEntry.Generation = character.Generation;
                    dbEntry.Glasses = character.Glasses;
                    dbEntry.InFamily = character.InFamily;
                    dbEntry.Goal = character.Goal;
                    dbEntry.Age = character.Age;
                    dbEntry.Chronotype = character.Chronotype;
                    dbEntry.Career = character.Career;
                    dbEntry.Parent1 = character.Parent1;
                    dbEntry.Parent2 = character.Parent2;
                    dbEntry.Partner = character.Partner;
                }
            }
            context.SaveChanges();
        }

        public void SaveCharacterAge(Character character)
        {
            Character dbEntry = context.Characters.Find(character.CharacterId);
            if (dbEntry != null)
            {
                dbEntry.Age = character.Age;
            }
            context.SaveChanges();
        }

        public void DeleteCharacter(Character character)
        {
            var family = context.Families.First(f => f.FamilyId == character.Family);
            if (family.FamilyId == character.Family && !context.Characters
                                    .Any(c => c.Family == family.FamilyId && c.Generation == family.Generation 
                                                && c.CharacterId != character.CharacterId))
            {
                family.Generation--;
            }
            context.Characters.Remove(character);
            context.SaveChanges();
        }
    }
}
