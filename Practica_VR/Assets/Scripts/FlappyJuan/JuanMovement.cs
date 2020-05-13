using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuanMovement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float maxSpeed = 3f;
    [SerializeField] float jumpForce = 600f;
    [SerializeField] Transform upLimit;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Se limita la altura del jugador para que no sobrepase el límite superior de la pantalla
        if (transform.position.y > upLimit.position.y)
        {
            transform.position = new Vector3(transform.position.x, upLimit.position.y, transform.position.z);
            rb.velocity = Vector2.zero;
        }
    }

    public void Jump()
    {
        rb.AddForce(new Vector2(0f, jumpForce));
        if (rb.velocity.magnitude > maxSpeed)
            rb.velocity = rb.velocity.normalized * maxSpeed;
    }

}
