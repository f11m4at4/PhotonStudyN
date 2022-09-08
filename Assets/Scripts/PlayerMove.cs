using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


//��, CharacterController�� ���

public class PlayerMove : MonoBehaviourPun
{
    //�ӷ�
    public float moveSpeed = 5;
    //characterController ���� ����
    CharacterController cc;

    //�߷�
    float gravity = -9.81f;
    //�����Ŀ�
    public float jumpPower = 5;
    //y���� �ӷ�
    float yVelocity;


    void Start()
    {        
        //characterController �� ����
        cc = GetComponent<CharacterController>();
        //����ü���� �ִ�ü������ ����
        currHp = maxHp;
    }

    void Update()
    {
        //���࿡ ������ �ƴ϶�� �Լ��� �����ڴ�.
        if (photonView.IsMine == false) return;

        // WSAD�� ������ ��,��,��,��� �̵�
        //1. WSAD�� ��ȣ�� ����.
        float h = Input.GetAxisRaw("Horizontal"); //A : -1, D : 1, ������ ������ : 0
        float v = Input.GetAxisRaw("Vertical");

        //2. ���� ��ȣ�� ������ �����.
        Vector3 dir = transform.forward * v + transform.right * h; // new Vector3(h, 0, v);
        //������ ũ�⸦ 1���Ѵ�.
        dir.Normalize();

        //���࿡ �ٴڿ� ����ִٸ� yVelocity�� 0���� ����
        if (cc.isGrounded)
        {
            yVelocity = 0;
        }

        //���࿡ �����̹�(Jump)�� ������
        if (Input.GetButtonDown("Jump"))
        {
            //yVelocity�� jumpPower�� ����
            yVelocity = jumpPower;
        }      

        //yVelocity���� �߷����� ���ҽ�Ų��.
        yVelocity += gravity * Time.deltaTime;

        //dir.y�� yVelocity���� ����
        dir.y = yVelocity;

        //3. �� �������� ��������.
        //P = P0 + vt
        cc.Move(dir * moveSpeed * Time.deltaTime);
    }


    //���� ü��
    public int maxHp = 10;
    public int currHp;
    //�ǰݵǾ��� �� ȣ��Ǵ� �Լ�
    public void OnDamaged()
    {
        //1. ���� ü�� 1 �ٿ��ְ�
        currHp--;
        print("����ü�� : " + currHp);
        //2. ���࿡ ���� ü���� 0���� ���ų� �۾�����
        if(currHp <= 0)
        {
            //3. ���� �ı��Ѵ�.
            Destroy(gameObject);
        }
    }
}
