using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FlashObjectAlpha : MonoBehaviour
{
    // Attributes
    SpriteRenderer m_SpriteRenderer;

    private void Awake()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // to show that you have been hit the object will flash
    public IEnumerator FlashAlpha()
    {
        Color c = m_SpriteRenderer.color;
        for (int i = 0; i < 3; i++)
        {
            m_SpriteRenderer.color = new Color(c.r, c.g, c.b, 0.5f);
            yield return new WaitForSeconds(0.1f);
            m_SpriteRenderer.material.color = m_SpriteRenderer.color = new Color(c.r, c.g, c.b, 1);
            yield return new WaitForSeconds(0.1f);

        }
    }
}
