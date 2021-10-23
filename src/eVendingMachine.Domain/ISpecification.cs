namespace eVendingMachine.Domain
{
    public interface ISpecification<T>
    {
        bool IsSatisfiedBy(T entity);
    }
}
