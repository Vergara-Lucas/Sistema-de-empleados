namespace CRUDCORE.Interface
{
    public interface IController<T>
    {
        T Save(T model);
    }
}