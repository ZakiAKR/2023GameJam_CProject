using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// タイピング部分の処理

// インスペクター上から文字列を変種出来るようにする
[Serializable]
public class Question
{
    public string title;
    public string romaji;
}

public class TypingManager : MonoBehaviour
{
    // タイピングの状態を格納するリストの変数
    private List<char> _romaji = new List<char>();
    // リストの配列の要素数で使用されている変数
    private int _romajiIndex = 0;

    // インスタンスを生成する
    [SerializeField] Question[] _questions = new Question[12];

    // 画面に表示するためのTextMeshProを格納する変数
    [SerializeField]  TextMeshProUGUI _textTitle;
    [SerializeField]  TextMeshProUGUI _textRomaji;

    // Start is called before the first frame update
    void Start()
    {
        // TextMeshProのコンポーネントを取得
        _textTitle = GetComponent<TextMeshProUGUI>();
        _textRomaji = GetComponent<TextMeshProUGUI>();

        // 問題の初期化の関数を呼び出し
        Initi_Question();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // キー入力時に呼び出されるイベント関数
    private void OnGUI()
    {
        if (Event.current.type == EventType.KeyDown)
        {
            // キーが入力された時の処理
            Debug.Log("Event.current.type : " + Event.current.type);
            Debug.Log("EventType.KeyDown : " + EventType.KeyDown);
            Debug.Log("Event.current.KeyCode : " + Event.current.keyCode);
            Debug.Log("GetChange_KeyCode(Event.crrent.KeyCode) : " + GetChange_KeyCode(Event.current.keyCode));

            // 入力されたキーコードを変換して、変換した文字が正しい文字が判定した結果に酔って処理が変わる
            switch (InputKey(GetChange_KeyCode(Event.current.keyCode)))
            {
                case 1:
                case 2:
                    _romajiIndex++;
                    if (_romaji[_romajiIndex] == ' ')
                    {
                        Initi_Question();
                    }
                    else
                    {
                        _textRomaji.text = Generate_Romaji();
                    }
                    break;
                 case 3:
                    // ミスタイプ時の処理
                    break;
            }
        }
    }

    //入力が正しいかを判定する関数
    int InputKey(char inputMoji)
    {
        char prevChar3 = _romajiIndex >= 3 ? _romaji[_romajiIndex - 3] : '\0';
        char prevChar2 = _romajiIndex >= 2 ? _romaji[_romajiIndex - 2] : '\0';
        char prevChar = _romajiIndex >= 1 ? _romaji[_romajiIndex - 1] : '\0';
        
        char currentMoji = _romaji[_romajiIndex];

        char nextChar = _romaji[_romajiIndex + 1];
        char nextChar2 = nextChar == ' ' ? ' ' : _romaji[_romajiIndex + 2];

        // 入力が無い場合
        if (inputMoji == '\0')
        {
            return 0;
        }

        // 入力が正しい場合
        if (inputMoji == currentMoji)
        {
            return 1;
        }

        //「い」
        if (inputMoji == 'y' && currentMoji == 'i' &&
            (prevChar == '\0' || prevChar == 'a' || prevChar == 'i' || prevChar == 'u' || prevChar == 'e' ||
             prevChar == 'o'))
        {
            _romaji.Insert(_romajiIndex, 'y');
            return 2;
        }

        if (inputMoji == 'y' && currentMoji == 'i' && prevChar == 'n' && prevChar2 == 'n' &&
            prevChar3 != 'n')
        {
            _romaji.Insert(_romajiIndex, 'y');
            return 2;
        }

        if (inputMoji == 'y' && currentMoji == 'i' && prevChar == 'n' && prevChar2 == 'x')
        {
            _romaji.Insert(_romajiIndex, 'y');
            return 2;
        }

        //「う」
        if (inputMoji == 'w' && currentMoji == 'u' && (prevChar == '\0' || prevChar == 'a' || prevChar == 'i' ||
                                                       prevChar == 'u' || prevChar == 'e' || prevChar == 'o'))
        {
            _romaji.Insert(_romajiIndex, 'w');
            return 2;
        }

        if (inputMoji == 'w' && currentMoji == 'u' && prevChar == 'n' && prevChar2 == 'n' && prevChar3 != 'n')
        {
            _romaji.Insert(_romajiIndex, 'w');
            return 2;
        }

        if (inputMoji == 'w' && currentMoji == 'u' && prevChar == 'n' && prevChar2 == 'x')
        {
            _romaji.Insert(_romajiIndex, 'w');
            return 2;
        }

        if (inputMoji == 'h' && prevChar2 != 't' && prevChar2 != 'd' && prevChar == 'w' &&
            currentMoji == 'u')
        {
            _romaji.Insert(_romajiIndex, 'h');
            return 2;
        }

        //「か」「く」「こ」
        if (inputMoji == 'c' && prevChar != 'k' &&
            currentMoji == 'k' && (nextChar == 'a' || nextChar == 'u' || nextChar == 'o'))
        {
            _romaji[_romajiIndex] = 'c';
            return 2;
        }

        //「く」
        if (inputMoji == 'q' && prevChar != 'k' && currentMoji == 'k' && nextChar == 'u')
        {
            _romaji[_romajiIndex] = 'q';
            return 2;
        }

        //「し」
        if (inputMoji == 'h' && prevChar == 's' && currentMoji == 'i')
        {
            _romaji.Insert(_romajiIndex, 'h');
            return 2;
        }

        //「じ」
        if (inputMoji == 'j' && currentMoji == 'z' && nextChar == 'i')
        {
            _romaji[_romajiIndex] = 'j';
            return 2;
        }

        //「しゃ」「しゅ」「しぇ」「しょ」
        if (inputMoji == 'h' && prevChar == 's' && currentMoji == 'y')
        {
            _romaji[_romajiIndex] = 'h';
            return 2;
        }

        //「じゃ」「じゅ」「じぇ」「じょ」
        if (inputMoji == 'z' && prevChar != 'j' && currentMoji == 'j' &&
            (nextChar == 'a' || nextChar == 'u' || nextChar == 'e' || nextChar == 'o'))
        {
            _romaji[_romajiIndex] = 'z';
            _romaji.Insert(_romajiIndex + 1, 'y');
            return 2;
        }

        //「し」「せ」
        if ( inputMoji == 'c' && prevChar != 's' && currentMoji == 's' &&
            (nextChar == 'i' || nextChar == 'e'))
        {
            _romaji[_romajiIndex] = 'c';
            return 2;
        }

        //「ち」
        if (inputMoji == 'c' && prevChar != 't' && currentMoji == 't' && nextChar == 'i')
        {
            _romaji[_romajiIndex] = 'c';
            _romaji.Insert(_romajiIndex + 1, 'h');
            return 2;
        }

        //「ちゃ」「ちゅ」「ちぇ」「ちょ」
        if (inputMoji == 'c' && prevChar != 't' && currentMoji == 't' && nextChar == 'y')
        {
            _romaji[_romajiIndex] = 'c';
            return 2;
        }

        //「cya」=>「cha」
        if (inputMoji == 'h' && prevChar == 'c' && currentMoji == 'y')
        {
            _romaji[_romajiIndex] = 'h';
            return 2;
        }

        //「つ」
        if (inputMoji == 's' && prevChar == 't' && currentMoji == 'u')
        {
            _romaji.Insert(_romajiIndex, 's');
            return 2;
        }

        //「つぁ」「つぃ」「つぇ」「つぉ」
        if (inputMoji == 'u' && prevChar == 't' && currentMoji == 's' &&
            (nextChar == 'a' || nextChar == 'i' || nextChar == 'e' || nextChar == 'o'))
        {
            _romaji[_romajiIndex] = 'u';
            _romaji.Insert(_romajiIndex + 1, 'x');
            return 2;
        }

        if (inputMoji == 'u' && prevChar2 == 't' && prevChar == 's' &&
            (currentMoji == 'a' || currentMoji == 'i' || currentMoji == 'e' || currentMoji == 'o'))
        {
            _romaji.Insert(_romajiIndex, 'u');
            _romaji.Insert(_romajiIndex + 1, 'x');
            return 2;
        }

        //「てぃ」
        if (inputMoji == 'e' && prevChar == 't' && currentMoji == 'h' && nextChar == 'i')
        {
            _romaji[_romajiIndex] = 'e';
            _romaji.Insert(_romajiIndex + 1, 'x');
            return 2;
        }

        //「でぃ」
        if (inputMoji == 'e' && prevChar == 'd' && currentMoji == 'h' && nextChar == 'i')
        {
            _romaji[_romajiIndex] = 'e';
            _romaji.Insert(_romajiIndex + 1, 'x');
            return 2;
        }

        //「でゅ」
        if (inputMoji == 'e' && prevChar == 'd' && currentMoji == 'h' && nextChar == 'u')
        {
            _romaji[_romajiIndex] = 'e';
            _romaji.Insert(_romajiIndex + 1, 'x');
            _romaji.Insert(_romajiIndex + 2, 'y');
            return 2;
        }

        //「とぅ」
        if (inputMoji == 'o' && prevChar == 't' && currentMoji == 'w' && nextChar == 'u')
        {
            _romaji[_romajiIndex] = 'o';
            _romaji.Insert(_romajiIndex + 1, 'x');
            return 2;
        }

        //「どぅ」
        if (inputMoji == 'o' && prevChar == 'd' && currentMoji == 'w' && nextChar == 'u')
        {
            _romaji[_romajiIndex] = 'o';
            _romaji.Insert(_romajiIndex + 1, 'x');
            return 2;
        }

        //「ふ」
        if (inputMoji == 'f' && currentMoji == 'h' && nextChar == 'u')
        {
            _romaji[_romajiIndex] = 'f';
            return 2;
        }

        //「ふぁ」「ふぃ」「ふぇ」「ふぉ」
        if (inputMoji == 'w' && prevChar == 'f' &&
            (currentMoji == 'a' || currentMoji == 'i' || currentMoji == 'e' || currentMoji == 'o'))
        {
            _romaji.Insert(_romajiIndex, 'w');
            return 2;
        }

        if (inputMoji == 'y' && prevChar == 'f' && (currentMoji == 'i' || currentMoji == 'e'))
        {
            _romaji.Insert(_romajiIndex, 'y');
            return 2;
        }

        if (inputMoji == 'h' && prevChar != 'f' && currentMoji == 'f' &&
            (nextChar == 'a' || nextChar == 'i' || nextChar == 'e' || nextChar == 'o'))
        {
    
                _romaji[_romajiIndex] = 'h';
                _romaji.Insert(_romajiIndex + 1, 'u');
                _romaji.Insert(_romajiIndex + 2, 'x');

            return 2;
        }

        if (inputMoji == 'u' && prevChar == 'f' &&
            (currentMoji == 'a' || currentMoji == 'i' || currentMoji == 'e' || currentMoji == 'o'))
        {
            _romaji.Insert(_romajiIndex, 'u');
            _romaji.Insert(_romajiIndex + 1, 'x');
            return 2;
        }
        //「ん」
        if (inputMoji == 'n' && prevChar2 != 'n' && prevChar == 'n' && currentMoji != 'a' && currentMoji != 'i' &&
            currentMoji != 'u' && currentMoji != 'e' && currentMoji != 'o' && currentMoji != 'y')
        {
            _romaji.Insert(_romajiIndex, 'n');
            return 2;
        }

        if (inputMoji == 'x' && prevChar != 'n' && currentMoji == 'n' && nextChar != 'a' && nextChar != 'i' &&
            nextChar != 'u' && nextChar != 'e' && nextChar != 'o' && nextChar != 'y')
        {
            if (nextChar == 'n')
            {
                _romaji[_romajiIndex] = 'x';
            }
            else
            {
                _romaji.Insert(_romajiIndex, 'x');
            }

            return 2;
        }

        //「きゃ」「にゃ」など
        if (inputMoji == 'i' && currentMoji == 'y' &&
            (prevChar == 'k' || prevChar == 's' || prevChar == 't' || prevChar == 'n' || prevChar == 'h' ||
             prevChar == 'm' || prevChar == 'r' || prevChar == 'g' || prevChar == 'z' || prevChar == 'd' ||
             prevChar == 'b' || prevChar == 'p') &&
            (nextChar == 'a' || nextChar == 'u' || nextChar == 'e' || nextChar == 'o'))
        {
            if (nextChar == 'e')
            {
                _romaji[_romajiIndex] = 'i';
                _romaji.Insert(_romajiIndex + 1, 'x');
            }
            else
            {
                _romaji.Insert(_romajiIndex, 'i');
                _romaji.Insert(_romajiIndex + 1, 'x');
            }

            return 2;
        }

        //「しゃ」「ちゃ」など
        if (inputMoji == 'i' &&
            (currentMoji == 'a' || currentMoji == 'u' || currentMoji == 'e' || currentMoji == 'o') &&
            (prevChar2 == 's' || prevChar2 == 'c') && prevChar == 'h')
        {
            if (nextChar == 'e')
            {
                _romaji.Insert(_romajiIndex, 'i');
                _romaji.Insert(_romajiIndex + 1, 'x');
            }
            else
            {
                _romaji.Insert(_romajiIndex, 'i');
                _romaji.Insert(_romajiIndex + 1, 'x');
                _romaji.Insert(_romajiIndex + 2, 'y');
            }

            return 2;
        }

        //「しゃ」を「c」
        if (inputMoji == 'c' && currentMoji == 's' && prevChar != 's' && nextChar == 'y' &&
            (nextChar2 == 'a' || nextChar2 == 'u' || nextChar2 == 'e' || nextChar2 == 'o'))
        {
            if (nextChar2 == 'e')
            {
                _romaji[_romajiIndex] = 'c';
                _romaji[_romajiIndex + 1] = 'i';
                _romaji.Insert(_romajiIndex + 1, 'x');
            }
            else
            {
                _romaji[_romajiIndex] = 'c';
                _romaji.Insert(_romajiIndex + 1, 'i');
                _romaji.Insert(_romajiIndex + 2, 'x');
            }

            return 2;
        }

        //「っ」
        if ((inputMoji == 'x' || inputMoji == 'l') &&
            (currentMoji == 'k' && nextChar == 'k' || currentMoji == 's' && nextChar == 's' ||
             currentMoji == 't' && nextChar == 't' || currentMoji == 'g' && nextChar == 'g' ||
             currentMoji == 'z' && nextChar == 'z' || currentMoji == 'j' && nextChar == 'j' ||
             currentMoji == 'd' && nextChar == 'd' || currentMoji == 'b' && nextChar == 'b' ||
             currentMoji == 'p' && nextChar == 'p'))
        {
            _romaji[_romajiIndex] = inputMoji;
            _romaji.Insert(_romajiIndex + 1, 't');
            _romaji.Insert(_romajiIndex + 2, 'u');
            return 2;
        }

        //「っか」「っく」「っこ」
        if ( inputMoji == 'c' && currentMoji == 'k' && nextChar == 'k' &&
            (nextChar2 == 'a' || nextChar2 == 'u' || nextChar2 == 'o'))
        {
            _romaji[_romajiIndex] = 'c';
            _romaji[_romajiIndex + 1] = 'c';
            return 2;
        }

        //「っく」
        if ( inputMoji == 'q' && currentMoji == 'k' && nextChar == 'k' && nextChar2 == 'u')
        {
            _romaji[_romajiIndex] = 'q';
            _romaji[_romajiIndex + 1] = 'q';
            return 2;
        }

        //「っし」「っせ」
        if (inputMoji == 'c' && currentMoji == 's' && nextChar == 's' &&
        (nextChar2 == 'i' || nextChar2 == 'e'))
        {
            _romaji[_romajiIndex] = 'c';
            _romaji[_romajiIndex + 1] = 'c';
            return 2;
        }

        //「っちゃ」「っちゅ」「っちぇ」「っちょ」
        if (inputMoji == 'c' && currentMoji == 't' && nextChar == 't' && nextChar2 == 'y')
        {
            _romaji[_romajiIndex] = 'c';
            _romaji[_romajiIndex + 1] = 'c';
            return 2;
        }

        //「っち」
        if (inputMoji == 'c' && currentMoji == 't' && nextChar == 't' && nextChar2 == 'i')
        {
            _romaji[_romajiIndex] = 'c';
            _romaji[_romajiIndex + 1] = 'c';
            _romaji.Insert(_romajiIndex + 2, 'h');
            return 2;
        }

        //「l」と「x」
        if (inputMoji == 'x' && currentMoji == 'l')
        {
            _romaji[_romajiIndex] = 'x';
            return 2;
        }

        if (inputMoji == 'l' && currentMoji == 'x')
        {
            _romaji[_romajiIndex] = 'l';
            return 2;
        }

        // 入力が間違っている場合
        return 3;
    }

    // 入力されたキーコードをchar型に変換する関数
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

    // 問題の初期化の関数
    void Initi_Question()
    {
        // 乱数で数値を生成
        int _random = UnityEngine.Random.Range(0, _questions.Length);

        // Questionクラスに配列を追加
        Question question = _questions[_random];

        // 要素数を初期化
        _romajiIndex = 0;

        // リストの中身を空にする
        _romaji.Clear();

        // Question.romaji（String型）をChar型の配列に変換
        char[] characters =question.romaji.ToCharArray();

        // Questionクラスの配列を_romajiリストに追加する
        foreach (char character in characters)
        {
            _romaji.Add(character);
        }

        // 文字列の最期に空白を追加して、「タイピングの終わり」を示す
        _romaji.Add(' ');

        //
        _textTitle.text=question.title;
        //
        _textRomaji.text = Generate_Romaji();
    }

    // 入力前と入力後の文字の色を変化して表示
    string Generate_Romaji()
    {
        // 文字の色をタグ機能で指定
        string text = "<style=typed>";

        // _romajiリスト分処理を繰り返す
        for (int i = 0; i < _romaji.Count; i++)
        {
            // 文字が空白あったら処理を飛ばす
            if (_romaji[i] == ' ')
            {
                break;
            }
            // リストの要素数を合っていた場合に色を変える
            if (i == _romajiIndex)
            {
                text += "</style><style=untyped>";
            }

            // 文字を代入する
            text += _romaji[i];
        }
        // 文字の色を変える
        text += "</style>";

        return text;
    }
}
