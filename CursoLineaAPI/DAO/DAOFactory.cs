using CursoLineaAPI.EF;


namespace CursoLineaAPI.DAO
{
    public abstract class DAOFactory
    {
        protected static DAOFactory factory = new EFDAOFactory();
        public static DAOFactory GetFactory()
        {
            return factory;
        }
        public abstract EFCursoDAO GetCursoDAO();
        public abstract EFEstudianteDAO GetEstudianteDAO();
        public abstract EFProfesorDAO GetEFProfesorDAO();


    }
}
