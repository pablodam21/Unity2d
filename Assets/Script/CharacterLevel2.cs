using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class CharacterLevel2 : MonoBehaviour
{
    public float Speed = 0.0f;
    public float lateralMovement = 2.0f;
    public float jumpMovement = 400.0f;
    float movementButton = 0.0f;
    public Transform groundCheck;
    private Animator animator;
    private Rigidbody2D rigidbody2d;
    public bool grounded = true;
    private int life = 3;
    private int star = 0;
	public Text lifeInfo;
	public Text starInfo;



// Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
		lifeInfo.text = " " + life;
		starInfo.text = " " + star;
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.Linecast(transform.position,
            groundCheck.position,
            1 << LayerMask.NameToLayer("Ground"));
        if (grounded && Input.GetButtonDown("Jump"))
            rigidbody2d.AddForce(Vector2.up * jumpMovement);
        if (grounded)
            animator.SetTrigger("Grounded");
        else
            animator.SetTrigger("Jump");

        Speed = lateralMovement * movementButton;

        transform.Translate(Vector2.right * Speed * Time.deltaTime);
        animator.SetFloat("Speed", Mathf.Abs(Speed));
        if (Speed < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);

        if (life == 0)
        {
            Application.LoadLevel("GameOver");
        }

        if (star == 3)
        {
            Application.LoadLevel("Winner");
        }




    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Star")
        {
            star++;
            Destroy(collider.gameObject);
			starInfo.text = " " + star;
        }

        if (collider.tag == "Zoom")
            GameObject.Find("MainVirtual").GetComponent<CinemachineVirtualCamera>().enabled = false;

        if (collider.gameObject.tag == "Enemy")
        {
            life--;
			lifeInfo.text = " " + life;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Zoom")
            GameObject.Find("MainVirtual").GetComponent<CinemachineVirtualCamera>().enabled = true;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "MobilePaltform")
            transform.SetParent(other.transform);
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "MobilePaltform")
            transform.SetParent(null);
    }

    public void Jump()
    {
        if (grounded)
            rigidbody2d.AddForce(Vector2.up * jumpMovement);
    }

    public void Move(float amount)
    {
        movementButton = amount;
        print("hika");
    }
}