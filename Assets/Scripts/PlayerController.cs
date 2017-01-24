using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float tilt;
    public Boundary boundary;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    public int firePenalty;

    public Score score;

    private AudioSource audioSource;

    private float nextFire;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        bool hitFire = Input.GetButton("Fire1") || Input.GetKeyDown("space");
        if (hitFire && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            audioSource.Play();

            //! Remove some points from score per shot
            RetrievePenaltyToScore(firePenalty);
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Rigidbody rigidBody = GetComponent<Rigidbody>();

        Vector3 velocity = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rigidBody.velocity = velocity * speed;

        rigidBody.position = new Vector3(
            Mathf.Clamp(rigidBody.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(rigidBody.position.z, boundary.zMin, boundary.zMax)
            );

        float zRotation = -tilt * rigidBody.velocity.x;
        rigidBody.rotation = Quaternion.Euler(0.0f, 0.0f, zRotation);
    }

    public void RetrievePenaltyToScore(int decrement)
    {
        score.AddScore(-decrement);
    }
}
