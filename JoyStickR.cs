using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//모바일 조이스틱 조작

public class JoyStickR : UIset, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private RectTransform backPos;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float rotateSpeed;

    private Vector2 zeroPos, temp;
    private bool go = false;

    void Start()
    {
        zeroPos = backPos.anchoredPosition + new Vector2(-backPos.rect.width / 2, backPos.rect.height / 2);
        temp = new Vector2(Screen.width - rectTransform.rect.width / 2, rectTransform.rect.height / 2);
        rectTransform.anchoredPosition = zeroPos;
    }

    void Update()
    {
        Rotation();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        TouchTrace(eventData.position);
        go = true;
    }
    public void OnDrag(PointerEventData eventData)
    {
        TouchTrace(eventData.position);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition = zeroPos;
        go = false;
    }

    private void TouchTrace(Vector2 eventData) // 조이스틱 구현
    {
        if (Vector2.Distance(eventData - temp, zeroPos) > backPos.rect.width / 2)
        {
            rectTransform.anchoredPosition = zeroPos + ((eventData - temp) - zeroPos).normalized * backPos.rect.width / 2;
        }
        else
        {
            rectTransform.anchoredPosition = eventData - temp;
        }
    }

    public void Rotation() // 실제 이동
    {
        if (go)
        {
            if (rectTransform.anchoredPosition.x > zeroPos.x + 10.0f)
            {
                player.transform.eulerAngles += new Vector3(0f, rotateSpeed * Time.deltaTime, 0f);
            }
            else if (rectTransform.anchoredPosition.x < zeroPos.x - 10.0f)
            {
                player.transform.eulerAngles -= new Vector3(0f, rotateSpeed * Time.deltaTime, 0f);
            }
        }
    }

    private void OnDisable()
    {
        rectTransform.anchoredPosition = zeroPos;
        go = false;
    }


    // 민감도 조절
    public void SensitiveUp()
    {
        if (rotateSpeed < 30)
        {
            rotateSpeed += 2;
        }
    }
    public void SeneitiveDown()
    {
        if (rotateSpeed > 10)
        {
            rotateSpeed -= 2;
        }
    }

}
