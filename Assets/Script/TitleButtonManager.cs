using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TitleButtonManager : MonoBehaviour
{
    [SerializeField] GameObject[] _selectButton=new GameObject[2];
    private Vector3[] _seleceScale = new Vector3[2]; 
    
    private GameObject _button;

    //選択状態がサイズの変更時用の変数
    public float scallSpeed;  //サイズが変わるスピード
    public float maxTime;  //大きさが最大になる時間
    private float time;  //時間の保存用変数
    private bool enlarge = true;

    // Start is called before the first frame update
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(_selectButton[0]);

        for (int i = 0; i < _selectButton.Length; i++)
        {
            _seleceScale[i] = _selectButton[i].transform.localScale;
        }
    }

    // Update is called once per frame
    void Update()
    {
        _button = EventSystem.current.currentSelectedGameObject;

        if (_button == _selectButton[0])
        {
            Scaling(_selectButton[0]);
            _selectButton[1].transform.localScale = Reset_ImageScale(_seleceScale[1]);
        }
        if (_button == _selectButton[1])
        {
            Scaling(_selectButton[1]);
            _selectButton[0].transform.localScale = Reset_ImageScale(_seleceScale[0]);
        }
    }

    //拡大縮小の演出の処理 : Processing of scaling direction
    void Scaling(GameObject image)
    {
        scallSpeed = Time.deltaTime * 0.1f;

        if (time < 0) { enlarge = true; }
        if (time > maxTime) { enlarge = false; }

        if (enlarge)
        {
            time += Time.deltaTime;
            image.transform.localScale += new Vector3(scallSpeed, scallSpeed, scallSpeed);
        }
        else
        {
            time -= Time.deltaTime;
            image.transform.localScale -= new Vector3(scallSpeed, scallSpeed, scallSpeed);
        }
    }

    //大きさの初期化 : Size initialization
    Vector3 Reset_ImageScale(Vector3 afterObj)
    {
        return afterObj;
    }
}
