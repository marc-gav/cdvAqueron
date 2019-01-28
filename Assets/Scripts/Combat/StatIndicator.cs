using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatIndicator : MonoBehaviour {

	public GameObject player;
    private Stats statsScrpt;
	public GameObject fillGameObject;
	private Image fillImage;
	private Slider slider;

	void Awake()
	{
        statsScrpt = player.GetComponent<Stats>();
		slider = GetComponent<Slider>();
        fillImage = fillGameObject.GetComponent<Image>();
	}

	void Update()
	{
        slider.value = statsScrpt.Health / statsScrpt.maxHealth.Value;
        CheckValue0();
    }

    public void CheckValue0() {
        if (slider.value <= 0) {
            fillImage.enabled = false;
        } else {
            fillImage.enabled = true;
        }
    }
}
