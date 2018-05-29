using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField]
    private float m_Speed = 0.0f;
    private bool m_Explode = false;
    private bool m_Destroy = false;
    [SerializeField]
    private bool m_FacingRight = true;
    [SerializeField]
    private GameObject m_Explosion;
    private float m_LifeTime = 0.0f;
    private float m_LifeSpan = 500.0f;

    // Use this for initialization
    void Awake ()
    {

    }

    void Start()
    {
        // Set scale to determine direction
        if (!m_FacingRight)
        {
            transform.localScale = new Vector3(-1f, 1f,1f);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {

        // Update lifetime
        m_LifeTime = Time.time;

        if (m_LifeTime >= m_LifeSpan)
        {
            GameObject tmpExplosion = (GameObject)Instantiate(m_Explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        // TODO: Set directon based on turret direction
        if (m_FacingRight)
        {
            transform.Translate(Vector3.right * m_Speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(-Vector3.right * m_Speed * Time.deltaTime);
        }

	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        // Set explosion true when missile hits rigidBody
        // Instantiate the explosion game object at the same position as missile
        GameObject tmpExplosion = (GameObject)Instantiate(m_Explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public bool GetDestroy() { return m_Destroy; }
    public bool GetFacingRight() { return m_FacingRight; }
    public void SetFacingRight(bool right) { m_FacingRight = right; }
}
