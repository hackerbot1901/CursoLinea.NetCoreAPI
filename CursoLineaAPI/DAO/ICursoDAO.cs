using CursoLineaAPI.Modelos;

namespace CursoLineaAPI.DAO
{


    public interface ICursoDAO : IGenericDAO<Curso, int>
    {
        // Methods
        public List<Curso> GetAll();
        
    }


}