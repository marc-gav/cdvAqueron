using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarCode : MonoBehaviour {

    public GameObject target;
    private Image healthBar;
    private Stats stats;
    private void Start()
    {
        stats = target.GetComponent<Stats>();
        healthBar = GetComponent<Image>();
    }

    private void Update()
    {
        healthBar.fillAmount = stats.Health / stats.maxHealth.Value;
    }


}
