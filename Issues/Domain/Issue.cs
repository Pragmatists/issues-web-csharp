namespace Issues.Domain
{
    public class Issue
    {
        public Issue()
        {
        }

        public Issue(int id, string title)
        {
            ID = id;
            Title = title;
        }

        public string Title { get; set; }

        public int ID { get; set; }
    }
}