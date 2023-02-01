using Domain.Abstract;
using System.Linq;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace MjauriziaSims.Controllers
{
    public class RandomizerController : Controller
    {
        private readonly IGoalRepository _goalRepository;
        private readonly IPreferenceRepository _preferenceRepository;
        private readonly ICareerRepository _careerRepository;

        public RandomizerController(
            IGoalRepository goalRepository,
            IPreferenceRepository preferenceRepository,
            ICareerRepository careerRepository)
        {
            _goalRepository = goalRepository;
            _preferenceRepository = preferenceRepository;
            _careerRepository = careerRepository;
        }

        public ViewResult Goal()
        {
            return View(_goalRepository.Goals.Where(g => !g.IsChild).OrderBy(g => g.Title).ToList());
        }
        public ViewResult ChildGoal()
        {
            return View(_goalRepository.Goals.Where(g => g.IsChild).ToList());
        }
        public ViewResult Career()
        {
            return View(_careerRepository.Careers.ToList());
        }
        public ViewResult PreferenceColor()
        {
            return View(_preferenceRepository.Preferences
                .Where(p => p.Category == (Preference.Categories)0).ToList());
        }
        public ViewResult PreferenceMusic()
        {
            return View(_preferenceRepository.Preferences
                .Where(p => p.Category == (Preference.Categories)1).ToList());
        }
        public ViewResult PreferenceHobby()
        {
            return View(_preferenceRepository.Preferences
                .Where(p => p.Category == (Preference.Categories)2).ToList());
        }
        public ViewResult PreferenceDesign()
        {
            return View(_preferenceRepository.Preferences
                .Where(p => p.Category == (Preference.Categories)3).ToList());
        }
        public ViewResult PreferenceClothes()
        {
            return View(_preferenceRepository.Preferences
                .Where(p => p.Category == (Preference.Categories)4).ToList());
        }
    }
}