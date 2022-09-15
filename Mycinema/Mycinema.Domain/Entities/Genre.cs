using Mycinema.Domain.Common;

namespace Mycinema.Domain.Entities;

public class Genre : BaseDomainModel
{
    public string Name { get; private set; } = string.Empty;
    public virtual ICollection<Movie> Movies { get; private set; }

    public Genre(string name)
    {
        Name = name;
        Movies = new List<Movie>();
    }
}
