﻿using MediatR;
using Velox.Api.Infrastructure.DTO;


namespace Velox.Api.Features.Tournament.Commands
{
    public class CreateTournamentCommand : IRequest<TournamentDTO>
    {
        public string TournamentName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
    }
}
