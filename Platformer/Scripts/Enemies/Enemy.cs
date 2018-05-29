using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private Animator m_Anim;

    private bool m_Firing = true;

    private float m_Time = 0f;


    [SerializeField]
    private float m_FireRate = 3f;

    [SerializeField]
    private float m_StartDelay = 3f;

    [SerializeField]
    private bool m_FacingRight;

    [SerializeField]
    private GameObject m_Missile;

    private void Awake()
    {
        m_Anim = GetComponent<Animator>();
    }

    void Start()
    {
        InvokeRepeating("FireProjectile", m_StartDelay, m_FireRate);
    }
	
	// Update is called once per frame
	void Update ()
    {
        // Update time
        m_Time = Time.time;
        
        // Set animation
        m_Anim.SetBool("Firing", m_Firing);
    }

    void FireProjectile()
    {
        m_Firing = !m_Firing;

        if (m_Firing)
        {
            if (m_FacingRight)
            {
                // Change direction of missile
                Quaternion dir = Quaternion.Euler(0, 0, -180);
                GameObject tmpMissile = (GameObject)Instantiate(m_Missile, transform.position, dir);
            }
            else
            {
                GameObject tmpMissile = (GameObject)Instantiate(m_Missile, transform.position, transform.rotation);
            }
        }
    }

    bool FireTime()
    {
        if (m_Time >= m_FireRate)
        {
            m_Time = Time.time;
            return true;
        }
        else
        {
            return false;
        }
    }
}
