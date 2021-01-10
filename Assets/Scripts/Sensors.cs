using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensors : MonoBehaviour
{

    [Header("Sensors")]
    public float sensorRange = 3.5f;
    public Vector3 frontSensorOrigin = new Vector3(0.0f, 0.2f, 0.5f);
    public float frontOffsetSensorOrigin = 0.45f;
    public float frontAngledSensorTheta = 30f;


    // Update is called once per frame
   private void FixedUpdate()
    {
        SensorArray();
    }
    private void SensorArray()
    {
        RaycastHit hit;
        Vector3 sensorOrigin = transform.position + frontSensorOrigin;

        //Front Sensor
        if (Physics.Raycast(sensorOrigin, transform.forward, out hit, sensorRange))
        {
            Debug.DrawLine(sensorOrigin, hit.point); //Will draw a line in the direction of the sensor if the raycast detects a hit
        }

        //Front Right Sensor
        sensorOrigin.x += frontOffsetSensorOrigin;
        if (Physics.Raycast(sensorOrigin, transform.forward, out hit, sensorRange))
        {
            Debug.DrawLine(sensorOrigin, hit.point);
        }

        //Front Right Angled Sensor
        if (Physics.Raycast(sensorOrigin, Quaternion.AngleAxis(frontAngledSensorTheta, transform.up) * transform.forward, out hit, sensorRange)) //Creates a sensor to look what is at an angle out in front of the car
        {
            Debug.DrawLine(sensorOrigin, hit.point);
        }

        //Front Left Sensor
        sensorOrigin.x -= frontOffsetSensorOrigin * 2; //Mulitiplied by 2 to get back to origin, then negative x values
        if (Physics.Raycast(sensorOrigin, transform.forward, out hit, sensorRange))
        {
            Debug.DrawLine(sensorOrigin, hit.point);
        }

        //Front Left Angled Sensor
        if (Physics.Raycast(sensorOrigin, Quaternion.AngleAxis(-frontAngledSensorTheta, transform.up) * transform.forward, out hit, sensorRange)) //Creates a sensor to look what is at an angle out in front of the car
        {
            Debug.DrawLine(sensorOrigin, hit.point);
        }

    }

}
