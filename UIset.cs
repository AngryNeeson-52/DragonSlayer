using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�ػ󵵿� ���� UI ��ġ�� ũ�� ����

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
        if (Screen.width * 3 / 4 < Screen.height) // ������ ȭ�� ũ�� ���� �ڵ�(�� �ڵ带 ������ S Z Fold���� ��Ĩ�ϴ�.)
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
        else // ���� ���� ���� ����
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
