using Domain.Abstract;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using MjauriziaSims.Models;
using Microsoft.AspNetCore.Authorization;

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
        private readonly MessageManager.MessageManager _msgManager;

        public FamilyController(
            IFamilyRepository familyRepository, 
            IUserRepository userRepository, 
            ICharacterRepository characterRepository,
            IGoalRepository goalRepository,
            IPreferenceRepository preferenceRepository,
            ICareerRepository careerRepository,
            MessageManager.MessageManager msgManager)
        {
            _familyRepository = familyRepository;
            _userRepository = userRepository;
            _characterRepository = characterRepository;
            _goalRepository = goalRepository;
            _preferenceRepository = preferenceRepository;
            _careerRepository = careerRepository;
            _msgManager = msgManager;
        }

        [HttpGet]
        public ViewResult Index(int user = 0)
        {
            var familiesWithUsersQuery = _familyRepository.Families
                .Join(_userRepository.Users.ToList(), 
                    f => f.UserId, 
                    u => u.UserId,
                (f, u) => new FamiliesWithUser(){ Family = f, User = u.Username });

            var myFamilies = false; 
            if (user > 0)
            {
                familiesWithUsersQuery = familiesWithUsersQuery.Where(f => f.Family.UserId == user);
                if (user.ToString() == User.FindFirst("UserId").Value)
                {
                    myFamilies = true;
                }
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
                EditRules = editRules,
                MsgManager = _msgManager,
                MyFamilies = myFamilies
            };
            return View(familiesViewModel);
        }

        public ViewResult List(int familyId)
        {
            var familyWithUser = _familyRepository.Families.Join(_userRepository.Users.ToList(),
                f => f.UserId,
                u => u.UserId,
                (f, u) => new FamiliesWithUser() { Family = f, User = u.Username }).
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
                Characters = _characterRepository.Characters.Where(c => c.Family == familyId).ToList(),
                Goals = _goalRepository.Goals,
                Preferences = _preferenceRepository.Preferences,
                Careers = _careerRepository.Careers,
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
        
        [Authorize]
        public ViewResult Edit(int id)
        {;
            var family = _familyRepository.Families.First(f => f.FamilyId == id);

            var userId = User.FindFirst("UserId").Value;
            var canEdit = family.UserId.ToString() == userId;
            if (canEdit)
            {
                var familyCreationModel = new FamilyCreationModel
                {
                    Family = family,
                    MsgManager = _msgManager
                };
                return View(familyCreationModel);
            }
            else
            {
                throw new Exception("Access Denied");
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(Family family)
        {
            _familyRepository.SaveFamily(family);

            return Redirect($"/Family/{family.FamilyId}");
        }
    }
}