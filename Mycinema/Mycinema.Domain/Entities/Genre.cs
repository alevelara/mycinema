using Mycinema.Domain.Common;

namespace Mycinema.Domain.Entities;

public class Genre : BaseDomainModel
{
    public string Name { get; private set; } = string.Empty;
    public virtual ICollection<Movie> Movies { get; private set; }

    public Genre(int id, string name) : base(id)
    {
        Name = name;
        Movies = new List<Movie>();
    }
}
