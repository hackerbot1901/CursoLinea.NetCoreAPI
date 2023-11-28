using CursoLineaAPI.EF;
using CursoLineaAPI.Modelos;
using CursoLineaAPI.DAO;

namespace CursoLineaAPI.DAO
{
    public class EFEstudianteDAO(Type persistenceClass) : EFGenericDAO<Estudiante, int>(persistenceClass), IEstudianteDAO
    {
    }
}