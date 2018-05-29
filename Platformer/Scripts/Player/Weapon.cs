using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Attributes
    public float FireRate;
    public float Damage;
    public LayerMask WhatToHit;

    PlayerScore m_PlayerScore;

    [SerializeField] Transform m_BulletTrailPrefab;
    [SerializeField] Transform m_MuzzleFlashPrefab;

    float m_TimeToFire;
    Transform m_FirePoint;

    // Used to get reference to component attatched to object
    private void Awake()
    {
        m_FirePoint = transform.Find("FirePoint");
        if (m_FirePoint == null)
        {
            Debug.LogWarning("Could not find 'FirePoint' transform.");
        }

        m_PlayerScore = GetComponentInParent<PlayerScore>();
        if (m_PlayerScore == null)
        {
            Debug.LogWarning("Could not find 'PlayerScore' script object.");
        }
    }

    // Runs once a frame
    private void Update()
    {
        if (FireRate == 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetButton("Fire1") && Time.time > m_TimeToFire)
            {
                m_TimeToFire = Time.time + 1 / FireRate;
                Shoot();
            }
        }
    }

    // Use raycasting to 'shoot' bullet and check if we hit something
    void Shoot()
    {
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                    Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 firePointPosition = new Vector2(m_FirePoint.position.x, m_FirePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition-firePointPosition, 100, WhatToHit);
        ShootEffect();
        Debug.DrawLine(firePointPosition, (mousePosition-firePointPosition) * 100, Color.cyan);

        if (hit.collider != null)
        {
            Debug.DrawLine(firePointPosition, hit.point, Color.red);
            Debug.Log(hit.collider.tag + " Was hit. " + Damage + " Damage done");
            m_PlayerScore.UpdateScore(5);
        }
    }

    // Creates Muzzle flash and bullet trail shooting fx
    void ShootEffect()
    {
        // Create Bullet trail
        Instantiate(m_BulletTrailPrefab, m_FirePoint.position, m_FirePoint.rotation);
        Transform clone = Instantiate(m_MuzzleFlashPrefab, m_FirePoint.position, m_FirePoint.rotation);
        clone.parent = m_FirePoint;
        float size = Random.Range(0.6f, 0.9f);
        clone.localScale = new Vector3(size, size);
        Destroy(clone.gameObject, 0.02f);
    }

}
