using CursoLineaAPI.DAO;
using CursoLineaAPI.Modelos;
namespace CursoLineaAPI.EF
{
    public class EFCursoDAO(Type persistenceClass) : EFGenericDAO<Curso, int>(persistenceClass), ICursoDAO
    {
        
        public List<Curso> GetAll()
        {
            return [.. dbContext.Set<Curso>()];
        }

    }
}

