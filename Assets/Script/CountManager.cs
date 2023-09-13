using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountManager : MonoBehaviour
{
    public static int countMojiNum;
    public static int missMojiNum;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("CountMoji : "+countMojiNum);
        Debug.Log("missMoji : "+missMojiNum);
    }
}
