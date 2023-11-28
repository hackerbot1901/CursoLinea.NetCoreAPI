using CursoLineaAPI.DAO;
using CursoLineaAPI.Modelos;

namespace CursoLineaAPI.EF
{
    public class EFDAOFactory : DAOFactory
    {
        public override EFCursoDAO GetCursoDAO()
        {
            return new EFCursoDAO(typeof(Curso));
        }

        public override EFProfesorDAO GetEFProfesorDAO()
        {
            return new EFProfesorDAO(typeof(Profesor));
        }

        public override EFEstudianteDAO GetEstudianteDAO()
        {
            return new EFEstudianteDAO(typeof(Estudiante));
        }
    }
}
