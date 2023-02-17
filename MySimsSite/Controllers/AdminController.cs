using Domain.Abstract;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.AccessControl;
using WebUI.Models;

namespace MjauriziaSims.Controllers
{
    [Authorize(Roles="Admin")]
    public class AdminController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IFamilyRepository _familyRepository;
        private readonly ICharacterRepository _characterRepository;
        private readonly IGoalRepository _goalRepository;
        private readonly IPreferenceRepository _preferenceRepository;
        private readonly ICareerRepository _careerRepository;
        private readonly IInheritanceLawRepository _inheritanceLawRepository;
        private readonly MessageManager.MessageManager _messageManager;

        public AdminController(
            IUserRepository userRepository, 
            IFamilyRepository familyRepository, 
            ICharacterRepository characterRepository, 
            IGoalRepository goalRepository, 
            IPreferenceRepository preferenceRepository,
            ICareerRepository careerRepository,
            IInheritanceLawRepository inheritanceLawRepository,
            MessageManager.MessageManager messageManager
        )
        {
            _userRepository = userRepository;
            _familyRepository = familyRepository;
            _characterRepository = characterRepository;
            _goalRepository = goalRepository;
            _preferenceRepository = preferenceRepository;
            _careerRepository = careerRepository;
            _inheritanceLawRepository = inheritanceLawRepository;
            _messageManager = messageManager;
        }

        [HttpGet]
        public ViewResult Users()
        {
            return View(_userRepository.Users);
        }
        [HttpGet]
        public ViewResult User(int id)
        {
            var user = new User();

            if (id > 0)
            {
                user = _userRepository.Users.First(f => f.UserId == id);
            }

            return View(user);
        }
        [HttpPost]
        public ActionResult User(User user)
        {
            user.Password = AccountController.EncryptPassword(user.Password, user.Email);
            _userRepository.SaveUser(user);

            return Redirect("/Admin/Users/");
        }
        [HttpPost]
        public ActionResult DeleteUser(int id)
        {
            var user = _userRepository.Users.First(f => f.UserId == id);
            _userRepository.DeleteUser(user);

            return Redirect("/Admin/Users/");
        }

        public ViewResult Families()
        {
            return View(_familyRepository.Families);
        }

        public ViewResult Family(int id)
        {
            var family = new Family();

            if (id > 0)
            {
                family = _familyRepository.Families.First(f => f.FamilyId == id);
            }

            return View(family);
        }

        [HttpPost]
        public ActionResult Family(Family family)
        {
            _familyRepository.SaveFamily(family);

            return Redirect("/Admin/Families/");
        }

        public ActionResult DeleteFamily(int id)
        {
            var family = _familyRepository.Families.First(f => f.FamilyId == id);
            _familyRepository.DeleteFamily(family);

            return Redirect("/Admin/Families/");
        }


        public ViewResult Characters()
        {
            return View(_characterRepository.Characters);
        }
        
        public ViewResult Character(int id, int familyId = 0)
        {
            var character = new Character();

            if (id > 0)
            {
                character = _characterRepository.Characters.First(c => c.CharacterId == id);
            }
            else
            {
                character.Family = familyId;
            }

            var characterVewModel = new CharacterViewModel(
                _familyRepository.Families.First(f => f.FamilyId == character.Family),
                character,
                _goalRepository.Goals,
                _preferenceRepository.Preferences,
                _careerRepository.Careers,
                _messageManager
            );

            return View(characterVewModel);
        }
        
        [HttpPost]
        public ActionResult Character(Character character)
        {
            _characterRepository.SaveCharacter(character);

            return Redirect("/Admin/Characters/");
        }

        public ActionResult DeleteCharacter(int id)
        {
            var character = _characterRepository.Characters.First(c => c.CharacterId == id);
            _characterRepository.DeleteCharacter(character);

            return Redirect("/Admin/Characters/");
        }


        public ViewResult Goals()
        {
            return View(_goalRepository.Goals);
        }

        public ViewResult Goal(int id)
        {
            var goal = new Goal();

            if (id > 0)
            {
                goal = _goalRepository.Goals.First(g => g.GoalId == id);
            }

            return View(goal);
        }


        [HttpPost]
        public ActionResult Goal(Goal goal)
        {
            _goalRepository.SaveGoal(goal);

            return Redirect("/Admin/Goals/");
        }


        public ViewResult Preferences()
        {
            return View(_preferenceRepository.Preferences);
        }

        public ViewResult Preference(int id)
        {
            var preference = new Preference();

            if (id > 0)
            {
                preference = _preferenceRepository.Preferences.First(p => p.PreferenceId == id);
            }

            return View(preference);
        }


        [HttpPost]
        public ActionResult Preference(Preference preference)
        {
            _preferenceRepository.SavePreference(preference);

            return Redirect("/Admin/Preferences/");
        }


        [HttpGet]
        public ViewResult InheritanceLaws()
        {
            return View(_inheritanceLawRepository.InheritanceLaws);
        }

        [HttpGet]
        public ViewResult InheritanceLaw(int id)
        {
            var law = new InheritanceLaw();
            if (id > 0)
            {
                law = _inheritanceLawRepository.InheritanceLaws.First(c => c.InheritanceId == id);
            }

            return View(law);
        }
        
        [HttpPost]
        public ActionResult InheritanceLaw(InheritanceLaw law)
        {
            _inheritanceLawRepository.SaveInheritanceLaw(law);

            return Redirect("/Admin/InheritanceLaws/");
        }


        [HttpGet]
        public ViewResult Careers()
        {
            return View(_careerRepository.Careers);
        }

        [HttpGet]
        public ViewResult Career(int id)
        {
            var career = new Career();

            if (id > 0)
            {
                career = _careerRepository.Careers.First(c => c.CareerId == id);
            }

            return View(career);
        }

        [HttpPost]
        public ActionResult Career(Career career)
        {
            _careerRepository.SaveCareer(career);

            return Redirect("/Admin/Careers/");
        }
    }
}