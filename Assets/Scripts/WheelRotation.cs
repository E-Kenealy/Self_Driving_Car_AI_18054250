using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotation : MonoBehaviour
{
    public WheelCollider correspondingWheelCollider;//Allows user to select correct wheel in Unity

    private Vector3 wheelPosition = new Vector3();
    private Quaternion wheelRotation = new Quaternion();

    // Update is called once per frame
    private void Update()
    {
        correspondingWheelCollider.GetWorldPose(out wheelPosition, out wheelRotation); //Outputs the wheel collider to the new Vec3 and Qaurternion
        transform.position = wheelPosition;
        transform.rotation = wheelRotation;

    }
}
