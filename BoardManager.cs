using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// 게임 내 보드(스텟, 스킬, 설정) 관련 코드

public class BoardManager : MonoBehaviour
{
    [SerializeField]
    private GameObject statUI, skillUI, setUI, announceText, selected;
    [SerializeField]
    private Text hpText, mpText, atkText, walletText, skillName, skillinfo, skillDesc;
    [SerializeField]
    private Image selectedIcon, effectSoundUI, fieldSoundUI, sensitiveUI;


    private PlayerStats playerStats;
    private SkillDic skillDic;
    private MasterSound masterSound;

    private bool statopen = true, skillopen = true, setopen = true;
    private int clicked;
    private float sensitive = 0.5f;

    private void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        skillDic = FindObjectOfType<SkillDic>();
        masterSound = FindObjectOfType<MasterSound>();
    }

    private void CloseAll()
    {
        statopen = true;
        skillopen = true;
        setopen = true;
        statUI.SetActive(false);
        skillUI.SetActive(false);
        setUI.SetActive(false);
    }

    //세팅 보드 관련 코드
    #region
    public void OpenSettings() // 세팅 창 열기
    {
        if (setopen)
        {
            CloseAll();
            setopen = false;
            SetUpdate();
            setUI.SetActive(true);
        }
        else
        {
            CloseAll();
        }
    }
    private void SetUpdate() // 세팅 창 업데이트
    {
        effectSoundUI.fillAmount = masterSound.effectVol;
        fieldSoundUI.fillAmount = masterSound.fieldVol;
        sensitiveUI.fillAmount = sensitive;
    }

    public void SensitiveChangeP()
    {
        if (sensitive < 1)
        {
            sensitive += 0.1f;
        }
        SetUpdate();
    }
    public void SensitiveChangeM()
    {
        if (sensitive > 0)
        {
            sensitive -= 0.1f;
        }
        SetUpdate();
    }
    public void EffectVolChangeP()
    {
        if (masterSound.effectVol < 1.0f)
        {
            masterSound.effectVol += 0.1f;
            masterSound.SetVol();
            SetUpdate();
        }
    }
    public void EffectVolChangeM()
    {
        if (masterSound.effectVol > 0)
        {
            masterSound.effectVol -= 0.1f;
            masterSound.SetVol();
            SetUpdate();
        }
    }
    public void FieldVolChangeP()
    {
        if (masterSound.fieldVol < 1.0f)
        {
            masterSound.fieldVol += 0.1f;
            masterSound.SetVol();
            SetUpdate();
        }
    }
    public void FieldVolChangeM()
    {
        if (masterSound.fieldVol > 0)
        {
            masterSound.fieldVol -= 0.1f;
            masterSound.SetVol();
            SetUpdate();
        }
    }
    #endregion

    // 스킬 보드 관련 코드
    #region    
    public void OpenSkills() // 스킬 창 열기
    {
        if (skillopen)
        {
            CloseAll();
            skillopen = false;
            SkillUpdate(skillDic.selectedSkill);
            skillUI.SetActive(true);
        }
        else
        {
            CloseAll();
        }
    }
    public void SkillUpdate(int num) // 아이콘 클릭 시 스킬 업데이트
    {
        clicked = num;
        skillName.text = skillDic.skills[num].name;
        skillinfo.text = "데미지 : 공격력 + " + skillDic.skills[num].damage
                       + "   MP : " + skillDic.skills[num].requireMP
                       + "   쿨타임 : " + skillDic.skills[num].coolTime;
        skillDesc.text = skillDic.skills[num].skillExplane;
    }
   
    public void SelectSkill() // 선택 스킬 적용
    {
        skillDic.selectedSkill = clicked;
        skillDic.IconSet();
        selectedIcon.sprite = skillDic.skills[skillDic.selectedSkill].Icon;
        selected.SetActive(true);
    }


    #endregion

    // 스텟 보드 관련 코드
    #region 
    public void OpenStats() // 스탯 창 열기
    {
        if (statopen)
        {
            CloseAll();
            statopen = false;
            statsUpdate();
            statUI.SetActive(true);
        }
        else
        {
            CloseAll();
        }
    }
    private void statsUpdate() // 스텟 창 업데이트
    {
        hpText.text = "체력 : " + playerStats.MaxHP + " / " + playerStats.CurrentHP;
        mpText.text = "마나 : " + playerStats.MaxMP + " / " + playerStats.CurrentMP;
        atkText.text = "공격력 : " + playerStats.ATK.ToString();
        walletText.text = "골드 : " + playerStats.Wallet.ToString();
    }

    public void HPUP()
    {
        if (playerStats.WalletChange(-10))
        {
            playerStats.MaxHP += 5;
            playerStats.CurrentHP += 5;
            playerStats.HPUpdate();
            statsUpdate();

            Vector3 camera = Camera.main.WorldToScreenPoint(playerStats.transform.position);
            GameObject prefab = Instantiate(announceText);
            prefab.GetComponent<TextFloating>().TextSet("최대 체력이 상승했습니다.", Color.green, camera);
        }
        else
        {
            Vector3 camera = Camera.main.WorldToScreenPoint(playerStats.transform.position);
            GameObject prefab = Instantiate(announceText);
            prefab.GetComponent<TextFloating>().TextSet("골드가 부족합니다.", Color.red, camera);
        }
    }

    public void MPUP()
    {
        if (playerStats.WalletChange(-10))
        {
            playerStats.MaxMP += 10;
            playerStats.CurrentMP += 10;
            playerStats.MPUpdate();
            statsUpdate();

            Vector3 camera = Camera.main.WorldToScreenPoint(playerStats.transform.position);
            GameObject prefab = Instantiate(announceText);
            prefab.GetComponent<TextFloating>().TextSet("최대 마나가 상승했습니다.", Color.green, camera);
        }
        else
        {
            Vector3 camera = Camera.main.WorldToScreenPoint(playerStats.transform.position);
            GameObject prefab = Instantiate(announceText);
            prefab.GetComponent<TextFloating>().TextSet("골드가 부족합니다.", Color.red, camera);
        }
    }

    public void ATKUP()
    {
        if (playerStats.WalletChange(-10))
        {
            playerStats.ATK += 1;
            statsUpdate();

            Vector3 camera = Camera.main.WorldToScreenPoint(playerStats.transform.position);
            GameObject prefab = Instantiate(announceText);
            prefab.GetComponent<TextFloating>().TextSet("공격력이 상승했습니다.", Color.green, camera);
        }
        else
        {
            Vector3 camera = Camera.main.WorldToScreenPoint(playerStats.transform.position);
            GameObject prefab = Instantiate(announceText);
            prefab.GetComponent<TextFloating>().TextSet("골드가 부족합니다.", Color.red, camera);
        }
    }
    #endregion
}
