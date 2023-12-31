namespace PersnoLib;

public class PostalService
{
    private IPostalRepository _repository;
    private Random _rand = new Random();
    public List<string> PostalCodes { get; } = new List<string>
    {
        "12345",
        "67890",
        "23456",
        "78901",
    };

    public List<string> Towns { get; } = new List<string>
    {
        "SomeTown",
        "AnotherTown",
        "YetAnotherTown",
        "LastTown"
    };

    public PostalService()
    {
    
    }

    public string GetRandomPostalAndTown()
    {
        var randomPostal = PostalCodes[_rand.Next(PostalCodes.Count)];
        var randomTown = Towns[_rand.Next(Towns.Count)];

        return $"{randomPostal} - {randomTown}";
    }
}