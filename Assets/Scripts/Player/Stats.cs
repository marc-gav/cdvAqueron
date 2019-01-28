using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour {

    public Stat maxHealth; //La vida máxima.
    public Stat maxMana; //La ira máxima. La ira se llama `mana` internamente.
    public Stat shield; //La "defensa" del personaje. Es una salud añadida que no se recarga en el tiempo y se consume permanentemente.
    public Stat speed; //La velocidad. Se usa tanto como para el movimiento como para el ataque.
    public Stat luck; //La suerte. Se utiliza para rolls de estadísticas, mejoras, loot...
    public Stat attack; //El ataque. Cambia el daño al atacar.
    public Stat healthRegen; //Lo rápido que se regenera la vida por segundo.
    public Stat manaDecay; //Lo rápido que decae la ira por segundo. Es un valor positivo.

    private float health; //La salud actual. Se cumple que 0 <= `health` < (`maxHealth.Value` + `shield.Value`).
    private float mana; //La ira actual. Se cumple que 0 <= `mana` < `maxMana`.
    private bool healthRegenEnabled, manaDecayEnabled;

    public float Health {
        get { return health; }
    }

    public float Mana
    {
        get { return mana; }
    }

    private void Awake() {
        SetHealthRegen(true);
        health = maxHealth.Value;
        mana = maxMana.Value;
    }
    
    private void Update() {
        if (healthRegenEnabled) {
            health += healthRegen.Value * Time.deltaTime;
        }
        if (manaDecayEnabled) {
            mana -= manaDecay.Value * Time.deltaTime;
        }
    }

    //Baja la vida del personaje en `damage` puntos de daño. Pasar un valor negativo para curar.
    public void TakeDamage(float damage) {
        health -= damage;
    }

    public void ResetHealthMana() {
        health = maxHealth.Value;
        mana = maxMana.Value;
    }

    //Baja la cantidad de ira en mana puntos si es posible. Si no es posible no realiza cambios, excepto si `force == true`. 
    //Como la ira no puede ser negativa, si se intenta consumir más ira de la que hay disponible esta se pondrá a cero.
    //Devuelve `true` si `mana` es menor o igual que la ira disponible para consumir, y `false` en caso contrario.
    //Ten en cuenta que el método puede realizar cambios sobre la ira y devolver `false` si `force == true`.
    public bool ConsumeMana(float mana, bool force = false) {
        bool enoughMana = (this.mana >= mana);
        if (enoughMana || force) {
            this.mana -= mana;
            if (this.mana < 0) { mana = 0f; }
        }
        return enoughMana;
    }

    //Reanuda o detiene la regeneración de salud en el tiempo.
    public void SetHealthRegen(bool enabled = true) {
        healthRegenEnabled = enabled;
    }

    //Reanuda o detiene la pérdida de ira en el tiempo.
    public void SetManaDecay(bool enabled = true) {
        manaDecayEnabled = enabled;
    }
}

[System.Serializable]
public class Stat {

    //Define los posibles estados en los que se puede encontrar una estadística:
    //NERFED (-1):  si su valor está por debajo de la base (los modificadores negativos ganan)
    //NEUTRAL (0):  si su valor es igual que la base (los modificadores se compensan)
    //BUFFED (1):   si su valor está por encima de la base (los modificadores positivos ganan)
    public enum Status { Nerfed=-1, Neutral, Buffed }

    private const float NEUTRAL_ERROR_MARGIN = 0.1f;

    public float baseValue; //"base" es una palabra reservada
    private float percentModifier;
    private float flatModifier;

    //El valor final de la estadística, con todos los modificadores aplicados. Devuelve `Base * PercentModifier + FlatModifier`.
    public float Value {
        get { return Base * (1 + PercentModifier) + FlatModifier; }
    }

    //El valor total de los modificadores multiplicativos.
    public float PercentModifier {
        get { return percentModifier; }
    }

    //El valor total de los modificadores simples.
    public float FlatModifier {
        get { return flatModifier; }
    }

    //El valor base de la estadística.
    public float Base {
        get { return baseValue; }
        set { baseValue = value; }
    }

    //Si el valor es positivo (>=0)
    public bool IsPositive {
        get { return Value >= 0; }
    }

    //Obtiene el estado de la estadística
    public Status CurrentStatus {
        get {
            if (Value - Base < NEUTRAL_ERROR_MARGIN) {
                return Status.Neutral;
            } else if (Value > Base) {
                return Status.Buffed;
            } else {
                return Status.Nerfed;
            }
        }
    }


    //Constructores
    public Stat(float baseValue = 0f, float initialPercentModifier = 0f, float initialFlatModifier = 0f) {
        Base = baseValue;
        percentModifier = initialFlatModifier;
        flatModifier = initialFlatModifier;
    }


    //Añade el modificador `1.0f + value` a `PercentModifier`. Por ejemplo:
    //para aumentar un 15% la estadística `value` debe ser `0.15f`, y para reducirla un 71% debe ser `-0.71f`. 
    //Devuelve el cambio en puntos que ha sufrido la estadística.
    public float AddPercentModifier(float value) {
        float previous = Value;
        percentModifier += value;
        return Value - previous;
    }

    //Añade el modificador `value` a `FlatModifier`. `1` es un punto de la estadística.
    //Por ejemplo, para aumentar la estadística en 30 puntos `value` debe ser `30`.
    public void AddFlatModifier(int value) {
        flatModifier += value;
    }

    //Cambia FlatModifier hasta que la estadística (Value) tome un valor en el intervalo [min,max].
    //Devuelve el cambio en puntos que ha sufrido la estadística.
    public float Clamp(float min, float max) {
        float delta = 0f;
        if (Value > max) {
            delta = -Mathf.Abs(Value - max);
        } else if (Value < min) {
            delta = Mathf.Abs(min - Value);
        }
        flatModifier += delta;
        return delta;
    }

    //Reinicia los modificadores multiplicativos a `1.0f` (+0%). 
    //Devuelve el cambio en puntos que ha sufrido la estadística.
    public float ResetPercentModifier() {
        float previous = Value;
        percentModifier = 1f;
        return Value - previous;
    }

    //Reinicia los modificadores simples a `0.0f` (+0 puntos).
    //Devuelve el cambio en puntos que ha sufrido la estadística.
    public float ResetFlatModifier() {
        float previous = Value;
        flatModifier = 0f;
        return Value - previous;
    }

    //Reinicia todos los modificadores y devuelve la estadística al valor de su `Base`.
    //Devuelve el cambio en puntos que ha sufrido la estadística.
    public float Reset()
    {
        return ResetPercentModifier() + ResetFlatModifier();
    }

    //Asegura que el valor final será positivo. Si es menor que cero se establece en cero.
    //En caso contrario no se realizan cambios.
    public void AssurePositive() {
        if (Value < 0) {
            Clamp(0, Mathf.Infinity);
        }
    }
}
