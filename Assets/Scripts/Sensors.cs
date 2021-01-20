using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Sensors : MonoBehaviour
{

    [Header("Sensors")]
    public float frontSensorRange = 5.5f;
    public float sensorRange = 4.5f;
    public Vector3 frontSensorOrigin = new Vector3(0.0f, 0.2f, 0.5f);
    public float frontOffsetSensorOrigin = 0.45f;
    public float frontAngledSensorTheta = 30f;
    public float maxTurningAngle = 45f;

    private bool avoid = false;
    private float targetAngle = 0f;


    // Update is called once per frame
    private void FixedUpdate()
    {
        SensorArray();
    }
    private void SensorArray()
    {
        RaycastHit hit;
        Vector3 sensorOrigin = transform.position;

        //Moves sensors with car upon rotation
        sensorOrigin += transform.forward * frontSensorOrigin.z;
        sensorOrigin += transform.up * frontSensorOrigin.y;

        //Sets the state of the car to travel normallyin a straight line every update
        float torqueMod = 0f;
        avoid = false;

        //Front Right Sensor
        sensorOrigin += transform.right * frontOffsetSensorOrigin;
        if (Physics.Raycast(sensorOrigin, transform.forward, out hit, sensorRange))
        {
            if (hit.collider.CompareTag("Entity"))
            {
                avoid = true;
                torqueMod -= 1.0f;
                Debug.DrawLine(sensorOrigin, hit.point);
            }
        }

        //Front Right Angled Sensor
        else if (Physics.Raycast(sensorOrigin, Quaternion.AngleAxis(frontAngledSensorTheta, transform.up) * transform.forward, out hit, sensorRange)) //Creates a sensor to look what is at an angle out in front of the car
        {
            if (hit.collider.CompareTag("Entity"))
            {
                avoid = true;
                torqueMod -= 0.5f;
                Debug.DrawLine(sensorOrigin, hit.point);
            }
        }

        //Front Left Sensor
        sensorOrigin -= transform.right * frontOffsetSensorOrigin * 2; //Mulitiplied by 2 to get back to origin, then negative x values
        if (Physics.Raycast(sensorOrigin, transform.forward, out hit, sensorRange))
        {
            if (hit.collider.CompareTag("Entity"))
            {
                avoid = true;
                torqueMod += 1.0f;
                Debug.DrawLine(sensorOrigin, hit.point);
            }
        }

        //Front Left Angled Sensor
        else if (Physics.Raycast(sensorOrigin, Quaternion.AngleAxis(-frontAngledSensorTheta, transform.up) * transform.forward, out hit, sensorRange)) //Creates a sensor to look what is at an angle out in front of the car
        {
            if (hit.collider.CompareTag("Entity"))
            {
                avoid = true;
                torqueMod += 0.5f;
                Debug.DrawLine(sensorOrigin, hit.point);
            }
        }

        //Front Sensor
        if (torqueMod == 0)
        {
            if (Physics.Raycast(sensorOrigin, transform.forward, out hit, frontSensorRange))
            {
                if (hit.collider.CompareTag("Entity"))
                {
                    avoid = true;
                    if (hit.normal.x < 0) //If the normal of the object is angled to the left of the car...
                    {
                        torqueMod = -1; //...turn the car left to avoid it
                    }
                    else
                    {
                        torqueMod = 1;//Otherwise it will turn right to avoid the incoming object
                    }

                    Debug.DrawLine(sensorOrigin, hit.point); //Will draw a line in the direction of the sensor if the raycast detects a hit
                }
            }
        }

        if (avoid)
        {
            targetAngle = maxTurningAngle * torqueMod;
        }

    }

}