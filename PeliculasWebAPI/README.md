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

En el `ApplicationDBContext.cs` se establece la entidad `CinesSinUbicacion.cs` sin una llave primaria con el método `HasNoKey()` y con `ToSqlQuery()` se hace la consulta y finalmente con el método `ToView()` se evita que se guarde o agregue en la tabla de la Base de Datos. 

![entidadSinLlaveCinesSinUbicacion](/PeliculasWebAPI/images/entidadSinLlaveDbContext.png)

En el archivo `CinesController.cs` se hace el endpoint. 

![entidadSinLlaveCinesController](/PeliculasWebAPI/images/entidadSinLlaveCinesController.png)