using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OutGameManager : MonoBehaviour
{
    [SerializeField] public TimerManager _timeSystem;

    [SerializeField] public TranstionManager _transScene;

    [SerializeField] GameObject _typeingSystem;

    [SerializeField] AudioSource _audioSource;

    [SerializeField] GameObject _endPanel;

    [SerializeField] GameObject[] _selectButton=new GameObject[2];

    private bool _isEsc;

    // Start is called before the first frame update
    void Start()
    {
        _endPanel.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);

        _isEsc = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_timeSystem.isCountDown&& !_timeSystem.isFinish && Input.GetKeyDown(KeyCode.Escape))
        {
            _audioSource.volume = 0;

            _typeingSystem.SetActive(false);

            _endPanel.SetActive(true);

            Time.timeScale = 0f;

            EventSystem.current.SetSelectedGameObject(_selectButton[0]);
        }
    }

    public void OnClick_ReturnButton()
    {
        _endPanel.SetActive(false);

        _typeingSystem.SetActive(true);

        _audioSource.volume = 1;

        Time.timeScale = 1;
    }

}
