using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public GameObject target;
    public float offset = -0.1f;

    public LayerMask mask;
    private void Update()
    {
        PosOnHit();
    }
    public void PosOnHit()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward * 100, Color.yellow);

        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 100, mask))
        {
            target.transform.rotation = Quaternion.LookRotation( hit.normal);
            target.transform.position = hit.point + hit.normal * offset;
        }
    }
}
