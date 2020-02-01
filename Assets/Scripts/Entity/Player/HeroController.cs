using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : BaseEntity
{
    protected List<BaseEntity> _inRange = new List<BaseEntity>();
    protected BaseEntity _oldTower;

    private void FixedUpdate()
    {
        float move = Input.GetAxis("Horizontal");
        
        Move(move);
    }

    void Update()
    {
        int input = 0;
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
            if (CanJump())
            {
                AudioSource audioSource = gameObject.GetComponent<AudioSource>();
                AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);
            }
        }
    }

}