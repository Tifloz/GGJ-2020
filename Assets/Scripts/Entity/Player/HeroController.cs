using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : BaseEntity
{
    protected List<BaseEntity> _inRange = new List<BaseEntity>();
    protected BaseEntity _oldTower;
    public bool head = true;
    private bool facingLeft = true;
    private void FixedUpdate()
    {
        float move = Input.GetAxis("Horizontal");
        if (move < 0 && !facingLeft)
            reverseImage ();
        else if (move > 0 && facingLeft)
            reverseImage ();
        Move(move);

    }
	void reverseImage()
	{
		// Switch the value of the Boolean
		facingLeft = !facingLeft;
 
		// Get and store the local scale of the RigidBody2D
		Vector2 theScale = rb.transform.localScale;
 
		// Flip it around the other way
		theScale.x *= -1;
		rb.transform.localScale = theScale;
	}
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (head)
            {
                Jump();
                if (CanJump())
                {
                    AudioSource audioSource = gameObject.GetComponent<AudioSource>();
                    AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);
                    _anim.SetBool("jump",true);
                }
            }
            else
            {
                
            }
        }
    }
}