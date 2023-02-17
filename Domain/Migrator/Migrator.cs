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
        private readonly IMsgRepository _msgRepository;

        private static readonly Goal[] Goals = new [] {
            new Goal { Code = "academician", IsChild = false, Title = "goal_academician"},
            new Goal { Code = "actor", IsChild = false, Title = "goal_actor"},
            new Goal { Code = "adviser", IsChild = false, Title = "goal_adviser"},
            new Goal { Code = "animator", IsChild = false, Title = "goal_animator"},
            new Goal { Code = "archeologist", IsChild = false, Title = "goal_archeologist"},
            new Goal { Code = "artistic", IsChild = true, Title = "goal_artistic"},
            new Goal { Code = "athletic", IsChild = true, Title = "goal_athletic"},
            new Goal { Code = "baron", IsChild = false, Title = "goal_baron"},
            new Goal { Code = "bartender", IsChild = false, Title = "goal_bartender"},
            new Goal { Code = "camping", IsChild = false, Title = "goal_camping"},
            new Goal { Code = "caretaker", IsChild = false, Title = "goal_caretaker"},
            new Goal { Code = "celebrity", IsChild = false, Title = "goal_celebrity"},
            new Goal { Code = "chief", IsChild = false, Title = "goal_chief"},
            new Goal { Code = "citizen", IsChild = false, Title = "goal_citizen"},
            new Goal { Code = "clan", IsChild = false, Title = "goal_clan"},
            new Goal { Code = "cleanliness", IsChild = false, Title = "goal_cleanliness"},
            new Goal { Code = "collector", IsChild = false, Title = "goal_collector"},
            new Goal { Code = "comedian", IsChild = false, Title = "goal_comedian"},
            new Goal { Code = "craft", IsChild = false, Title = "goal_craft"},
            new Goal { Code = "dirty", IsChild = false, Title = "goal_dirty"},
            new Goal { Code = "econovator", IsChild = false, Title = "goal_econovator"},
            new Goal { Code = "evil", IsChild = false, Title = "goal_evil"},
            new Goal { Code = "extreme", IsChild = false, Title = "goal_extreme"},
            new Goal { Code = "family", IsChild = false, Title = "goal_family"},
            new Goal { Code = "fisherman", IsChild = false, Title = "goal_fisherman"},
            new Goal { Code = "friend", IsChild = false, Title = "goal_friend"},
            new Goal { Code = "friendly", IsChild = true, Title = "goal_friendly"},
            new Goal { Code = "gardener", IsChild = false, Title = "goal_gardener"},
            new Goal { Code = "goodVampire", IsChild = false, Title = "goal_goodVampire"},
            new Goal { Code = "heartbreaker", IsChild = false, Title = "goal_heartbreaker"},
            new Goal { Code = "heredity", IsChild = false, Title = "goal_heredity"},
            new Goal { Code = "island", IsChild = false, Title = "goal_island"},
            new Goal { Code = "jungle", IsChild = false, Title = "goal_jungle"},
            new Goal { Code = "knitting", IsChild = false, Title = "goal_knitting"},
            new Goal { Code = "leader", IsChild = false, Title = "goal_leader"},
            new Goal { Code = "love", IsChild = false, Title = "goal_love"},
            new Goal { Code = "mechanic", IsChild = false, Title = "goal_mechanic"},
            new Goal { Code = "mischief", IsChild = false, Title = "goal_mischief"},
            new Goal { Code = "mountain", IsChild = false, Title = "goal_mountain"},
            new Goal { Code = "multitasking", IsChild = false, Title = "goal_multitasking"},
            new Goal { Code = "musician", IsChild = false, Title = "goal_musician"},
            new Goal { Code = "painter", IsChild = false, Title = "goal_painter"},
            new Goal { Code = "parent", IsChild = false, Title = "goal_parent"},
            new Goal { Code = "peaceOfMind", IsChild = false, Title = "goal_peaceOfMind"},
            new Goal { Code = "pets", IsChild = false, Title = "goal_pets"},
            new Goal { Code = "potions", IsChild = false, Title = "goal_potions"},
            new Goal { Code = "prodigy", IsChild = true, Title = "goal_prodigy"},
            new Goal { Code = "programmer", IsChild = false, Title = "goal_programmer"},
            new Goal { Code = "rich", IsChild = false, Title = "goal_rich"},
            new Goal { Code = "secret", IsChild = false, Title = "goal_secret"},
            new Goal { Code = "slut", IsChild = false, Title = "goal_slut"},
            new Goal { Code = "spaWorker", IsChild = false, Title = "goal_spaWorker"},
            new Goal { Code = "sport", IsChild = false, Title = "goal_sport"},
            new Goal { Code = "vampireMaster", IsChild = false, Title = "goal_vampireMaster"},
            new Goal { Code = "wizard", IsChild = false, Title = "goal_wizard"},
            new Goal { Code = "writer", IsChild = false, Title = "goal_writer"},
            new Goal { Code = "zen", IsChild = false, Title = "goal_zen"},
            new Goal { Code = "werewolf", IsChild = false, Title = "goal_werewolf"},
            new Goal { Code = "partyGoer", IsChild = false, Title = "goal_partyGoer"},
            new Goal { Code = "legend", IsChild = false, Title = "goal_legend"},
            new Goal { Code = "drama", IsChild = false, Title = "goal_drama"},
            new Goal { Code = "goal", IsChild = false, Title = "goal_goal"},
            new Goal { Code = "batuu", IsChild = false, Title = "goal_batuu"},
            new Goal { Code = "batuuPrivateer", IsChild = false, Title = "goal_batuuPrivateer"},
        };

        private static readonly Preference[] Preferences = new[]
        {
            new Preference { Category = (Preference.Categories)2, Code = "actor", Title = "preference_actor" },
            new Preference { Category = (Preference.Categories)1, Code = "alternative", Title = "preference_alternative" },
            new Preference { Category = (Preference.Categories)1, Code = "americana", Title = "preference_americana" },
            new Preference { Category = (Preference.Categories)1, Code = "artSong", Title = "preference_artSong" },
            new Preference { Category = (Preference.Categories)2, Code = "baking", Title = "preference_baking" },
            new Preference { Category = (Preference.Categories)1, Code = "baroque", Title = "preference_baroque" },
            new Preference { Category = (Preference.Categories)3, Code = "basics", Title = "preference_basics" },
            new Preference { Category = (Preference.Categories)0, Code = "black", Title = "preference_black" },
            new Preference { Category = (Preference.Categories)0, Code = "blue", Title = "preference_blue" },
            new Preference { Category = (Preference.Categories)1, Code = "blues", Title = "preference_blues" },
            new Preference { Category = (Preference.Categories)3, Code = "boho", Title = "preference_boho" },
            new Preference { Category = (Preference.Categories)2, Code = "bowling", Title = "preference_bowling" },
            new Preference { Category = (Preference.Categories)0, Code = "brown", Title = "preference_brown" },
            new Preference { Category = (Preference.Categories)1, Code = "classical", Title = "preference_classical" },
            new Preference { Category = (Preference.Categories)2, Code = "comedy", Title = "preference_comedy" },
            new Preference { Category = (Preference.Categories)1, Code = "concentration", Title = "preference_concentration" },
            new Preference { Category = (Preference.Categories)3, Code = "conceptual", Title = "preference_conceptual" },
            new Preference { Category = (Preference.Categories)3, Code = "contemporary", Title = "preference_contemporary" },
            new Preference { Category = (Preference.Categories)2, Code = "cooking", Title = "preference_cooking" },
            new Preference { Category = (Preference.Categories)3, Code = "cosmolux", Title = "preference_cosmolux" },
            new Preference { Category = (Preference.Categories)2, Code = "crossStitch", Title = "preference_cross-stitch" },
            new Preference { Category = (Preference.Categories)2, Code = "dancing", Title = "preference_dancing" },
            new Preference { Category = (Preference.Categories)1, Code = "djMusic", Title = "preference_djMusic" },
            new Preference { Category = (Preference.Categories)2, Code = "dj", Title = "preference_dj" },
            new Preference { Category = (Preference.Categories)1, Code = "electronica", Title = "preference_electronica" },
            new Preference { Category = (Preference.Categories)3, Code = "fairyTale", Title = "preference_fairyTale" },
            new Preference { Category = (Preference.Categories)2, Code = "fishing", Title = "preference_fishing" },
            new Preference { Category = (Preference.Categories)2, Code = "fitness", Title = "preference_fitness" },
            new Preference { Category = (Preference.Categories)3, Code = "french", Title = "preference_french" },
            new Preference { Category = (Preference.Categories)2, Code = "gaming", Title = "preference_gaming" },
            new Preference { Category = (Preference.Categories)3, Code = "garden", Title = "preference_garden" },
            new Preference { Category = (Preference.Categories)2, Code = "gardening", Title = "preference_gardening" },
            new Preference { Category = (Preference.Categories)3, Code = "gothic", Title = "preference_gothic" },
            new Preference { Category = (Preference.Categories)0, Code = "gray", Title = "preference_gray" },
            new Preference { Category = (Preference.Categories)0, Code = "green", Title = "preference_green" },
            new Preference { Category = (Preference.Categories)2, Code = "guitar", Title = "preference_guitar" },
            new Preference { Category = (Preference.Categories)2, Code = "handiness", Title = "preference_handiness" },
            new Preference { Category = (Preference.Categories)1, Code = "hipHop", Title = "preference_hip-hop" },
            new Preference { Category = (Preference.Categories)1, Code = "islandMusic", Title = "preference_islandMusic" },
            new Preference { Category = (Preference.Categories)3, Code = "island", Title = "preference_island" },
            new Preference { Category = (Preference.Categories)1, Code = "japanese", Title = "preference_japanese" },
            new Preference { Category = (Preference.Categories)1, Code = "jazz", Title = "preference_jazz" },
            new Preference { Category = (Preference.Categories)1, Code = "kidsPop", Title = "preference_kidsPop" },
            new Preference { Category = (Preference.Categories)1, Code = "kids", Title = "preference_kids" },
            new Preference { Category = (Preference.Categories)2, Code = "knitting", Title = "preference_knitting" },
            new Preference { Category = (Preference.Categories)1, Code = "latinDance", Title = "preference_latinDance" },
            new Preference { Category = (Preference.Categories)1, Code = "latino", Title = "preference_latino" },
            new Preference { Category = (Preference.Categories)1, Code = "light", Title = "preference_light" },
            new Preference { Category = (Preference.Categories)1, Code = "lullabies", Title = "preference_lullabies" },
            new Preference { Category = (Preference.Categories)2, Code = "mediaProduction", Title = "preference_mediaProduction" },
            new Preference { Category = (Preference.Categories)1, Code = "metal", Title = "preference_metal" },
            new Preference { Category = (Preference.Categories)3, Code = "midCentury", Title = "preference_midCentury" },
            new Preference { Category = (Preference.Categories)2, Code = "mischief", Title = "preference_mischief" },
            new Preference { Category = (Preference.Categories)3, Code = "mission", Title = "preference_mission" },
            new Preference { Category = (Preference.Categories)2, Code = "mixology", Title = "preference_mixology" },
            new Preference { Category = (Preference.Categories)3, Code = "modern", Title = "preference_modern" },
            new Preference { Category = (Preference.Categories)1, Code = "newAge", Title = "preference_newAge" },
            new Preference { Category = (Preference.Categories)1, Code = "nuDisco", Title = "preference_nuDisco" },
            new Preference { Category = (Preference.Categories)0, Code = "orange", Title = "preference_orange" },
            new Preference { Category = (Preference.Categories)2, Code = "painting", Title = "preference_painting" },
            new Preference { Category = (Preference.Categories)3, Code = "patio", Title = "preference_patio" },
            new Preference { Category = (Preference.Categories)2, Code = "photography", Title = "preference_photography" },
            new Preference { Category = (Preference.Categories)2, Code = "piano", Title = "preference_piano" },
            new Preference { Category = (Preference.Categories)0, Code = "pink", Title = "preference_pink" },
            new Preference { Category = (Preference.Categories)2, Code = "pipeOrgan", Title = "preference_pipeOrgan" },
            new Preference { Category = (Preference.Categories)1, Code = "pop", Title = "preference_pop" },
            new Preference { Category = (Preference.Categories)2, Code = "programming", Title = "preference_programming" },
            new Preference { Category = (Preference.Categories)3, Code = "queen", Title = "preference_queen" },
            new Preference { Category = (Preference.Categories)0, Code = "red", Title = "preference_red" },
            new Preference { Category = (Preference.Categories)2, Code = "researchDebate", Title = "preference_research-debate" },
            new Preference { Category = (Preference.Categories)1, Code = "retro", Title = "preference_retro" },
            new Preference { Category = (Preference.Categories)2, Code = "robotics", Title = "preference_robotics" },
            new Preference { Category = (Preference.Categories)2, Code = "rockClimbing", Title = "preference_rockClimbing" },
            new Preference { Category = (Preference.Categories)2, Code = "rocket", Title = "preference_rocket" },
            new Preference { Category = (Preference.Categories)1, Code = "romance", Title = "preference_romance" },
            new Preference { Category = (Preference.Categories)1, Code = "rustic", Title = "preference_rustic" },
            new Preference { Category = (Preference.Categories)2, Code = "singing", Title = "preference_singing" },
            new Preference { Category = (Preference.Categories)2, Code = "skiing", Title = "preference_skiing" },
            new Preference { Category = (Preference.Categories)2, Code = "snowboarding", Title = "preference_snowboarding" },
            new Preference { Category = (Preference.Categories)1, Code = "spooky", Title = "preference_spooky" },
            new Preference { Category = (Preference.Categories)1, Code = "s-pop", Title = "preference_s-pop" },
            new Preference { Category = (Preference.Categories)1, Code = "strangeTunes", Title = "preference_strangeTunes" },
            new Preference { Category = (Preference.Categories)1, Code = "summer", Title = "preference_summer" },
            new Preference { Category = (Preference.Categories)0, Code = "violet", Title = "preference_violet" },
            new Preference { Category = (Preference.Categories)2, Code = "violin", Title = "preference_violin" },
            new Preference { Category = (Preference.Categories)2, Code = "wellness", Title = "preference_wellness" },
            new Preference { Category = (Preference.Categories)0, Code = "white", Title = "preference_white" },
            new Preference { Category = (Preference.Categories)1, Code = "winter", Title = "preference_winter" },
            new Preference { Category = (Preference.Categories)1, Code = "world", Title = "preference_world" },
            new Preference { Category = (Preference.Categories)2, Code = "writing", Title = "preference_writing" },
            new Preference { Category = (Preference.Categories)1, Code = "yard", Title = "preference_yard" },
            new Preference { Category = (Preference.Categories)0, Code = "yellow", Title = "preference_yellow" },
            new Preference { Category = (Preference.Categories)4, Code = "base", Title = "preference_base" },
            new Preference { Category = (Preference.Categories)4, Code = "bohema", Title = "preference_bohema" },
            new Preference { Category = (Preference.Categories)4, Code = "active", Title = "preference_active" },
            new Preference { Category = (Preference.Categories)4, Code = "rock", Title = "preference_rock" },
            new Preference { Category = (Preference.Categories)4, Code = "village", Title = "preference_village" },
            new Preference { Category = (Preference.Categories)4, Code = "streetstyle", Title = "preference_streetstyle" },
            new Preference { Category = (Preference.Categories)4, Code = "hipster", Title = "preference_hipster" },
            new Preference { Category = (Preference.Categories)4, Code = "preppy", Title = "preference_preppy" },
            new Preference { Category = (Preference.Categories)4, Code = "elegant", Title = "preference_elegant" }
        };                              

        private static readonly Career[] Careers = new[]
        {
            new Career { Code = "actor", Title = "career_actor" },
            new Career { Code = "aqualunger", Title = "career_aqualunger" },
            new Career { Code = "astronaut", Title = "career_astronaut" },
            new Career { Code = "barista", Title = "career_barista" },
            new Career { Code = "buildingEngineer", Title = "career_buildingEngineer" },
            new Career { Code = "business", Title = "career_business" },
            new Career { Code = "chief", Title = "career_chief" },
            new Career { Code = "criminal", Title = "career_criminal" },
            new Career { Code = "critic", Title = "career_critic" },
            new Career { Code = "designer", Title = "career_designer" },
            new Career { Code = "detective", Title = "career_detective" },
            new Career { Code = "doctor", Title = "career_doctor" },
            new Career { Code = "ecologist", Title = "career_ecologist" },
            new Career { Code = "engineer", Title = "career_engineer" },
            new Career { Code = "fashion", Title = "career_fashion" },
            new Career { Code = "fisherman", Title = "career_fisherman" },
            new Career { Code = "freelancerCreator", Title = "career_freelancerCreator" },
            new Career { Code = "freelancerGhostbuster", Title = "career_freelancerGhostbuster" },
            new Career { Code = "freelancerPainter", Title = "career_freelancerPainter" },
            new Career { Code = "freelancerPhotographer", Title = "career_freelancerPhotographer" },
            new Career { Code = "freelancerProgrammer", Title = "career_freelancerProgrammer" },
            new Career { Code = "freelancerWriter", Title = "career_freelancerWriter" },
            new Career { Code = "gardener", Title = "career_gardener" },
            new Career { Code = "handyman", Title = "career_handyman" },
            new Career { Code = "lawyer", Title = "career_lawyer" },
            new Career { Code = "military", Title = "career_military" },
            new Career { Code = "nanny", Title = "career_nanny" },
            new Career { Code = "painter", Title = "career_painter" },
            new Career { Code = "performer", Title = "career_performer" },
            new Career { Code = "politician", Title = "career_politician" },
            new Career { Code = "programmer", Title = "career_programmer" },
            new Career { Code = "rescuer", Title = "career_rescuer" },
            new Career { Code = "sarariman", Title = "career_sarariman" },
            new Career { Code = "scientist", Title = "career_scientist" },
            new Career { Code = "secretAgent", Title = "career_secretAgent" },
            new Career { Code = "salesman", Title = "career_salesman" },
            new Career { Code = "simfluencer", Title = "career_simfluencer" },
            new Career { Code = "socialMedia", Title = "career_socialMedia" },
            new Career { Code = "sport", Title = "career_sport" },
            new Career { Code = "streamer", Title = "career_streamer" },
            new Career { Code = "teacher", Title = "career_teacher" },
            new Career { Code = "waiter", Title = "career_waiter" },
            new Career { Code = "writer", Title = "career_writer" }
        };

        private static readonly InheritanceLaw[] InheritanceLaws = new[]
        {
            new InheritanceLaw
            {
                Category = (InheritanceCategory)0, AllowsManualChoice = false, Code = "matriarchy", IsStrict = false, Title = "inheritance_matriarchy",
                Value = 1
            },
            new InheritanceLaw
            {
                Category = (InheritanceCategory)0, AllowsManualChoice = false, Code = "smatriarchy", IsStrict = true,
                Title = "inheritance_smatriarchy", Value = 1
            },
            new InheritanceLaw
            {
                Category = (InheritanceCategory)0, AllowsManualChoice = false, Code = "patriarchy", IsStrict = false, 
                Title = "inheritance_patriarchy", Value = 0
            },
            new InheritanceLaw
            {
                Category = (InheritanceCategory)0, AllowsManualChoice = false, Code = "spatriarchy", IsStrict = true,
                Title = "inheritance_spatriarchy", Value = 0
            },
            new InheritanceLaw
            {
                Category = (InheritanceCategory)0, AllowsManualChoice = true, Code = "equality", IsStrict = false, 
                Title = "inheritance_equality", Value = 2
            },
            new InheritanceLaw
            {
                Category = (InheritanceCategory)0, AllowsManualChoice = false, Code = "sequality", IsStrict = true,
                Title = "inheritance_sequality", Value = 2
            },
            new InheritanceLaw
            {
                Category = (InheritanceCategory)1, AllowsManualChoice = false, Code = "straditional", IsStrict = true,
                Title = "inheritance_straditional", Value = 0
            },
            new InheritanceLaw
            {
                Category = (InheritanceCategory)1, AllowsManualChoice = false, Code = "traditional", IsStrict = false, 
                Title = "inheritance_traditional", Value = 0
            },
            new InheritanceLaw
            {
                Category = (InheritanceCategory)1, AllowsManualChoice = true, Code = "modern", IsStrict = false,
                Title = "inheritance_modern", Value = 2
            },
            new InheritanceLaw
            {
                Category = (InheritanceCategory)1, AllowsManualChoice = false, Code = "foster", IsStrict = false,
                Title = "inheritance_foster", Value = 1
            },
            new InheritanceLaw
            {
                Category = (InheritanceCategory)1, AllowsManualChoice = false, Code = "sfoster", IsStrict = true,
                Title = "inheritance_sfoster", Value = 1
            },
            new InheritanceLaw
            {
                Category = (InheritanceCategory)2, AllowsManualChoice = false, Code = "firstborn", IsStrict = true,
                Title = "inheritance_firstborn", Value = 0
            },
            new InheritanceLaw
            {
                Category = (InheritanceCategory)2, AllowsManualChoice = false, Code = "lastborn", IsStrict = true,
                Title = "inheritance_lastborn", Value = 1
            },
            new InheritanceLaw
            {
                Category = (InheritanceCategory)2, AllowsManualChoice = true, Code = "livingwill", IsStrict = true,
                Title = "inheritance_livingwill", Value = 2
            },
            new InheritanceLaw
            {
                Category = (InheritanceCategory)2, AllowsManualChoice = true, Code = "merit", IsStrict = true,
                Title = "inheritance_merit", Value = 2
            },
            new InheritanceLaw
            {
                Category = (InheritanceCategory)2, AllowsManualChoice = true, Code = "strength", IsStrict = true,
                Title = "inheritance_strength", Value = 2
            },
            new InheritanceLaw
            {
                Category = (InheritanceCategory)2, AllowsManualChoice = false, Code = "random", IsStrict = true,
                Title = "inheritance_random", Value = 2
            },
            new InheritanceLaw
            {
                Category = (InheritanceCategory)2, AllowsManualChoice = true, Code = "exemplar", IsStrict = true,
                Title = "inheritance_exemplar", Value = 2
            },
            new InheritanceLaw
            {
                Category = (InheritanceCategory)2, AllowsManualChoice = true, Code = "democracy", IsStrict = true,
                Title = "inheritance_democracy", Value = 2
            },
            new InheritanceLaw
            {
                Category = (InheritanceCategory)2, AllowsManualChoice = true, Code = "magicalbloodline", IsStrict = true,
                Title = "inheritance_magicalbloodline", Value = 2
            },
            new InheritanceLaw
            {
                Category = (InheritanceCategory)2, AllowsManualChoice = true, Code = "magicalstrength", IsStrict = true,
                Title = "inheritance_magicalstrength", Value = 2
            },
            new InheritanceLaw
            {
                Category = (InheritanceCategory)3, AllowsManualChoice = true, Code = "xenoarchy", IsStrict = false,
                Title = "inheritance_xenoarchy", Value = 2
            },
            new InheritanceLaw
            {
                Category = (InheritanceCategory)3, AllowsManualChoice = true, Code = "xenophobic", IsStrict = false,
                Title = "inheritance_xenophobic", Value = 2
            },
            new InheritanceLaw
            {
                Category = (InheritanceCategory)3, AllowsManualChoice = true, Code = "brood", IsStrict = false,
                Title = "inheritance_brood", Value = 2
            },
            new InheritanceLaw
            {
                Category = (InheritanceCategory)3, AllowsManualChoice = true, Code = "tolerant", IsStrict = false,
                Title = "inheritance_tolerant", Value = 2
            },
        };

        private static readonly Msg[] Messages = new[] {
            new Msg {Code = "goal_academician", MsgRu = "Академик", MsgEn = "Academic"},
            new Msg {Code = "goal_actor", MsgRu = "Опытный актер", MsgEn = "Master Actor"},
            new Msg {Code = "goal_adviser", MsgRu = "Надежный сосед", MsgEn = "Neighborhood Confidante"},
            new Msg {Code = "goal_animator", MsgRu = "Душа компании", MsgEn = "Party Animal"},
            new Msg {Code = "goal_archeologist", MsgRu = "Специалист по археологии", MsgEn = "Archaeology Scholar"},
            new Msg {Code = "goal_artistic", MsgRu = "Творческое дарование", MsgEn = "Artistic Prodigy"},
            new Msg {Code = "goal_athletic", MsgRu = "Непослушный проказник", MsgEn = "Rambunctious Scamp"},
            new Msg {Code = "goal_baron", MsgRu = "Барон", MsgEn = "Mansion Baron"},
            new Msg {Code = "goal_bartender", MsgRu = "Лучший бармен", MsgEn = "Master Mixologist"},
            new Msg {Code = "goal_camping", MsgRu = "Любитель свежего воздуха", MsgEn = "Outdoor Enthusiast"},
            new Msg {Code = "goal_caretaker", MsgRu = "Сельский смотритель", MsgEn = "Country Caretaker"},
            new Msg {Code = "goal_celebrity", MsgRu = "Мировая знаменитость", MsgEn = "World-Famous Celebrity"},
            new Msg {Code = "goal_cheese", MsgRu = "Жареный сыр", MsgEn = "Grilled Cheese"},
            new Msg {Code = "goal_chief", MsgRu = "Шеф-повар", MsgEn = "Master Chef"},
            new Msg {Code = "goal_citizen", MsgRu = "Коренной горожанин", MsgEn = "City Native"},
            new Msg {Code = "goal_clan", MsgRu = "Семья вампиров", MsgEn = "Vampire Family"},
            new Msg {Code = "goal_cleanliness", MsgRu = "Идеальная чистота", MsgEn = "Perfectly Pristine"},
            new Msg {Code = "goal_collector", MsgRu = "Куратор", MsgEn = "The Curator"},
            new Msg {Code = "goal_comedian", MsgRu = "Популярный шутник", MsgEn = "Joke Star"},
            new Msg {Code = "goal_craft", MsgRu = "Искусный мастер", MsgEn = "Master Maker"},
            new Msg {Code = "goal_dirty", MsgRu = "Невероятная грязь", MsgEn = "Fabulously Filthy"},
            new Msg {Code = "goal_econovator", MsgRu = "Эконоватор", MsgEn = "Eco Innovator"},
            new Msg {Code = "goal_evil", MsgRu = "Враг народа", MsgEn = "Public Enemy"},
            new Msg {Code = "goal_extreme", MsgRu = "Любитель экстрима", MsgEn = "Extreme Sports Enthusiast"},
            new Msg {Code = "goal_family", MsgRu = "Счастливая семья", MsgEn = "Big Happy Family"},
            new Msg {Code = "goal_fisherman", MsgRu = "Рыбак-ас", MsgEn = "Angling Ace"},
            new Msg {Code = "goal_friend", MsgRu = "Мировой друг", MsgEn = "Friend of the World"},
            new Msg {Code = "goal_friendly", MsgRu = "Светский львенок", MsgEn = "Social Butterfly"},
            new Msg {Code = "goal_gardener", MsgRu = "Независимый ботаник", MsgEn = "Freelance Botanist"},
            new Msg {Code = "goal_goodVampire", MsgRu = "Хороший вампир", MsgEn = "Good Vampire"},
            new Msg {Code = "goal_heartbreaker", MsgRu = "Подлый партнер", MsgEn = "Villainous Valentine"},
            new Msg {Code = "goal_heredity", MsgRu = "Хорошая наследственность", MsgEn = "Successful Lineage"},
            new Msg {Code = "goal_island", MsgRu = "Пляжная жизнь", MsgEn = "Beach Life"},
            new Msg {Code = "goal_jungle", MsgRu = "Исследователь джунглей", MsgEn = "Jungle Explorer"},
            new Msg {Code = "goal_knitting", MsgRu = "Повелитель вязания", MsgEn = "Lord of the Knits"},
            new Msg {Code = "goal_leader", MsgRu = "Главарь", MsgEn = "Leader of the Pack"},
            new Msg {Code = "goal_love", MsgRu = "Родственная душа", MsgEn = "Soulmate"},
            new Msg {Code = "goal_mechanic", MsgRu = "Мозговитый чудак", MsgEn = "Nerd Brain"},
            new Msg {Code = "goal_mischief", MsgRu = "Большой бедокур", MsgEn = "Chief of Mischief"},
            new Msg {Code = "goal_mountain", MsgRu = "Турист на горе Комореби", MsgEn = "Mt. Komorebi Sightseer"},
            new Msg {Code = "goal_multitasking", MsgRu = "Человек эпохи Возрождения", MsgEn = "Renaissance Sim"},
            new Msg {Code = "goal_musician", MsgRu = "Музыкальный талант", MsgEn = "Musical Genius"},
            new Msg {Code = "goal_painter", MsgRu = "Исключительный художник", MsgEn = "Painter"},
            new Msg {Code = "goal_parent", MsgRu = "Супер-родитель", MsgEn = "Super Parent"},
            new Msg {Code = "goal_peaceOfMind", MsgRu = "Душевное равновесие", MsgEn = "Inner Peace"},
            new Msg {Code = "goal_pets", MsgRu = "Друг животных", MsgEn = "Friend of the Animals"},
            new Msg {Code = "goal_potions", MsgRu = "Творец зелий", MsgEn = "Purveyor of Potions"},
            new Msg {Code = "goal_prodigy", MsgRu = "Вундеркинд", MsgEn = "Whiz Kid"},
            new Msg {Code = "goal_programmer", MsgRu = "Компьютерный гений", MsgEn = "Computer Whiz"},
            new Msg {Code = "goal_rich", MsgRu = "Сказочное богатство", MsgEn = "Fabulously Wealthy"},
            new Msg {Code = "goal_secret", MsgRu = "Тайна Стрейнджервиля", MsgEn = "StrangerVille Mystery"},
            new Msg {Code = "goal_slut", MsgRu = "Серийный романтик", MsgEn = "Serial Romantic"},
            new Msg {Code = "goal_spaWorker", MsgRu = "Специалист сферы заботы о себе", MsgEn = "Self-Care Specialist"},
            new Msg {Code = "goal_sport", MsgRu = "Культурист", MsgEn = "Bodybuilder"},
            new Msg {Code = "goal_vampireMaster", MsgRu = "Повелитель вампиров", MsgEn = "Master Vampire"},
            new Msg {Code = "goal_wizard", MsgRu = "Чародейство", MsgEn = "Spellcraft & Sorcery"},
            new Msg {Code = "goal_writer", MsgRu = "Популярный автор", MsgEn = "Bestselling Author"},
            new Msg {Code = "goal_zen", MsgRu = "Учитель дзен", MsgEn = "Zen Guru"},
            new Msg {Code = "goal_werewolf", MsgRu = "Посвящение в оборотни", MsgEn = "Werewolf Initiate"},
            new Msg {Code = "goal_werewolfLone", MsgRu = "Одинокий волк", MsgEn = "Lone Wolf"},
            new Msg {Code = "goal_werewolfCollective", MsgRu = "Представитель Общины", MsgEn = "Emissary of the Collective"},
            new Msg {Code = "goal_werewolfWildfang", MsgRu = "Ренегат Клыков", MsgEn = "Wildfang Renegade"},
            new Msg {Code = "goal_werewolfSeeker", MsgRu = "Искатель лекарства", MsgEn = "Cure Seeker"},
            new Msg {Code = "goal_partyGoer", MsgRu = "Разгульная жизнь", MsgEn = "Live Fast"},
            new Msg {Code = "goal_legend", MsgRu = "Легенда при жизни", MsgEn = "Admired Icon"},
            new Msg {Code = "goal_drama", MsgRu = "Лама с драмой", MsgEn = "Drama Llama"},
            new Msg {Code = "goal_goal", MsgRu = "Вижу цель, не вижу препятствий", MsgEn = "Goal Oriented"},
            new Msg {Code = "goal_batuu", MsgRu = "Надежда или Порядок", MsgEn = "Hope VS Order"},
            new Msg {Code = "goal_batuuHope", MsgRu = "Символ Надежды", MsgEn = "Paragon of Hope"},
            new Msg {Code = "goal_batuuOrder", MsgRu = "Силовик Ордена", MsgEn = "Enforcer of Order"},
            new Msg {Code = "goal_batuuPrivateer", MsgRu = "Галактический приватир", MsgEn = "Galactic Privateer"},
            new Msg {Code = "preference_actor", MsgRu = "Актёрское мастерство", MsgEn = "Acting"},
            new Msg {Code = "preference_alternative", MsgRu = "Альтернативная музыка", MsgEn = "Alternative"},
            new Msg {Code = "preference_americana", MsgRu = "Американа", MsgEn = "Americana"},
            new Msg {Code = "preference_artSong", MsgRu = "Авторская песня", MsgEn = "Singer Songwriter"},
            new Msg {Code = "preference_baking", MsgRu = "Выпечка", MsgEn = "Baking"},
            new Msg {Code = "preference_baroque", MsgRu = "Музыка эпохи барокко", MsgEn = "Baroque"},
            new Msg {Code = "preference_basics", MsgRu = "Минимализм", MsgEn = "Basics"},
            new Msg {Code = "preference_black", MsgRu = "Черный", MsgEn = "Black"},
            new Msg {Code = "preference_blue", MsgRu = "Синий", MsgEn = "Blue"},
            new Msg {Code = "preference_blues", MsgRu = "Блюз", MsgEn = "Blues"},
            new Msg {Code = "preference_boho", MsgRu = "Богемный стиль", MsgEn = "Boho"},
            new Msg {Code = "preference_bowling", MsgRu = "Боулинг", MsgEn = "Bowling"},
            new Msg {Code = "preference_brown", MsgRu = "Коричневый", MsgEn = "Brown"},
            new Msg {Code = "preference_classical", MsgRu = "Классическая музыка", MsgEn = "Classical"},
            new Msg {Code = "preference_comedy", MsgRu = "Комедия", MsgEn = "Comedy"},
            new Msg {Code = "preference_concentration", MsgRu = "Музыка для концентрации внимания", MsgEn = "Focus"},
            new Msg {Code = "preference_conceptual", MsgRu = "Концептуальный загородный стиль", MsgEn = "Suburban Contemporary"},
            new Msg {Code = "preference_contemporary", MsgRu = "Современный стиль", MsgEn = "Contemporary"},
            new Msg {Code = "preference_cooking", MsgRu = "Кулинария", MsgEn = "Cooking"},
            new Msg {Code = "preference_cosmolux", MsgRu = "Космолюкс", MsgEn = "Cosmolux"},
            new Msg {Code = "preference_cross-stitch", MsgRu = "Вышивание крестиком", MsgEn = "Cross-Stitch"},
            new Msg {Code = "preference_dancing", MsgRu = "Танцы", MsgEn = "Dancing"},
            new Msg {Code = "preference_djMusic", MsgRu = "Музыка из будки ди-джея", MsgEn = "DJ Booth"},
            new Msg {Code = "preference_dj", MsgRu = "Мастерство ди-джея", MsgEn = "DJ Mixing"},
            new Msg {Code = "preference_electronica", MsgRu = "Электронная музыка", MsgEn = "Electronica"},
            new Msg {Code = "preference_fairyTale", MsgRu = "Сказочный стиль", MsgEn = "Storybook"},
            new Msg {Code = "preference_fishing", MsgRu = "Рыбная ловля", MsgEn = "Fishing"},
            new Msg {Code = "preference_fitness", MsgRu = "Фитнес", MsgEn = "Fitness"},
            new Msg {Code = "preference_french", MsgRu = "Французская провинция", MsgEn = "French Country"},
            new Msg {Code = "preference_gaming", MsgRu = "Видеоигры", MsgEn = "Video Gaming"},
            new Msg {Code = "preference_garden", MsgRu = "Садовая тематика", MsgEn = "Garden"},
            new Msg {Code = "preference_gardening", MsgRu = "Садоводство", MsgEn = "Gardening"},
            new Msg {Code = "preference_gothic", MsgRu = "Фермерский стиль", MsgEn = "Gothic Farmhouse"},
            new Msg {Code = "preference_gray", MsgRu = "Серый", MsgEn = "Gray"},
            new Msg {Code = "preference_green", MsgRu = "Зеленый", MsgEn = "Green"},
            new Msg {Code = "preference_guitar", MsgRu = "Игра на гитаре", MsgEn = "Guitar"},
            new Msg {Code = "preference_handiness", MsgRu = "Механика", MsgEn = "Handiness"},
            new Msg {Code = "preference_hip-hop", MsgRu = "Хип-хоп", MsgEn = "Hip Hop"},
            new Msg {Code = "preference_islandMusic", MsgRu = "Музыка с острова", MsgEn = "Island"},
            new Msg {Code = "preference_island", MsgRu = "Островная тематика", MsgEn = "Island"},
            new Msg {Code = "preference_japanese", MsgRu = "Японская народная музыка", MsgEn = "Japanese Folk"},
            new Msg {Code = "preference_jazz", MsgRu = "Джаз", MsgEn = "Jazz"},
            new Msg {Code = "preference_kidsPop", MsgRu = "Поп-музыка для детей", MsgEn = "Tween Pop"},
            new Msg {Code = "preference_kids", MsgRu = "Музыка для детей", MsgEn = "Kids Radio"},
            new Msg {Code = "preference_knitting", MsgRu = "Вязание", MsgEn = "Knitting"},
            new Msg {Code = "preference_latinDance", MsgRu = "Латиноамериканская поп-музыка", MsgEn = "Latin Pop"},
            new Msg {Code = "preference_latino", MsgRu = "Латино", MsgEn = "Latin"},
            new Msg {Code = "preference_light", MsgRu = "Лёгкая музыка", MsgEn = "Easy Listening"},
            new Msg {Code = "preference_lullabies", MsgRu = "Колыбельные", MsgEn = "Lullabies"},
            new Msg {Code = "preference_mediaProduction", MsgRu = "Медиапроизводство", MsgEn = "Media Production"},
            new Msg {Code = "preference_metal", MsgRu = "МУЗЫКА В ЖАНРЕ МЕТАЛЛ", MsgEn = "Metal"},
            new Msg {Code = "preference_midCentury", MsgRu = "Модерн 50х", MsgEn = "Mid Century"},
            new Msg {Code = "preference_mischief", MsgRu = "Пакости", MsgEn = "Mischief"},
            new Msg {Code = "preference_mission", MsgRu = "Строгий стиль", MsgEn = "Mission"},
            new Msg {Code = "preference_mixology", MsgRu = "Смешивание напитков", MsgEn = "Mixology"},
            new Msg {Code = "preference_modern", MsgRu = "Стиль модерн", MsgEn = "Modern"},
            new Msg {Code = "preference_newAge", MsgRu = "Музыка в жанре «Новая волна»", MsgEn = "New Age"},
            new Msg {Code = "preference_nuDisco", MsgRu = "Музыка ню-диско", MsgEn = "NuDisco"},
            new Msg {Code = "preference_orange", MsgRu = "Оранжевый", MsgEn = "Orange"},
            new Msg {Code = "preference_painting", MsgRu = "Рисование", MsgEn = "Painting"},
            new Msg {Code = "preference_patio", MsgRu = "Декор террас", MsgEn = "Patio"},
            new Msg {Code = "preference_photography", MsgRu = "Фотография", MsgEn = "Photography"},
            new Msg {Code = "preference_piano", MsgRu = "Игра на пианино", MsgEn = "Piano"},
            new Msg {Code = "preference_pink", MsgRu = "Розовый", MsgEn = "Pink"},
            new Msg {Code = "preference_pipeOrgan", MsgRu = "Игра на органе", MsgEn = "Pipe Organ"},
            new Msg {Code = "preference_pop", MsgRu = "Поп-музыка", MsgEn = "Pop"},
            new Msg {Code = "preference_programming", MsgRu = "Программирование", MsgEn = "Programming"},
            new Msg {Code = "preference_queen", MsgRu = "Стиль королевы Анны", MsgEn = "Queen Anne"},
            new Msg {Code = "preference_red", MsgRu = "Красный", MsgEn = "Red"},
            new Msg {Code = "preference_research-debate", MsgRu = "Научные исследования и дебаты", MsgEn = "Research and Debate"},
            new Msg {Code = "preference_retro", MsgRu = "Музыка в жанре ретро", MsgEn = "Retro"},
            new Msg {Code = "preference_robotics", MsgRu = "Робототехника", MsgEn = "Robotics"},
            new Msg {Code = "preference_rockClimbing", MsgRu = "Скалолазание", MsgEn = "Rock Climbing"},
            new Msg {Code = "preference_rocket", MsgRu = "Ракетостроение", MsgEn = "Rocket Science"},
            new Msg {Code = "preference_romance", MsgRu = "Романтическая музыка", MsgEn = "Romance"},
            new Msg {Code = "preference_rustic", MsgRu = "Сельская эстетика", MsgEn = "Cottagecore"},
            new Msg {Code = "preference_singing", MsgRu = "Пение", MsgEn = "Singing"},
            new Msg {Code = "preference_skiing", MsgRu = "Катание на лыжах", MsgEn = "Skiing"},
            new Msg {Code = "preference_snowboarding", MsgRu = "Катание на сноуборде", MsgEn = "Snowboarding"},
            new Msg {Code = "preference_spooky", MsgRu = "Жуткая музыка", MsgEn = "Spooky"},
            new Msg {Code = "preference_s-pop", MsgRu = "С-поп", MsgEn = "S-Pop"},
            new Msg {Code = "preference_strangeTunes", MsgRu = "Странные мелодии", MsgEn = "Strange Tunes"},
            new Msg {Code = "preference_summer", MsgRu = "Летняя музыка", MsgEn = "Summer Strut"},
            new Msg {Code = "preference_violet", MsgRu = "Фиолетовый", MsgEn = "Violet"},
            new Msg {Code = "preference_violin", MsgRu = "Игра на скрипке", MsgEn = "Violin"},
            new Msg {Code = "preference_wellness", MsgRu = "Здоровый образ жизни", MsgEn = "Wellness"},
            new Msg {Code = "preference_white", MsgRu = "Белый", MsgEn = "White"},
            new Msg {Code = "preference_winter", MsgRu = "Рождественская музыка", MsgEn = "Winter Holiday"},
            new Msg {Code = "preference_world", MsgRu = "Музыка мира", MsgEn = "World"},
            new Msg {Code = "preference_writing", MsgRu = "Писательство", MsgEn = "Writing"},
            new Msg {Code = "preference_yard", MsgRu = "Дворовая музыка", MsgEn = "Backyard"},
            new Msg {Code = "preference_yellow", MsgRu = "Желтый", MsgEn = "Yellow"},
            new Msg {Code = "preference_base", MsgRu = "Базовая одежда", MsgEn = "Basics"},
            new Msg {Code = "preference_bohema", MsgRu = "Богемная одежда", MsgEn = "Boho"},
            new Msg {Code = "preference_active", MsgRu = "Одежда для активного отдыха вне дома", MsgEn = "Outdoorsy"},
            new Msg {Code = "preference_rock", MsgRu = "Рокерская одежда", MsgEn = "Rocker"},
            new Msg {Code = "preference_village", MsgRu = "Сельская одежда", MsgEn = "Country"},
            new Msg {Code = "preference_streetstyle", MsgRu = "Уличная одежда", MsgEn = "Streetwear"},
            new Msg {Code = "preference_hipster", MsgRu = "Хипстерская одежда", MsgEn = "Hipster"},
            new Msg {Code = "preference_preppy", MsgRu = "Школьная одежда", MsgEn = "Preppy"},
            new Msg {Code = "preference_elegant", MsgRu = "Элегантная одежда", MsgEn = "Polished"},
            new Msg {Code = "career_actor", MsgRu = "Актер", MsgEn = "Actor"},
            new Msg {Code = "career_aqualunger", MsgRu = "Водолаз", MsgEn = "Diver"},
            new Msg {Code = "career_astronaut", MsgRu = "Космонавт", MsgEn = "Astronaut"},
            new Msg {Code = "career_barista", MsgRu = "Бармен", MsgEn = "Barista"},
            new Msg {Code = "career_buildingEngineer", MsgRu = "Строительный инженер", MsgEn = "Civil Designer"},
            new Msg {Code = "career_business", MsgRu = "Бизнесмен", MsgEn = "Business"},
            new Msg {Code = "career_chief", MsgRu = "Кулинар", MsgEn = "Culinary"},
            new Msg {Code = "career_criminal", MsgRu = "Преступник", MsgEn = "Criminal"},
            new Msg {Code = "career_critic", MsgRu = "Критик", MsgEn = "Critic"},
            new Msg {Code = "career_designer", MsgRu = "", MsgEn = "Interior Designer"},
            new Msg {Code = "career_detective", MsgRu = "Детектив", MsgEn = "Detective"},
            new Msg {Code = "career_doctor", MsgRu = "Доктор", MsgEn = "Doctor"},
            new Msg {Code = "career_ecologist", MsgRu = "Эколог", MsgEn = "Conservationist"},
            new Msg {Code = "career_engineer", MsgRu = "Инженер", MsgEn = "Engineer"},
            new Msg {Code = "career_fashion", MsgRu = "Законодатель стиля", MsgEn = "Style Influencer"},
            new Msg {Code = "career_fisherman", MsgRu = "Рыболов", MsgEn = "Fisherman"},
            new Msg {Code = "career_freelancerCreator", MsgRu = "Фрилансер: Внештатный производитель", MsgEn = "Freelancer Fabricator"},
            new Msg {Code = "career_freelancerGhostbuster", MsgRu = "Фрилансер: Паранормальный сыщик", MsgEn = "Freelancer Paranormal Investigator"},
            new Msg {Code = "career_freelancerPainter", MsgRu = "Фрилансер: Графический дизайнер", MsgEn = "Freelancer Artist"},
            new Msg {Code = "career_freelancerPhotographer", MsgRu = "Фрилансер: Модный фотограф", MsgEn = "Freelancer Fashion Photographer"},
            new Msg {Code = "career_freelancerProgrammer", MsgRu = "Фрилансер: Внештатный программист", MsgEn = "Freelancer Programmer"},
            new Msg {Code = "career_freelancerWriter", MsgRu = "Фрилансер: Внештатный корреспондент", MsgEn = "Freelancer Writer"},
            new Msg {Code = "career_gardener", MsgRu = "Садовод", MsgEn = "Gardener"},
            new Msg {Code = "career_handyman", MsgRu = "Разнорабочий", MsgEn = "Manual Laborer"},
            new Msg {Code = "career_lawyer", MsgRu = "Юрист", MsgEn = "Law"},
            new Msg {Code = "career_military", MsgRu = "Военный служащий", MsgEn = "Military"},
            new Msg {Code = "career_nanny", MsgRu = "Няня", MsgEn = "Babysitter"},
            new Msg {Code = "career_painter", MsgRu = "Художник", MsgEn = "Painter"},
            new Msg {Code = "career_performer", MsgRu = "Исполнитель", MsgEn = "Entertainer"},
            new Msg {Code = "career_politician", MsgRu = "Политик", MsgEn = "Politician"},
            new Msg {Code = "career_programmer", MsgRu = "Технический специалист", MsgEn = "Tech Guru"},
            new Msg {Code = "career_rescuer", MsgRu = "Спасатель", MsgEn = "Lifeguard"},
            new Msg {Code = "career_sarariman", MsgRu = "Сарариман", MsgEn = "Salaryperson"},
            new Msg {Code = "career_scientist", MsgRu = "Ученый", MsgEn = "Scientist"},
            new Msg {Code = "career_secretAgent", MsgRu = "Тайный агент", MsgEn = "Secret Agent"},
            new Msg {Code = "career_salesman", MsgRu = "Продавец", MsgEn = "Retail Employee"},
            new Msg {Code = "career_simfluencer", MsgRu = "Симфлюэнсер", MsgEn = "Simfluencer"},
            new Msg {Code = "career_socialMedia", MsgRu = "Социальные сети", MsgEn = "Social Media"},
            new Msg {Code = "career_sport", MsgRu = "Спортсмен", MsgEn = "Athlete"},
            new Msg {Code = "career_streamer", MsgRu = "Игровой стример", MsgEn = "Video Game Streamer"},
            new Msg {Code = "career_teacher", MsgRu = "Преподаватель", MsgEn = "Education"},
            new Msg {Code = "career_waiter", MsgRu = "Официант", MsgEn = "Fast Food Employee"},
            new Msg {Code = "career_writer", MsgRu = "Писатель", MsgEn = "Writer"},
            new Msg {Code = "inheritance_matriarchy", MsgRu = "Матриархат", MsgEn = "Matriarchy"},
            new Msg {Code = "inheritance_smatriarchy", MsgRu = "Строгий матриархат", MsgEn = "Strict matriarchy"},
            new Msg {Code = "inheritance_patriarchy", MsgRu = "Патриархат", MsgEn = "Patriarchy"},
            new Msg {Code = "inheritance_spatriarchy", MsgRu = "Строгий патриархат", MsgEn = "Strict patriarchy"},
            new Msg {Code = "inheritance_equality", MsgRu = "Равенство", MsgEn = "Equality"},
            new Msg {Code = "inheritance_sequality", MsgRu = "Строгое равенство", MsgEn = "Strict equality"},
            new Msg {Code = "inheritance_straditional", MsgRu = "Строгий традиционный", MsgEn = "Strict traditional"},
            new Msg {Code = "inheritance_traditional", MsgRu = "Традиционный", MsgEn = "Traditional"},
            new Msg {Code = "inheritance_modern", MsgRu = "Современный", MsgEn = "Modern"},
            new Msg {Code = "inheritance_foster", MsgRu = "Опекунский", MsgEn = "Foster"},
            new Msg {Code = "inheritance_sfoster", MsgRu = "Строгий опекунский", MsgEn = "Strict foster"},
            new Msg {Code = "inheritance_firstborn", MsgRu = "Перворожденный", MsgEn = "First Born"},
            new Msg {Code = "inheritance_lastborn", MsgRu = "Молодость", MsgEn = "Last Born"},
            new Msg {Code = "inheritance_livingwill", MsgRu = "Последняя воля", MsgEn = "Living Will"},
            new Msg {Code = "inheritance_merit", MsgRu = "По заслугам", MsgEn = "Merit"},
            new Msg {Code = "inheritance_strength", MsgRu = "Сила", MsgEn = "Strength"},
            new Msg {Code = "inheritance_random", MsgRu = "Случайность", MsgEn = "Random"},
            new Msg {Code = "inheritance_exemplar", MsgRu = "Семейная черта", MsgEn = "Exemplar"},
            new Msg {Code = "inheritance_democracy", MsgRu = "Демократический", MsgEn = "Democracy"},
            new Msg {Code = "inheritance_magicalbloodline", MsgRu = "Магический род", MsgEn = "Magical Bloodline"},
            new Msg {Code = "inheritance_magicalstrength", MsgRu = "Магическая сила", MsgEn = "Magical Strength"},
            new Msg {Code = "inheritance_xenoarchy", MsgRu = "Ксеноархат", MsgEn = "Xenoarchy"},
            new Msg {Code = "inheritance_xenophobic", MsgRu = "Ксенофобия", MsgEn = "Xenophobic"},
            new Msg {Code = "inheritance_brood", MsgRu = "Выведение", MsgEn = "Brood"},
            new Msg {Code = "inheritance_tolerant", MsgRu = "Толерантность", MsgEn = "Tolerant"},
            new Msg {Code = "inheritanceCategory_Gender", MsgRu = "Гендерный принцип", MsgEn = "Gender law"},
            new Msg {Code = "inheritanceCategory_Bloodline", MsgRu = "Принцип чистокровности", MsgEn = "Bloodline law"},
            new Msg {Code = "inheritanceCategory_Heir", MsgRu = "Принцип выбора", MsgEn = "Heir law"},
            new Msg {Code = "inheritanceCategory_Species", MsgRu = "Расовый принцип", MsgEn = "Species law"},
            new Msg {Code = "age_Baby", MsgRu = "Младенец", MsgEn = "Baby"},
            new Msg {Code = "age_Toddler", MsgRu = "Тоддлер", MsgEn = "Toddler"},
            new Msg {Code = "age_Child", MsgRu = "Ребенок", MsgEn = "Child"},
            new Msg {Code = "age_Teen", MsgRu = "Подросток", MsgEn = "Teen"},
            new Msg {Code = "age_Young", MsgRu = "Молодой", MsgEn = "Young"},
            new Msg {Code = "age_Adult", MsgRu = "Взрослый", MsgEn = "Adult"},
            new Msg {Code = "age_Old", MsgRu = "Пожилой", MsgEn = "Old"},
            new Msg {Code = "chronotype_MorningPerson", MsgRu = "Жаворонок", MsgEn = "Morning Person"},
            new Msg {Code = "chronotype_NightPerson", MsgRu = "Сова", MsgEn = "Night Owl"},
            new Msg {Code = "gender_Male", MsgRu = "Мужчина", MsgEn = "Male"},
            new Msg {Code = "gender_Female", MsgRu = "Женщина", MsgEn = "Female"},
            new Msg {Code = "err_emailNotConfirmed", 
                    MsgRu = "Пожалуйста, подтвердите свой email", 
                    MsgEn = "You should confirm your email first"},
            new Msg {Code = "err_incorrectPass", 
                    MsgRu = "Неверный логин или пароль", 
                    MsgEn = "Incorrect login or password"},
            new Msg {Code = "registrationSubject", 
                    MsgRu = "MjauriziaSims: Регистрация", 
                    MsgEn = "Registration at MjauriziaSims"},
            new Msg {Code = "registrationText", 
                    MsgRu = "Пройдите по ссылке, чтобы подтвердить свой email:\n <url>", 
                    MsgEn = "To confirm your email follow the link:\n <url>"},
            new Msg {Code = "err_registration", 
                    MsgRu = "Пользователь с таким логином уже существует", 
                    MsgEn = "User with this login already exists"},
            new Msg {Code = "err_recovery", 
                    MsgRu = "Не существует пользователя с таким email", 
                    MsgEn = "No user with such email was register"},
            new Msg {Code = "recoveryText", 
                    MsgRu = "Перейдите по ссылке, чтобы сбросить пароль: <url>", 
                    MsgEn = "Follow link to set new password:\n <url>"},
            new Msg {Code = "recoverySubject", MsgRu = "MjauriziaSims: восстановление пароля", MsgEn = "Reset password for MjauriziaSims"},
            new Msg {Code = "err_PassReset", 
                    MsgRu = "Неверная ссылка. Пожалуйста, попробуйте восстановить пароль еще раз.", 
                    MsgEn = "Your link is wrong. Please, try to reset password again."},
            new Msg {Code = "err_registrationEmail", 
                    MsgRu = "Пользователь с таким email уже существует. " +
                            "Если вы не помните свои данные для входа, воспользуйтесь восстановлением пароля.", 
                    MsgEn = "User with such email already exists. " +
                            "If you don't remember your login or password, please, use forgot password button"},
            new Msg {Code = "err_registrationGoogle", 
                    MsgRu = "Пользователь с таким email уже существует. " +
                            "Пожалуйста, авторизируйтесь через Google, чтобы войти в свой аккаунт.", 
                    MsgEn = "User with such email already exists. " +
                            "Please, use Google Authentication to enter your account."},
            new Msg {Code = "err_recoveryGoogle", 
                    MsgRu = "Пожалуйста, авторизируйтесь через Google, чтобы войти в свой аккаунт.", 
                    MsgEn = "Please, use Google Authentication to enter your account."},
        };

        public Migrator(
            IGoalRepository goalRepository,
            IPreferenceRepository preferenceRepository,
            ICareerRepository careerRepository,
            IInheritanceLawRepository inheritanceRepository,
            IMsgRepository msgRepository)
        {
            _goalRepository = goalRepository;
            _preferenceRepository = preferenceRepository;
            _careerRepository = careerRepository;
            _inheritanceRepository = inheritanceRepository;
            _msgRepository = msgRepository;
        }

        public void Migrate()
        {
            foreach (var goal in Goals)
            {
                if (_goalRepository.Goals.FirstOrDefault(g => g.Code == goal.Code) == null)
                {
                    _goalRepository.SaveGoal(goal);
                }
            }

            foreach (var preference in Preferences)
            {
                if (_preferenceRepository.Preferences.FirstOrDefault(p =>
                        p.Code == preference.Code && p.Category == preference.Category)
                    == null)
                {
                    _preferenceRepository.SavePreference(preference);
                }
            }

            foreach (var career in Careers)
            {
                if (_careerRepository.Careers.FirstOrDefault(c => c.Code == career.Code) == null)
                {
                    _careerRepository.SaveCareer(career);
                }
            }

            foreach (var law in InheritanceLaws)
            {
                if (_inheritanceRepository.InheritanceLaws.
                        FirstOrDefault(l => l.Category == law.Category && l.Code == law.Code)
                    == null)
                {
                    _inheritanceRepository.SaveInheritanceLaw(law);
                }
            }

            foreach (var msg in Messages)
            {
                if (_msgRepository.Messages.FirstOrDefault(g => g.Code == msg.Code) == null)
                {
                    _msgRepository.SaveMsg(msg);
                }
            }
        }
    }
}
