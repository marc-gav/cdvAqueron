using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ContadorTiempo : MonoBehaviour
{
    List<Color> colores;
    [Range(3f, 10f)]
    public float periodoTiempoColor;
    private TextMeshProUGUI text;
    private float tiempo;
    private void Start()
    {
        colores = new List<Color>();
        colores.Add(Color.green);
        colores.Add(Color.red);
        colores.Add(Color.white);
        colores.Add(Color.cyan);
        text = GetComponentInChildren<TextMeshProUGUI>();
        tiempo = 0;
        StartCoroutine(changeColor(periodoTiempoColor));
    }

    private void Update()
    {
        tiempo += Time.deltaTime;
        double tiempoToShow = Math.Round(tiempo, 1);
        text.text = "" + tiempoToShow + " s";
    }

    private IEnumerator changeColor(float periodo)
    {
        while(true)
        {
            text.color = colores[UnityEngine.Random.Range(0, colores.Count - 1)];
            yield return new WaitForSeconds(periodo);
        }
    }
}
