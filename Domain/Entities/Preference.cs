namespace Domain.Entities
{
    public class Preference
    {
        public enum Categories
        {
            Color,
            Music,
            Hobby,
            Decor,
            Clothes
        }
        public int PreferenceId { get; set; }

        public string Title { get; set; }

        public Categories Category { get; set; }
    }
}
