namespace Domain.Entities
{
    public class Preference
    {
        public enum Categories
        {
            Color,
            Music,
            Hobby,
            Decor
        }
        public int PreferenceId { get; set; }

        public string Title { get; set; }

        public int Category { get; set; }
    }
}
