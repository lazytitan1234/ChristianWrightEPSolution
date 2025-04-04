using Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace DataAccess.Repositories
{
    public class PollFileRepository : IPollRepository
    {
        private readonly string _filePath = "polls.json";

        public void CreatePoll(string title, string option1Text, string option2Text, string option3Text,
                               int option1VotesCount, int option2VotesCount, int option3VotesCount,
                               DateTime dateCreated)
        {
            var polls = GetPolls().ToList();
            int newId = polls.Any() ? polls.Max(p => p.Id) + 1 : 1;

            var newPoll = new Poll
            {
                Id = newId,
                Title = title,
                Option1Text = option1Text,
                Option2Text = option2Text,
                Option3Text = option3Text,
                Option1VotesCount = option1VotesCount,
                Option2VotesCount = option2VotesCount,
                Option3VotesCount = option3VotesCount,
                DateCreated = dateCreated
            };

            polls.Add(newPoll);
            File.WriteAllText(_filePath, JsonSerializer.Serialize(polls));
        }

        public IEnumerable<Poll> GetPolls()
        {
            if (!File.Exists(_filePath))
                return new List<Poll>();

            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Poll>>(json) ?? new List<Poll>();
        }

        public void Vote(int pollId, int optionNumber)
        {
            var polls = GetPolls().ToList();
            var poll = polls.FirstOrDefault(p => p.Id == pollId);

            if (poll != null)
            {
                switch (optionNumber)
                {
                    case 1: poll.Option1VotesCount++; break;
                    case 2: poll.Option2VotesCount++; break;
                    case 3: poll.Option3VotesCount++; break;
                }

                File.WriteAllText(_filePath, JsonSerializer.Serialize(polls));
            }
        }
    }
}
