using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //속력
    public float bulletSpeed = 10;

    //폭발효과 공장
    public GameObject exploFactory;
    void Start()
    {
        
    }

    void Update()
    {
        //앞으로 간다!! P = P0 + vt
        transform.position += transform.forward * bulletSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        //폭발효과 만든다.
        GameObject explo = Instantiate(exploFactory);
        //폭발효과를 나의 위치로 놓는다.
        explo.transform.position = transform.position;
        //2초뒤에 폭발효과 파괴
        Destroy(explo, 2);

        //나 자신을 파괴한다.
        Destroy(gameObject);
    }
}
