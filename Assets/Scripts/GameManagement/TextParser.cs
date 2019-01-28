using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextParser : MonoBehaviour {
    
    public float speed;
    public TextAsset textFile;
    public TextMeshProUGUI text;
    private string[] textToShow;
    private int currentLine = -1;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        ParseText();
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            currentLine++;
            currentLine = Mathf.Clamp(currentLine, 0, textToShow.Length - 1);
            Debug.Log(currentLine);
            StartCoroutine(ShowText(currentLine));
        }
        else if (Input.GetMouseButtonDown(1))
        {
            currentLine--;
            currentLine = Mathf.Clamp(currentLine, 0, textToShow.Length - 1);
            Debug.Log(currentLine);
            StartCoroutine(ShowText(currentLine));
        }
    }

    private void ParseText()
    {
        string content = textFile.text;
        if (content == "")
            content = System.Text.Encoding.Default.GetString(textFile.bytes);
        textToShow = content.Split('\n');
    }
    private IEnumerator ShowText(int currentLine)
    {
        string currentShow = "";
        char[] showArray = textToShow[currentLine].ToCharArray();
        for (int i = 0; i < showArray.Length; i++)
        {
            currentShow += showArray[i];
            text.text = currentShow;
            yield return new WaitForSeconds(speed);
        }
    }
}
