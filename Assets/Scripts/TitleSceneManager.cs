using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TitleSceneManager : MonoBehaviour
{
    public Text title;
    public Button startBtn;

    void Start()
    {
        StartCoroutine("TitleMovement");
    }

    void Update()
    {
    }

    private IEnumerator TitleMovement()
    {
        title.transform.DOLocalMove(new Vector2(0, 80), 3f);
        yield return new WaitForSeconds(1f);
        startBtn.transform.DOLocalMove(new Vector2(0, -100), 2f);
    }
}
