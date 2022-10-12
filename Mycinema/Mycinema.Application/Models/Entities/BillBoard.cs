namespace Mycinema.Application.Models.Entities;

public class BillBoard
{
    public List<BillBoardWeeks> Weeks{ get; private set; }	

	public BillBoard()
	{
		Weeks = new List<BillBoardWeeks>();
	}

	public BillBoard(List<BillBoardWeeks> weeks)
	{
		Weeks = weeks;
	}
}
