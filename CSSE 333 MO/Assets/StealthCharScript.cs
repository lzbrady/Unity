using UnityEngine;
using System.Collections;

public class StealthCharScript : MonoBehaviour {

    public float speed;
    public float jumpHeight;
    public float turnSpeed;
    public Terrain terrain;

    GameObject currentInteraction;
    CapsuleCollider sCollider;
    Rigidbody rb;
    Animator anim;
    bool canJump;
    Transform currentRotation;
    bool hittingFloor;


    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        canJump = true;
        currentRotation = transform;
        sCollider = GetComponent<CapsuleCollider>();
        hittingFloor = false;
	}

    void FixedUpdate()
    {
        Turn();
        Move();
        Attack();
        hittingFloor = false;
    }

    void Turn()
    {
        float turn = Input.GetAxis("Mouse X");
        currentRotation.Rotate(new Vector3(0.0f, Input.GetAxis("Mouse X") * turnSpeed, 0.0f));
        transform.rotation = currentRotation.rotation;
    }

    void Attack()
    {
        if (Input.GetKey("q"))
        {
            anim.SetTrigger("AttackHeavy");
        }
        if (Input.GetKey("e"))
        {
            anim.SetTrigger("AttackLight");
        }
    }

    void AddInteractable(GameObject obj)
    {
        currentInteraction = obj;
    }

    void RemoveInteractable()
    {
        currentRotation = null;
    }

    void Move()
    {
        Vector3 ws = Vector3.zero;
        Vector3 ad = Vector3.zero;
        bool Moving = false;
        Vector2 forward = new Vector2(transform.forward.x, transform.forward.z).normalized;
        Vector2 sideWays = new Vector2(transform.forward.z, -transform.forward.x).normalized;
        Vector3 upward = new Vector3(0.0f, 1.0f, 0.0f);
        if (Input.GetKey("w"))
        {
            Moving = true;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                ws = new Vector3(forward.x * speed * 1.5f, rb.velocity.y, forward.y * speed * 1.5f);
                anim.SetBool("Running", true);
                anim.SetBool("Walking", false);
            }
            else
            {
                ws = new Vector3(forward.x * speed, rb.velocity.y, forward.y * speed);
                anim.SetBool("Walking", true);
                anim.SetBool("Running", false);
            }
        }
        else if (Input.GetKey("s"))
        {
            Moving = true;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                ws = new Vector3(forward.x * -speed * 1.5f, rb.velocity.y, forward.y * -speed * 1.5f);
                anim.SetBool("Running", true);
                anim.SetBool("Walking", false);
            }
            else
            {
                ws = new Vector3(forward.x * -speed, rb.velocity.y, forward.y * -speed);
                anim.SetBool("Walking", true);
                anim.SetBool("Running", false);
            }
        }
        if (Input.GetKey("a"))
        {
            Moving = true;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                ad = new Vector3(sideWays.x * -speed * 1.8f, rb.velocity.y, sideWays.y * -speed * 1.8f);
                anim.SetBool("Running", true);
                anim.SetBool("Walking", false);
            }
            else
            {
                ad = new Vector3(sideWays.x * -speed, rb.velocity.y, sideWays.y * -speed);
                anim.SetBool("Walking", true);
                anim.SetBool("Running", false);
            }
        }
        else if (Input.GetKey("d"))
        {
            Moving = true;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                ad = new Vector3(sideWays.x * speed * 1.8f, rb.velocity.y, sideWays.y * speed * 1.8f);
                anim.SetBool("Running", true);
                anim.SetBool("Walking", false);
            }
            else
            {
                ad = new Vector3(sideWays.x * speed, rb.velocity.y, sideWays.y * speed);
                anim.SetBool("Walking", true);
                anim.SetBool("Running", false);
            }
        }
        
        rb.velocity = new Vector3((ws.x + ad.x) / 2, rb.velocity.y, (ws.z + ad.z) / 2);
        if (!Moving)
        {
            anim.SetBool("Walking", false);
            anim.SetBool("Running", false);
        }

        if (!canJump)
        {
            anim.SetBool("Jump", false);
        }
        if (Input.GetKey("space") && canJump)
        {
            rb.AddForce(upward * jumpHeight, ForceMode.Impulse);
            anim.SetBool("Jump", true);
            canJump = false;
        }
        if (!hittingFloor && canJump)
        {
            rb.AddForce(new Vector3(0.0f, -30f, 0.0f), ForceMode.Impulse);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("floor"))
        {
            canJump = true;
            anim.SetBool("Jump", false);
            hittingFloor = true;
        }
    }

}
