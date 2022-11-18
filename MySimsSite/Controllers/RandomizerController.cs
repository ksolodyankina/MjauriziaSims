using Domain.Abstract;
using System.Linq;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
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
            return View(_goalRepository.Goals.ToList());
        }
    }
}