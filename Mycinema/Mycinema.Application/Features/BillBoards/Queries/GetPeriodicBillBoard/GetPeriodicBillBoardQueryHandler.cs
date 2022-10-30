using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Mycinema.Application.Contracts.Infrastructure;
using Mycinema.Application.Contracts.Repositories;
using Mycinema.Application.Models.DTOs.Entities.TmdbAPI;
using Mycinema.Application.Models.Entities;
using Mycinema.Domain.Entities;

namespace Mycinema.Application.Features.BillBoards.Queries.GetPeriodicBillBoard;

public class GetPeriodicBillBoardQueryHandler : IRequestHandler<GetPeriodicBillBoardQuery, BillBoard>
{
    
    private readonly ILogger<GetPeriodicBillBoardQueryHandler> _logger;    

    public GetPeriodicBillBoardQueryHandler(ILogger<GetPeriodicBillBoardQueryHandler> logger)
    {        
        _logger = logger;        
    }

    public async Task<BillBoard> Handle(GetPeriodicBillBoardQuery request, CancellationToken cancellationToken)
    {
        
        List<TvShowRecommendation> tvShowRecommendations = await GetTVShowRecomendations(request.StartDateTime, request.EndDateTime);        
        List<MovieRecommendation> moviesRecommendations = await GetMoviesRecomendations(request.StartDateTime, request.EndDateTime);

        if (request.HaveSimilarMovies)
        {
            var similarMovies = await GetMostSuccesfulMoviesFromDb(request.StartDateTime, request.EndDateTime);
            moviesRecommendations.AddRange(similarMovies);
        }
       
        var billBoardFactory = new BillBoardFactory(request.StartDateTime, request.EndDateTime, moviesRecommendations, tvShowRecommendations, request.NumberOfScreensForBigRooms, request.NumberOfScreensForSmallRooms);

        _logger.LogInformation("Billboard created succesfully");

        return billBoardFactory.CreateBillBoard();
    }

    
    
}
