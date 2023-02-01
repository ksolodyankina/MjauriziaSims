using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Migrator
{
    public class Migrator
    {
        private readonly IGoalRepository _goalRepository;
        private readonly IPreferenceRepository _preferenceRepository;
        private readonly ICareerRepository _careerRepository;
        private readonly IInheritanceLawRepository _inheritanceRepository;

        private static readonly Goal[] Goals = new [] {
            new Goal {IsChild = false, Title = "Academician"},
            new Goal {IsChild = false, Title = "Actor"},
            new Goal {IsChild = false, Title = "Adviser"},
            new Goal {IsChild = false, Title = "Animator"},
            new Goal {IsChild = false, Title = "Archeologist"},
            new Goal {IsChild = true, Title = "Artistic"},
            new Goal {IsChild = true, Title = "Athletic"},
            new Goal {IsChild = false, Title = "Baron"},
            new Goal {IsChild = false, Title = "Bartender"},
            new Goal {IsChild = false, Title = "Camping"},
            new Goal {IsChild = false, Title = "Caretaker"},
            new Goal {IsChild = false, Title = "Celebrity"},
            new Goal {IsChild = false, Title = "Cheese"},
            new Goal {IsChild = false, Title = "Chief"},
            new Goal {IsChild = false, Title = "Citizen"},
            new Goal {IsChild = false, Title = "Clan"},
            new Goal {IsChild = false, Title = "Cleanliness"},
            new Goal {IsChild = false, Title = "Collector"},
            new Goal {IsChild = false, Title = "Comedian"},
            new Goal {IsChild = false, Title = "Craft"},
            new Goal {IsChild = false, Title = "Dirty"},
            new Goal {IsChild = false, Title = "Econovator"},
            new Goal {IsChild = false, Title = "Evil"},
            new Goal {IsChild = false, Title = "Extreme"},
            new Goal {IsChild = false, Title = "Family"},
            new Goal {IsChild = false, Title = "Fisherman"},
            new Goal {IsChild = false, Title = "Friend"},
            new Goal {IsChild = true, Title = "Friendly"},
            new Goal {IsChild = false, Title = "Gardener"},
            new Goal {IsChild = false, Title = "Good vampire"},
            new Goal {IsChild = false, Title = "Heartbreaker"},
            new Goal {IsChild = false, Title = "Heredity"},
            new Goal {IsChild = false, Title = "Island"},
            new Goal {IsChild = false, Title = "Jungle"},
            new Goal {IsChild = false, Title = "Knitting"},
            new Goal {IsChild = false, Title = "Leader"},
            new Goal {IsChild = false, Title = "Love"},
            new Goal {IsChild = false, Title = "Mechanic"},
            new Goal {IsChild = false, Title = "Mischief"},
            new Goal {IsChild = false, Title = "Mountain"},
            new Goal {IsChild = false, Title = "Multitasking"},
            new Goal {IsChild = false, Title = "Musician"},
            new Goal {IsChild = false, Title = "Painter"},
            new Goal {IsChild = false, Title = "Parent"},
            new Goal {IsChild = false, Title = "Peace of mind"},
            new Goal {IsChild = false, Title = "Pets"},
            new Goal {IsChild = false, Title = "Potions"},
            new Goal {IsChild = true, Title = "Prodigy"},
            new Goal {IsChild = false, Title = "Programmer"},
            new Goal {IsChild = false, Title = "Rich"},
            new Goal {IsChild = false, Title = "Secret"},
            new Goal {IsChild = false, Title = "Slut"},
            new Goal {IsChild = false, Title = "Spa worker"},
            new Goal {IsChild = false, Title = "Sport"},
            new Goal {IsChild = false, Title = "Vampire master"},
            new Goal {IsChild = false, Title = "Wizard"},
            new Goal {IsChild = false, Title = "Writer"},
            new Goal {IsChild = false, Title = "Zen"},
            new Goal {IsChild = false, Title = "Werewolf"},
            new Goal {IsChild = false, Title = "Party-goer"},
            new Goal {IsChild = false, Title = "Legend"},
            new Goal {IsChild = false, Title = "Drama"},
            new Goal {IsChild = false, Title = "Goal"}
        };

        private static readonly Preference[] Preferences = new[]
        {
            new Preference { Category = (Preference.Categories)2, Title = "Actor" },
            new Preference { Category = (Preference.Categories)1, Title = "Alternative" },
            new Preference { Category = (Preference.Categories)1, Title = "Americana" },
            new Preference { Category = (Preference.Categories)1, Title = "Art song" },
            new Preference { Category = (Preference.Categories)2, Title = "Baking" },
            new Preference { Category = (Preference.Categories)1, Title = "Baroque" },
            new Preference { Category = (Preference.Categories)3, Title = "Basics" },
            new Preference { Category = (Preference.Categories)0, Title = "Black" },
            new Preference { Category = (Preference.Categories)0, Title = "Blue" },
            new Preference { Category = (Preference.Categories)1, Title = "Blues" },
            new Preference { Category = (Preference.Categories)3, Title = "Boho" },
            new Preference { Category = (Preference.Categories)2, Title = "Bowling" },
            new Preference { Category = (Preference.Categories)0, Title = "Brown" },
            new Preference { Category = (Preference.Categories)1, Title = "Classical" },
            new Preference { Category = (Preference.Categories)2, Title = "Comedy" },
            new Preference { Category = (Preference.Categories)1, Title = "Concentration" },
            new Preference { Category = (Preference.Categories)3, Title = "Conceptual" },
            new Preference { Category = (Preference.Categories)3, Title = "Contemporary" },
            new Preference { Category = (Preference.Categories)2, Title = "Cooking" },
            new Preference { Category = (Preference.Categories)3, Title = "Cosmolux" },
            new Preference { Category = (Preference.Categories)2, Title = "Cross-stitch" },
            new Preference { Category = (Preference.Categories)2, Title = "Dancing" },
            new Preference { Category = (Preference.Categories)1, Title = "Dj music" },
            new Preference { Category = (Preference.Categories)2, Title = "Dj" },
            new Preference { Category = (Preference.Categories)1, Title = "Electronica" },
            new Preference { Category = (Preference.Categories)3, Title = "Fairy tale" },
            new Preference { Category = (Preference.Categories)2, Title = "Fishing" },
            new Preference { Category = (Preference.Categories)2, Title = "Fitness" },
            new Preference { Category = (Preference.Categories)3, Title = "French" },
            new Preference { Category = (Preference.Categories)2, Title = "Gaming" },
            new Preference { Category = (Preference.Categories)3, Title = "Garden" },
            new Preference { Category = (Preference.Categories)2, Title = "Gardening" },
            new Preference { Category = (Preference.Categories)3, Title = "Gothic" },
            new Preference { Category = (Preference.Categories)0, Title = "Gray" },
            new Preference { Category = (Preference.Categories)0, Title = "Green" },
            new Preference { Category = (Preference.Categories)2, Title = "Guitar" },
            new Preference { Category = (Preference.Categories)2, Title = "Handiness" },
            new Preference { Category = (Preference.Categories)1, Title = "Hip-Hop" },
            new Preference { Category = (Preference.Categories)1, Title = "Island music" },
            new Preference { Category = (Preference.Categories)3, Title = "Island" },
            new Preference { Category = (Preference.Categories)1, Title = "Japanese" },
            new Preference { Category = (Preference.Categories)1, Title = "Jazz" },
            new Preference { Category = (Preference.Categories)1, Title = "Kids pop" },
            new Preference { Category = (Preference.Categories)1, Title = "Kids" },
            new Preference { Category = (Preference.Categories)2, Title = "Knitting" },
            new Preference { Category = (Preference.Categories)1, Title = "Latin dance" },
            new Preference { Category = (Preference.Categories)1, Title = "Latino" },
            new Preference { Category = (Preference.Categories)1, Title = "Light" },
            new Preference { Category = (Preference.Categories)1, Title = "Lullabies" },
            new Preference { Category = (Preference.Categories)2, Title = "Media production" },
            new Preference { Category = (Preference.Categories)1, Title = "Metal" },
            new Preference { Category = (Preference.Categories)3, Title = "Midcentury" },
            new Preference { Category = (Preference.Categories)2, Title = "Mischief" },
            new Preference { Category = (Preference.Categories)3, Title = "Mission" },
            new Preference { Category = (Preference.Categories)2, Title = "Mixology" },
            new Preference { Category = (Preference.Categories)3, Title = "Modern" },
            new Preference { Category = (Preference.Categories)1, Title = "Newage" },
            new Preference { Category = (Preference.Categories)1, Title = "Nudisco" },
            new Preference { Category = (Preference.Categories)0, Title = "Orange" },
            new Preference { Category = (Preference.Categories)2, Title = "Painting" },
            new Preference { Category = (Preference.Categories)3, Title = "Patio" },
            new Preference { Category = (Preference.Categories)2, Title = "Photography" },
            new Preference { Category = (Preference.Categories)2, Title = "Piano" },
            new Preference { Category = (Preference.Categories)0, Title = "Pink" },
            new Preference { Category = (Preference.Categories)2, Title = "Pipe organ" },
            new Preference { Category = (Preference.Categories)1, Title = "Pop" },
            new Preference { Category = (Preference.Categories)2, Title = "Programming" },
            new Preference { Category = (Preference.Categories)3, Title = "Queen" },
            new Preference { Category = (Preference.Categories)0, Title = "Red" },
            new Preference { Category = (Preference.Categories)2, Title = "Research-Debate" },
            new Preference { Category = (Preference.Categories)1, Title = "Retro" },
            new Preference { Category = (Preference.Categories)2, Title = "Robotics" },
            new Preference { Category = (Preference.Categories)2, Title = "Rockclimbing" },
            new Preference { Category = (Preference.Categories)2, Title = "Rocket" },
            new Preference { Category = (Preference.Categories)1, Title = "Romance" },
            new Preference { Category = (Preference.Categories)1, Title = "Rustic" },
            new Preference { Category = (Preference.Categories)2, Title = "Singing" },
            new Preference { Category = (Preference.Categories)2, Title = "Skiing" },
            new Preference { Category = (Preference.Categories)2, Title = "Snowboarding" },
            new Preference { Category = (Preference.Categories)1, Title = "Spooky" },
            new Preference { Category = (Preference.Categories)1, Title = "S-pop" },
            new Preference { Category = (Preference.Categories)1, Title = "Strange tunes" },
            new Preference { Category = (Preference.Categories)1, Title = "Summer" },
            new Preference { Category = (Preference.Categories)0, Title = "Violet" },
            new Preference { Category = (Preference.Categories)2, Title = "Violin" },
            new Preference { Category = (Preference.Categories)2, Title = "Wellness" },
            new Preference { Category = (Preference.Categories)0, Title = "White" },
            new Preference { Category = (Preference.Categories)1, Title = "Winter" },
            new Preference { Category = (Preference.Categories)1, Title = "World" },
            new Preference { Category = (Preference.Categories)2, Title = "Writing" },
            new Preference { Category = (Preference.Categories)1, Title = "Yard" },
            new Preference { Category = (Preference.Categories)0, Title = "Yellow" },
            new Preference { Category = (Preference.Categories)4, Title = "Base" },
            new Preference { Category = (Preference.Categories)4, Title = "Bohema" },
            new Preference { Category = (Preference.Categories)4, Title = "Active" },
            new Preference { Category = (Preference.Categories)4, Title = "Rock" },
            new Preference { Category = (Preference.Categories)4, Title = "Village" },
            new Preference { Category = (Preference.Categories)4, Title = "Streetstyle" },
            new Preference { Category = (Preference.Categories)4, Title = "Hipster" },
            new Preference { Category = (Preference.Categories)4, Title = "Preppy" },
            new Preference { Category = (Preference.Categories)4, Title = "Elegant" }
        };                              

        private static readonly Career[] Careers = new[]
        {
            new Career { Title = "Actor" },
            new Career { Title = "Aqualunger" },
            new Career { Title = "Astronaut" },
            new Career { Title = "Barista" },
            new Career { Title = "Building engineer" },
            new Career { Title = "Business" },
            new Career { Title = "Chief" },
            new Career { Title = "Criminal" },
            new Career { Title = "Critic" },
            new Career { Title = "Designer" },
            new Career { Title = "Detective" },
            new Career { Title = "Doctor" },
            new Career { Title = "Ecologist" },
            new Career { Title = "Engineer" },
            new Career { Title = "Fashion" },
            new Career { Title = "Fisherman" },
            new Career { Title = "Freelancer Creator" },
            new Career { Title = "Freelancer Ghostbuster" },
            new Career { Title = "Freelancer Painter" },
            new Career { Title = "Freelancer Photographer" },
            new Career { Title = "Freelancer Programmer" },
            new Career { Title = "Freelancer Writer" },
            new Career { Title = "Gardener" },
            new Career { Title = "Handyman" },
            new Career { Title = "Military" },
            new Career { Title = "Nanny" },
            new Career { Title = "Painter" },
            new Career { Title = "Performer" },
            new Career { Title = "Politician" },
            new Career { Title = "Programmer" },
            new Career { Title = "Rescuer" },
            new Career { Title = "Sarariman" },
            new Career { Title = "Scientist" },
            new Career { Title = "Secret agent" },
            new Career { Title = "Salesman" },
            new Career { Title = "Simfluencer" },
            new Career { Title = "Social media" },
            new Career { Title = "Sport" },
            new Career { Title = "Streamer" },
            new Career { Title = "Teacher" },
            new Career { Title = "Waiter" },
            new Career { Title = "Writer" }
        };

        private static readonly InheritanceLaw[] InheritanceLaws = new[]
        {
            new InheritanceLaw
            {
                Category = (InheritanceCategory)0, AllowsManualChoice = false, Code = "matriarchy", IsStrict = false, Title = "Matriarchy",
                Value = 1
            },
            new InheritanceLaw
            {
                Category = (InheritanceCategory)0, AllowsManualChoice = false, Code = "smatriarchy", IsStrict = true,
                Title = "Strict Matriarchy", Value = 1
            },
            new InheritanceLaw
            {
                Category = (InheritanceCategory)0, AllowsManualChoice = false, Code = "patriarchy", IsStrict = false, Title = "Patriarchy",
                Value = 0
            },
            new InheritanceLaw
            {
                Category = (InheritanceCategory)0, AllowsManualChoice = false, Code = "spatriarchy", IsStrict = true,
                Title = "Strict Patriarchy", Value = 0
            },
            new InheritanceLaw
            {
                Category = (InheritanceCategory)0, AllowsManualChoice = true, Code = "equality", IsStrict = false, Title = "Equality",
                Value = 2
            },
            new InheritanceLaw
            {
                Category = (InheritanceCategory)0, AllowsManualChoice = false, Code = "sequality", IsStrict = true,
                Title = "Strict Equality", Value = 2
            },
            new InheritanceLaw
            {
                Category = (InheritanceCategory)1, AllowsManualChoice = false, Code = "straditional", IsStrict = true,
                Title = "Strict Traditional", Value = 0
            },
            new InheritanceLaw
            {
                Category = (InheritanceCategory)1, AllowsManualChoice = false, Code = "traditional", IsStrict = false, 
                Title = "Traditional", Value = 0
            },
            new InheritanceLaw
            {
                Category = (InheritanceCategory)1, AllowsManualChoice = true, Code = "modern", IsStrict = false,
                Title = "Modern", Value = 2
            },
            new InheritanceLaw
            {
                Category = (InheritanceCategory)1, AllowsManualChoice = false, Code = "foster", IsStrict = false,
                Title = "Foster", Value = 1
            },
            new InheritanceLaw
            {
                Category = (InheritanceCategory)1, AllowsManualChoice = false, Code = "sfoster", IsStrict = true,
                Title = "Strict Foster", Value = 1
            },
            new InheritanceLaw
            {
                Category = (InheritanceCategory)2, AllowsManualChoice = false, Code = "firstborn", IsStrict = true,
                Title = "First Born", Value = 0
            },
            new InheritanceLaw
            {
                Category = (InheritanceCategory)2, AllowsManualChoice = false, Code = "lastborn", IsStrict = true,
                Title = "Last Born", Value = 1
            },
            new InheritanceLaw
            {
                Category = (InheritanceCategory)2, AllowsManualChoice = true, Code = "livingwill", IsStrict = true,
                Title = "Living Will", Value = 2
            },
            new InheritanceLaw
            {
                Category = (InheritanceCategory)2, AllowsManualChoice = true, Code = "merit", IsStrict = true,
                Title = "Merit", Value = 2
            },
            new InheritanceLaw
            {
                Category = (InheritanceCategory)2, AllowsManualChoice = true, Code = "strength", IsStrict = true,
                Title = "Strength", Value = 2
            },
            new InheritanceLaw
            {
                Category = (InheritanceCategory)2, AllowsManualChoice = false, Code = "random", IsStrict = true,
                Title = "Random", Value = 2
            },
            new InheritanceLaw
            {
                Category = (InheritanceCategory)2, AllowsManualChoice = true, Code = "exemplar", IsStrict = true,
                Title = "Exemplar", Value = 2
            },
            new InheritanceLaw
            {
                Category = (InheritanceCategory)2, AllowsManualChoice = true, Code = "democracy", IsStrict = true,
                Title = "Democracy", Value = 2
            },
            new InheritanceLaw
            {
                Category = (InheritanceCategory)2, AllowsManualChoice = true, Code = "magicalbloodline", IsStrict = true,
                Title = "Magical Bloodline", Value = 2
            },
            new InheritanceLaw
            {
                Category = (InheritanceCategory)2, AllowsManualChoice = true, Code = "magicalstrength", IsStrict = true,
                Title = "Magical Strength", Value = 2
            },
            new InheritanceLaw
            {
                Category = (InheritanceCategory)3, AllowsManualChoice = true, Code = "xenoarchy", IsStrict = false,
                Title = "Xenoarchy", Value = 2
            },
            new InheritanceLaw
            {
                Category = (InheritanceCategory)3, AllowsManualChoice = true, Code = "xenophobic", IsStrict = false,
                Title = "Xenophobic", Value = 2
            },
            new InheritanceLaw
            {
                Category = (InheritanceCategory)3, AllowsManualChoice = true, Code = "brood", IsStrict = false,
                Title = "Brood", Value = 2
            },
            new InheritanceLaw
            {
                Category = (InheritanceCategory)3, AllowsManualChoice = true, Code = "tolerant", IsStrict = false,
                Title = "Tolerant", Value = 2
            },
        };

        public Migrator(
            IGoalRepository goalRepository,
            IPreferenceRepository preferenceRepository,
            ICareerRepository careerRepository,
            IInheritanceLawRepository inheritanceRepository)
        {
            _goalRepository = goalRepository;
            _preferenceRepository = preferenceRepository;
            _careerRepository = careerRepository;
            _inheritanceRepository = inheritanceRepository;
        }

        public void Migrate()
        {
            foreach (var goal in Goals)
            {
                if (_goalRepository.Goals.FirstOrDefault(g => g.Title == goal.Title) == null)
                {
                    _goalRepository.SaveGoal(goal);
                }
            }

            foreach (var preference in Preferences)
            {
                if (_preferenceRepository.Preferences.FirstOrDefault(p =>
                        p.Title == preference.Title && p.Category == preference.Category)
                    == null)
                {
                    _preferenceRepository.SavePreference(preference);
                }
            }

            foreach (var career in Careers)
            {
                if (_careerRepository.Careers.FirstOrDefault(c => c.Title == career.Title) == null)
                {
                    _careerRepository.SaveCareer(career);
                }
            }

            foreach (var law in InheritanceLaws)
            {
                if (_inheritanceRepository.InheritanceLaws.
                        FirstOrDefault(l => l.Category == law.Category && l.Title == law.Title)
                    == null)
                {
                    _inheritanceRepository.SaveInheritanceLaw(law);
                }
            }
        }
    }
}
