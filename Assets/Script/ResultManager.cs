using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

// リザルト画面に関するソースコード

public class ResultManager : MonoBehaviour
{
    [Space(10)]

    // 打った文字の数を表示するtextを取得
    [SerializeField] private TextMeshProUGUI _mojiNum;
    // 間違えて打った数を表示するtextを取得
    [SerializeField] private TextMeshProUGUI _missNum;
    // 評価を表示するtextを取得
    [SerializeField] private TextMeshProUGUI _evalation;

    [Space(10)]

    //「戻る」というボタンをオブジェクトとして取得
    [SerializeField] private GameObject _selectButton;

    [Space(10)]

    // 文字の数を表示する待ちの時間を設定する変数
    public float firstWaitTime;
    // 評価の文字を表示する待ちの時間を設定する変数
    public float secondWaitTime;

    // 拡大縮小する大きさを設定する変数
    public float scallSize;
    // ボタンの大きさが最大になる時間を設定する変数
    public float maxTime;
    // ボタンの拡大縮小のスピード
    public float moveSpeed;
    // 時間を保存する変数
    private float time=0;
    // 拡大縮小を切り替える判定をする変数
    private bool enlarge = true;

    // 打った文字の数を保存する変数
    private int _countMoji;
    // 間違えて打った文字の数を保存する変数
    private int _missMoji;

    // Start is called before the first frame update
    void Start()
    {
        // 参照した打った文字の数を保存用の変数に代入
        _countMoji = CountManager.countMojiNum;

        // 参照した間違えて打った文字の数を保存用の変数に代入
        _missMoji = CountManager.missMojiNum;

        // 文字を表示しないようにする処理
        _mojiNum.text = "";
        _missNum.text = "";
        _evalation.text = "";

        // 「戻る」のボタンを選択状態にしておく
        EventSystem.current.SetSelectedGameObject(_selectButton);
    }

    // Update is called once per frame
    void Update()
    {
        // 文字の演出をするコルーチン
        StartCoroutine(Set_Text());

        // ボタンが拡大縮小する演出の処理
        Scaling_Button(_selectButton);
    }

    // 表示する文字を時間を空けて表示する演出のコルーチン
    private IEnumerator Set_Text()
    {
        // 打った文字を表示するまでの待ちの時間
        yield return new WaitForSeconds(firstWaitTime);

        // 打った文字の数を表示する
        _mojiNum.text = _countMoji.ToString();

        // 間違えて打った文字を表示するまでの待ちの時間
        yield return new WaitForSeconds(firstWaitTime);

        // 間違えて打った文字の数を表示する
        _missNum.text = _missMoji.ToString();

        // 評価を表示するまでの待ちの時間
        yield return new WaitForSeconds(secondWaitTime);

        // 評価する基準の処理
        if ((_countMoji / 10) <= _missMoji)
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

    // ボタンを拡大縮小する演出の関数
    void Scaling_Button(GameObject image)
    {
        // 動きを滑らかにする処理
        scallSize = Time.deltaTime * moveSpeed;

        // 拡大縮小を時間で切り替える処理
        if (time < 0) { enlarge = true; }
        if (time > maxTime) { enlarge = false; }

        // オブジェクトの大きさを変える処理
        if (enlarge)
        {
            // 拡大するために時間を増やす
            time += Time.deltaTime;

            // オブジェクトの大きさを大きくする
            image.transform.localScale += new Vector3(scallSize, scallSize, scallSize);
        }
        else
        {
            // 縮小するために時間を減らす
            time -= Time.deltaTime;

            // オブジェクトの大きさを小さくする
            image.transform.localScale -= new Vector3(scallSize, scallSize, scallSize);
        }
    }

}
