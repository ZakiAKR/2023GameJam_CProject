using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// シーン遷移の処理

public class TranstionManager : MonoBehaviour
{
    // SEを流してからシーン遷移に移るまでの時間を保存する変数
    public float seWaitTime;

    private void Start()
    {
        // ゲーム内の時間を進める
        Time.timeScale = 1;
    }

    public void To_Title()
    {
        // ゲーム内の時間を進める
        Time.timeScale = 1;

        // SEを鳴らした後時間を置くためのコルーチン
        StartCoroutine(Sound_SceneSE(0));
    }

    public void To_Setting()
    {
        // ゲーム内の時間を進める
        Time.timeScale = 1;

        // SEを鳴らした後時間を置くためのコルーチン
        StartCoroutine(Sound_SceneSE(1));
    }

    public void To_Main()
    {
        // ゲーム内の時間を進める
        Time.timeScale = 1;

        // SEを鳴らした後時間を置くためのコルーチン
        StartCoroutine(Sound_SceneSE(2));
    }

    public void To_Result()
    {
        // ゲーム内の時間を進める
        Time.timeScale = 1;

        // SEを鳴らした後時間を置くためのコルーチン
        StartCoroutine(Sound_SceneSE(3));
    }

    public void End_Game()
    {
        // ゲーム内の時間を進める
        Time.timeScale = 1;

        // SEを鳴らした後時間を置くためのコルーチン
        StartCoroutine(Sound_EndSE());
    }


    // SEを鳴らした後時間を置くためのコルーチン（シーン遷移用）
    private IEnumerator Sound_SceneSE(int numScene)
    {
        yield return new WaitForSeconds(seWaitTime);

        SceneManager.LoadScene(numScene);
    }


    // SEを鳴らした後時間を置くためのコルーチン（ゲーム終了用）
    private IEnumerator Sound_EndSE()
    {
        yield return new WaitForSeconds(seWaitTime);

        Application.Quit();
    }
}
