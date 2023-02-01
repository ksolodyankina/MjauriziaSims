using Domain.Abstract;
using Domain.Entities;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace MjauriziaSims.Controllers
{
    public class CharacterController : Controller
    {
        private readonly IFamilyRepository _familyRepository;
        private readonly ICharacterRepository _characterRepository;
        private readonly IGoalRepository _goalRepository;
        private readonly IPreferenceRepository _preferenceRepository;
        private readonly ICareerRepository _careerRepository;
        private readonly IInheritanceLawRepository _lawRepository;

        private enum CreationType
        {
            NewHeir,
            GiveBirth,
            GetMarried
        }
        private enum PreferenceCategory
        {
            Color,
            Music,
            Hobby,
            Decor,
            Clothes
        }

        public CharacterController(
            IFamilyRepository familyRepository, 
            ICharacterRepository characterRepository, 
            IGoalRepository goalRepository, 
            IPreferenceRepository preferenceRepository,
            ICareerRepository careerRepository,
            IInheritanceLawRepository lawRepository)
        {
            _familyRepository = familyRepository;
            _characterRepository = characterRepository;
            _goalRepository = goalRepository;
            _preferenceRepository = preferenceRepository;
            _careerRepository = careerRepository;
            _lawRepository = lawRepository;
        }

        public ActionResult MakeOlder(int id = 1)
        {
            var character = _characterRepository.Characters.First(c => c.CharacterId == id);
            if (character.Age <= Ages.Old)
            {
                character.Age++;
                if (character.Age == Ages.Toddler || character.Age == Ages.Adult || character.Age == Ages.Old)
                {
                    character.Glasses = RandomizeGlasses(character);
                }
                else if (character.Age == Ages.Child)
                {
                    character.Goal = RandomizeGoal(character);
                    character.Color = RandomizePreferences(PreferenceCategory.Color);
                    character.Chronotype = RandomizeChronotype();
                }
                else if (character.Age == Ages.Teen)
                {
                    character.Goal = RandomizeGoal(character);
                    character.Music = RandomizePreferences(PreferenceCategory.Music);
                    character.Hobby = RandomizePreferences(PreferenceCategory.Hobby);
                    character.Clothes = RandomizePreferences(PreferenceCategory.Clothes);
                    character.Sexuality = RandomizeSexuality();
                }
                else if (character.Age == Ages.Young)
                {
                    var nextHeir = GetNextHeir(character.Family);
                    if (nextHeir != null && nextHeir.CharacterId == character.CharacterId)
                    {
                        SetNewHeir(character);
                    }
                    character.Decor = RandomizePreferences(PreferenceCategory.Decor);
                    character.Career = RandomizeCareer(character);
                }
            }
            _characterRepository.SaveCharacter(character);
            return Redirect($"/Family/{character.Family}");
        }
        
        public ActionResult KickOut(int id = 1)
        {
            var character = _characterRepository.Characters.First(c => c.CharacterId == id);
            character.InFamily = false; 
            _characterRepository.SaveCharacter(character);
            return Redirect($"/Family/{character.Family}");
        }
        
        public ActionResult MakeHeir(int id = 1)
        {
            var character = _characterRepository.Characters.First(c => c.CharacterId == id);
            if (GetNextHeir(character.Family) == null)
            {
                SetNewHeir(character);
            }
            return Redirect($"/Family/{character.Family}");
        }

        public ViewResult Create(int familyId, int type)
        {
            var character = new Character()
            {
                Family = familyId
            };

            switch (type)
            {
                case (int)CreationType.NewHeir:
                    character.Generation = 1;
                    character.IsHeir = true;
                    break;
                case (int)CreationType.GiveBirth:
                    character.Generation = _characterRepository.Characters
                                    .OrderBy(c => -c.CharacterId)
                                    .First(c => c.Family == familyId && c.InLow)
                                    .Generation + 1;
                    break;
                case (int)CreationType.GetMarried:
                    character.Generation = _familyRepository.Families.First(f => f.FamilyId == familyId).Generation;
                    character.InLow = true;
                    break;
            }

            var characterVewModel = new CharacterViewModel(
                _familyRepository.Families.First(f => f.FamilyId == familyId),
                character,
                _goalRepository.Goals,
                _preferenceRepository.Preferences,
                _careerRepository.Careers
            );

            return View(characterVewModel);
        }

        [HttpPost]
        public ActionResult Create(Character character)
        {
            _characterRepository.SaveCharacter(character);
            var family = _familyRepository.Families.First(c => c.FamilyId == character.Family);
            if (family.Generation < character.Generation)
            {
                family.Generation++;
                _familyRepository.SaveFamily(family);
            }
            else if (character.InLow)
            {
                var partner = _characterRepository.Characters
                    .First(c => c.Family == character.Family && c.Generation == character.Generation && c.IsHeir);
            }
            return Redirect($"/Family/{family.FamilyId}");
        }

        public Character GetNextHeir(int familyId)
        {
            return null;
        }


        private bool RandomizeGlasses(Character character)
        {
            if (character.Glasses)
            {
                return true;
            }
            else
            {
                var parent1HasGlasses = true;
                var parent2HasGlasses = false;

                if (!character.InLow)
                {
                    var parent1 = _characterRepository.Characters
                        .Where(c => c.Generation == character.Generation - 1);
                    if (parent1.Any())
                    {
                        parent1HasGlasses = parent1.First().Glasses;
                    }

                    var parent2 = _characterRepository.Characters
                        .Where(c => c.Generation == character.Generation - 1 && c.InLow == true);
                    if (parent2.Any())
                    {
                        parent2HasGlasses = parent2.First().Glasses;
                    }
                }

                var random = new Random();
                return random.Next(1, 10) < 1 + (parent1HasGlasses ? 4 : 0) + (parent2HasGlasses ? 4 : 0);
            }
        }


        private int RandomizeGoal(Character character)
        {
            var random = new Random();
            var goals = _goalRepository.Goals.Where(g => g.IsChild == (character.Age == Ages.Child));
            var goalNumber = random.Next(1, goals.Count());
            var goalId = -1;
            var i = 1;
            foreach (var goal in goals)
            {
                if (i == goalNumber)
                {
                    goalId = goal.GoalId;
                    break;
                }
                i++;
            }
            return goalId;
        }

        private int RandomizePreferences(PreferenceCategory category)
        {
            var random = new Random();
            var preferences = _preferenceRepository.Preferences.Where(p => p.Category == (Preference.Categories)category);
            var preferenceNumber = random.Next(1, preferences.Count());
            var preferenceId = -1;
            var i = 1;
            foreach (var preference in preferences)
            {
                if (i == preferenceNumber)
                {
                    preferenceId = preference.PreferenceId;
                    break;
                }
                i++;
            }
            return preferenceId;
        }

        private Chronotypes RandomizeChronotype()
        {
            var random = new Random();
            var chronotype = random.Next(1, 100);
            if (chronotype == 1)
            {
                return Chronotypes.Hiperproductive;
            }
            else if (chronotype <= 50)
            {
                return Chronotypes.Morning_Person;
            }
            else if (chronotype < 100)
            {
                return Chronotypes.Nigth_Person;
            }
            else
            {
                return Chronotypes.Sleepyhead;
            }
        }

        private Sexualities RandomizeSexuality()
        {
            var random = new Random();
            var sexuality = random.Next(1, 5);
            if (sexuality == 1)
            {
                return Sexualities.Heterosexual;
            }
            else if (sexuality < 5)
            {
                return Sexualities.Bisexual;
            }
            else
            {
                return Sexualities.Homosexual;
            }
        }

        private int RandomizeCareer(Character character)
        {
            var result = -1;
            var family = _familyRepository.Families.First(f => f.FamilyId == character.Family);
            {
                var random = new Random();
                var careers = _careerRepository.Careers;
                var careerNumber = random.Next(1, careers.Count());
                var i = 1;
                foreach (var career in careers)
                {
                    if (i == careerNumber)
                    {
                        result = career.CareerId;
                        break;
                    }
                    i++;
                }
            }
            return result;
        }

        public void SetNewHeir(Character character)
        {
            var lastHeir = _characterRepository.Characters
                .First(c => c.Family == character.Family && c.IsHeir && c.Generation < character.Generation);
            lastHeir.IsHeir = false;
            _characterRepository.SaveCharacter(lastHeir);
            character.IsHeir = true;
            _characterRepository.SaveCharacter(lastHeir);
        }
    }
}