namespace Fiap.Api.Escola.Domain.Primitives;

public abstract class Entity(int id)
{
    public Entity()
        : this(id: 0)
    {
    }

    public int Id { get; set; } = id;

    public abstract string ToInsertQuery();

    public abstract string ToUpdateQuery();

    public abstract string ToDeleteQuery();
}
