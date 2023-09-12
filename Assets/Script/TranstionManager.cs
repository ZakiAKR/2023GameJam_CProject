using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TranstionManager : MonoBehaviour
{
    void To_Title()
    {
        SceneManager.LoadScene(0);
    }

    void To_Setting()
    {
        SceneManager.LoadScene(1);
    }

    void To_Main()
    {
        SceneManager.LoadScene(2);
    }

    void To_Result()
    {
        SceneManager.LoadScene(3);
    }
}
