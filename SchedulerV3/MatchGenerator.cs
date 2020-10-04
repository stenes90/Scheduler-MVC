using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace SchedulerV3.Models
{
    public class MatchGenerator
    {
        private ApplicationDbContext _context;

        public MatchGenerator(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Match> GenerateMatches(Tournament tournament)
        {
            var classes = _context.Classes
                .Include(z => z.PlayingDates
                .Select(x => x.Courts))
                .Where(c => c.TournamentId == tournament.Id).ToList();

            tournament.Classes = classes;

            var matches = new List<Match>();
            var maxRounds = classes.Select(c => c.NumberOfRounds).ToList().Max();
            var actualRound = 1;
            int i = 0;
            while (maxRounds > 0)
            {
                for (i = 0; i < classes.Count; i++)
                {
                    var actualClass = classes[i];
                    for (int j = 0; j < actualClass.MatchesPerRound; j++)
                    {
                        if (actualRound > actualClass.NumberOfRounds)
                        {
                            continue;
                        }


                        var match = new Match();
                        match.Class = actualClass;
                        match.Round = actualRound;
                        match.IsScheduled = false;
                        match.Tournament = tournament;
                        matches.Add(match);
                    }

                }
                actualRound++;
                maxRounds--;


            }

            tournament.Matches = matches;
            return matches;
        }
    }
}
