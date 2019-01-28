using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public const float SLOW_TIME_SCALE = 0.65f;
    private bool slowed = false;

    private Stats playerStats;
	public static GameManager Instance { get; private set; }    //singleton

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {

        if (Input.GetButtonDown("Cancel"))
        {
            if (!slowed)
            {
                LoadScene("EscenaPausa", LoadSceneMode.Additive);
                EnterSlowTime(-1, 0);
            }
                
            else
            {
                int index = SceneManager.GetSceneByName("EscenaPausa").buildIndex;
                SceneManager.UnloadSceneAsync(index);
                ExitSlowTime();
            }
        }
            
    }

    public void EnterSlowTime(float seconds, float timeScale = SLOW_TIME_SCALE) {
        if (slowed == false) {
            slowed = true;
            Time.timeScale = timeScale;
            Time.fixedDeltaTime = 0.02F * Time.timeScale;
            if(seconds >= 0)
            {
                Invoke("ExitSlowTime", seconds);
            }
            
        }
    }

    public void ExitSlowTime() 
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.deltaTime;
        slowed = false;
    }

    public void LoadScene(string name, LoadSceneMode mode) {
        SceneManager.LoadScene(name, mode);
    }

    public void RestartGame(float time)
    {
        StartCoroutine(RestartIntro(time));
    }

    private IEnumerator RestartIntro(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("Intro", LoadSceneMode.Single);
    }
}