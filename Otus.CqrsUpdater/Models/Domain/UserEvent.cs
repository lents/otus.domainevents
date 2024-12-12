namespace Otus.CqrsUpdater.Models.Domain
{
    public class UserEvent
    {
        public int Id { get; set; }

        public string FullName { get; set; }


        public List<Document> Documents { get; set; }
    }

    public class Document
    {

    }
}
