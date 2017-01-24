using UnityEngine;
using System.Collections;

public class EvasiveManeuver : MonoBehaviour
{
    public float dodge;
    public float smoothing;
    public float tilt;

    public Vector2 startWait;
    public Vector2 maneuverTime;
    public Vector2 maneuverWait;
    public Boundary boundary;

    private float currentZSpeed;
    private float targetManeuver;
    private Rigidbody rigidBody;

	void Start ()
    {
        rigidBody = GetComponent<Rigidbody>();
        currentZSpeed = rigidBody.velocity.z;
        StartCoroutine(Evade());
    }

    IEnumerator Evade()
    {
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));

        while (true)
        {
            //! Dodge randomly, always reverting to the middle area to avoid going out
            targetManeuver = Random.Range(1, dodge) * (-Mathf.Sign(transform.position.x));
            //! Maneuver for some randomly chosen time
            yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));
            targetManeuver = 0.0f;
            //! Wait a bit until next maneuver
            yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverWait.y));
        }
    }
	
	void FixedUpdate()
    {
        float newManeuver = Mathf.MoveTowards(rigidBody.velocity.x, targetManeuver, Time.deltaTime * smoothing);
        rigidBody.velocity = new Vector3(newManeuver, 0.0f, currentZSpeed);
        rigidBody.position = new Vector3(
            Mathf.Clamp(rigidBody.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(rigidBody.position.z, boundary.zMin, boundary.zMax)
            );

        float zRotation = -tilt * rigidBody.velocity.x;
        rigidBody.rotation = Quaternion.Euler(0.0f, 0.0f, zRotation);
    }
}
