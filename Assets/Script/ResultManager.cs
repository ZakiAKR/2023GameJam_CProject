using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultManager : MonoBehaviour
{
    [SerializeField] public CountManager _countSystem;
 
    [SerializeField] TextMeshProUGUI _mojiNum;

    [SerializeField] TextMeshProUGUI _missNum;

    [SerializeField] TextMeshProUGUI _evalation;

    private int _countMoji;

    private int _missMoji;

    private int _sumNum;
    private int _total;

    public float firstWaitTime;
    public float secondWaitTime;

    // Start is called before the first frame update
    void Start()
    {
        _countMoji = CountManager.countMojiNum;

        _missMoji = CountManager.missMojiNum;

        _mojiNum.text = "";
        _missNum.text = "";
        _evalation.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Set_Text());
    }

    private IEnumerator Set_Text()
    {
        yield return new WaitForSeconds(firstWaitTime);

        _mojiNum.text = _countMoji.ToString();

        yield return new WaitForSeconds(firstWaitTime);

        _missNum.text = _missMoji.ToString();

        yield return new WaitForSeconds(secondWaitTime);

        if ((_countMoji / 10) >= _missMoji)
        {
            _evalation.text = "A";
        }
        else if ((_countMoji / 10) < _missMoji && (_countMoji / 30) >= _missMoji)
        {
            _evalation.text = "B";
        }
        else if ((_countMoji / 30) < _missMoji && (_countMoji / 70) >= _missMoji)
        {
            _evalation.text = "C";
        }
        else
        {
            _evalation.text = "D";
        }
    }
}
