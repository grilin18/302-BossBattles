using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class GoonMovement : MonoBehaviour
{
    public FootRaycast footLeft;
    public FootRaycast footRight;
    
    public float speed = 2;
    public float walkSpreadX = .4f;
    public float walkSpreadY = .4f;
    public float walkSpreadZ = .8f;
    public float walkFootSpeed = 4;

    private CharacterController pawn;


    // Start is called before the first frame update
    void Start()
    {
        pawn = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        Vector3 move = transform.forward * v + transform.right * h;
        if (move.sqrMagnitude > 1) move.Normalize();

        pawn.SimpleMove(move * speed);

        // animate feet
        AnimateWalk();

    }

    delegate void MoveFoot(float time, float x, FootRaycast foot);

    void AnimateWalk()
    {
        MoveFoot moveFoot = (t, x, foot) => {
            float y = Mathf.Cos(t) * walkSpreadY;
            float z = Mathf.Sin(t) * walkSpreadZ;

            if (y < 0) y = 0;

            foot.transform.localPosition = new Vector3(x, y, z);
        };

        float t = Time.time * walkFootSpeed;

        moveFoot.Invoke(t, -walkSpreadX, footLeft);
        moveFoot.Invoke(t + Mathf.PI, walkSpreadX, footRight);

    }
}
