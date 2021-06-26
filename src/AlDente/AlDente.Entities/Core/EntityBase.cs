namespace AlDente.Entities.Core
{
    public abstract class EntityBase : EntityBase<int>
    {
    }

    public abstract class EntityBase<TKey>
    {
        public TKey Id { get; set; }
    }
}
