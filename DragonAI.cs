using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 드래곤 보스 행동 관련

public class DragonAI : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy, tail, fireBall, stomp, bodySlam, breath, bossHealth, bossWall, fieldSound, endPortal;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private Rigidbody rigid;
    [SerializeField]
    private float moveSpeed, attackPattern, tailRange, basicRange, flySpeed, fireSpeed, turnSpeed;
    [SerializeField]
    private AudioSource enemySound;
    [SerializeField]
    private string bgmSelect;

    private Vector3 dir, attackPos;
    WaitForSeconds waittime = new WaitForSeconds(3.0f);
    WaitForSeconds waittime1 = new WaitForSeconds(0.5f);

    public void GetPlayer(Collider player) // 드래곤 행동 시작
    {
        bossWall.SetActive(true);
        StartCoroutine(DragonCoroutine(player));
    }


    IEnumerator DragonCoroutine(Collider player) // 드래곤 행동 패턴
    {
        anim.SetBool("Aggro", true);
        enemy.transform.LookAt(player.transform);
        yield return new WaitForSeconds(6.0f);

        bossHealth.SetActive(true);

        while (true)
        {
            //attackPattern = 1;

            if (attackPattern == 0) // 꼬리 공격
            {
                dir = new Vector3(player.transform.position.x, 0, player.transform.position.z) - enemy.transform.position;
                while (Vector3.Distance(player.transform.position, enemy.transform.position) > tailRange)
                {
                    anim.SetBool("Moving", true);
                    dir = new Vector3(player.transform.position.x, 0, player.transform.position.z) - enemy.transform.position;
                    enemy.transform.rotation = Quaternion.Lerp(enemy.transform.rotation, Quaternion.LookRotation(dir), turnSpeed * Time.deltaTime);
                    enemy.transform.position += enemy.transform.forward * moveSpeed * Time.deltaTime;

                    yield return null;
                }

                while (Quaternion.Angle(enemy.transform.rotation, Quaternion.LookRotation(dir)) >= 5)
                {
                    anim.SetBool("Moving", true);
                    dir = new Vector3(player.transform.position.x, 0, player.transform.position.z) - enemy.transform.position;
                    enemy.transform.rotation = Quaternion.Lerp(enemy.transform.rotation, Quaternion.LookRotation(dir), turnSpeed * Time.deltaTime);
                    enemy.transform.position -= enemy.transform.forward * moveSpeed * Time.deltaTime;

                    yield return null;
                }

                anim.SetBool("Moving", false);
                anim.SetFloat("AttackType", 0);
                anim.SetTrigger("Attack");
                enemy.GetComponent<SoundDic>().PlaySound("Roar");
                yield return waittime;
            }
            else if (attackPattern == 1) // 브레스
            {
                dir = new Vector3(player.transform.position.x, 0, player.transform.position.z) - enemy.transform.position;
                while (Vector3.Distance(player.transform.position, enemy.transform.position) > basicRange)
                {
                    anim.SetBool("Moving", true);
                    dir = new Vector3(player.transform.position.x, 0, player.transform.position.z) - enemy.transform.position;
                    enemy.transform.rotation = Quaternion.Lerp(enemy.transform.rotation, Quaternion.LookRotation(dir), turnSpeed * Time.deltaTime);
                    enemy.transform.position += enemy.transform.forward * moveSpeed * Time.deltaTime;

                    yield return null;
                }

                while (Quaternion.Angle(enemy.transform.rotation, Quaternion.LookRotation(dir)) >= 5)
                {
                    anim.SetBool("Moving", true);
                    dir = new Vector3(player.transform.position.x, 0, player.transform.position.z) - enemy.transform.position;
                    enemy.transform.rotation = Quaternion.Lerp(enemy.transform.rotation, Quaternion.LookRotation(dir), turnSpeed * Time.deltaTime);
                    enemy.transform.position -= enemy.transform.forward * moveSpeed * Time.deltaTime;

                    yield return null;
                }

                anim.SetBool("Moving", false);
                anim.SetFloat("AttackType", 1);
                anim.SetTrigger("Attack");
                enemy.GetComponent<SoundDic>().PlaySound("Breath");
                yield return waittime1;
                yield return waittime1;

                GameObject prefab = Instantiate(fireBall);
                prefab.transform.position = breath.transform.position;
                dir = player.transform.position - breath.transform.position;
                prefab.GetComponent<Rigidbody>().AddForce(dir * fireSpeed);

                yield return waittime;
            }
            else // 스톰프
            {
                //  날기
                rigid.GetComponent<Collider>().enabled = false;
                enemy.GetComponent<SoundDic>().PlaySound("Fly");
                anim.SetTrigger("Fly");

                for(float i = 0; i < 3; i += Time.deltaTime)
                {
                    dir = player.transform.position - enemy.transform.position;
                    enemy.transform.rotation = Quaternion.Lerp(enemy.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime);
                    if (Vector3.Distance(player.transform.position, enemy.transform.position) < 10)
                    {
                        enemy.transform.position -= enemy.transform.forward * moveSpeed * Time.deltaTime;
                    }
                    yield return null;
                }

                attackPos = new Vector3(player.transform.position.x, -3, player.transform.position.z);
                yield return waittime1;

                //  대시
                anim.SetTrigger("Dash");
                bodySlam.SetActive(true);
                while (Vector3.Distance(attackPos, enemy.transform.position) > 1.5)
                {
                    enemy.transform.position = Vector3.Lerp(enemy.transform.position, attackPos, flySpeed * Time.deltaTime);
                    yield return null;
                }
                bodySlam.SetActive(false);

                //  착지
                enemy.GetComponent<SoundDic>().PlaySound("Crash");
                anim.SetTrigger("Crash");
                rigid.GetComponent<Collider>().enabled = true;
                enemy.transform.position = new Vector3(enemy.transform.position.x, 0, enemy.transform.position.z);
                enemy.transform.LookAt(new Vector3(attackPos.x, 0, attackPos.z));

                stomp.SetActive(true);
                yield return waittime1;
                stomp.SetActive(false);
            }

            yield return waittime;
            attackPattern = Random.Range(0, 3);
        }
    }

    private void OnDisable()
    {
        if (bossWall != null) // 유니티 자체 오류
        {
            bossWall.SetActive(false);
        }
        if (endPortal != null) // 유니티 자체 오류
        {
            endPortal.SetActive(true);
        }
        if (fieldSound != null) // 유니티 자체 오류
        {
            fieldSound.GetComponent<SoundDic>().PlaySound(bgmSelect);
        }
    }
}
