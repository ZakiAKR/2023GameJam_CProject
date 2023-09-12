using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// ÉVÅ[ÉìëJà⁄ÇÃèàóù

public class TranstionManager : MonoBehaviour
{
    public float seWaitTime;

    private void Start()
    {
        Time.timeScale = 1;
    }

    public void To_Title()
    {
        Time.timeScale = 1;
        StartCoroutine(Sound_SceneSE(0));
    }

    public void To_Setting()
    {
        Time.timeScale = 1;
        StartCoroutine(Sound_SceneSE(1));
    }

    public void To_Main()
    {
        Time.timeScale = 1;
        StartCoroutine(Sound_SceneSE(2));
    }

    public void To_Result()
    {
        Time.timeScale = 1;
        StartCoroutine(Sound_SceneSE(3));
    }

    public void End_Game()
    {
        Time.timeScale = 1;
        StartCoroutine(Sound_EndSE());
    }

    private IEnumerator Sound_SceneSE(int numScene)
    {
        yield return new WaitForSeconds(seWaitTime);

        SceneManager.LoadScene(numScene);
    }

    private IEnumerator Sound_EndSE()
    {
        yield return new WaitForSeconds(seWaitTime);

        Application.Quit();
    }
}
