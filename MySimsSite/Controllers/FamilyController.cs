using Domain.Abstract;
using System.Linq;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace MjauriziaSims.Controllers
{
    public class FamilyController : Controller
    {
        private readonly IFamilyRepository _familyRepository;
        private readonly ICharacterRepository _characterRepository;
        private readonly IGoalRepository _goalRepository;
        private readonly IPreferenceRepository _preferenceRepository;
        private readonly ICareerRepository _careerRepository;
        private readonly IInheritanceLawRepository _inheritanceRepository;

        public FamilyController(
            IFamilyRepository familyRepository, 
            ICharacterRepository characterRepository,
            IGoalRepository goalRepository,
            IPreferenceRepository preferenceRepository,
            ICareerRepository careerRepository,
            IInheritanceLawRepository inheritanceRepository)
        {
            _familyRepository = familyRepository;
            _characterRepository = characterRepository;
            _goalRepository = goalRepository;
            _preferenceRepository = preferenceRepository;
            _careerRepository = careerRepository;
            _inheritanceRepository = inheritanceRepository;
        }

        public ViewResult Index()
        {
            var familiesViewModel = new FamiliesViewModel
            {
                Families = _familyRepository.Families.ToList(),
                InheritanceLaws = _inheritanceRepository.InheritanceLaws.ToList()
            };
            return View(familiesViewModel);
        }

        public ViewResult List(int familyId = 1)
        {
            var familyViewModel = new FamilyViewModel
            {
                Family = _familyRepository.Families.FirstOrDefault(f => f.FamilyId == familyId),
                Characters = _characterRepository.Characters.Where(c => c.Family == familyId),
                Goals = _goalRepository.Goals,
                Preferences = _preferenceRepository.Preferences,
                Careers = _careerRepository.Careers,
                InheritanceLaws = _inheritanceRepository.InheritanceLaws
            };

            return View(familyViewModel);
        }
        public ViewResult Create()
        {
            FamilyViewModel familyViewModel = new FamilyViewModel
            {
                Family = new Family(),
                Characters = _characterRepository.Characters,
                Goals = _goalRepository.Goals,
                Preferences = _preferenceRepository.Preferences,
                Careers = _careerRepository.Careers,
                InheritanceLaws = _inheritanceRepository.InheritanceLaws
            };
            return View(familyViewModel);
;       }

        [HttpPost]
        public ActionResult Create(Family family)
        {
            _familyRepository.SaveFamily(family);
            
            return Redirect($"/Character/Create/{family.FamilyId}");
        }
    }
}