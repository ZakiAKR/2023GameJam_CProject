using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

// �^�C�s���O�����̃\�[�X�R�[�h

// �C���X�y�N�^�[�ォ�當�����ώ�o����悤�ɂ���
[Serializable]
public class Question
{
    public string mondai;
    public string romaji;
}

public class TypingManager : MonoBehaviour
{
    //�uQuestion�v�N���X�̃C���X�^���X�𐶐�����
    [SerializeField] private Question[] _questions = new Question[51];

    [Space(10)]

    // ���̕�����\������text���擾
    [SerializeField] private TextMeshProUGUI _textMondai;
    // �𓚂̕�����\������text���擾
    [SerializeField] private TextMeshProUGUI _textRomaji;

    [Space(10)]

    // �J�E���g�_�E����ɖ��Ɖ𓚂�\�����������߁A�uTimerManager�v���擾
    [SerializeField] public TimerManager _timeSystem;

    [Space(10)]

    // ���Ɖ𓚂�\������Ԋu�̕ϐ�
    public float intervalWaitMoji;

    // �^�C�s���O�̏�Ԃ��i�[���郊�X�g�̕ϐ�
    private List<char> _kaitou = new List<char>();
    // ���X�g�̔z��̗v�f���Ŏg�p����Ă���ϐ�
    private int _kaitouIndex = 0;

    // �ł��������̐����v������ϐ�
    private int _count;
    // �ԈႦ�đł��������̐��𐔂���ϐ�
    private int _miss;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // �ł��������̐����Ǘ��p�̕ϐ��ɑ��
        CountManager.countMojiNum = _count;

        // �ԈႦ�đł��������̐����Ǘ��p�̕ϐ��ɑ��
        CountManager.missMojiNum = _miss;
    }

    // �L�[���͎��ɌĂяo�����C�x���g�֐�
    private void OnGUI()
    {
        // ���͂��ꂽ��񂪃L�[���������Ƃ��ɓ��͂̏ꍇ
        if (Event.current.type == EventType.KeyDown)
        {
            // ���͂��ꂽ�L�[�R�[�h��ϊ����āA�ϊ��������������������������肵�����ʂɐ����ď������ς��
            switch (InputKey(GetChange_KeyCode(Event.current.keyCode)))
            {
                case 1:
                case 2:
                    // ��v�f�������Z���邱�Ƃł��̕������󔒂������ꍇ�A�������������ĐV���������o���B����ȊO�͕����̐F��ς���B
                    _kaitouIndex++;

                    // ����̕������󔒂������ꍇ�̏���
                    if (_kaitou[_kaitouIndex] == ' ')
                    {
                        // ���̏������̊֐����Ăяo��
                        Initi_Question();
                    }
                    else
                    {
                        // ������ł������𑪒�
                        _count++;

                        // �����̐F��ς���
                        _textRomaji.text = Generate_Romaji();
                    }
                    break;
                case 3:
                    // �~�X�^�C�s���O�̐��𑪒�
                    _miss++;
                    //������ł������𑪒�
                    _count++;
                    break;
            }
        }
    }

    //���͂����������𔻒肷��֐�
    int InputKey(char inputMoji)
    {
        // char �ϐ��� = �v�f���� N �܂��� N �ȏ�̏ꍇ ? (true) N �O�̕����̏�� : (false)null
        // ���͂��Ă镶���̂P�O�̉𓚂̕����̏���ۑ�����ϐ�
        char prevChar = _kaitouIndex >= 1 ? _kaitou[_kaitouIndex - 1] : '\0';
        // ���͂��Ă镶���̂Q�O�̉𓚂̕����̏���ۑ�����ϐ�
        char prevChar2 = _kaitouIndex >= 2 ? _kaitou[_kaitouIndex - 2] : '\0';
        // ���͂��Ă镶���̂R�O�̉𓚂̕����̏���ۑ�����ϐ�
        char prevChar3 = _kaitouIndex >= 3 ? _kaitou[_kaitouIndex - 3] : '\0';

        // �𓚂ɋL�ڂ���Ă��镶���̏���ۑ�����ϐ�
        char currentMoji = _kaitou[_kaitouIndex];

        // ���͂��Ă镶���̂P��̉𓚂̕����̏���ۑ�����ϐ�
        char nextChar = _kaitou[_kaitouIndex + 1];
        // char �ϐ��� = ���̕������󔒂������ꍇ ? (true) �� : (false)�Q��̕����̏��
        // ���͂��Ă镶���̂Q��̉𓚂̕����̏���ۑ�����ϐ�
        char nextChar2 = nextChar == ' ' ? ' ' : _kaitou[_kaitouIndex + 2];

        // ���͂������ꍇ
        if (inputMoji == '\0')
        {
            return 0;
        }

        // ���͂��������ꍇ
        if (inputMoji == currentMoji)
        {
            return 1;
        }

        // ��O����
        //�u���v�̕���
        // ���͂��ꂽ�������uy�v�A�𓚂̕������ui�v�̏ꍇ ���u���v��u�ɂ�v�Ȃǂ̕����ƍ��ʉ����邽�߂Ɉ�O�̕������u�Ȃ��v�ua�v�ui�v�uu�v�ue�v�uo�v�������ꍇ
        if (inputMoji == 'y' && currentMoji == 'i' && (prevChar == '\0' || prevChar == 'a' || prevChar == 'i' || prevChar == 'u' || prevChar == 'e' || prevChar == 'o'))
        {
            _kaitou.Insert(_kaitouIndex, 'y');
            _kaitou.Insert(_kaitouIndex + 1, 'i');
            return 2;
        }
        // ���͂��ꂽ�������uy�v�A�𓚂̕������ui�v�̏ꍇ ���O�̕������u��v�Łun�v[n]�Ƃ����𓚂̕����ł��u����v�s�̕����ƍ��ʉ����邽�߂ɂR�O�̕������un�v�ȊO�������ꍇ
        if (inputMoji == 'y' && currentMoji == 'i' && prevChar == 'n' && prevChar2 == 'n' && prevChar3 != 'n')
        {
            _kaitou.Insert(_kaitouIndex, 'y');
            _kaitou.Insert(_kaitouIndex + 1, 'i');
            return 2;
        }
        // ���͂��ꂽ�������uy�v�A�𓚂̕������ui�v�̏ꍇ�@���O�̕������u��v�Łux�v�un�v�Ƃ����𓚂̕����������ꍇ
        if (inputMoji == 'y' && currentMoji == 'i' && prevChar == 'n' && prevChar2 == 'x')
        {
            _kaitou.Insert(_kaitouIndex, 'y');
            _kaitou.Insert(_kaitouIndex + 1, 'i');
            return 2;
        }

        //�u���v�̕���
        // ���͂��ꂽ�������uw�v�A�𓚂̕������uu�v�̏ꍇ ���u���v��u�ɂ�v�Ȃǂ̕����ƍ��ʉ����邽�߂Ɉ�O�̕������u�Ȃ��v�ua�v�ui�v�uu�v�ue�v�uo�v�������ꍇ
        if (inputMoji == 'w' && currentMoji == 'u' && (prevChar == '\0' || prevChar == 'a' || prevChar == 'i' || prevChar == 'u' || prevChar == 'e' || prevChar == 'o'))
        {
            _kaitou.Insert(_kaitouIndex, 'w');
            _kaitou.Insert(_kaitouIndex + 1, 'u');
            return 2;
        }


        //�u���v�u���v�u���v
        if (inputMoji == 'c' && currentMoji == 'k' && prevChar != 'k' && (nextChar == 'a' || nextChar == 'u' || nextChar == 'o'))
        {
            _kaitou[_kaitouIndex] = 'c';
            return 2;
        }

        //�u���v
        if (inputMoji == 'q' && currentMoji == 'k' && prevChar != 'k' && nextChar == 'u')
        {
            _kaitou[_kaitouIndex] = 'q';
            return 2;
        }

        //�u���v
        if (inputMoji == 'h' && currentMoji == 'i' && prevChar == 's')
        {
            _kaitou.Insert(_kaitouIndex, 'h');
            return 2;
        }

        //�u���v
        if (inputMoji == 'j' && currentMoji == 'z' && nextChar == 'i')
        {
            _kaitou[_kaitouIndex] = 'j';
            return 2;
        }

        //�u����v�u����v�u�����v�u����v
        if (inputMoji == 'h' && currentMoji == 'y' && prevChar == 's')
        {
            _kaitou[_kaitouIndex] = 'h';
            return 2;
        }

        //�u����v�u����v�u�����v�u����v
        if (inputMoji == 'z' && currentMoji == 'j' && prevChar != 'j' && (nextChar == 'a' || nextChar == 'u' || nextChar == 'e' || nextChar == 'o'))
        {
            _kaitou[_kaitouIndex] = 'z';
            _kaitou.Insert(_kaitouIndex + 1, 'y');
            return 2;
        }

        //�u���v�u���v
        if (inputMoji == 'c' && currentMoji == 's' && prevChar != 's' && (nextChar == 'i' || nextChar == 'e'))
        {
            _kaitou[_kaitouIndex] = 'c';
            return 2;
        }

        //�u���v
        if (inputMoji == 'c' && currentMoji == 't' && prevChar != 't' && nextChar == 'i')
        {
            _kaitou[_kaitouIndex] = 'c';
            _kaitou.Insert(_kaitouIndex + 1, 'h');
            return 2;
        }

        //�u����v�u����v�u�����v�u����v
        if (inputMoji == 'c' && currentMoji == 't' && prevChar != 't' && nextChar == 'y')
        {
            _kaitou[_kaitouIndex] = 'c';
            return 2;
        }

        //�ucya�v=>�ucha�v
        if (inputMoji == 'h' && currentMoji == 'y' && prevChar == 'c')
        {
            _kaitou[_kaitouIndex] = 'h';
            return 2;
        }

        //�u�v
        if (inputMoji == 's' && currentMoji == 'u' && prevChar == 't')
        {
            _kaitou.Insert(_kaitouIndex, 's');
            return 2;
        }

        //�u���v�u���v�u���v�u���v
        if (inputMoji == 'u' && currentMoji == 's' && prevChar == 't' && (nextChar == 'a' || nextChar == 'i' || nextChar == 'e' || nextChar == 'o'))
        {
            _kaitou[_kaitouIndex] = 'u';
            _kaitou.Insert(_kaitouIndex + 1, 'x');
            return 2;
        }

        if (inputMoji == 'u' && (currentMoji == 'a' || currentMoji == 'i' || currentMoji == 'e' || currentMoji == 'o') && prevChar == 's' && prevChar2 == 't')
        {
            _kaitou.Insert(_kaitouIndex, 'u');
            _kaitou.Insert(_kaitouIndex + 1, 'x');
            return 2;
        }

        //�u�Ă��v
        if (inputMoji == 'e' && currentMoji == 'h' && prevChar == 't' && nextChar == 'i')
        {
            _kaitou[_kaitouIndex] = 'e';
            _kaitou.Insert(_kaitouIndex + 1, 'x');
            return 2;
        }

        //�u�ł��v
        if (inputMoji == 'e' && currentMoji == 'h' && prevChar == 'd' && nextChar == 'i')
        {
            _kaitou[_kaitouIndex] = 'e';
            _kaitou.Insert(_kaitouIndex + 1, 'x');
            return 2;
        }

        //�u�ł�v
        if (inputMoji == 'e' && currentMoji == 'h' && prevChar == 'd' && nextChar == 'u')
        {
            _kaitou[_kaitouIndex] = 'e';
            _kaitou.Insert(_kaitouIndex + 1, 'x');
            _kaitou.Insert(_kaitouIndex + 2, 'y');
            return 2;
        }

        //�u�Ƃ��v
        if (inputMoji == 'o' && currentMoji == 'w' && prevChar == 't' && nextChar == 'u')
        {
            _kaitou[_kaitouIndex] = 'o';
            _kaitou.Insert(_kaitouIndex + 1, 'x');
            return 2;
        }

        //�u�ǂ��v
        if (inputMoji == 'o' && currentMoji == 'w' && prevChar == 'd' && nextChar == 'u')
        {
            _kaitou[_kaitouIndex] = 'o';
            _kaitou.Insert(_kaitouIndex + 1, 'x');
            return 2;
        }

        //�u�Ӂv
        if (inputMoji == 'f' && currentMoji == 'h' && nextChar == 'u')
        {
            _kaitou[_kaitouIndex] = 'f';
            return 2;
        }

        //�u�ӂ��v�u�ӂ��v�u�ӂ��v�u�ӂ��v
        if (inputMoji == 'w' && (currentMoji == 'a' || currentMoji == 'i' || currentMoji == 'e' || currentMoji == 'o') && prevChar == 'f')
        {
            _kaitou.Insert(_kaitouIndex, 'w');
            return 2;
        }

        if (inputMoji == 'y' && (currentMoji == 'i' || currentMoji == 'e') && prevChar == 'f')
        {
            _kaitou.Insert(_kaitouIndex, 'y');
            return 2;
        }

        if (inputMoji == 'h' && currentMoji == 'f' && prevChar != 'f' && (nextChar == 'a' || nextChar == 'i' || nextChar == 'e' || nextChar == 'o'))
        {

            _kaitou[_kaitouIndex] = 'h';
            _kaitou.Insert(_kaitouIndex + 1, 'u');
            _kaitou.Insert(_kaitouIndex + 2, 'x');

            return 2;
        }

        if (inputMoji == 'u' && (currentMoji == 'a' || currentMoji == 'i' || currentMoji == 'e' || currentMoji == 'o') && prevChar == 'f')
        {
            _kaitou.Insert(_kaitouIndex, 'u');
            _kaitou.Insert(_kaitouIndex + 1, 'x');
            return 2;
        }

        //�u��v
        if (inputMoji == 'n' && currentMoji != 'a' && currentMoji != 'i' && currentMoji != 'u' && currentMoji != 'e' && currentMoji != 'o' && currentMoji != 'y' && prevChar == 'n' && prevChar2 != 'n')
        {
            _kaitou.Insert(_kaitouIndex, 'n');
            return 2;
        }

        if (inputMoji == 'x' && currentMoji == 'n' && prevChar != 'n' && nextChar != 'a' && nextChar != 'i' && nextChar != 'u' && nextChar != 'e' && nextChar != 'o' && nextChar != 'y')
        {
            if (nextChar == 'n')
            {
                _kaitou[_kaitouIndex] = 'x';
            }
            else
            {
                _kaitou.Insert(_kaitouIndex, 'x');
            }

            return 2;
        }

        //�u����v�u�ɂ�v�Ȃ�
        if (inputMoji == 'i' && currentMoji == 'y' &&
            (prevChar == 'k' || prevChar == 's' || prevChar == 't' || prevChar == 'n' || prevChar == 'h' || prevChar == 'm' ||
             prevChar == 'r' || prevChar == 'g' || prevChar == 'z' || prevChar == 'd' || prevChar == 'b' || prevChar == 'p') &&
            (nextChar == 'a' || nextChar == 'u' || nextChar == 'e' || nextChar == 'o'))
        {
            if (nextChar == 'e')
            {
                _kaitou[_kaitouIndex] = 'i';
                _kaitou.Insert(_kaitouIndex + 1, 'x');
            }
            else
            {
                _kaitou.Insert(_kaitouIndex, 'i');
                _kaitou.Insert(_kaitouIndex + 1, 'x');
            }

            return 2;
        }

        //�u����v�u����v�Ȃ�
        if (inputMoji == 'i' && (currentMoji == 'a' || currentMoji == 'u' || currentMoji == 'e' || currentMoji == 'o') && prevChar == 'h' && (prevChar2 == 's' || prevChar2 == 'c'))
        {
            if (nextChar == 'e')
            {
                _kaitou.Insert(_kaitouIndex, 'i');
                _kaitou.Insert(_kaitouIndex + 1, 'x');
            }
            else
            {
                _kaitou.Insert(_kaitouIndex, 'i');
                _kaitou.Insert(_kaitouIndex + 1, 'x');
                _kaitou.Insert(_kaitouIndex + 2, 'y');
            }

            return 2;
        }

        //�u����v���uc�v
        if (inputMoji == 'c' && currentMoji == 's' && prevChar != 's' && nextChar == 'y' && (nextChar2 == 'a' || nextChar2 == 'u' || nextChar2 == 'e' || nextChar2 == 'o'))
        {
            if (nextChar2 == 'e')
            {
                _kaitou[_kaitouIndex] = 'c';
                _kaitou[_kaitouIndex + 1] = 'i';
                _kaitou.Insert(_kaitouIndex + 1, 'x');
            }
            else
            {
                _kaitou[_kaitouIndex] = 'c';
                _kaitou.Insert(_kaitouIndex + 1, 'i');
                _kaitou.Insert(_kaitouIndex + 2, 'x');
            }

            return 2;
        }

        //�u���v
        if ((inputMoji == 'x' || inputMoji == 'l') &&
            (currentMoji == 'k' && nextChar == 'k' || currentMoji == 's' && nextChar == 's' ||
             currentMoji == 't' && nextChar == 't' || currentMoji == 'g' && nextChar == 'g' ||
             currentMoji == 'z' && nextChar == 'z' || currentMoji == 'j' && nextChar == 'j' ||
             currentMoji == 'd' && nextChar == 'd' || currentMoji == 'b' && nextChar == 'b' ||
             currentMoji == 'p' && nextChar == 'p'))
        {
            _kaitou[_kaitouIndex] = inputMoji;
            _kaitou.Insert(_kaitouIndex + 1, 't');
            _kaitou.Insert(_kaitouIndex + 2, 'u');
            return 2;
        }

        //�u�����v�u�����v�u�����v
        if (inputMoji == 'c' && currentMoji == 'k' && nextChar == 'k' && (nextChar2 == 'a' || nextChar2 == 'u' || nextChar2 == 'o'))
        {
            _kaitou[_kaitouIndex] = 'c';
            _kaitou[_kaitouIndex + 1] = 'c';
            return 2;
        }

        //�u�����v
        if (inputMoji == 'q' && currentMoji == 'k' && nextChar == 'k' && nextChar2 == 'u')
        {
            _kaitou[_kaitouIndex] = 'q';
            _kaitou[_kaitouIndex + 1] = 'q';
            return 2;
        }

        //�u�����v�u�����v
        if (inputMoji == 'c' && currentMoji == 's' && nextChar == 's' && (nextChar2 == 'i' || nextChar2 == 'e'))
        {
            _kaitou[_kaitouIndex] = 'c';
            _kaitou[_kaitouIndex + 1] = 'c';
            return 2;
        }

        //�u������v�u������v�u�������v�u������v
        if (inputMoji == 'c' && currentMoji == 't' && nextChar == 't' && nextChar2 == 'y')
        {
            _kaitou[_kaitouIndex] = 'c';
            _kaitou[_kaitouIndex + 1] = 'c';
            return 2;
        }

        //�u�����v
        if (inputMoji == 'c' && currentMoji == 't' && nextChar == 't' && nextChar2 == 'i')
        {
            _kaitou[_kaitouIndex] = 'c';
            _kaitou[_kaitouIndex + 1] = 'c';
            _kaitou.Insert(_kaitouIndex + 2, 'h');
            return 2;
        }

        //�ul�v�Ɓux�v
        if (inputMoji == 'x' && currentMoji == 'l')
        {
            _kaitou[_kaitouIndex] = 'x';
            return 2;
        }

        if (inputMoji == 'l' && currentMoji == 'x')
        {
            _kaitou[_kaitouIndex] = 'l';
            return 2;
        }

        // ���͂��Ԉ���Ă���ꍇ
        return 3;
    }

    // ���͂��ꂽ�L�[�R�[�h��char�^�ɕϊ�����֐�
    char GetChange_KeyCode(KeyCode keyCode)
    {
        switch (keyCode)
        {
            case KeyCode.A:
                return 'a';
            case KeyCode.B:
                return 'b';
            case KeyCode.C:
                return 'c';
            case KeyCode.D:
                return 'd';
            case KeyCode.E:
                return 'e';
            case KeyCode.F:
                return 'f';
            case KeyCode.G:
                return 'g';
            case KeyCode.H:
                return 'h';
            case KeyCode.I:
                return 'i';
            case KeyCode.J:
                return 'j';
            case KeyCode.K:
                return 'k';
            case KeyCode.L:
                return 'l';
            case KeyCode.M:
                return 'm';
            case KeyCode.N:
                return 'n';
            case KeyCode.O:
                return 'o';
            case KeyCode.P:
                return 'p';
            case KeyCode.Q:
                return 'q';
            case KeyCode.R:
                return 'r';
            case KeyCode.S:
                return 's';
            case KeyCode.T:
                return 't';
            case KeyCode.U:
                return 'u';
            case KeyCode.V:
                return 'v';
            case KeyCode.W:
                return 'w';
            case KeyCode.X:
                return 'x';
            case KeyCode.Y:
                return 'y';
            case KeyCode.Z:
                return 'z';
            case KeyCode.Minus:
                return '-';
            case KeyCode.Space:
                return ' ';
            default:
                return '\0';
        }
    }

    // ���̏������̊֐�
    public void Initi_Question()
    {
        // text�̕\��������
        _textMondai.text = "";
        _textRomaji.text = "";

        // �����Ő��l�𐶐�
        int _random = UnityEngine.Random.Range(0, _questions.Length);

        // Question�N���X�ɔz���ǉ�
        Question question = _questions[_random];

        // �v�f����������
        _kaitouIndex = 0;

        // ���X�g�̒��g����ɂ���
        _kaitou.Clear();

        // Question.romaji�iString�^�j��Char�^�̔z��ɕϊ�
        char[] characters = question.romaji.ToCharArray();

        // Question�N���X�̔z���_kaitou���X�g�ɒǉ�����
        foreach (char character in characters)
        {
            _kaitou.Add(character);
        }

        // ������̍Ŋ��ɋ󔒂�ǉ����āA�u�^�C�s���O�̏I���v������
        _kaitou.Add(' ');

        TimerManager._typeTime = TimerManager.typeTime;

        // ���Ɖ𓚂̕\������^�C�~���O�����炷���߂̃R���[�`��
        StartCoroutine(Display_Wait(question));
    }

    // ���͑O�Ɠ��͌�̕����̐F��ω����ĕ\��
    string Generate_Romaji()
    {
        // �����̐F���^�O�@�\�Ŏw��
        string text = "<style=typed>";

        // _kaitou���X�g���������J��Ԃ�
        for (int i = 0; i < _kaitou.Count; i++)
        {
            // �������󔒂������珈�����΂�
            if (_kaitou[i] == ' ')
            {
                break;
            }
            // ���X�g�̗v�f���������Ă����ꍇ�ɐF��ς���
            if (i == _kaitouIndex)
            {
                text += "</style><style=untyped>";
            }

            // ������������
            text += _kaitou[i];
        }
        // �����̐F��ς���
        text += "</style>";

        return text;
    }

    // ���Ɖ𓚂�\������Ԋu���󂯂邽�߂̊֐�
    private IEnumerator Display_Wait(Question question)
    {
        yield return new WaitForSeconds(intervalWaitMoji);

        //����\������
        _textMondai.text = question.mondai;

        yield return new WaitForSeconds(intervalWaitMoji);

        // �����̐F�𔼓����F�ɂ���
        _textRomaji.text = Generate_Romaji();
    }
}
