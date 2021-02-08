using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Kutian.Domain.Entities;
using Kutian.Domain.ViewModels;
using Kutian.Utilities.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kutian.Infrastructure.Features.Users.Queries
{
    public class Login
    {
        public class Query : IRequest<LoginResponse>
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                RuleFor(x => x.Username).NotNull();
                RuleFor(x => x.Password).NotNull();
            }
        }

        public class Handler : IRequestHandler<Query, LoginResponse>
        {
            private readonly IDatabaseContext _databaseContext;
            private readonly IPasswordHasher _passwordHasher;
            private readonly ITokenProvider _tokenProvider;

            public Handler(IDatabaseContext databaseContext, IPasswordHasher passwordHasher, ITokenProvider tokenProvider)
            {
                _tokenProvider = tokenProvider;
                _passwordHasher = passwordHasher;
                _databaseContext = databaseContext;
            }

            public async Task<LoginResponse> Handle(Query query, CancellationToken cancellationToken)
            {
                var user = await _databaseContext
                    .Set<User>().
                    SingleOrDefaultAsync(x => x.Username.ToLower() == query.Username.ToLower(), cancellationToken);

                if (user == null)
                    throw new Exception($"Could not find user: { user.Username }");

                var correctPassword = user.Password == _passwordHasher.HashPassword(user.Salt, query.Password);

                if (!correctPassword)
                    throw new Exception($"Incorrect password for user: { user.Username }");

                var loginResponse = new LoginResponse
                {
                    AccessToken = _tokenProvider.GetToken(
                        query.Username,
                        user.Roles
                        .Select(x => new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", x.Name))
                        .ToList()),
                    UserId = user.UserId
                };

                return loginResponse;
            }
        }
    }
}