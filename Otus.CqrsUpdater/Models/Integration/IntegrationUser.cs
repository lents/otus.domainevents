namespace Otus.CqrsUpdater.Models.Integration
{

    public class IntegrationDocument
    {

        public int Id { get; set; }

        public string Name { get; set; }


    }

    public class IntegrationUser
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<IntegrationDocument> Documents { get; set; }
    }
}
