using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float movespeed;
	public float jumpforce;
	public bool feetContact;


	private Animator animator;
	private Rigidbody2D rb;
	// Start is called before the first frame update
	void Start()
	{
		animator = GetComponentInChildren<Animator>();
		rb = GetComponent<Rigidbody2D>();
		
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.UpArrow) && canJump())
		{
			animator.SetTrigger("Jump");
			rb.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
		}
		if (Input.GetButtonDown("Fire1"))
		{
			animator.SetTrigger("Attack");
		}
		if (Input.GetKey(KeyCode.RightArrow))
		{
			animator.SetBool("Running", true);
			transform.position = (Vector2)transform.position + new Vector2(movespeed, 0) * Time.deltaTime;
			transform.rotation = Quaternion.Euler(0, 0, 0);
		}
		else if (Input.GetKey(KeyCode.LeftArrow))
		{
			animator.SetBool("Running", true);
			transform.position = (Vector2)transform.position + new Vector2(-movespeed, 0) * Time.deltaTime;
			transform.rotation = Quaternion.Euler(0, 180, 0);
		} else
		{
			animator.SetBool("Running", false);
		}
	}

	private bool canJump() {
		return feetContact;
	}
}
