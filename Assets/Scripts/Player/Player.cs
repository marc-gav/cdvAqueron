using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private PlayerAnimations animations;
    private Stats stats;
    private PlayerInputs inputs;

    private void Update()
    {
        if(CheckDeath())
        {
            inputs.enabled = false;
        }
    }

    bool CheckDeath()
    {
        return stats.Health > 0;
    }
}
