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

	[HttpPost(BillBoardRoutes.GetIntelligentBoard)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
	public async Task<ActionResult<BillBoard>> GetIntelligentBoard([FromBody] GetPeriodicBillBoardQuery request)
	{
		return await _mediator.Send(request);
	}
}
