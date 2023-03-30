using Domain.Abstract;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using MjauriziaSims.Models;

namespace MjauriziaSims.Controllers
{
    public class RandomizerController : Controller
    {
        private readonly IEnumerable<Goal> _goals;
        private readonly IEnumerable<Preference> _preferences;
        private readonly IEnumerable<Career> _careers;
        private readonly IEnumerable<Pack> _packs;
        private readonly MessageManager.MessageManager _msgManager;

        public RandomizerController(
            IGoalRepository goalRepository,
            IPreferenceRepository preferenceRepository,
            ICareerRepository careerRepository,
            IPackRepository packRepository,
            MessageManager.MessageManager msgManager)
        {
            _goals = goalRepository.Goals.ToList();
            _preferences = preferenceRepository.Preferences.ToList();
            _careers = careerRepository.Careers.ToList();
            _packs = packRepository.Packs.ToList();
            _msgManager = msgManager;
        }

        public ViewResult Goal()
        {
            var model = new RandomizerViewModel()
            {
                Goals = _goals.Where(g => !g.IsChild).OrderBy(g => g.Title).ToList(),
                Packs = _packs.Where(p => _goals.Where(g => !g.IsChild).Select(g => g.Pack).Contains(p.PackId)),
                MsgManager = _msgManager
            };
            return View(model);
        }
        public ViewResult ChildGoal()
        {
            var model = new RandomizerViewModel()
            {
                Goals = _goals.Where(g => g.IsChild).ToList(),
                Packs = _packs.Where(p => _goals.Where(g => g.IsChild).Select(g => g.Pack).Contains(p.PackId)),
                MsgManager = _msgManager
            };
            return View(model);
        }
        public ViewResult Career()
        {
            var model = new RandomizerViewModel()
            {
                Careers = _careers,
                Packs = _packs.Where(p => _careers.Select(c => c.Pack).Contains(p.PackId)),
                MsgManager = _msgManager
            };
            return View(model);
        }
        public ViewResult PreferenceColor()
        {
            var model = new RandomizerViewModel()
            {
                Preferences = _preferences.Where(p => p.Category == (PreferenceCategories)0).ToList(),
                MsgManager = _msgManager
            };
            return View(model);
        }
        public ViewResult PreferenceMusic()
        {
            var preferences = _preferences.Where(p => p.Category == (PreferenceCategories)1).ToList();
            var model = new RandomizerViewModel()
            {
                Preferences = preferences,
                Packs = _packs.Where(p => preferences.Select(v => v.Pack).Contains(p.PackId)),
                MsgManager = _msgManager
            };
            return View(model);
        }
        public ViewResult PreferenceHobby()
        {
            var preferences = _preferences.Where(p => p.Category == (PreferenceCategories)2).ToList();
            var model = new RandomizerViewModel()
            {
                Preferences = preferences,
                Packs = _packs.Where(p => preferences.Select(p => p.Pack).Contains(p.PackId)),
                MsgManager = _msgManager
            };
            return View(model);
        }
        public ViewResult PreferenceDesign()
        {
            var model = new RandomizerViewModel()
            {
                Preferences = _preferences.Where(p => p.Category == (PreferenceCategories)3).ToList(),
                MsgManager = _msgManager
            };
            return View(model);
        }
        public ViewResult PreferenceClothes()
        {
            var model = new RandomizerViewModel()
            {
                Preferences = _preferences.Where(p => p.Category == (PreferenceCategories)4).ToList(),
                MsgManager = _msgManager
            };
            return View(model);
        }
        public ViewResult PreferenceTopics()
        {
            var model = new RandomizerViewModel()
            {
                Preferences = _preferences.Where(p => p.Category == (PreferenceCategories)5).ToList(),
                MsgManager = _msgManager
            };
            return View(model);
        }
        public ViewResult PreferenceCharacteristic()
        {
            var model = new RandomizerViewModel()
            {
                Preferences = _preferences.Where(p => p.Category == (PreferenceCategories)6).ToList(),
                MsgManager = _msgManager
            };
            return View(model);
        }
    }
}