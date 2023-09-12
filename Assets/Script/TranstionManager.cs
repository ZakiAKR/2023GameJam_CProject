using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// ƒV[ƒ“‘JˆÚ‚Ìˆ—

public class TranstionManager : MonoBehaviour
{
    public void To_Title()
    {
        SceneManager.LoadScene(0);
    }

    public void To_Setting()
    {
        SceneManager.LoadScene(1);
    }

    public void To_Main()
    {
        SceneManager.LoadScene(2);
    }

    public void To_Result()
    {
        SceneManager.LoadScene(3);
    }
}
