using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private PlayerHealth m_PlayerHealth;
        private bool m_Jump;
        private bool m_Firing;

        // used to get reference to component attatched to GameObject
        private void Awake()
        {
            m_PlayerHealth = GetComponent<PlayerHealth>();
            m_Character = GetComponent<PlatformerCharacter2D>();
            m_Firing = false;
        }

        // Runs once a frame
        private void Update()
        {
            if (!m_Jump)
            {
                // Read the jump input in Update so button presses aren't missed.
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }

            // Set firing animation
            m_Firing = Input.GetKey(KeyCode.Mouse0);
        }

        // Used to update rigidbody and other physic components
        private void FixedUpdate()
        {
            // Read the inputs.
            bool crouch = Input.GetKey(KeyCode.LeftControl);
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            // Pass all parameters to the character control script.
            if (m_PlayerHealth.StunTime <= 0)
            {
                m_Character.Move(h, crouch, m_Jump);
            }
            else
            {
                m_PlayerHealth.StunTime -= Time.deltaTime;
            }
            
            m_Jump = false;
        }
    }
}
