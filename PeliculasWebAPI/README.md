# Resumen de la sección 5: Configurando Propiedades

Se vieron los 3 tipos de configuraciones de Entity Framework, por _Convención_, por _Anotaciones de Datos_ y por el _API Fluente_.

### Llaves Primarias 

Se pueden configurar como _Guids_ o como identificadores globales únicos, de esta forma no se tiene el mismo valor repetido en distintas tablas o en distintas bases de datos. 

### Ignorar

Se pueden ignorar clases y propiedades de esta forma no se guardan en la Base de Datos cosas que no se quieren guardar. 

### Índices

Los índices sirven para dar velocidad a algunos Querys, además se pueden utilizar índices únicos para garantizar que un valor no se repita en una columna. 

### Conversiones de Valores

Nos permiten transformar datos que entran y salen de la Base de Datos. 

### Entidades sin Llave Primaria

Nos permite mapear los resultados de un Query arbitrario o una vista a una clase de C#.

Por ejemplo en __Cines__:

En el `ApplicationDBContext.cs` se establece la entidad `CinesSinUbicacion.cs` sin una llave primaria con el método `HasNoKey()` y con `ToSqlQuery()` se hace la consulta y finalmente con el método `ToView()` se evita que se guarde o agregue en la tabla de la Base de Datos. 

![entidadSinLlaveCinesSinUbicacion](/PeliculasWebAPI/images/entidadSinLlaveDbContext.png)

En el archivo `CinesController.cs` se hace el endpoint. 

![entidadSinLlaveCinesController](/PeliculasWebAPI/images/entidadSinLlaveCinesController.png)

Como resultado nos devolverá

![cinesSinUbicacion](/PeliculasWebAPI/images/cinesSinUbicacion.PNG)

Por ejemplo en __Creando una vista__

Haciendo una migración para crear una vista y que se pueda ser usado en la Entidad `PeliculaConteos`

![migracionVista](/PeliculasWebAPI/images/migracionVistaEntidadSinLlave.png)

En el `ApplicationDBContext.cs` se establece la entidad `PeliculaConteos.cs` sin una llave primaria con el método `HasNoKey()` y con el método `ToView()` se evita que se guarde o agregue en la tabla de la Base de Datos. 

![entidadSinLlaveContext2](/PeliculasWebAPI/images/entidadSinLlaveDbContext2.png)

En el archivo `PeliculasController.cs` agregamos el endpoint correspondiente.

![entidadSinLlaveController](/PeliculasWebAPI/images/entidadSinLlavePeliculasController.png)

Finalmente en nuestra Base de Datos debe devolver algo similar en el endpoint.

![creandoVista](/PeliculasWebAPI/images/creandoVista.PNG)

El resultado del endpoint es el siguiente:

![peliculasConteos](/PeliculasWebAPI/images/peliculaConteos.PNG)

### Propiedades Sombra

Permite definir columnas en nuestra Base de Datos que no van a tener propiedades correspondientes en la clase de C#.

En el `ApplicationDBContext.cs` se establece la propriedad `Datetime` a la columna `FechaCreacion`, esta se guardará en la base de datos y que será del tipo `datetime2` definida con el `HasColumnType()`.

![propiedadSombra](/PeliculasWebAPI/images/propiedadSombra.png)

Se hace la migración para que pueda persistir en nuestra Base de Datos.

![propiedadSombra](/PeliculasWebAPI/images/propiedadSombraMigracion.png)

Posteriormente en nuestro `GeneroController.cs` para que se pueda acceder a la columna creada.

![propiedadSombraGenero](/PeliculasWebAPI/images/propiedadSombraGeneroController.png)

Como resultado, nos devuelve lo siguiente:

![generoId](/PeliculasWebAPI/images/generoId.PNG)

### Automatizar Configuraciones

Podemos configurar en el _API Fluente_ mediante de reflexión en C#, así se puede configurar para que todas la propiedades que tengan un URL en su nombre no sean de tipo `unicode`.

En el `ApplicationDBContext.cs` se acceder a todos los modelos (entidades) y se crea el tipo _URL_.

![configTipoDato](/PeliculasWebAPI/images/configTipoDatoURLDbContext.png)

Posteriormente se hace la migración para que pueda persistir en nuestra Base de Datos:

![migracionFotoURL](/PeliculasWebAPI/images/migracionFotoURL.png)