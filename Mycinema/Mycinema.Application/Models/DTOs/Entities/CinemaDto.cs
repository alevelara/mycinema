namespace Mycinema.Application.Models.DTOs.Entities;

public  class CinemaDto
{
    public int Id { get; set; } 
    public string Name { get;  set; } 
    public DateTime OpenSince { get;  set; }
    public int CityId { get;  set; }
}
