using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PoseButtonManager : MonoBehaviour
{
    [SerializeField] GameObject[] _selectButton = new GameObject[3];
    [SerializeField] GameObject[] _backButton=new GameObject[3];
    private Vector3[] _seleceScale = new Vector3[3];

    private GameObject _button;


    // Start is called before the first frame update
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(_selectButton[0]);
        _backButton[0].SetActive(false);
        _backButton[1].SetActive(true);
        _backButton[2].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        _button= EventSystem.current.currentSelectedGameObject;

        if (_button == _selectButton[0])
        {
            _backButton[0].SetActive(false);
            _backButton[1].SetActive(true);
            _backButton[2].SetActive(true);
        }
        if (_button == _selectButton[1])
        {
            _backButton[1].SetActive(false);
            _backButton[0].SetActive(true);
            _backButton[2].SetActive(true);
        }
        if (_button == _selectButton[2])
        {
            _backButton[2].SetActive(false);
            _backButton[1].SetActive(true);
            _backButton[0].SetActive(true);
        }
    }

}
