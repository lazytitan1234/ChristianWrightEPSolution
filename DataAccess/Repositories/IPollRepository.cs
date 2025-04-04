using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace DataAccess.Repositories
{
    public interface IPollRepository
    {
        void CreatePoll(string title, string option1Text, string option2Text, string option3Text,
                        int option1VotesCount, int option2VotesCount, int option3VotesCount,
                        DateTime dateCreated);

        IEnumerable<Poll> GetPolls();

        void Vote(int pollId, int optionNumber);
    }
}