# Resumen de la sección 4: Crear, Actualizar y Borrar Datos

__Modelo Conectado__

Es cuando el DbContext carga una entidad y es el mismo que usamos para editarla.

__Modelo Desconectado__

Es lo contrario al modelo conectado, es decir, es cuando intentamos editar una entidad utlizando DbContext distinto al que la ha cargado. 

__Add__

Podemos usar add para cambiar el status del entidad en memoria, para cuando se usa `SaveChangesAsync()` la entidad sea insertada en la Base de Datos. 

__Mapeo Flexible__

Nos sirve para mapear un campo hacia una columna, envés de una propiedad a una columna, esto nos da flexibilidad para hacer transformaciones a la data antes de insertarla en la Base de Datos. 

__Update__

Podemos marcar un registro como modificado, lo que significa que cuando se ejecuta `SaveChangesAsync()`, Entity Framework se va encargar de actualizar el registro correspondiente en la Base de Datos. 

__Remove__

Podemos utilizar esta propiedad para actualizar el status de una entidad a borrada, para que cuando se ejecuta `SaveChangesAsync()` el registro correspondiente de la Base de Datos sea eliminado. 

__Borrado Suave o Lógico__

Nos permite marcar un registro como borrado, pero sin removerlo de la tabla, esto es útil cuando queremos permitir una funcionalidad de borrado pero necesitamos conservar la data para un uso futuro. 

__Filtros a nivel del modelo__

Lo usamos para configurar filtros por defecto en nuestra entidades, podemos saltarnos dichos filtros en los querys que deseemos. 