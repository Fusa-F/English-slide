using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnglishTextController : MonoBehaviour
{
    public string[] engTexts;
    [SerializeField] public Text text;

    [SerializeField][Range(0.001f, 0.3f)]
    float textSpeed = 0.05f;
    [SerializeField] public int[] lineNum; 
    [SerializeField] public int[] deleteNum; 

    private string currentText = string.Empty;  //現在の文字列
    private string prevText = string.Empty;     //前の文字列
    private float time = 0;                     //表示にかかる時間
    private float timeElapsed = 1;              //文字列の表示を開始した時間
    private int currentLine = 0;                //現在の行番号
    private int lastText = -1;                  //表示中の文字数
    private bool isStart = false;               //始めるタイミング判定

    //文字の表示が完了しているか
    public bool IsComplete
    {
        get{ return Time.time > timeElapsed + time; }
    }

    void Start()
    {
    }

    void Update()
    {
        if(!isStart)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                SetNextLine();
                isStart = true;
            }
        }
        if(IsComplete)
        {
            if(currentLine < engTexts.Length && Input.GetKeyDown(KeyCode.RightArrow))
            {
                bool isLineChange = false; 
                prevText += currentText;
                foreach (int num in lineNum)
                {
                    if(currentLine == num)
                    {
                        prevText += "\n";
                        isLineChange = true;
                    }
                }
                foreach (int num in deleteNum)
                {
                    if(currentLine == num)
                    {
                        prevText = "\n";
                        isLineChange = true;
                    }
                }
                if(!isLineChange)
                {
                    prevText += "/";
                }

                SetNextLine();
            }
        }else
        {
            if(Input.GetKeyDown(KeyCode.RightArrow))
            {
                time = 0;
            }
        }

        //経過時間が想定表示時間の何%か確認し、表示文字数を出す
        int displayCount = (int)(Mathf.Clamp01((Time.time - timeElapsed) / time) * currentText.Length);
        //表示文字数が前回の表示文字数と異なる場合テキストを更新
        if(displayCount != lastText)
        {
            if(prevText != null)
            {
                text.text = prevText + currentText.Substring(0, displayCount);
            }else {
                text.text = currentText.Substring(0, displayCount);
            }
            lastText = displayCount;
        }
    }

    void SetNextLine()
    {
        currentText = engTexts[currentLine];
        time = currentText.Length * textSpeed;
        timeElapsed = Time.time;
        currentLine ++;
        lastText = -1;
    }
}
