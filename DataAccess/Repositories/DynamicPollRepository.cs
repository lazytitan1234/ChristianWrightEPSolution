using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.HttpOverrides;


namespace DataAccess.Repositories
{
    public class DynamicPollRepository : IPollRepository
    {
        private readonly PollRepository _sqlRepo;
        private readonly PollFileRepository _fileRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DynamicPollRepository(PollRepository sqlRepo, PollFileRepository fileRepo, IHttpContextAccessor accessor)
        {
            _sqlRepo = sqlRepo;
            _fileRepo = fileRepo;
            _httpContextAccessor = accessor;
        }

        private string? GetCurrentMode()
        {
            return _httpContextAccessor.HttpContext?.Session.GetString("RepoMode") ?? "sql";
        }

        private IPollRepository CurrentRepo =>
            GetCurrentMode() == "file" ? _fileRepo : _sqlRepo;

        public void CreatePoll(string title, string option1Text, string option2Text, string option3Text,
                               int option1VotesCount, int option2VotesCount, int option3VotesCount,
                               DateTime dateCreated)
            => CurrentRepo.CreatePoll(title, option1Text, option2Text, option3Text,
                                      option1VotesCount, option2VotesCount, option3VotesCount, dateCreated);

        public IEnumerable<Poll> GetPolls() => CurrentRepo.GetPolls();

        public void Vote(int pollId, int optionNumber, string userId)
            => CurrentRepo.Vote(pollId, optionNumber, userId);
    }
}
