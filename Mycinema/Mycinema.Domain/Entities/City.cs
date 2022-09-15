using Mycinema.Domain.Common;

namespace Mycinema.Domain.Entities;

public class City : BaseDomainModel
{
    public string Name { get; private set; } = string.Empty;
    public int Population { get; private set; } = 0;

    public City(string name, int population)
    {
        Name = name;
        Population = population;
    }
}
