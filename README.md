# Mutant
La aplicacion esta escrita en capas

## Capa de servicios
En esta capa es la capa de la API la que se encarga de transformar los objetos que viene de core en Json, los objetos json que vienen de los post en objetos del core. Y manejar las rutas de acceso a los servicios

## Capa Core
La capa core esta representada por el proyecto MutantCore. En este proyecto se realiza todo tipo de calculos que tengan que ver con el negocio, como calcular si se mutante o no.

## Capa de Datos
En esta capa se abstrae el acceso a datos, de forma el proyecto core no tenga nocion de como se guarda en la base de datos ni que base es utilizada.


## Resolucion del algoritmo
Para reslver el algoritmo se decide recorrer la matriz que representa el adn de 3 maneras, obteniendo las diagonales que su largo es mayor a 4. Recorriendo las lineas horizontales y tambien las lineas verticales.
La clase [AdnValidatorExtensions.cs](https://github.com/matiascarro/Mutant/blob/2dc0306b64e94c75e01c008ea827dfcdadeb2ac2/Mutant/ValueObjet/AdnValidatorExtensions.cs) llama a estas 3 funciones en paralelo, si la suma de las lineas que cumplen la condicion es mayor a 1, entonces estamo frente a un mutante, en otro caso es un humano.

