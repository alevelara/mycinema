using Mycinema.Domain.Common;

namespace Mycinema.Domain.Entities;

public class City : BaseDomainModel
{
    public string Name { get; private set; } = string.Empty;
    public int Population { get; private set; } = 0;

    public City(int id, string name, int population) : base(id)
    {
        Name = name;
        Population = population;
    }
}
