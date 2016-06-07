namespace Issues.Domain
{
    public class Issue
    {

        public string Title { get; set; }
        public int ID { get; set; }
        public IssueStatus Status { get; set; }

        public Issue()
        {
            Status = IssueStatus.OPEN;
        }

        public Issue(int id, string title) : this(title)
        {
            ID = id;
        }

        public Issue(string title) : this()
        {
            Title = title;
        }

        public void FixedIn(ProductVersion productVersion)
        {
        }

        public void Close()
        {
            Status = IssueStatus.CLOSED;
        }

        public class IssueStatus
        {
            public static IssueStatus OPEN = new OpenIssueStatus();
            public static IssueStatus CLOSED = new ClosedIssueStatus();
            public string Name { set; get; }

            protected IssueStatus(string name)
            {
                Name = name;
            }

            protected IssueStatus() { }

            internal class OpenIssueStatus : IssueStatus
            {
                public OpenIssueStatus() : base("OPEN")
                {
                }
            }

            internal class ClosedIssueStatus : IssueStatus
            {
                public ClosedIssueStatus() : base("CLOSED")
                {
                }
            }
        }
    }
}