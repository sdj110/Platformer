using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private float m_Delay = 0.1f;

	// Use this for initialization
	void Start ()
    {
        Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + m_Delay);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
