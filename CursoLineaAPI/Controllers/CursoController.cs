using CursoLineaAPI.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CursoLineaAPI.Controllers
{
    [ApiController]
    [Route("curso")]
    public class CursoController : ControllerBase
    {
        [HttpPost("agregar")]
        public dynamic AgregarNuevoCurso(Modelos.Curso curso)
        {
            try
            {
                Curso nuevoCurso = new()
                {
                    Nombre = curso.Nombre,
                    Descripcion = curso.Descripcion,
                    DuracionHoras = curso.DuracionHoras,
                };
                using var contexto = new CursoLineaContext();
                contexto.Cursos.Add(nuevoCurso);
                contexto.SaveChanges();
                return Ok(new { message = "Curso agregado exitosamente" });
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new { message = "Error al guardar en la base de datos", error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Se produjo un error inesperado", error = ex.Message });
            }
        }

        [HttpGet("listado")]
        public dynamic ListarCursos()
        {
            try
            {
                using var contexto = new CursoLineaContext();
                var cursos = contexto.Cursos.ToList();
                return Ok(new { cursos, message = "Cursos listados exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Se produjo un error al listar los cursos", error = ex.Message });
            }
        }

    [HttpPut("editar")]
    public dynamic EditarCursoExistente([FromQuery] int id, Curso cursoActualizado)
    {
        try
        {
            using (var contexto = new CursoLineaContext())
            {
                var cursoEnBaseDeDatos = contexto.Cursos.Find(id);

                if (cursoEnBaseDeDatos == null)
                {
                    return NotFound(new { message = "Curso no encontrado" });
                }

                // Actualizar propiedades del curso en base a los valores recibidos
                cursoEnBaseDeDatos.Nombre = cursoActualizado.Nombre;
                cursoEnBaseDeDatos.Descripcion = cursoActualizado.Descripcion;
                cursoEnBaseDeDatos.DuracionHoras = cursoActualizado.DuracionHoras;
                // Actualiza otras propiedades según sea necesario
                contexto.SaveChanges();
                return Ok(new
                {
                    curso = cursoEnBaseDeDatos,
                    message = "Curso actualizado exitosamente"
                });
            }
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(500, new { message = "Error al guardar en la base de datos", error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Se produjo un error inesperado", error = ex.Message });
        }
    }

        [HttpPost("inscribir")]
        public dynamic InscribirEstudiante(int idCurso, Estudiante estudiante)
        {
            try
            {
                using var contexto = new CursoLineaContext();
                var cursoSeleccionado = contexto.Cursos.Find(idCurso);

                if (cursoSeleccionado == null)
                {
                    return NotFound("Curso no encontrado");
                }

                estudiante.CursoId = idCurso;
                contexto.Estudiantes.Add(estudiante);
                contexto.SaveChanges();

                return Ok(new { message = "Estudiante inscrito exitosamente" });
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new { message = "Error al inscribir al estudiante", error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Se produjo un error inesperado", error = ex.Message });
            }
        }
    }
}
