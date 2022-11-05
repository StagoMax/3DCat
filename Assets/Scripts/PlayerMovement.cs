using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //����һ��3Dʸ��������ʾ����ƶ�
    Vector3 m_Movement;
    //��ȡ�û����뷽��
    float horizontal;
    float vertical;
    float moveSpeed;
    //����һ�� ����
    Rigidbody m_Rigidbody;
    Animator m_Animator;

    string yejian = "laiyejian";

    AudioSource m_AudioSource;
    //����Ԫ����ʾ3d��Ϸ�е���ת ��ʼ������ת�Ƕ�Ϊ0
    Quaternion m_Rotation=Quaternion.identity;
    //��ת�ٶȣ�*time��deltatime������ת��������Ƕ�
    public float turnspeed = 20f;
    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_AudioSource = GetComponent<AudioSource>();
        moveSpeed = 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }
    private void FixedUpdate()
    {
        m_Movement.Set(horizontal, 0.0f, vertical);
        m_Movement.Normalize();

        bool hasHorizontal=!Mathf.Approximately(horizontal, 0.0f);
        bool hasVertical=!Mathf.Approximately(vertical, 0.0f);
        bool isWalking = hasHorizontal || hasVertical;

        m_Animator.SetBool("isWalking",isWalking);
        //�����ת����
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnspeed * Time.deltaTime, 0f);
        m_Rotation=Quaternion.LookRotation(desiredForward);

        if (isWalking)
        {
            if(!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
            else
            {
                m_AudioSource.Stop();
            }
        }
    }
    private void OnAnimatorMove()
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude * moveSpeed);
        m_Rigidbody.MoveRotation(m_Rotation);
    }
}
//TODO:
//1.��ȡ������룬�ڳ������ƶ���ҽ�ɫ
//2.�����ƶ���Ҫ����ת��
//3.����Ҫ���Ƕ���
