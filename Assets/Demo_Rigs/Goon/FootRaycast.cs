using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootRaycast : MonoBehaviour
{
    private float distanceBetweenGroundAndIK = 0;

    private Quaternion startingRot;

    private Vector3 groundPosition;
    private Quaternion groundRotation;

    public float raycastLength = 2;

    // Start is called before the first frame update
    void Start()
    {
        distanceBetweenGroundAndIK = transform.localPosition.y;
        startingRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        





    }

    private void FindGround()
    {
        Vector3 origin = transform.position + Vector3.up * raycastLength / 2;
        Vector3 direction = Vector3.down;



        Debug.DrawRay(origin, direction * raycastLength, Color.blue);

        if (Physics.Raycast(origin, direction, out RaycastHit hitInfo, raycastLength))
        {
            groundPosition = hitInfo.point + Vector3.up * distanceBetweenGroundAndIK;

            Quaternion worldNeutral = transform.parent.rotation * startingRot;

            groundRotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal) * worldNeutral;
        }
    }
}
