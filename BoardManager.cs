using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// ���� �� ����(����, ��ų, ����) ���� �ڵ�

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

    //���� ���� ���� �ڵ�
    #region
    public void OpenSettings() // ���� â ����
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
    private void SetUpdate() // ���� â ������Ʈ
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

    // ��ų ���� ���� �ڵ�
    #region    
    public void OpenSkills() // ��ų â ����
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
    public void SkillUpdate(int num) // ������ Ŭ�� �� ��ų ������Ʈ
    {
        clicked = num;
        skillName.text = skillDic.skills[num].name;
        skillinfo.text = "������ : ���ݷ� + " + skillDic.skills[num].damage
                       + "   MP : " + skillDic.skills[num].requireMP
                       + "   ��Ÿ�� : " + skillDic.skills[num].coolTime;
        skillDesc.text = skillDic.skills[num].skillExplane;
    }
   
    public void SelectSkill() // ���� ��ų ����
    {
        skillDic.selectedSkill = clicked;
        skillDic.IconSet();
        selectedIcon.sprite = skillDic.skills[skillDic.selectedSkill].Icon;
        selected.SetActive(true);
    }


    #endregion

    // ���� ���� ���� �ڵ�
    #region 
    public void OpenStats() // ���� â ����
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
    private void statsUpdate() // ���� â ������Ʈ
    {
        hpText.text = "ü�� : " + playerStats.MaxHP + " / " + playerStats.CurrentHP;
        mpText.text = "���� : " + playerStats.MaxMP + " / " + playerStats.CurrentMP;
        atkText.text = "���ݷ� : " + playerStats.ATK.ToString();
        walletText.text = "��� : " + playerStats.Wallet.ToString();
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
            prefab.GetComponent<TextFloating>().TextSet("�ִ� ü���� ����߽��ϴ�.", Color.green, camera);
        }
        else
        {
            Vector3 camera = Camera.main.WorldToScreenPoint(playerStats.transform.position);
            GameObject prefab = Instantiate(announceText);
            prefab.GetComponent<TextFloating>().TextSet("��尡 �����մϴ�.", Color.red, camera);
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
            prefab.GetComponent<TextFloating>().TextSet("�ִ� ������ ����߽��ϴ�.", Color.green, camera);
        }
        else
        {
            Vector3 camera = Camera.main.WorldToScreenPoint(playerStats.transform.position);
            GameObject prefab = Instantiate(announceText);
            prefab.GetComponent<TextFloating>().TextSet("��尡 �����մϴ�.", Color.red, camera);
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
            prefab.GetComponent<TextFloating>().TextSet("���ݷ��� ����߽��ϴ�.", Color.green, camera);
        }
        else
        {
            Vector3 camera = Camera.main.WorldToScreenPoint(playerStats.transform.position);
            GameObject prefab = Instantiate(announceText);
            prefab.GetComponent<TextFloating>().TextSet("��尡 �����մϴ�.", Color.red, camera);
        }
    }
    #endregion
}
