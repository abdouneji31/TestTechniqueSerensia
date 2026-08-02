using TestTechniqueSerensia.Interface;

namespace TestTechniqueSerensia.Entities
{
    public class AmTheTest : IAmTheTest
    {
        public int GetDifferenceScore(string dest, string src)
        {
            if (string.IsNullOrEmpty(src))
                throw new ArgumentNullException(nameof(src));

            if (string.IsNullOrEmpty(dest))
                throw new ArgumentNullException(nameof(dest));

            // pas du tout similaire (pas assez de lettres));
            if (dest.Length > src.Length)
                return int.MaxValue; 

            var bestScore = int.MaxValue;

            for (int i = 0; i <= src.Length - dest.Length; i++)
            {
                int score = 0;

                for (int j = 0; j < dest.Length; j++)
                {
                    if (dest[j] != src[i+j])
                        score++;
                }
                if(score < bestScore)
                    bestScore = score;
            }

            return bestScore;
        }

        public IEnumerable<string> GetSuggestions(string term, IEnumerable<string> choices, int numberOfSuggestions)
        {
            var scores = new Dictionary<string, int>(); 

            if (choices == null || !choices.Any())
                return [];

            foreach(var choice in choices)
            {
               int score = GetDifferenceScore(term, choice);

                if (scores.ContainsKey(choice) || score == int.MaxValue)
                    continue;

                scores.Add(choice, score);
               
            }

            return scores.OrderBy(s => s.Value)
                         .ThenBy(s => Math.Abs(s.Key.Length - term.Length)) //  les termes les plus proche en longueur du terme recherché
                         .ThenBy(s => s.Key) // ordre alphabétique 
                         .Take(numberOfSuggestions)
                         .Select(s => s.Key);
        }
    }
}
