﻿using Domain.Entities;
using MediatR;

namespace Application.Commands
{
    public class NextSeason : IRequest<List<League>>
    {
    }
}
