using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ETControler : MonoBehaviour
{
    [SerializeField] Transform Target;
    [SerializeField] bool CanMove = true;
    [SerializeField] float MoveSpeed;
    [SerializeField] float RotationSpeed;
    [SerializeField] LayerMask CanStepOn;

    void Update()
    {
        if(CanMove) Move();
        Rotate();
    }

    void Move()
    {
        //Moving in X and Z axis
        float distanceToMoved = MoveSpeed * Time.deltaTime;
        transform.Translate(transform.forward * distanceToMoved, Space.World);
        
        //Moving in the Y axis
        RaycastHit downHit;
        if (Physics.Raycast(transform.position + new Vector3(0, 100, 0), Vector3.down, out downHit, 500, CanStepOn))
            transform.position = new Vector3(transform.position.x, downHit.point.y, transform.position.z);
    }

    void Rotate()
    {
        // Determine which direction to rotate towards
        Vector3 targetDirection = new Vector3(Target.position.x, transform.position.y, Target.position.z) - transform.position; //The Y position of the target is being switched for the Y position of this transform, to make sure this transform never points up or down.

        // The step size is equal to speed times frame time.
        float singleStep = RotationSpeed * Mathf.Deg2Rad * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}
