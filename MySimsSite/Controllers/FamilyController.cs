using Domain.Abstract;
using System.Linq;
using System.Runtime.CompilerServices;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MjauriziaSims.Controllers
{
    public class FamilyController : Controller
    {
        private readonly IFamilyRepository _familyRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICharacterRepository _characterRepository;
        private readonly IGoalRepository _goalRepository;
        private readonly IPreferenceRepository _preferenceRepository;
        private readonly ICareerRepository _careerRepository;
        private readonly IInheritanceLawRepository _inheritanceRepository;
        private readonly MessageManager.MessageManager _msgManager;

        public FamilyController(
            IFamilyRepository familyRepository, 
            IUserRepository userRepository, 
            ICharacterRepository characterRepository,
            IGoalRepository goalRepository,
            IPreferenceRepository preferenceRepository,
            ICareerRepository careerRepository,
            IInheritanceLawRepository inheritanceRepository,
            MessageManager.MessageManager msgManager)
        {
            _familyRepository = familyRepository;
            _userRepository = userRepository;
            _characterRepository = characterRepository;
            _goalRepository = goalRepository;
            _preferenceRepository = preferenceRepository;
            _careerRepository = careerRepository;
            _inheritanceRepository = inheritanceRepository;
            _msgManager = msgManager;
        }

        [HttpGet]
        public ViewResult Index(int user = 0)
        {
            var familiesWithUsersQuery = _familyRepository.Families
                .Join(_userRepository.Users, 
                    f => f.UserId, 
                    u => u.UserId,
                (f, u) => new FamiliesWithUser(){ Family = f, User = u.Login });
            if (user > 0)
            {
                familiesWithUsersQuery = familiesWithUsersQuery.Where(f => f.Family.UserId == user);
            }
            var familiesWithUsers = familiesWithUsersQuery.ToList();

            var editRules = new Dictionary<int, bool>() { };
            foreach (var family in familiesWithUsers)
            {
                if (User.Identity.IsAuthenticated)
                {
                    var userId = User.FindFirst("UserId").Value;
                    editRules[family.Family.FamilyId] = family.Family.UserId == Int32.Parse(userId);
                }
                else
                {
                    editRules[family.Family.FamilyId] = false;
                }
            }

            var familiesViewModel = new FamiliesViewModel
            {
                FamiliesWithUsers = familiesWithUsers,
                InheritanceLaws = _inheritanceRepository.InheritanceLaws.ToList(),
                EditRules = editRules,
                MsgManager = _msgManager
            };
            return View(familiesViewModel);
        }

        public ViewResult List(int familyId)
        {
            var familyWithUser = _familyRepository.Families.Join(_userRepository.Users,
                f => f.UserId,
                u => u.UserId,
                (f, u) => new FamiliesWithUser() { Family = f, User = u.Login }).
                FirstOrDefault(f => f.Family.FamilyId == familyId);

            var canEdit = false;
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirst("UserId").Value;
                canEdit = familyWithUser.Family.UserId == Int32.Parse(userId);
            }

            var familyViewModel = new FamilyViewModel
            {
                Family = familyWithUser,
                Characters = _characterRepository.Characters.Where(c => c.Family == familyId),
                Goals = _goalRepository.Goals,
                Preferences = _preferenceRepository.Preferences,
                Careers = _careerRepository.Careers,
                InheritanceLaws = _inheritanceRepository.InheritanceLaws,
                CanEdit = canEdit,
                MsgManager = _msgManager
            };

            return View(familyViewModel);
        }

        [Authorize]
        public ViewResult Create()
        {
            var userId = User.FindFirst("UserId").Value;
            var familyCreationModel = new FamilyCreationModel
            {
                Family = new Family() {UserId = Int32.Parse(userId)},
                InheritanceLaws = _inheritanceRepository.InheritanceLaws,
                MsgManager = _msgManager
            };
            return View(familyCreationModel);
;       }

        [HttpPost]
        [Authorize]
        public ActionResult Create(Family family)
        {
            _familyRepository.SaveFamily(family);
            
            return Redirect($"/Character/Create/{family.FamilyId}");
        }
    }
}