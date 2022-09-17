using Mycinema.Domain.Common;

namespace Mycinema.Domain.Entities;

public class Cinema : BaseDomainModel
{
    public string Name { get; private set; } = string.Empty;
    public DateTime OpenSince { get; private set; } = DateTime.MinValue;
    public int CityId { get; private set; }

    public virtual City City{ get; private set; }

    public Cinema(int id, string name, DateTime openSince, int cityId) : base(id)
    {
        Name = name;
        OpenSince = openSince;
        CityId = cityId;
    }


}
