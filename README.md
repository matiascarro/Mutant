# Mutant
La aplicacion esta escrita en capas

## Capa de servicios
En esta capa es la capa de la API la que se encarga de transformar los objetos que viene de core en Json, los objetos json que vienen de los post en objetos del core. Y manejar las rutas de acceso a los servicios. Se crearon simplemente 2 controladores, uno que relaziza un ping para test y otro de los cuales tiene 2 enpoints. Un enpoint para exponer el servicio IsMutan que es un Get a MutantController, y otro que se stats para obtener estadisticas de las pruebas.

## Capa Core
La capa core esta representada por el proyecto MutantCore. En este proyecto se realiza todo tipo de calculos que tengan que ver con el negocio, como calcular si se mutante o no.

## Capa de Datos
En esta capa se abstrae el acceso a datos, de forma el proyecto core no tenga nocion de como se guarda en la base de datos ni que base es utilizada. Para guardar los datos se utiliza la base noSql de amazon DynamoDB. Usa como Key un requestId que es un numero GUID mappeado a string. Y como sort key utiliza un campo IsMutant que es un booleano repesentado en la base como string.


## Resolucion del algoritmo
Para reslver el algoritmo se decide recorrer la matriz que representa el adn de 3 maneras, obteniendo las diagonales que su largo es mayor a 4. Recorriendo las lineas horizontales y tambien las lineas verticales.
La clase [AdnValidatorExtensions.cs](https://github.com/matiascarro/Mutant/blob/2dc0306b64e94c75e01c008ea827dfcdadeb2ac2/Mutant/ValueObjet/AdnValidatorExtensions.cs) llama a estas 3 funciones en paralelo, si la suma de las lineas que cumplen la condicion es mayor a 1, entonces estamo frente a un mutante, en otro caso es un humano. A su vez estas son las unicas clases testeadas con Unit test

## Evito uso de Excepciones
Para evitar el uso de excepciones, se utiliza la clase [Result.cs](https://github.com/matiascarro/Mutant/blob/2dc0306b64e94c75e01c008ea827dfcdadeb2ac2/Mutant/ServiceResult/Result.cs). Esta clase es un wrapper del objeto resultante o de un mensaje de error si este ocurriera.

## Test Funcional
Para testear funcionalmente la aplicacion hay que bajarse la imagen de DynamoDB containerizada con el siguiente comando **docker pull amazon/dynamodb-local**  y luego el siguiente comando **docker run -p 8000:8000 amazon/dynamodb-local**. El test se encuentra en el archivo [FunctionalTest.cs](https://github.com/matiascarro/Mutant/blob/2dc0306b64e94c75e01c008ea827dfcdadeb2ac2/MutantCoreTest/FunctionalTest/FunctionalTest.cs)
Basicamente este levanta un servidor web, a su vez mockea la base de datos que esta en el container.



