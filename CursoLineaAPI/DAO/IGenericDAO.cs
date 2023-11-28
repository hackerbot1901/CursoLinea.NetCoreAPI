


namespace CursoLineaAPI.DAO
{
    public interface IGenericDAO<T, ID>
    {
        // CRUD methods
        void Create(T entity);
        T Read(ID id);
        void Update(T entity);
        void Delete(T entity);
    }
}

