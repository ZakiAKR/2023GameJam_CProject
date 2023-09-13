using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResultButtonManager : MonoBehaviour
{
    [SerializeField] GameObject _selectButton;
    private Vector3 _seleceScale;

    //選択状態がサイズの変更時用の変数
    public float moveSpeed;  //サイズが変わるスピード

    public float scallSpeed;  //サイズが変わるスピード
    public float maxTime;  //大きさが最大になる時間
    private float time;  //時間の保存用変数
    private bool enlarge = true;

    // Start is called before the first frame update
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(_selectButton);

        _seleceScale = _selectButton.transform.localScale;

        _selectButton.transform.localScale = Reset_ImageScale(_seleceScale);
    }

    // Update is called once per frame
    void Update()
    {
        Scaling(_selectButton);
    }

    //拡大縮小の演出の処理 : Processing of scaling direction
    void Scaling(GameObject image)
    {
        scallSpeed = Time.deltaTime * 0.01f;

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
