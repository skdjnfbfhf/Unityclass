using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //플레이어의 이동속도
    private float speed = 5f;
    private float jumpForce = 40f;

    private int jumpCnt = 0;

    //Vector3 : 3차원 공간을 나타내기 위한 구조체. 3D 공간에서 위치, 방향 등을 표현하는데 사용
    private Vector3 movement;

    //Rigidbody : 오브젝트가 물리적으로 동작하게 함. 중력의 영향을 받고 오브젝트가 사실적으로 움직이게 함
    private Rigidbody rigid;

    private void Start()
    {
        //Rigidbody의 속성에 접근하기
        rigid = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        //Input.GetAxis : 수평이나 수직 버튼의 입력을 받으면 -1f ~ 1f의 값을 반환
        //Horizontal : 키보드 A,D의 값
        //Vertical : 키보드 W,S의 값
        movement.x = Input.GetAxis("Horizontal");
        movement.z = Input.GetAxis("Vertical");

        //Normalize : 벡터를 정규화 해줌
        movement.Normalize();

        //rigid.velocity 즉 rigid의 속도에 movement(키보드 입력값) * 속도를 곱한다.
        rigid.velocity = movement * speed;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCnt < 1)
        {
            rigid.AddForce(Vector3.up * jumpForce,ForceMode.Impulse);
            jumpCnt++;
        }
        Physics.gravity = new Vector3(0,-50f,0);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            jumpCnt = 0;
        }
    }
}
