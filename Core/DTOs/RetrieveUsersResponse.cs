namespace Core.DTOs
{
    public class RetrieveUsersResponse
    {
        public string name { get; set; }
        public string login { get; set; }
        public string company { get; set; }
        public int NoOfFollowers { get; set; }
        public int NoOfPublicRepo { get; set; }
        public int AverageNoOfFollowersPerRepo { get; set; }
    }
}
