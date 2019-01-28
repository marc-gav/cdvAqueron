using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class IndicadorEnemigosRestantes : MonoBehaviour {

    public TextMeshProUGUI text;
	
    public void SetText(string value)
    {
        text.text = value;
    }
}
