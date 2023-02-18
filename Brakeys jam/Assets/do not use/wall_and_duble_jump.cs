using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall_and_duble_jump : MonoBehaviour
{
    public bool iswalled;
    public bool iswalljumping;
    public float movement;
    public Playerr pl;
    public bool facingright;
    public float walljumpx;
    public float walljumpy;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(movement > 0 && !facingright)
        {
            flip();
        }
        else if(movement < 0 && facingright)
        {
            flip();
        }
        else
        {

        }
        if (facingright)
        {

              if (iswalled && Input.GetButtonDown("Jump") && movement > 0)
              {
                   pl.rb.velocity = new Vector2(walljumpx, walljumpy);
                flip();
              }
           
        }
        else
        {
            if (iswalled && Input.GetButtonDown("Jump") && movement < 0)
            {
                pl.rb.velocity = new Vector2(-walljumpx, -walljumpy);
                flip();
            }
           
        }
        movement = Input.GetAxisRaw("Horizontal");
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Ground")
        {
            iswalled = true;
        }
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            iswalled = false;
        }
    }
    void flip()
    {
        facingright = !facingright;
        transform.Rotate(0f, 180f, 0f);
    }
   
}
