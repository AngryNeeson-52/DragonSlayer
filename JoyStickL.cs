using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//모바일 조이스틱 조작

public class JoyStickL : UIset, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private float playerSpeedFW;
    [SerializeField]
    private float playerSpeedetc;
    [SerializeField]
    private RectTransform backPos;

    private Vector2 zeroPos, temp;
    private Vector3 direction;
    private bool go = false, canMove = true;
    private float ver, hor, speed;

    void Start()
    {
        temp = new Vector2(rectTransform.rect.width / 2, rectTransform.rect.height / 2);
        zeroPos = backPos.anchoredPosition + new Vector2(backPos.rect.width / 2, backPos.rect.height / 2);
        rectTransform.anchoredPosition = zeroPos;
    }

    void Update()
    {
        Move();
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
        anim.SetBool("Moving", false);
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

        if (rectTransform.anchoredPosition.y > zeroPos.y + 10.0f)
        {
            ver = 1;
            speed = playerSpeedFW;
        }
        else if (rectTransform.anchoredPosition.y < zeroPos.y + -10.0f)
        {
            ver = -1;
            speed = playerSpeedetc;
        }
        else
        {
            ver = 0;
        }

        if (rectTransform.anchoredPosition.x > zeroPos.x + 10.0f)
        {
            hor = 1;
            speed = playerSpeedetc;
        }
        else if (rectTransform.anchoredPosition.x < zeroPos.x + -10.0f)
        {
            hor = -1;
            speed = playerSpeedetc;
        }
        else
        {
            hor = 0;
        }
    }

    private void Move() // 모바일 이동
    {
        if (go && canMove)
        {
            if (Vector2.Distance(rectTransform.anchoredPosition, zeroPos) >  10.0f)
            {
                anim.SetFloat("Vertical", ver);
                anim.SetFloat("Horizontal", hor);
                anim.SetBool("Moving", true);

                direction = (ver * player.transform.forward + hor * player.transform.right).normalized;

                player.transform.position += direction * speed * Time.deltaTime;

            }
            else
            {
                anim.SetBool("Moving", false);
            }
        }
    }

    private void OnDisable()
    {
        rectTransform.anchoredPosition = zeroPos;
        go = false;
        anim.SetBool("Moving", false);
    }

    public void CantMove()
    {
        canMove = false;
    }

    public void CanMove()
    {
        canMove = true;
    }

}