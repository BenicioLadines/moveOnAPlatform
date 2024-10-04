using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    Vector3 inputDir;
    Rigidbody rb;
    public float moveSpeed;
    public float jumpPower;
    bool onGround;
    public Rigidbody inheritedPos;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleDirectionInput();

        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpPower, rb.velocity.z);
            onGround = false;
        }
    }

    private void FixedUpdate()
    {
        Vector3 playerInput = new Vector3(
                inputDir.x * moveSpeed * Time.fixedDeltaTime,
                rb.velocity.y,
                inputDir.z * moveSpeed * Time.fixedDeltaTime);
        

        if(inheritedPos != null)
        {
            rb.velocity = playerInput + inheritedPos.velocity;
        }
        else
        {
            rb.velocity = playerInput;
        }

        
    }

    void HandleDirectionInput()
    {
        inputDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts[0].normal == Vector3.up)
        {
            Debug.Log("hi");
            onGround = true;
        }
        
        if(collision.collider.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            inheritedPos = rb;
        }
                

    }

    private void OnCollisionExit(Collision collision)
    {
        inheritedPos = null;
    }

}
