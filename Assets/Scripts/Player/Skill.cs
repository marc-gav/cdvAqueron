using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Stats))]
public abstract class Skill : MonoBehaviour {

    public enum CastType { CUSTOM, SELF, TARGET, POINT, LINE, CONE, CIRCLE, AREA }
    public enum TargetType { CUSTOM, ALL, ALLY, ENEMY, PLAYERONLY }
    public enum CastError { NOT_AVAILABLE = 1, INSUFFICIENT_MANA }

    public new string name;
    public float lockTime;
    public float cost;
    public float range;
    public float theta;
    public CastType type;
    public TargetType target;
    public float cooldownTime;
    public bool activeEffect;
    public float maxActiveTime;

    private bool isActive;
    private float activeTime;
    private Stats stats;
    private float cooldown;

    public bool IsActive {
        get { return isActive; }
    }

    public bool IsCoolingdown
    {
        get { return cooldown > 0; }
    }

    public bool IsAvailable {
        get { return cooldown <= 0 && !isActive && stats.Mana >= cost; }
    }

    void Awake() {
        stats = GetComponent<Stats>();
        cooldown = 0f;
    }

    void Update() {
        if (cooldown > 0) {
            cooldown -= Time.deltaTime;
            if (cooldown <= 0) { RefreshCooldown(); }
        }
        if (isActive) {
            if(activeEffect) ActiveEffect();
            activeTime += Time.deltaTime;
            if(activeTime >= maxActiveTime) ExitActive();
        }
    }

    public void Cast(float costReduction = 0f, float cooldownReduction = 0f) {
        OnAttemptCast();
        if (!IsAvailable) {
            OnDrop(CastError.NOT_AVAILABLE);
            return;
        }
        if (!stats.ConsumeMana(cost)) {
            OnDrop(CastError.INSUFFICIENT_MANA);
            return;
        }
        OnFinishCast(Run());
    }

    public void EnterCooldown(float cooldownReduction = 0f) {
        cooldown = cooldownTime * (1f - cooldownReduction);
        OnEnterCooldown(cooldown);
    }

    public void RefreshCooldown() {
        cooldown = 0;
        OnExitCooldown();
    }

    public void EnterActive() {
        isActive = true;
        OnEnterActive();
    }

    public void ExitActive() {
        isActive = false;
        OnExitActive(activeTime);
        activeTime = 0f;
    }

    protected abstract bool Run();

    protected virtual void OnAttemptCast() {

    }

    protected virtual void OnDrop(CastError error) {

    }

    protected virtual void OnFinishCast(bool success) {

    }

    protected virtual void OnEnterCooldown(float time) {

    }

    protected virtual void OnExitCooldown() {

    }

    protected virtual void OnEnterActive() {

    }

    protected virtual void OnExitActive(float time) {

    }

    protected virtual void ActiveEffect() {

    }
}
