using CursoLineaAPI.DAO;
using CursoLineaAPI.Modelos;

namespace CursoLineaAPI.EF
{
    public class EFProfesorDAO(Type persistenceClass) : EFGenericDAO<Profesor, int>(persistenceClass), IProfesorDAO
    {
    }
}