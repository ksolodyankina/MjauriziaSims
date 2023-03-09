using Domain.Abstract;
using Domain.Entities;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;
using Microsoft.AspNetCore.Authorization;

namespace MjauriziaSims.Controllers
{
    [Authorize]
    public class CharacterController : Controller
    {
        private readonly IFamilyRepository _familyRepository;
        private readonly ICharacterRepository _characterRepository;
        private readonly IGoalRepository _goalRepository;
        private readonly IPreferenceRepository _preferenceRepository;
        private readonly ICareerRepository _careerRepository;
        private readonly MessageManager.MessageManager _messageManager;

        private enum CreationType
        {
            FirstCharacter,
            NewCharacter,
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
            MessageManager.MessageManager messageManager)
        {
            _familyRepository = familyRepository;
            _characterRepository = characterRepository;
            _goalRepository = goalRepository;
            _preferenceRepository = preferenceRepository;
            _careerRepository = careerRepository;
            _messageManager = messageManager;
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
                }
                else if (character.Age == Ages.Young)
                {
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
        
        public ViewResult Create(int type, int familyId = 0, int partnerId = 0)
        {
            var family = _familyRepository.Families.FirstOrDefault(f => f.FamilyId == familyId);
            if (partnerId != 0)
            {
                var partner = _characterRepository.Characters.FirstOrDefault(c => c.CharacterId == partnerId);
                family = _familyRepository.Families.FirstOrDefault(f => f.FamilyId == partner.Family);
            }
            var canEdit = User.FindFirst("UserId").Value == family.UserId.ToString();

            if (canEdit)
            {
                var character = new Character()
                {
                    Family = family.FamilyId,
                    Partner = partnerId
                };

                var characterVewModel = new CharacterViewModel()
                {
                    Family = family,
                    Character = character,
                    Goals = _goalRepository.Goals.ToList(),
                    Preferences = _preferenceRepository.Preferences.ToList(),
                    Careers = _careerRepository.Careers.ToList(),
                    MsgManager = _messageManager,
                    Characters = _characterRepository.Characters
                        .Where(c => c.Family == family.FamilyId && c.InFamily 
                                && c.Age >= Ages.Young && c.CharacterId != character.CharacterId)
                        .ToList()
                };

                return View(characterVewModel);
            }
            else
            {
                throw new Exception("Access Denied");
            }
        }

        [HttpPost]
        public ActionResult Create(Character character)
        {
            var family = _familyRepository.Families.FirstOrDefault(f => f.FamilyId == character.Family);

            if (character.Parent1 == 0 && character.Parent2 != 0)
            {
                character.Parent1 = character.Parent2;
                character.Parent2 = 0;
            }
            if (character.Partner != 0)
            {
                var partner = _characterRepository.Characters.FirstOrDefault(c => c.CharacterId == character.Partner);
                if (partner != null)
                {
                    character.Generation = partner.Generation;
                }
            }
            else if (character.Parent1 != 0)
            {
                var parent = _characterRepository.Characters.FirstOrDefault(c => c.CharacterId == character.Parent1);
                if (parent != null)
                {
                    character.Generation = parent.Generation + 1;
                }
                if (character.Parent2 != 0)
                {
                    var parent2 =
                        _characterRepository.Characters.FirstOrDefault(c => c.CharacterId == character.Parent2);
                    if (parent.Generation > parent2.Generation)
                    {
                        character.Generation = parent2.Generation + 1;
                    }
                }
            }
            _characterRepository.SaveCharacter(character);

            if (character.Partner != 0)
            {
                var characterId = _characterRepository.Characters.First(c => c.Partner == character.Partner).CharacterId;
                var partner = _characterRepository.Characters.FirstOrDefault(c => c.CharacterId == character.Partner);
                if (partner != null)
                {
                    partner.Partner = characterId;
                    _characterRepository.SaveCharacter(partner);
                }
            }

            if (family.Generation < character.Generation)
            {
                family.Generation++;
                _familyRepository.SaveFamily(family);
            }
            return Redirect($"/Family/{family.FamilyId}");
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            var character = _characterRepository.Characters.First(c => c.CharacterId == id);

            var family = _familyRepository.Families.First(f => f.FamilyId == character.Family);
            var canEdit = User.FindFirst("UserId").Value == family.UserId.ToString();

            if (canEdit)
            {
                var characterVewModel = new CharacterViewModel() 
                {
                    Family = _familyRepository.Families.First(f => f.FamilyId == character.Family),
                    Character = character,
                    Goals = _goalRepository.Goals.ToList(),
                    Preferences = _preferenceRepository.Preferences.ToList(),
                    Careers = _careerRepository.Careers.ToList(),
                    MsgManager = _messageManager,
                    Characters = _characterRepository.Characters
                        .Where(c => c.Family == family.FamilyId && c.InFamily 
                                && c.Age >= Ages.Young && c.CharacterId != character.CharacterId)
                        .ToList()
                };

                return View(characterVewModel);
            }
            else
            {
                throw new Exception("Access Denied");
            }
        }
        [HttpPost]
        public ActionResult Edit(Character character)
        {
            _characterRepository.SaveCharacter(character);

            return Redirect($"/Family/{character.Family}");
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

                var parent1 = _characterRepository.Characters.FirstOrDefault(c => c.CharacterId == character.Parent1);
                if (parent1 != null)
                {
                    parent1HasGlasses = parent1.Glasses;
                }

                var parent2 = _characterRepository.Characters.FirstOrDefault(c => c.CharacterId == character.Parent2);
                if (parent2 != null)
                {
                    parent2HasGlasses = parent2.Glasses;
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
            var chronotype = random.Next(1, 2);
            if (chronotype == 1)
            {
                return Chronotypes.MorningPerson;
            }
            else
            {
                return Chronotypes.NightPerson;
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
    }
}