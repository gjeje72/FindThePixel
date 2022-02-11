using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed;

    public Camera MainCamera;
    public Vector2 screenBounds;
    public bool gravity;

    private Vector3 initPos;
    private float objectWidth;
    private float objectHeight;

    private  Rigidbody2D playerRigidBody => this.gameObject.GetComponent<Rigidbody2D>();
    private int activeScene => SceneManager.GetActiveScene().buildIndex;

    // Start is called before the first frame update
    private void Start()
    {
        screenBounds = this.MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, this.MainCamera.transform.position.z));
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x;
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y;
        initPos = this.gameObject.transform.position;
    }

    // Update is called once per frame
    public void Update()
    {
        if (!Input.anyKey && !this.gravity)
            this.playerRigidBody.velocity = new Vector2(0, 0);

        if (Input.GetKeyUp("up") || Input.GetKeyUp("down"))
            this.playerRigidBody.velocity = new Vector2(this.playerRigidBody.velocity.x , 0);

        if (Input.GetKeyUp("left") || Input.GetKeyUp("right"))
            this.playerRigidBody.velocity = new Vector2(0, this.playerRigidBody.velocity.y);

        if (Input.GetKey("up") && !this.gravity)
            this.playerRigidBody.AddForce(new Vector2(0,3), ForceMode2D.Force);

        if (Input.GetKey("down") && !this.gravity)
            this.playerRigidBody.AddForce(new Vector2(0, -3), ForceMode2D.Force);

        if (Input.GetKey("left"))
            this.playerRigidBody.AddForce(new Vector2(-3, 0), ForceMode2D.Force);

        if (Input.GetKey("right"))
            this.playerRigidBody.AddForce(new Vector2(3, 0), ForceMode2D.Force);

        if (Input.GetKey(KeyCode.A))
        {
            this.gameObject.transform.position = initPos;
            this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        }
    }

    //  Method used when player collides with a collider 2D.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.activeScene == 5 || this.activeScene == 10)
        {
            var collisionNumber = collision.name.Substring(4);

            if (collision.CompareTag("jumpIn") && collision.name == $"jump{collisionNumber}")
                transform.position = GameObject.Find($"jumpOut{collisionNumber}").transform.position;
        }

        if (this.activeScene == 6 || this.activeScene == 8)
        {
            if (collision.CompareTag("laser"))
                this.gameObject.transform.position = initPos;
        }
    }

    // Method used to constrain player in the screen.
    public void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + objectHeight, screenBounds.x - objectHeight);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + objectWidth, screenBounds.y - objectWidth);
        transform.position = viewPos;
    }
}
