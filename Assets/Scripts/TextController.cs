using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TextController : MonoBehaviour
{
    public string[] texts;
    [SerializeField] public Text text;
    [SerializeField] public GameObject textPanel;
    [SerializeField] public GameObject codePanel;

    //textPanel
    private RectTransform rectTrans;
    private float width;
    private float height;
    private Vector2 pos;

    [SerializeField][Range(0.001f, 0.3f)]
    float textSpeed = 0.05f;
    [SerializeField] public int panelSizeChangeNum;

    private string currentText = string.Empty;  //現在の文字列
    private float time = 0;                     //表示にかかる時間
    private float timeElapsed = 1;              //文字列の表示を開始した時間
    private int currentLine = 0;                //現在の行番号
    private int lastText = -1;                  //表示中の文字数

    //文字の表示が完了しているか
    public bool IsComplete
    {
        get{ return Time.time > timeElapsed + time; }
    }

    void Start()
    {
        SetNextLine();
        rectTrans = textPanel.GetComponent<RectTransform>();
        width = rectTrans.sizeDelta.x;
        height = rectTrans.sizeDelta.y;
        pos = textPanel.transform.position;

        codePanel.transform.position = new Vector2(-14, 1);
    }

    void Update()
    {
        if(IsComplete)
        {
            if(currentLine < texts.Length && Input.GetMouseButtonDown(0))
            {
                SetNextLine();
            }
        }else
        {
            if(Input.GetMouseButtonDown(0))
            {
                time = 0;
            }
        }

        if(currentLine > 1 && Input.GetMouseButtonDown(1))
        {
            currentLine -= 2;
            SetNextLine();
        }
        if(currentLine == panelSizeChangeNum)
        {
            rectTrans.DOSizeDelta(new Vector2(width, height - 60), 1f);
            textPanel.transform.DOLocalMove(new Vector2(pos.x, pos.y - 170), 1f);
            codePanel.transform.DOLocalMove(new Vector2(-196, 45), 1f);
        }

        //経過時間が想定表示時間の何%か確認し、表示文字数を出す
        int displayCount = (int)(Mathf.Clamp01((Time.time - timeElapsed) / time) * currentText.Length);
        //表示文字数が前回の表示文字数と異なる場合テキストを更新
        if(displayCount != lastText)
        {
            text.text = currentText.Substring(0,displayCount);
            lastText = displayCount;
        }
    }

    void SetNextLine()
    {
        currentText = texts[currentLine];
        time = currentText.Length * textSpeed;
        timeElapsed = Time.time;
        currentLine ++;
        lastText = -1;
    }
}
