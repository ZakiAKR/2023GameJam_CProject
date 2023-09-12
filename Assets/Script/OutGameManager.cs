using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OutGameManager : MonoBehaviour
{
    // カウントダウンと全体の時間の判定を使用するために「TimerManager」を取得
    [SerializeField] public TimerManager _timeSystem;

    // 全体の時間が終わった跡にインゲームが続かないようにするために「InGameSystem」を取得
    [SerializeField] GameObject _typeingSystem;

    // audioの音量を消すために「AudioSource」を取得
    [SerializeField] AudioSource _audioSource;

    // ESCキーが押されたときに表示するパネル
    [SerializeField] GameObject _endPanel;

    // ボタンを選択状態にしておくオブジェクト
    [SerializeField] GameObject _selectButton;

    public float soundVolume = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        // 初期化
        _endPanel.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
    }

    // Update is called once per frame
    void Update()
    {
        // カウントダウン後と全体の時間内にESCキーが使用できるようにする処理
        if (_timeSystem.isCountDown&& !_timeSystem.isFinish && Input.GetKeyDown(KeyCode.Escape))
        {
            // 音量を０にする
            _audioSource.volume = 0;

            // InGameSystemを使用できなくする
            _typeingSystem.SetActive(false);

            // パネルを表示する
            _endPanel.SetActive(true);

            // ゲーム内の時間を止める
            Time.timeScale = 0f;

            // 最初に選択しておくボタンを設定する
            EventSystem.current.SetSelectedGameObject(_selectButton);
        }

        // 全体の時間が終わった場合
        if (_timeSystem.isFinish)
        {
            // InGameSystemを使用できなくする
            _typeingSystem.SetActive(false);
        }
    }

    // Escキーを押したときに出る「続きへRETUEN」のための関数
    public void OnClick_ReturnButton()
    {
        // パネルを非表示にする
        _endPanel.SetActive(false);

        // InGameSystemを使用できるようにする
        _typeingSystem.SetActive(true);

        // audioのボリュームを上げる
        _audioSource.volume = soundVolume;

        // ゲーム内の時間を進める
        Time.timeScale = 1;
    }

}
