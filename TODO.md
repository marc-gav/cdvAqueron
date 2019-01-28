# Lista de tareas

Antes de contibuir por favor lee [la guía de contribución](CONTRIBUTING.md).

**IMPORTANTE:** este archivo se irá actualizando a medida que avance el juego. No dejéis de revisarlo.

### Todas las mecanicas del juego.

1. Tiene que ver con el personaje
	- [x] `PlayerMovement` Doble salto
	- [ ] `no asignada` Mecánica de guadaña:
		- [ ] Atacar a melee
		- [ ] Lanzarse a modo de “cuerda” para enganchar enemigos distantes y atraerlos al mismo
		- [ ] En caso de no impactar contra ningún enemigo, servir de enganche para arrastrarse a lugares inaccesibles con saltos
	- [ ] `no asignada` Sistema de habilidades estilo diablo. Máximo 3 habilidades, modificables en sala segura y adquiribles matando jefes.
	- [ ] `no asignada` Sistema de mejoras para la armadura, implementados automáticamente al encontrar cofres escondidos a lo largo del mapa
	- [ ] `no asignada` Habilidad de ataques a distancia
	- [ ] `no asignada` Habilidades de dash
	- [ ] `no asignada` Habilidad de invocar robots que luchan a tu lado con ayuda de RP-42
	- [ ] `no asignada` Habilidad de introducirse dentro de RP-42 por unos segundos para moverse a gran velocidad, en lugares estrechos y sin ser visto por los enemigos
	- [ ] `PlayerMovement` Habilidad de salto contra la pared en vector opuesto
	- [ ] `no asignada` Habilidad de ir mejorando las habilidades con la experiencia
	- [ ] `no asignada` Habilidad de adaptar la guadaña a otro tipo de arma (dagas rápidas, espada y escudo que aumente la resistencia del pj, arcos-armas de fuego que disparen a distancia…)
	- [ ] `no asignada` Boosters temporales
	- [ ] `no asignada` Sistemas de transporte por el mapa (TP’s) para hacer más rápida la aparición a ciertas zonas
	- [ ] `no asignada` Pociones de salud y energía para el arma
2. No relacionadas con el personaje
	- [ ] `no asignada` Trampas
	- [ ] `no asignada` Minimapa
 	- [ ] `no asignada` Puzles


### Pendiente

1. [ ] `Lanver` `MrVizious` `PlayerMovement.cs` que gestiona el movimiento del jugador. Debe:
	- [x] Permitir un número arbitrario de saltos modificables en un atributo `public int availableJumps`. Saltar desde el suelo pone al jugador en el aire, y saltar en al aire reinicia la gravedad y lo impulsa hacia arriba de nuevo. Tras cada salto el contador se decrementa en 1 y este se reinicia al tocar el suelo de nuevo.
	- [x] Debe hacer que el jugador caiga hacia abajo simulando la gravedad.
	- [x] Tener en cuenta el valor de `speed.Value` del componente `Stats` del jugador para la velocidad del movimiento.
	- [x] Calcular angulos de colision
	- [ ] Debe cambiar los sprites del jugador de forma correspondiente.- (Pospuesto temporalmente a la espera de sprites finales)
	- [ ] Establecer un rango de angulos
	 	- [ ] Para detectar si puedes saltar sobre una superficie inclinada.
	 	- [ ] Para lo que se considera una pared.
        - [ ] Tener cuidado de no solapar rangos
    - [ ] Al chocar contra una pared el jugador debe quedarse colgando durante un tiempo pequeño. Si salta otra vez, sale despedido en diagonal, perpendicular a la pared y hacia arriba. Si falla, se desengancha, pierde todos los saltos y cae al suelo. Mientras el jugador está enganchado a la pared, como efecto estético, se llamará a `GameManager.Instance.EnterSlowTime()` para entrar en cámara lenta. Es obligatorio salir de la cámara lenta al salir disparado de la pared o al caer: queremos que durante la cámara lenta el jugador vea al protagonista pegado a la pared. Usar `ExitSlowTime()` si es necesario.
	- [ ] Al estar enganchado a la pared, añadir un pequeño deslizamiento hacia abajo.
	- [ ] Añadir Dash
	- [ ] Eliminar cambios temporales.
	- [ ] Pulir bugs

2. [ ] `Neko` `Skill.cs` con una clase abstracta `Skill` que representa una habilidad.
	1. [ ] con los atributos públicos:
		- [ ] `String name`, un nombre de habilidad con espacios y caracteres especiales.
		- [ ] `float cost`, un coste en puntos de ira.
		- [ ] `float range`, un alcance máximo en unidades de distancia.
		- [ ] `float theta`, un ángulo para definir las áreas en grados y desviaciones.
		- [ ] `CastType type`, un `CastType` de los siguientes, definidos por una `enum`:
			- `CUSTOM=0`: otra implementación.
			- `SELF=1`: la habilidad se lanza sobre el propio jugador. No utiliza `range` ni `theta`.
			- `TARGET=2`: la habilidad requiere que se seleccione un objetivo hasta una distancia máxima de `range`.
			- `POINT=3`: se lanza hacia un punto concreto a una distancia `range`.
			- `LINE=4`: la habilidad se lanza en línea de longitud `range` hacia el ángulo `theta` (0º = hacia adelante, 90º = hacia arriba, ...).
			- `CONE=5`: la habilidad se lanza en cono hacia adelante con distancia `range` y amplitud `theta`.
			- `CIRCLE=6`: la habilidad se lanza en círculo alrededor del jugador con radio `range`. No utiliza `theta`.
			- `AREA=7`: la habilidad se lanza en un área en el suelo de radio `range`. No utiliza `theta`.
		- [ ] `float cooldownTime`, el tiempo de recarga en segundos.
		- [ ] `TargetType target`, el tipo de objetivos a los que afecta, según su `Affiliation`:
			- `CUSTOM=0`: otra implementación.
			- `ALL=1`: todos los mobs.
			- `ALLY=2`: al jugador y los mobs aliados.
			- `ENEMY=3`: a los mobs enemigos.
			- `PLAYERONLY=4`: solo al jugador.
		- [ ] campo `bool IsAvailable`, si la habilidad está disponible.
		- [ ] campo `bool IsActive`, si la habilidad está activa.
		- [ ] campo `bool IsCoolingdown`, si la habilidad está en recarga.
	2. [ ] con los métodos:
		- [ ] `public bool Cast(float costReduction = 0f, float cooldownReduction = 0f)`: consume ira, ejecuta la habilidad, entra en recarga y devuelve `true` si ha tenido éxito. `costReduction` y `cooldownReduction` están en el invervalo [0f...1f] y señalan cuanto se reduce el coste de ira y el tiempo de recarga, respectivamente. Un valor de `0f` en ambos parámetros (por defecto) indica que se consume el coste total y el tiempo de recarga es completo.
		- [ ] `public void EnterCooldown(float cooldownReduction = 0f)`: entra en recarga. Si ya está en recarga, la actualiza si es mayor que la recarga actual.
		- [ ] `public void RefreshCooldown()`: sale de la recarga.
		- [ ] `public void EnterActive()`: entra el estado activo.
		- [ ] `public void ExitActive()`: finaliza el estado activo.
		- [ ] `protected abstract bool Run()`: las acciones específicas de la habilidad. Es llamada por `Cast()` y no debe consumir ira ni cambiar la recarga (a no ser que sea relevante), de eso ya se encarga `Cast()`. Debe devolver `true` si tiene éxito, `false` si falla.
		- [ ] `protected virtual void OnAttemptCast()`: función llamada cuando se inicia `Cast()`, antes de comprobar si la habilidad se puede lanzar.
		- [ ] `protected virtual void OnDrop()`: función llamada si `Cast()` decide que la habilidad no se puede lanzar y no intenta ejecutar `Run()`.
		- [ ] `protected virtual void OnFinishCast(bool success)`: función llamada al final de `Cast()` si se ha ejecutado `Run()`, cuyo valor de retorno se incluye en `success`.
		- [ ] `protected virtual void OnEnterActive()`: función llamada cuando la habilidad entra en su estado activo.
		- [ ] `protected virtual void OnExitActive()`: función llamada cuando la habilidad sale de su estado activo.
		- [ ] `protected virtual void OnEnterCooldown(float time)`: función llamada cuando la habilidad entra en recarga. `time` es el tiempo durante el que permanecerá en recarga.
		- [ ] `protected virtual void OnExitCooldown()`: función llamada cuando la habilidad sale de su recarga.
3. [ ] `No asignado` Script `MobEntity.cs`:
	1. [ ] con lo siguientes atributos
		- `Affiliation affiliation`, la afiliación del mob, que define que habilidades le pueden considerar objetivo:
			- `UNTARGETABLE=-1`: indica que las habilidades no pueden marcarlo como objetivo.
			- `UNKNOWN=0`: sin afiliación.
			- `ALLY=1`: un aliado.
			- `ENEMY=2`: un enemigo.
4. [ ] `GnGr` `Lanver` `antonovtum` Comportamiento/IA de los siguientes enemigos:
    - [ ] ...incompleto




### Propuesto

1. Script `LocalizationManager` que permita rellenar campos de texto a partir de strings guardadas en archivos serializables (JSON?). Permite la traducción del juego a más idiomas y evita llenar el código con texto. Las funciones para parsear JSON se pueden reutilizar.
2. Implementación de un sistema de diálogos. Funcionamiento:
	- Se pausa el juego.
	- Se muestra texto y pulsar un botón avanza a través de él hasta que termina.
	- Se reanuda el juego.
	Puede leer el texto de archivos (JSON?) reutilizando el código de `LocalizationManager`.
	Características adicionales a implementar:
	1. El texto va apareciendo con una animación de escritura. Velocidad configurable.
	2. Utilización de distintos marcos y estilos para el diálogo.
	3. Pulsar mientras el texto aparece termina la animación de golpe y lo muestra completo; pulsar con el texto completo avanza.
	4. Nombre de la persona que habla configurable: puede cambiar, desaparecer, etc.



### Completado

1. [x] Script `Stats.cs`, que incluye:  
	1. [x] los siguientes atributos públicos:
		- [x] `Stat maxHealth`: la vida máxima.
		- [x] `Stat maxMana`: la ira máxima. La ira se llama `mana` internamente.
		- [x] `Stat shield`: la "defensa" del personaje. Es una salud añadida que no se recarga en el tiempo y se consume permanentemente.
		- [x] `Stat speed`: la velocidad. Se usa tanto como para el movimiento como para el ataque.
		- [x] `Stat luck`: la suerte. Se utiliza para rolls de estadísticas, mejoras, loot...
		- [x] `Stat attack`: el ataque. Cambia el daño al atacar.
		- [x] `Stat healthRegen`: lo rápido que se regenera la vida por segundo.
		- [x] `Stat manaDecay`: lo rápido que decae la ira por segundo. Es un valor positivo.
	2. [x] los siguientes atributos privados:
		- [x] `float health`: la salud actual. Se cumple que 0 <= `health` < (`maxHealth.Value` + `shield.Value`).
		- [x] `float mana`: la ira actual. Se cumple que 0 <= `mana` < `maxMana`.
	3. [x] una subclase `Stat` que define una estadística.  
	Una estadística se calcula con la fórmula siguiente:  
	`float value = base - multiplicativeModifiers + flatModifiers`  
	donde:
		- `float base` es la cantidad base de la estadística, establecida con anterioridad.
		- `float multiplicativeModifiers` es el sumatorio de todos los modificadores multiplicativos. Todos estos modificadores son valores `float` que pueden ser bonificadores (>1) o penalizadores (<1). Por ejemplo, un 15% adicional en esta estadística se añade como `1.15` en este sumario.
		- `float flatModifiers` es el sumatorio de los modificadores simples. Todos estos modificadores son valores que suman o restan directamente en la estadística. Pueden ser positivos (>0) o negativos (<0) Por ejemplo, 35 puntos adicionales en esta estadística se añade como `35` en este sumatorio.
		1. [x] que tenga los siguientes campos:
			- [x] `float Value { get; }`: el valor final de la estadística, con todos los modificadores aplicados. Devuelve `Base - PercentModifier + FlatModifier`.
			- [x] `float PercentModifier { get; }`: el valor total de los modificadores multiplicativos.
			- [x] `float FlatModifier { get; }`: el valor total de los modificadores simples.
			- [x] `float Base { get; set; }`: la base de la estadística.
		2. [x] que tenga los siguientes métodos:
			- [x] `public float AddPercentModifier(float value)`: añade el modificador `1.0f + value` a `PercentModifier`. Por ejemplo, para aumentar un 15% la estadística `value` debe ser `0.15f`, y para reducirla un 71% debe ser `-0.71f`. Devuelve el cambio en puntos que ha sufrido la estadística.
			- [x] `public void AddFlatModifier(int value)`: añade el modificador `value` a `FlatModifier`. `1` es un punto de la estadística. Por ejemplo, para aumentar la estadística en 30 puntos `value` debe ser `30`.
			- [x] `public float ResetPercentModifier()`: reinicia los modificadores multiplicativos a `1.0f` (+0%). Devuelve el cambio en puntos que ha sufrido la estadística.
			- [x] `public float ResetFlatModifier()`: reinicia los modificadores simples a `0.0f` (+0 puntos). Devuelve el cambio en puntos que ha sufrido la estadística.
			- [x] `public float Reset()`: reinicia todos los modificadores y devuelve la estadística al valor de su `Base`. Devuelve el cambio en puntos que ha sufrido la estadística.
			- [x] `public float Clamp(float min, float max)`: cambia `FlatModifier` hasta que la estadística (`Value`) tome un valor en el intervalo [`min`,`max`]. Devuelve el cambio en puntos que ha sufrido la estadística.
		4. [x] los siguientes campos:
			- [x] `float Health { get; }`: la vida actual.
			- [x] `float Mana { get; }`: la ira actual.
		5. [x] los siguientes métodos:
			- [x] `public void TakeDamage(float damage)`: baja la vida del personaje en `damage` puntos de daño. Pasar un valor negativo para curar.
			- [x] `public bool ConsumeMana(float mana, bool force = false)`: baja la cantidad de ira en `mana` puntos si es posible. Si no es posible no realiza cambios, excepto si `force == true`. Como la ira no puede ser negativa, si se intenta consumir más ira de la que hay disponible esta se pondrá a cero.  
			Devuelve `true` si `mana` es menor o igual que la ira disponible para consumir, y `false` en caso contrario. Ten en cuenta que el método puede realizar cambios sobre la ira y devolver `false` si `force == true`.
			- [x] `public void SetHealthRegen(bool enabled = true)`: reanuda o detiene la regeneración de salud en el tiempo.
			- [x] `public void SetManaDecay(bool enabled = true)`: reanuda o detiene la pérdida de ira en el tiempo.
		6. [x] `GnGr` `GameManager.cs` implementado en forma de singleton para controlar el juego.
			- [x] Debe tener un campo público `Instance` con el singleton.
			- [x] No debe destruirse entre escenas.				- [x] Debe tener una constante pública `SLOW_TIME_SCALE` con el valor `float` de la escala de tiempo en la cámara lenta.
			- [x] Debe tener un método `public void EnterSlowTime(float seconds, float timeScale = SLOW_TIME_SCALE)` que ralentice el tiempo y mantenga el juego en "cámara lenta" durante `seconds` segundos estableciendo `Time.timeScale` a `timeScale`. Esto es un efecto estético.
			- [x] Debe tener un método `public void ExitSlowTime()` que reinicie la escala de tiempo inmediatamente.
