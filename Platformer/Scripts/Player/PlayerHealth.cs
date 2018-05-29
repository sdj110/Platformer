using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerHealth : MonoBehaviour
{
    // Attributes
    [SerializeField] private Transform m_StartPos;
    [SerializeField] private Text m_HealthText;
    [SerializeField] private RectTransform m_HealthBar;
    Transform m_PlayerGfx;
    PlayerScore m_PlayerScore;
    Rigidbody2D m_RGB;
    SpriteRenderer m_SpriteRenderer;
    float m_HealthBarStartSize;
    int m_Health;
    int m_MaxHealth;
    public float StunTime;
    bool m_Immune;
    
    // Used to get reference to components
    void Awake()
    {
        m_RGB = GetComponent<Rigidbody2D>();
        m_PlayerScore = GetComponent<PlayerScore>();
        m_PlayerGfx = transform.Find("gfx");
        if (m_PlayerGfx == null)
        {
            Debug.LogWarning("Could not find gfx child on player");
        }
        m_SpriteRenderer = m_PlayerGfx.GetComponent<SpriteRenderer>();
    }

    // Initialize 
    void Start()
    {
        m_Health = 100;
        m_MaxHealth = 100;
        StunTime = 0;
        m_Immune = false;
        m_HealthBarStartSize = m_HealthBar.sizeDelta.x;
        SetHealthBar();
    }

    // check if player fell off
    private void Update()
    {
        if (transform.position.y <= -9f)
        {
            Invoke("Die", 2);
        }
    }

    // Reduce player health by value
    public void ReduceHealth(Vector2 force, int value)
    {
        if (!m_Immune)
        {
            if (m_Health - value <= 0)
            {
                Invoke("Die", 2);
            }
            else
            {
                m_Health -= value;
                SetHealthBar();
                KnockBack(force.normalized);
            }
        }
    }

    // Update the health text
    void SetHealthBar()
    {
        m_HealthText.text = m_Health + "/" + m_MaxHealth;
        float scaleX = ((float)m_Health / (float)m_MaxHealth);
        m_HealthBar.sizeDelta = new Vector2(m_HealthBarStartSize * scaleX, m_HealthBar.sizeDelta.y);
    }

    void KnockBack(Vector2 force)
    {
        StunTime = 0.2f;
        m_RGB.AddForce(force * 2, ForceMode2D.Impulse);
    }

    // Reset health and health HUD to full
    void ResetHealth()
    {
        m_Health = m_MaxHealth;
        SetHealthBar();
    }

    // Reset Player for new game
    void Die()
    {
        StartCoroutine(FlashAlpha());
        m_RGB.velocity = Vector2.zero;
        ResetHealth();
        m_PlayerScore.ResetScore();
        transform.position = m_StartPos.position;
    }

    // to show that you have been hit the object will flash
    public IEnumerator FlashAlpha()
    {
        m_Immune = true;
        Color c = m_SpriteRenderer.color;
        for (int i = 0; i < 2; i++)
        {
            m_SpriteRenderer.color = new Color(c.r, c.g, c.b, 0.5f);
            yield return new WaitForSeconds(0.5f);
            m_SpriteRenderer.material.color = m_SpriteRenderer.color = new Color(c.r, c.g, c.b, 1);
            yield return new WaitForSeconds(0.5f);

        }
        m_Immune = false;
    }

}
