using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.DataContext;

namespace DataAccess.Repositories
{
    public class PollRepository : IPollRepository
    {
        private readonly PollDbContext _context;

        public PollRepository(PollDbContext context)
        {
            _context = context;
        }

        public void CreatePoll(string title, string option1Text, string option2Text, string option3Text,
                               int option1VotesCount, int option2VotesCount, int option3VotesCount,
                               DateTime dateCreated)
        {
            var poll = new Poll
            {
                Title = title,
                Option1Text = option1Text,
                Option2Text = option2Text,
                Option3Text = option3Text,
                Option1VotesCount = option1VotesCount,
                Option2VotesCount = option2VotesCount,
                Option3VotesCount = option3VotesCount,
                DateCreated = dateCreated
            };

            _context.Polls.Add(poll);
            _context.SaveChanges();
        }

        public IEnumerable<Poll> GetPolls()
        {
            return _context.Polls
                           .OrderByDescending(p => p.DateCreated)
                           .ToList();
        }

        public void Vote(int pollId, int optionNumber)
        {
            var poll = _context.Polls.FirstOrDefault(p => p.Id == pollId);

            if (poll != null)
            {
                switch (optionNumber)
                {
                    case 1:
                        poll.Option1VotesCount++;
                        break;
                    case 2:
                        poll.Option2VotesCount++;
                        break;
                    case 3:
                        poll.Option3VotesCount++;
                        break;
                }

                _context.SaveChanges();
            }
        }
    }
}
