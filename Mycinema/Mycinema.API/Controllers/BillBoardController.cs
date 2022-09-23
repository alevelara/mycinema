using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mycinema.Application.Constants.Routes;
using Mycinema.Application.Features.BillBoards.Queries.GetPeriodicBillBoard;
using Mycinema.Application.Models.DTOs.Entities.TmdbAPI;
using System.Net;

namespace Mycinema.API.Controllers;

[ApiController]
[Route(Routes.APIController)]
public class BillBoardController
{
    private IMediator _mediator;

	public BillBoardController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpGet(BillBoardRoutes.GetIntelligentBoard)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<BillBoard>> GetIntelligentBoard([FromQuery] int numberOfScreensForBigRooms, [FromQuery] int numberOfScreensForSmallRooms, [FromQuery] DateTime startDateTime, [FromQuery] DateTime endDateTime, [FromQuery] bool haveSimilarMovies)
	{
		var request = new GetPeriodicBillBoardQuery(numberOfScreensForBigRooms, numberOfScreensForSmallRooms, startDateTime, endDateTime, haveSimilarMovies);
		return await _mediator.Send(request);
	}
}
