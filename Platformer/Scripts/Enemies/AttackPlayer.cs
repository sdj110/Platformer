using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AttackPlayer : MonoBehaviour
{
    // Attributes
    [SerializeField] int m_Damage;
    // Components
    Rigidbody2D m_RGB;

    // Get Reference to Component
    private void Awake()
    {
        m_RGB = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.CompareTag("Player"))
        {
            obj.GetComponent<PlayerHealth>().ReduceHealth(m_RGB.velocity, m_Damage);
            MoveBack();
        }
    }

    // Apply Backwards force
    void MoveBack()
    {
        m_RGB.AddForce(-m_RGB.velocity * 10, ForceMode2D.Impulse);
    }
}
