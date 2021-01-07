# PruebaCsvImporter_AcunaFacundo


# CsvImporter Utility
La siguiente solucion contiene la definicion y las librerias para el utility CsvImporter.
La aplicación permite la importación de un archivo CSV a un tabla en una base de datos local.


## Preparación y uso:
Antes de ejecutar la aplicación aseguresé de tener una conexión a internet estable, 
los parámetros sean correctos en el archivo appsettings.json y sobretodo ejecutar el archivo 
DatabaseQuery.sql en su motor MSSQL Server local, (el archivo esta en AcmeCorporation\CsvImporter.Data\Scripts).

La aplicación permite cargar datos desde una conexión remota pero también si se desea desde una carpeta local.
Esto debe ser definido en el archivo appsettings.json indicando el tipo de conexión del archivo (Local/Remote).


## Caracteristicas
La aplicación utiliza una implementación propia de IDataReader que agrega capacidades
para usarse junto a librerias ADO.NET comunes como SqlBulkCopy.
También se contemplo el uso de otras librerias o métodos como el uso de Datatable
pero hacian un mal uso de los recursos (este método cargaba todo el archivo CSV en memoria en la lectura).
El módulo proporciona un algoritmo y una configuración ultrarrápida para leer archivos CSV de gran tamaño y
las pruebas de rendimiento de esta aplicación ha mostrado un rendimiento igual o mejor 
que otras librerias publicadas en la web.
La implementación de IDataReader contiene validaciones para el formato de header del archivo CSV
como también validaciones para el delimitador, existencia de archivos, etc.


## Configuraciones
La aplicación permite la configuracion de parametros para poder modificar los datos tales como
la ubicación del archivo CSV, si se encuentra en una carpeta o si la conexión debe ser remota,
configuración de la base de datos y la tabla destino de los datos.

Las configuraciones pueden encontrarse en appsettings.json, para esto se usan los siguientes paquetes Nuget
La configuracion se inyecta en el metodo principal en lugar del metodo de inicio
como las aplicaciones web, pero el codigo es esencialmente el mismo.
  
       Microsoft.Extensions.Configuration
       Microsoft.Extensions.Configuration.FileExtensions
       Microsoft.Extensions.Configuration.Json
       Microsoft.Extensions.DependencyInjection

Para la inyeccion de dependencias se han utilizado los siguientes paquetes

	Microsoft.Extensions.Logging
	Microsoft.Extensions.Logging.Console
	Microsoft.Extensions.DependencyInjection
		
Para el logging se ha utilizado la librería seriilog, tiene mas funciones que la librería por default
del framework .NET

	Microsoft.Extensions.Hosting
	Serilog.Extensions.Logging
	Serilog.Settings.Configuration
	Serilog.Sinks.File	
	
## Unit Test
Las funciones mencionadas antes pueden encontrarse en el proyecto de Unit Test.
Primero deben configurar correctamente la clase Fixture con los valores correctos para
ejecutar las pruebas.

Para el proyecto de Unit testing se ha utilizado la librería XUnit debido a las configuraciones
que permite antes de los test unitarios.

