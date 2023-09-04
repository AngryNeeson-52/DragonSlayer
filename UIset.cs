using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//해상도에 맞춰 UI 위치와 크기 수정

public class UIset : MonoBehaviour
{
    public RectTransform rectTransform;
    [SerializeField]
    private float widthSize, heightSize, XPos, YPos;
    [SerializeField]
    private bool samesize, pos;

    private float screenheight;

    void Awake()
    {
        if (Screen.width * 3 / 4 < Screen.height) // 뚱폰용 화면 크기 조절 코드(이 코드를 갤럭시 S Z Fold에게 바칩니다.)
        {
            screenheight = Screen.height / 2;
        }
        else
        {
            screenheight = Screen.height;
        }

        if (!samesize)
        {
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.width * (widthSize / 100));
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, screenheight * (heightSize / 100));
        }
        else // 가로 세로 같은 기준
        {
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, screenheight * (widthSize / 100));
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, screenheight * (heightSize / 100));
        }

        if (pos)
        {
            rectTransform.anchoredPosition = new Vector2(Screen.width * (XPos / 100), screenheight * (YPos / 100));
        }
    }
}
