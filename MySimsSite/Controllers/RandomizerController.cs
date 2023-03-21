﻿using Domain.Abstract;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using MjauriziaSims.Models;

namespace MjauriziaSims.Controllers
{
    public class RandomizerController : Controller
    {
        private readonly IGoalRepository _goalRepository;
        private readonly IPreferenceRepository _preferenceRepository;
        private readonly ICareerRepository _careerRepository;
        private readonly MessageManager.MessageManager _msgManager;

        public RandomizerController(
            IGoalRepository goalRepository,
            IPreferenceRepository preferenceRepository,
            ICareerRepository careerRepository,
            MessageManager.MessageManager msgManager)
        {
            _goalRepository = goalRepository;
            _preferenceRepository = preferenceRepository;
            _careerRepository = careerRepository;
            _msgManager = msgManager;
        }

        public ViewResult Goal()
        {
            var model = new RandomizerViewModel()
            {
                Goals = _goalRepository.Goals.Where(g => !g.IsChild).OrderBy(g => g.Title).ToList(),
                MsgManager = _msgManager
            };
            return View(model);
        }
        public ViewResult ChildGoal()
        {
            var model = new RandomizerViewModel()
            {
                Goals = _goalRepository.Goals.Where(g => g.IsChild).ToList(),
                MsgManager = _msgManager
            };
            return View(model);
        }
        public ViewResult Career()
        {
            var model = new RandomizerViewModel()
            {
                Careers = _careerRepository.Careers.ToList(),
                MsgManager = _msgManager
            };
            return View(model);
        }
        public ViewResult PreferenceColor()
        {
            var model = new RandomizerViewModel()
            {
                Preferences = _preferenceRepository.Preferences
                    .Where(p => p.Category == (PreferenceCategories)0).ToList(),
                MsgManager = _msgManager
            };
            return View(model);
        }
        public ViewResult PreferenceMusic()
        {
            var model = new RandomizerViewModel()
            {
                Preferences = _preferenceRepository.Preferences
                    .Where(p => p.Category == (PreferenceCategories)1).ToList(),
                MsgManager = _msgManager
            };
            return View(model);
        }
        public ViewResult PreferenceHobby()
        {
            var model = new RandomizerViewModel()
            {
                Preferences = _preferenceRepository.Preferences
                    .Where(p => p.Category == (PreferenceCategories)2).ToList(),
                MsgManager = _msgManager
            };
            return View(model);
        }
        public ViewResult PreferenceDesign()
        {
            var model = new RandomizerViewModel()
            {
                Preferences = _preferenceRepository.Preferences
                    .Where(p => p.Category == (PreferenceCategories)3).ToList(),
                MsgManager = _msgManager
            };
            return View(model);
        }
        public ViewResult PreferenceClothes()
        {
            var model = new RandomizerViewModel()
            {
                Preferences = _preferenceRepository.Preferences
                    .Where(p => p.Category == (PreferenceCategories)4).ToList(),
                MsgManager = _msgManager
            };
            return View(model);
        }
        public ViewResult PreferenceTopics()
        {
            var model = new RandomizerViewModel()
            {
                Preferences = _preferenceRepository.Preferences
                    .Where(p => p.Category == (PreferenceCategories)5).ToList(),
                MsgManager = _msgManager
            };
            return View(model);
        }
        public ViewResult PreferenceCharacteristic()
        {
            var model = new RandomizerViewModel()
            {
                Preferences = _preferenceRepository.Preferences
                    .Where(p => p.Category == (PreferenceCategories)6).ToList(),
                MsgManager = _msgManager
            };
            return View(model);
        }
    }
}