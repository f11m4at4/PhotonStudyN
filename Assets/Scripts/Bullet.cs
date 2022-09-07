using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //�ӷ�
    public float bulletSpeed = 10;

    //����ȿ�� ����
    public GameObject exploFactory;
    void Start()
    {
        
    }

    void Update()
    {
        //������ ����!! P = P0 + vt
        transform.position += transform.forward * bulletSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        //����ȿ�� �����.
        GameObject explo = Instantiate(exploFactory);
        //����ȿ���� ���� ��ġ�� ���´�.
        explo.transform.position = transform.position;
        //2�ʵڿ� ����ȿ�� �ı�
        Destroy(explo, 2);

        //�� �ڽ��� �ı��Ѵ�.
        Destroy(gameObject);
    }
}
