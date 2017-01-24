using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour
{
    public float speed;

    void Start()
    {
        Rigidbody rigidBody = GetComponent<Rigidbody>();
        rigidBody.velocity = speed * transform.forward;
    }
}
