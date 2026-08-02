namespace TestTechniqueSerensia.Interface
{
    public interface IAmTheTest
    {
        IEnumerable<string> GetSuggestions(string term,IEnumerable<string> choices, int numberOfSuggestions);
        int GetDifferenceScore(string dest, string src);
    }
}
