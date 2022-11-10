using Domain.Abstract;
using System.Linq;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class FamilyController : Controller
    {
        private readonly IFamilyRepository _familyRepository;
        private readonly ICharacterRepository _characterRepository;
        private readonly IGoalRepository _goalRepository;
        private readonly IPreferenceRepository _preferenceRepository;
        private readonly ICareerRepository _careerRepository;

        public FamilyController(
            IFamilyRepository familyRepository, 
            ICharacterRepository characterRepository,
            IGoalRepository goalRepository,
            IPreferenceRepository preferenceRepository,
            ICareerRepository careerRepository)
        {
            _familyRepository = familyRepository;
            _characterRepository = characterRepository;
            _goalRepository = goalRepository;
            _preferenceRepository = preferenceRepository;
            _careerRepository = careerRepository;
        }

        public ViewResult Index()
        {
            return View(_familyRepository.Families);
        }

        public ViewResult List(int familyId = 1)
        {
            FamilyViewModel familyViewModel = new FamilyViewModel
            {
                Family = _familyRepository.Families.First(f => f.FamilyId == familyId),
                Characters = _characterRepository.Characters.Where(c => c.Family == familyId),
                Goals = _goalRepository.Goals,
                Preferences = _preferenceRepository.Preferences,
                Careers = _careerRepository.Careers
            };

            return View(familyViewModel);
        }
        public ViewResult Create()
        {
            var family = new Family();
            return View(family);
;       }

        [HttpPost]
        public ActionResult Create(Family family)
        {
            _familyRepository.SaveFamily(family);
            
            return Redirect($"/Character/Create/{family.FamilyId}");
        }
    }
}