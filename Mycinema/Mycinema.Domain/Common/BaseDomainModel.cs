namespace Mycinema.Domain.Common;

public abstract class BaseDomainModel
{
    public int Id { get; private set; }

    public BaseDomainModel(int id)
    {
        this.Id = id;
    }
}
