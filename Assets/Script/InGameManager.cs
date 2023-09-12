using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// 時間計測の処理

public class InGameManager : MonoBehaviour
{
    // カウントダウンを表示させるtextを取得
    [SerializeField] TextMeshProUGUI countDown;
    // カウントダウンを表示させるTextのオブジェクトを取得
    [SerializeField] GameObject countObj;

    // 全体の時間を表示させるtextを取得
    [SerializeField] TextMeshProUGUI lifeText;

    // 終了を表示させるtextを取得
    [SerializeField] TextMeshProUGUI overText;
    // 終了を表示させるtextのオブジェクトを取得
    [SerializeField] GameObject overObj;

    // シーン遷移をするために「TranstionManager」を取得
    [SerializeField] public TranstionManager _transScene;

    // 時間計測する用の変数
    private float _countDown = 3f;
    // カウントダウンの数字を表示するための変数
    private int _count;

    // Startの表示時間
    public int StartWaitTime;

    // Finishの表示時間
    public int FinishWaitTime;

    // 全体の時間を計測
    public float lifeTime;
    // 全体の時間の数字を表示するための変数
    private int _life;

    // Startに入る際の演出が終わったかどうかの判定
    private bool _isCountDown;

    // 全体の時間が終わったかどうかの判定
    private bool _isFinish;

    // Start is called before the first frame update
    void Start()
    {
        // 初期化
        countObj.SetActive(true);
        overObj.SetActive(false);

        _count = 0;
        _life = 0;

        _isCountDown = false;
        _isFinish = false;
    }

    // Update is called once per frame
    void Update()
    {
        // カウントダウンの処理
        if (_countDown >= 0)
        {
            // カウントダウン３秒から時間を減算していく
            _countDown-=Time.deltaTime;

            // textに表示するためにint型に変換。※数字の表示を３２１のみにしたいため１を加算
            _count = (int)_countDown+1;

            // textで現在のカウントダウンの残り時間を表示
            countDown.text= _count.ToString();
        }
        // _isCountDownの判定を付けるのは「Start」が表示されている時に全体の時間が計測されないようにするため
        if (!_isCountDown && _countDown <= 0)
        {
            // Startを表示するためのコルーチン
            StartCoroutine(Delay_StartText());
        }

        // 全体の時間の処理
        // _isFinishの判定を付けているのは「Finish」が表示されているときに全体の時間が計測されないようにするため
        if (_isCountDown&&!_isFinish)
        {
            // ゲーム全体の時間を減算していく
            lifeTime -= Time.deltaTime;

            _life = (int)lifeTime;

            // ゲーム全体の残り時間を表示する
            lifeText.text = _life.ToString();
        }
        if (lifeTime <= 0)
        {
            // 全体の時間が終了した
            _isFinish = true;

            // Finishを表示するためのコルーチン
            StartCoroutine(Delay_OverText());
        }

    }

    // Startを表示するためのコルーチン
    private IEnumerator Delay_StartText()
    {
        // textに「Start」を表示
        countDown.text = "START!!";

        // textを一定時間表示したままにする
        yield return new WaitForSeconds(StartWaitTime);

        // textのオブジェクトを非表示にする
        countObj.SetActive(false);

        // カウントダウンが終了した
        _isCountDown = true;
    }

    // Finishを表示するためのコルーチン
    private IEnumerator Delay_OverText()
    {
        // textのオブジェクトを表示にする
        overObj.SetActive(true);

        // textに「Finish」を表示
        overText.text = "FINISH!!";

        // textを一定時間表示したままにする
        yield return new WaitForSeconds(FinishWaitTime);

        // リザルト画面を遷移する
        _transScene.To_Result();
    }
}
