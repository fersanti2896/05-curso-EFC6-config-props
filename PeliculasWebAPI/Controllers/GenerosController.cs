using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasWebAPI.Entidades;

namespace PeliculasWebAPI.Controllers {
    [ApiController]
    [Route("api/generos")]
    public class GenerosController : ControllerBase {
        private readonly ApplicationDBContext context;

        public GenerosController(ApplicationDBContext context) {
            this.context = context;
        }

        /* endpoint */
        [HttpGet]
        public async Task<IEnumerable<Genero>> Get() {
            context.Logs.Add(new Log {
                                Id = Guid.NewGuid(),
                                Mensaje = "Ejecutando el método GenerosController.Get()"
                        });

            await context.SaveChangesAsync();

            return await context.Generos
                                .OrderBy(g => g.Nombre)
                                .ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Genero>> PorId(int id) {
            var genero = await context.Generos
                                .FirstOrDefaultAsync(p => p.Identificador == id);

            if (genero is null) {
                return NotFound();
            }

            return genero;
        }

        /* Filtración con Ordenamiento */
        [HttpGet("filtrar")]
        public async Task<IEnumerable<Genero>> FiltrarPorFrase(string nombre) {
            return await context.Generos
                                .Where(g => g.Nombre.Contains(nombre))
                                .OrderBy(g => g.Nombre)
                                .ToListAsync();
        }

        [HttpGet("paginacion")]
        public async Task<ActionResult<IEnumerable<Genero>>> GetPacionacion(int page = 1) {
            var registrosPagina = 2;
            var genero = await context.Generos
                                      .Skip((page - 1) * registrosPagina) /* Salta el primer registro */
                                      .Take(registrosPagina) /* Toma dos registros */
                                      .ToListAsync();
            if (genero is null) {
                NotFound();
            }

            return genero;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Genero genero) {
            var status = context.Entry(genero).State;
            
            /* Guarda el status */
            context.Add(genero);
            var status2 = context.Entry(genero).State;

            /* Agrega el estado genero a la tabla Generos */
            await context.SaveChangesAsync();
            var status3 = context.Entry(genero).State;

            return Ok();
        }

        [HttpPost("variosGeneros")]
        public async Task<ActionResult> Post(Genero[] generos) { 
            context.AddRange(generos);

            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("agregar2")]
        public async Task<ActionResult> Agregar2(int id) {
            var genero = await context.Generos
                                      .AsTracking()
                                      .FirstOrDefaultAsync(g => g.Identificador == id);

            if (genero is null) {
                return NotFound();
            }

            genero.Nombre += " 2";
            await context.SaveChangesAsync();

            return Ok();
        }

        /* Borrado normal */
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id) {
            var genero = await context.Generos
                                      .FirstOrDefaultAsync(gen => gen.Identificador == id);

            if (genero is null) {
                return NotFound();
            }

            context.Remove(genero);
            await context.SaveChangesAsync();

            return Ok();
        }

        /* Borrado lógico, se refiere a que no remueve el registro de la tabla
           sino solo se marca como un status de borrado */
        [HttpDelete("borradoLog/{id:int}")]
        public async Task<ActionResult> DeleteSuave(int id) {
            var genero = await context.Generos
                                      .AsTracking()
                                      .FirstOrDefaultAsync(gen => gen.Identificador == id);

            if (genero is null) {
                return NotFound();
            }

            genero.EstaBorrado = true;
            await context.SaveChangesAsync();

            return Ok();
        }

        /* Restaura un elemento borrado de manera temporal */
        [HttpPost("restaurar/{id:int}")]
        public async Task<ActionResult> PostRestaurar(int id) {
            var genero = await context.Generos
                                      .IgnoreQueryFilters()
                                      .AsTracking()
                                      .FirstOrDefaultAsync(gen => gen.Identificador == id);

            if (genero is null) {
                return NotFound();
            }

            genero.EstaBorrado = false;
            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
