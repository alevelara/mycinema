using FluentValidation;

namespace Mycinema.Application.Features.BillBoards.Queries.GetPeriodicBillBoard;

public class GetPeriodicBillBoardValidator : AbstractValidator<GetPeriodicBillBoardQuery>
{
	public GetPeriodicBillBoardValidator()
	{
		RuleFor(r => r.StartDateTime)
			.NotEmpty().WithMessage("{StartDateTime} can not be empty")
			.NotNull().WithMessage("{StartDateTime} can not be null")
			.GreaterThan(DateTime.MinValue).WithMessage("{StartDateTime} should be greater than 01/01/0001 00:00:00")
			.LessThan(DateTime.MaxValue).WithMessage("{StartDateTime} should be less than 12/31/9999 11:59:59 PM");

        RuleFor(r => r.EndDateTime)
            .NotEmpty().WithMessage("{EndDateTime} can not be empty")
            .NotNull().WithMessage("{EndDateTime} can not be null")
            .GreaterThan(DateTime.MinValue).WithMessage("{EndDateTime} should be greater than 01/01/0001 00:00:00")
            .LessThan(DateTime.MaxValue).WithMessage("{EndDateTime} should be less than 12/31/9999 11:59:59 PM");

        RuleFor(r => r.NumberOfScreensForBigRooms)
            .NotEmpty().WithMessage("{NumberOfScreensForBigRooms} can not be empty")
            .NotNull().WithMessage("{NumberOfScreensForBigRooms} can not be null")
            .GreaterThanOrEqualTo(0).WithMessage("{NumberOfScreensForBigRooms} should be a positive value")
            .LessThan(999).WithMessage("{NumberOfScreensForBigRooms} should be less than 999");

        RuleFor(r => r.NumberOfScreensForSmallRooms)
           .NotEmpty().WithMessage("{NumberOfScreensForSmallRooms} can not be empty")
           .NotNull().WithMessage("{NumberOfScreensForSmallRooms} can not be null")
           .GreaterThanOrEqualTo(0).WithMessage("{NumberOfScreensForSmallRooms} should be a positive value")
           .LessThan(999).WithMessage("{NumberOfScreensForSmallRooms} should be less than 999");

    }
}
