﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeliculasWebAPI.Entidades {
    // [Table("TablaGeneros", Schema = "peliculas")]
    public class Genero {
        public int Identificador { get; set; }
        //public int Id { get; set; }

        /* Forma de darle un limite a una propiedad */
        // [MaxLength(150)]
        
        /* Forma de que el campo no sea nulo */
        //[Required]
        public string Nombre { get; set; }
        public HashSet<Pelicula> Peliculas { get; set; }
    }
}
