namespace Domain.Entities
{
    public enum PreferenceCategories
    {
        Color,
        Music,
        Hobby,
        Decor,
        Clothes,
        ConversationTopics,
        Characteristic
    }    
    public class Preference
    {
        public int PreferenceId { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public PreferenceCategories Category { get; set; }
        public Ages MinAge { get; set; }
        public int Pack { get; set; }
    }
}
