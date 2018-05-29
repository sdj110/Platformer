using System.Collections;
using UnityEngine;

public class ArmRotation : MonoBehaviour
{
    [SerializeField] int m_RotationOffset;

    // Update is called once per frame
    void Update()
    {
        // Calculate the direction vector pointing from player to mouse
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize();

        float zRot = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, zRot + m_RotationOffset);
    }
}
