using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// 스킬 아이콘 움직이기

public class SkillIcon : UIset, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private int skillNum;
    [SerializeField]
    private RectTransform selectpos, iconPos;

    private BoardManager board;

    private Vector2 temp, zeroPos;

    private void Start()
    {
        board = FindObjectOfType<BoardManager>();
        zeroPos = new Vector2(Screen.width / 2, Screen.height / 2);
        temp = zeroPos + iconPos.anchoredPosition;
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        board.SkillUpdate(skillNum);
    }
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition = eventData.position - temp;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (Vector2.Distance(eventData.position, selectpos.anchoredPosition + zeroPos) < selectpos.rect.width / 2)
        {
            board.SelectSkill();
        }
        rectTransform.anchoredPosition = Vector2.zero;
    }
}
