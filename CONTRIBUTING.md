# Guía de contribución y formato

Esta guía detalla el formato común que vamos a seguir todos los programadores a la hora de escribir código. Por favor, revisa las reglas atentamente antes de enviar tus commits.

Estas guía sson realmente un recordatorio de las reglas de estilo que ya se aplican sobre C# en un gran número de entornos.

## Estilo de código

### Identificadores
1. Los identicadores llevarán mayúsculas y minúsculas según el siguiente formato:
* Atributos, variables: camelCase
* Campos: PascalCase
* Clases: PascalCase
* Funciones: PascalCase

### Espaciado
2. Escribiremos espacio tras las palabras reservadas, excepto cuando el siguiente carácter sea un signo de puntuación (como `;`).  
* `if (...)` en vez de `if(...)`  
* `return;` en vez de `return ;`

3. En los bucles `for`, los punto y coma (`;`) que separan inicialización, guarda y bucle irán precedidos y seguidos por un espacio.  
* `for (int i = 0 ; i < n ; i++)` en vez de `for (int i = 0;i < n;i++)`

4. Los operadores aritméticos, lógicos, de comparación y de asignación irán precedidos y seguidos de un espacio, a excepción de los operadores unarios.  
* `a + b * c` en vez de `a+b*c`  
* `int i = 0` en vez de `int i=0`  
* `a >= b` en vez de `a>=b`  
* `i++` en vez de `i ++`

5. Los paréntesis no se separán con espacios de su contenido, pero si de los elementos que se encuentran a su alrededor.   
*`a + (b * c)` en vez de `a + ( b * c )`

6. El operador de invocación `()` no se separará del nombre de los métodos, tenga argumentos o no.  
* `Awake()` en vez de `Awake ()`

7. Las comas que separan los argumentos dentro de las funciones y los índices irán seguidas por un espacio.   
* `Move(int x, int y, int dx, int dy)` en vez de `Move(int x,int y,int dx,int dy)`  
* `map[x, y]` en vez de `map[x,y]`

### Bloques
8. Las llaves que abren un bloque se colocarán en la misma línea de la sentencia que lo ha abierto, precedidas de un espacio.

9. Las llaves que cierran un bloque se colocarán en una línea nueva tras la última sentencia del bloque. Si esnecesario colocar una palabra reservada tras la llave, se colocará en la misma línea.

**Ejemplos:**
```
if (a < b) {
    
} else {
    
}
```
```
try {
    
} catch (ArgumentException e) {
    
} finally {
    
}
```

### Otros
10. Las cláusulas `where` se colocarán en una nueva línea.
```
protected abstract void OnCantMove<T>(T component)
    where T: Component;
```

## Contribuyendo
11. Los _commits_ podrán incluir un número ilimitado de cambios siempre que estos tengan una relación común y se puedan agrupar bajo un mismo objetivo.

12. Solamente podrán hacerse _push_ de _commits_ que sigan el flujo de trabajo y cumplan con os objetivos propuestos. Para enviar sugerencias y propuestas de cambio no solicitadas directamente deberá realizarse una _**merge request**_.

13. Antes de hacer _commit_ de tu trabajo comprueba en la consola de Unity que no provoque errores. Subir un script que provoque errores significará un _revert_.

_--SaltyKawaiiNeko❤️_