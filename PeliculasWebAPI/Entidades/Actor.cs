using System.ComponentModel.DataAnnotations.Schema;

namespace PeliculasWebAPI.Entidades {
    public class Actor {
        public int Id { get; set; }
        private string _nombre;
        public string Nombre {
            get { return _nombre; } 
            set {
                _nombre = string.Join(' ', value.Split(' ')
                                .Select(x => x[0].ToString()
                                                 .ToUpper() +
                                             x.Substring(1)
                                              .ToLower()
                                       )
                                 .ToArray()
                                );
                }
        }
        public string Biografia { get; set; }

        //[Column(TypeName = "Date")]
        public DateTime? FechaNac { get; set; }
        public HashSet<PeliculaActor> PeliculasActores { get; set; }
    }
}
