using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //创建一个3D矢量，来表示玩家移动
    Vector3 m_Movement;
    //获取用户输入方向
    float horizontal;
    float vertical;
    float moveSpeed;
    //创建一个 刚体
    Rigidbody m_Rigidbody;
    Animator m_Animator;

    string yejian = "laiyejian";

    AudioSource m_AudioSource;
    //用四元数表示3d游戏中的旋转 初始化，旋转角度为0
    Quaternion m_Rotation=Quaternion.identity;
    //旋转速度，*time。deltatime就是旋转允许的最大角度
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
        //获得旋转向量
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
//1.获取玩家输入，在场景中移动玩家角色
//2.除了移动还要考虑转动
//3.还需要考虑动画
