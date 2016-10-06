using UnityEngine;
using System.Collections;

public class PlayerMove3 : MonoBehaviour
{
    public float speed = 100f;
    //public Animator anim;

    public int timer;
    private int minutes;
    private int seconds;


    //	public float timer_f = 0f;
    //	public int timer_i = 0;

    private Vector3 movement;                   // The vector to store the direction of the player's movement.
    private Animator anim;                      // Reference to the animator component.
    private Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
    private int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
    private float camRayLength = 100f;

    private Vector3 moveDirection;
    private Vector3 transformDirection;


    void Awake()
    {
        // Create a layer mask for the floor layer.
        floorMask = LayerMask.GetMask("Floor");

        // Set up references.
        anim = GetComponent<Animator>();
        //		playerRigidbody = GetComponent <Rigidbody> ();
    }

    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();

        //		if (controller.isGrounded) {
        moveDirection = new Vector3(Input.GetAxis("Vertical"), 0, -Input.GetAxis("Horizontal"));
        //			moveDirection = transform.TransformDirection(moveDirection);
        transformDirection = new Vector3(-Input.GetAxis("Horizontal"), 0, -Input.GetAxis("Vertical"));
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp("w") || Input.GetKeyUp("s") || Input.GetKeyUp("a") || Input.GetKeyUp("d"))
        {
            foot.step++;
        }
        Debug.Log(foot.step);
        // Debug.Log (moveDirection);

        if (moveDirection != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(-transformDirection);
        moveDirection *= speed;

        //			if (Input.GetButton("Jump"))
        //				moveDirection.y = jumpSpeed;

        //		}
        //		moveDirection.y -= gravity * Time.deltaTime;
        //		var controller = GetComponent(CharacterController);
        //		controller.SimpleMove(vel);

        controller.SimpleMove(moveDirection * Time.deltaTime);

        //		timer_f = Time.deltaTime;
        //		timer_i += (int)timer_f % 60;
        //		Debug.Log (timer_f);

        //		Debug.Log (timer);
        if (Input.GetKeyUp(KeyCode.R))
        {
            Application.LoadLevel("scene4");
        }
    }

    void FixedUpdate()
    {
        // Store the input axes.
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // Move the player around the scene.
        //		Move (h, v);

        // Turn the player to face the mouse cursor.
        Turning();

        // Animate the player.
        //Animating (h, v);
    }
    // Use this for initialization
    void Move(float h, float v)
    {
        // Set the movement vector based on the axis input.
        movement.Set(h, 0f, v);

        // Normalise the movement vector and make it proportional to the speed per second.
        movement = movement.normalized * speed * Time.deltaTime;

        // Move the player to it's current position plus the movement.
        playerRigidbody.MovePosition(transform.position + movement);
    }
    void Turning()
    {
        // Create a ray from the mouse cursor on screen in the direction of the camera.
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Create a RaycastHit variable to store information about what was hit by the ray.
        RaycastHit floorHit;

        // Perform the raycast and if it hits something on the floor layer...
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            // Create a vector from the player to the point on the floor the raycast from the mouse hit.
            Vector3 playerToMouse = floorHit.point - transform.position;

            // Ensure the vector is entirely along the floor plane.
            playerToMouse.y = 0f;

            // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            // Set the player's rotation to this new rotation.
            playerRigidbody.MoveRotation(newRotation);
        }
    }

    void Animating(float h, float v)
    {
        // Create a boolean that is true if either of the input axes is non-zero.
        bool walking = h != 0f || v != 0f;

        // Tell the animator whether or not the player is walking.
        anim.SetBool("IsWalking", walking);
    }




    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        int pushPower = 10;
        anim.SetBool("push", true);

        Debug.Log(hit.gameObject.tag);
        if (hit.gameObject.tag == "Ground")
        {
            Application.LoadLevel("lose3");
        }
        //		Debug.Log (hit.gameObject.tag);



        Rigidbody body = hit.collider.attachedRigidbody;
        Debug.Log(body);

        if (body == null || body.isKinematic)
        {
            anim.SetBool("push", false);
            return;
        }
        else
        {
            anim.SetBool("push", true);
        }

        if (hit.gameObject.tag == "Finish")
        {
            Application.LoadLevel("scene4");
        }




        //		if (hit.moveDirection.y < -0.3F)
        //			return;

        //		Debug.Log (hit.gameObject.tag);

        //		if (hit.gameObject.tag == "Cube") {

        //			Debug.Log (hit);

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        Debug.Log(pushDir);


        body.MovePosition(body.transform.position + pushDir * 1);
        //		}
        //		body.velocity = pushDir * pushPower;
    }
}