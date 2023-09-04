using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//스킬 정리

[System.Serializable]
public class Skill
{
    public string name;
    public string skillExplane;
    public int skillNum;
    public float requireMP;
    public float coolTime;
    public float damage;
    public float motiontime;
    public Sprite Icon;
}

public class SkillDic : MonoBehaviour
{
    [SerializeField]
    private Image skillicon;

    public Skill[] skills;
    public int basicSkill;
    public int selectedSkill;

    private void Start()
    {
        IconSet();
    }

    public void IconSet()
    {
        skillicon.sprite = skills[selectedSkill].Icon;
    }
}
