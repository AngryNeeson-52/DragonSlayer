using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �÷��̾� �̵� ó��

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private GameObject player, attackSound;
    [SerializeField]
    private Rigidbody rigid;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private float jumpForce, rotateSpeed, playerSpeedFW, playerSpeedetc;


    private bool cpt = true; // ���콺 ȸ�� ���� ��ư ��ǻ�Ϳ�

    private Vector3 leftright, frontback, direction, checkRay = new Vector3(0, 0.1f, 0);
    private bool canMove = true, land;

    void Update()
    {
        /*    */
        if (Input.GetKeyDown(KeyCode.F))
        {
            cpt = !cpt;
        }

        if (cpt)
        {
            Rotation();
        }

        if (canMove)
        {
            Move();
            Jump();
        }
    }

    /*    */
    //����� �ּ�ó���Ұ�
    #region
    void Rotation() // �ü� ȸ�� ��ǻ�Ϳ�
    {
        if (Input.GetAxisRaw("Mouse X") != 0)
        {
            float moveAngle = player.transform.eulerAngles.y + Input.GetAxisRaw("Mouse X") * rotateSpeed * Time.deltaTime;

            player.transform.eulerAngles = new Vector3(0f, moveAngle, 0f);
        }
    }

    void Move() // �̵� ��ǻ�Ϳ�
    {

        if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
        {
            leftright = player.transform.right * Input.GetAxisRaw("Horizontal");
            frontback = player.transform.forward * Input.GetAxisRaw("Vertical");
            direction = (leftright + frontback).normalized;

            anim.SetFloat("Vertical", Input.GetAxisRaw("Vertical"));
            anim.SetFloat("Horizontal", Input.GetAxisRaw("Horizontal"));
            anim.SetBool("Moving", true);

            if (Input.GetAxisRaw("Vertical") > 0)
            {
                player.transform.position += direction * playerSpeedFW * Time.deltaTime;
            }
            else
            {
                player.transform.position += direction * playerSpeedetc * Time.deltaTime;
            }
        }
        else
        {
            anim.SetBool("Moving", false);
        }
    }
    #endregion

    void Jump() // ��ǻ�Ϳ� ������ �ٴ� Ȯ��
    {
        if ((Physics.Raycast(transform.position + checkRay, Vector3.down, 0.2f)))
        {
            if(land)
            {
                land = false;
                anim.SetBool("OnAir", false);
                attackSound.GetComponent<SoundDic>().PlaySound("Land");
            }

            /**/
            if (Input.GetKeyDown(KeyCode.Space)) // ��ǻ�Ϳ� ����� �ּ�ó��
            {
                anim.SetTrigger("Jump");
                rigid.velocity = transform.up * jumpForce;
            }

        }
        else
        {
            anim.SetBool("OnAir", true);
            land = true;
        }
    }

    public void CanMove()
    {
        canMove = true;
    }

    public void GetHit() // �ǰ� �ִϸ��̼�
    {
        canMove = false;
        anim.SetBool("Battle", true);
        anim.SetTrigger("GetHit");
        attackSound.GetComponent<SoundDic>().PlaySound("HitSound");
    }

    public void Attack(float AttackType) // ���� �ִϸ��̼�
    {
        canMove = false;
        anim.SetFloat("AttackType", AttackType);
        anim.SetTrigger("Attack");
        attackSound.GetComponent<SoundDic>().PlaySound(AttackType.ToString());
        anim.SetBool("Battle", true);
    }

    public void Peace() // ������ �ִϸ��̼�
    {
        anim.SetBool("Battle", false);   
    }

    public void Dead() // ��� �ִϸ��̼�
    {
        anim.SetBool("Dead", true);
        anim.SetTrigger("GetHit");
    }


    // �����
    #region 
    public void mobileJump() // ���� ��ư
    {
        if ((Physics.Raycast(transform.position + checkRay, Vector3.down, 0.2f)) && canMove)
        {
            anim.SetTrigger("Jump");
            rigid.velocity = transform.up * jumpForce;
        }
    }
    #endregion

}
