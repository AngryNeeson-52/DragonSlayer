using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 플레이어 공격, 피격, 전투관련

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
    //빌드시 주석처리할것
    #region
    void Update() // 컴퓨터용
    {
        if (canAttack)
        {
            BasicAttack();
            SkillAttack();
        }
    }

    void BasicAttack() // 기본 공격 컴퓨터용
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

    void SkillAttack() // 스킬 공격 컴퓨터용
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

    IEnumerator CoolTimeCoroutine(float coolTime, bool basic) // 공격 쿨타임
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

    IEnumerator AttackCoroutine(float attackTime, bool basic) // 공격 판정, 공격력 조정
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

    void UpdateNaturalRecovery() // 전투 상태 지속시 자연회복 초기화
    {
        if (naturalRecovery != null)
        {
            StopCoroutine(naturalRecovery);
        }

        naturalRecovery = StartCoroutine(NaturalRecovery());
    }

    IEnumerator NaturalRecovery() // 자연회복, 전투 상태 갱신
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

    public void GetHIt(float damage) // 피격 처리
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

    IEnumerator GetHitCoroutine() // 피격 무적 시간
    {
        playerMove.GetHit();
        yield return waittime2;
        playerMove.CanMove();
        joystick.CanMove();
        UpdateNaturalRecovery();
        playerHitBox.SetActive(true);
        canAttack = true;
    }

    private void NoMana() // 마나 부족 알림
    {
        if (announceFreq)
        {
            announceFreq = false;
            Vector3 camera = Camera.main.WorldToScreenPoint(this.transform.position);
            GameObject prefab = Instantiate(dmgText);
            prefab.GetComponent<TextFloating>().TextSet("마나가 부족합니다", Color.green, camera);
            StartCoroutine(AnnounceFreqCoroutine());
        }
    }

    IEnumerator AnnounceFreqCoroutine() // 알림 주기 설정
    {
        yield return waittime2;
        announceFreq = true;
    }


    // 모바일
    #region 
    public void mobileBasicAttack() // 공격 버튼
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

    public void mobileSkillAttack() // 스킬 공격
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
