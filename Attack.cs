using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �÷��̾� ����, �ǰ�, ��������

public class Attack : MonoBehaviour
{
    [SerializeField]
    private GameObject hitBoxATK, playerHitBox, dmgText;
    [SerializeField]
    private Image attackCool, skillCool;

    private PlayerMove playerMove;
    private PlayerStats playerStats;
    private SkillDic skillDic;
    private JoyStickL joystick;

    private bool canBasicAttack = true, canSkillAttack = true, combo = false, announceFreq = true, canAttack = true;

    Coroutine naturalRecovery;

    WaitForSeconds waittime = new WaitForSeconds(5.0f);
    WaitForSeconds waittime2 = new WaitForSeconds(0.5f);

    void Start()
    {
        playerMove = FindObjectOfType<PlayerMove>();
        playerStats = FindObjectOfType<PlayerStats>();
        skillDic = FindObjectOfType<SkillDic>();
        joystick = FindObjectOfType<JoyStickL>();

        UpdateNaturalRecovery();
    }
    /*    */
    //����� �ּ�ó���Ұ�
    #region
    void Update() // ��ǻ�Ϳ�
    {
        if (canAttack)
        {
            BasicAttack();
            SkillAttack();
        }
    }

    void BasicAttack() // �⺻ ���� ��ǻ�Ϳ�
    {
        if (Input.GetKeyDown(KeyCode.Q) && canBasicAttack)
        {
            if (playerStats.CurrentMPChange(skillDic.skills[skillDic.basicSkill].requireMP))
            {
                canAttack = false;
                canBasicAttack = false;

                if (combo)
                {
                    playerMove.Attack(0.5f);
                }
                else
                {
                    playerMove.Attack(skillDic.skills[skillDic.basicSkill].skillNum);
                }

                StartCoroutine(AttackCoroutine(skillDic.skills[skillDic.basicSkill].motiontime, true));
                UpdateNaturalRecovery();
                StartCoroutine(CoolTimeCoroutine(skillDic.skills[skillDic.basicSkill].coolTime, true));
            }
            else
            {
                NoMana();
            }
        }
    }

    void SkillAttack() // ��ų ���� ��ǻ�Ϳ�
    {
        if (Input.GetKeyDown(KeyCode.E) && canSkillAttack)
        {
            if (playerStats.CurrentMPChange(skillDic.skills[skillDic.selectedSkill].requireMP))
            {
                canAttack = false;
                canSkillAttack = false;

                playerMove.Attack(skillDic.skills[skillDic.selectedSkill].skillNum);

                StartCoroutine(AttackCoroutine(skillDic.skills[skillDic.selectedSkill].motiontime, false));
                UpdateNaturalRecovery();
                StartCoroutine(CoolTimeCoroutine(skillDic.skills[skillDic.selectedSkill].coolTime, false));
            }
            else
            {
                NoMana();
            }
        }
    }
    #endregion

    IEnumerator CoolTimeCoroutine(float coolTime, bool basic) // ���� ��Ÿ��
    {
        if (basic)
        {
            var runTime = 0.0f;
            attackCool.fillAmount = 1;
            while (runTime < coolTime)
            {
                runTime += Time.deltaTime;
                attackCool.fillAmount = 1 - runTime / coolTime;

                yield return null;
            }
            attackCool.fillAmount = 0;

            combo = !combo;
            canBasicAttack = true;
        }
        else 
        {
            var runTime = 0.0f;
            skillCool.fillAmount = 1;
            while (runTime < coolTime)
            {
                runTime += Time.deltaTime;
                skillCool.fillAmount = 1 - runTime / coolTime;

                yield return null;
            }
            skillCool.fillAmount = 0;

            combo = false;
            canSkillAttack = true;
        }
    }

    IEnumerator AttackCoroutine(float attackTime, bool basic) // ���� ����, ���ݷ� ����
    {
        playerStats.MPUpdate();

        if (!basic)
        {
            playerStats.ATK += skillDic.skills[skillDic.selectedSkill].damage;
        }

        hitBoxATK.SetActive(true);
        yield return new WaitForSeconds(attackTime);
        hitBoxATK.SetActive(false);

        if (!basic)
        {
            playerStats.ATK -= skillDic.skills[skillDic.selectedSkill].damage;
        }

        canAttack = true;
        playerMove.CanMove();
        joystick.CanMove();
    }

    void UpdateNaturalRecovery() // ���� ���� ���ӽ� �ڿ�ȸ�� �ʱ�ȭ
    {
        if (naturalRecovery != null)
        {
            StopCoroutine(naturalRecovery);
        }

        naturalRecovery = StartCoroutine(NaturalRecovery());
    }

    IEnumerator NaturalRecovery() // �ڿ�ȸ��, ���� ���� ����
    {
        while (true)
        {
            yield return waittime;
            playerMove.Peace();
            playerStats.CurrentHPChange(1.0f);
            playerStats.CurrentMPChange(1.0f);
            playerStats.HPUpdate();
            playerStats.MPUpdate();
        }
    }

    public void GetHIt(float damage) // �ǰ� ó��
    {
        playerHitBox.SetActive(false);
        canAttack = false;

        if (playerStats.CurrentHPChange(-damage))
        {
            playerStats.HPUpdate();
            StartCoroutine(GetHitCoroutine());
        }
        else
        {
            playerStats.HPUpdate();
            playerMove.Dead();
        }
    }

    IEnumerator GetHitCoroutine() // �ǰ� ���� �ð�
    {
        playerMove.GetHit();
        yield return waittime2;
        playerMove.CanMove();
        joystick.CanMove();
        UpdateNaturalRecovery();
        playerHitBox.SetActive(true);
        canAttack = true;
    }

    private void NoMana() // ���� ���� �˸�
    {
        if (announceFreq)
        {
            announceFreq = false;
            Vector3 camera = Camera.main.WorldToScreenPoint(this.transform.position);
            GameObject prefab = Instantiate(dmgText);
            prefab.GetComponent<TextFloating>().TextSet("������ �����մϴ�", Color.green, camera);
            StartCoroutine(AnnounceFreqCoroutine());
        }
    }

    IEnumerator AnnounceFreqCoroutine() // �˸� �ֱ� ����
    {
        yield return waittime2;
        announceFreq = true;
    }


    // �����
    #region 
    public void mobileBasicAttack() // ���� ��ư
    {
        if (canBasicAttack && canAttack)
        {
            if (playerStats.CurrentMPChange(skillDic.skills[skillDic.basicSkill].requireMP))
            {
                canAttack = false;
                canBasicAttack = false;
                joystick.CantMove();

                if (combo)
                {
                    playerMove.Attack(0.5f);
                }
                else
                {
                    playerMove.Attack(skillDic.skills[skillDic.basicSkill].skillNum);
                }

                StartCoroutine(AttackCoroutine(skillDic.skills[skillDic.basicSkill].motiontime, true));
                UpdateNaturalRecovery();
                StartCoroutine(CoolTimeCoroutine(skillDic.skills[skillDic.basicSkill].coolTime, true));
            }
            else
            {
                NoMana();
            }
        }
    }

    public void mobileSkillAttack() // ��ų ����
    {
        if (canSkillAttack && canAttack)
        {
            if (playerStats.CurrentMPChange(skillDic.skills[skillDic.selectedSkill].requireMP))
            {
                canAttack = false;
                canSkillAttack = false;
                joystick.CantMove();

                playerMove.Attack(skillDic.skills[skillDic.selectedSkill].skillNum);

                StartCoroutine(AttackCoroutine(skillDic.skills[skillDic.selectedSkill].motiontime, false));
                UpdateNaturalRecovery();
                StartCoroutine(CoolTimeCoroutine(skillDic.skills[skillDic.selectedSkill].coolTime, false));
            }
            else
            {
                NoMana();
            }
        }
    }
    #endregion
}
