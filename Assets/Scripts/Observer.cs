using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public Transform player;

    bool m_IsPlayerInRange;
    public MyGameEnding gameEnding;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(m_IsPlayerInRange)
        {
            Vector3 direction = player.position - transform.position + Vector3.up;

            Ray ray =new Ray(transform.position, direction);

            RaycastHit raycastHit;
            if (Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.transform == player)
                {
                    gameEnding.CaughtPlayer();
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"ontriggerenter is {other.gameObject.name}");
        if(other.transform==player)
        {
            m_IsPlayerInRange = true;
        }
    }
}
