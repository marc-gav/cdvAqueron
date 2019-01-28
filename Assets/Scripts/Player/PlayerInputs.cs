using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    private bool locked = false;

    //Player Movement
    private PlayerMovement pMovement;

    //Skills
    private AttackSkill attackSkill;
    private SkillDash dashSkill;

    private void Start()
    {
        attackSkill = GetComponent<AttackSkill>();
        dashSkill = GetComponent<SkillDash>();
        pMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        //Gets inputs
        if (!locked)
        {
            GetInputs();
        }
    }
    private void GetInputs()
    {
        float lockTime;
        //attack
        if (Input.GetButtonDown("Fire2") || Input.GetKeyDown(KeyCode.Q))
        {
            attackSkill.Cast();
            lockTime = attackSkill.lockTime;
            Lock();
            Invoke("UnLock", lockTime);
        }
        //jump
        else if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Jump"))
        {
            pMovement.Jump();
            lockTime = pMovement.jumpLockTime;
            Lock();
            Invoke("UnLock", lockTime);
        }
        //dash
        else if (Input.GetButtonUp("Fire3"))
        {
            dashSkill.Cast();
            lockTime = dashSkill.lockTime;
            Lock();
            Invoke("UnLock", lockTime);
        }
        else pMovement.Run();
    }

    public void Lock()
    {
        locked = true;
    }

    public void UnLock()
    {
        locked = false;
    }
}
