using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class ControlAnimation : MonoBehaviour
{
    // Attributes
    Vector2 m_LastPosition;

    // Components
    Rigidbody2D m_RGB;
    Animator m_Animator;

    // Used to get reference to component attatched to object
    void Awake()
    {
        m_RGB = GetComponentInParent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
    }

    // Initialize
    void Start()
    {
        m_LastPosition = Vector2.zero;   
    }

    // Update Animator properties
    public void UpdateAnimator(bool moving, bool jumping)
    {
        m_Animator.SetBool("IsMoving", moving);
        m_Animator.SetBool("IsJumping", jumping);
    }
}
