using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
  public Animator animator;
  BoxCollider2D bc;
  public float speed;
  public float jump;

  public ScoreController scoreController;

  
  private Rigidbody2D rb2d;
    private void Awake()
    {
        Debug.Log("Player Awake");
        bc = gameObject.GetComponent<BoxCollider2D>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }

    public void KillPlayer()
    {
        Debug.Log("Player Killed by enemy");
        animator.SetBool("Death",true);
       Invoke("ReloadScene",1f);
       
    }

    public void ReloadScene()
    {
        
        SceneManager.LoadScene("Start");
    }

    public void PickupKey()
    {
       Debug.Log("Key Picked up");
       scoreController.ScoreIncrease(10);
    }

    private void Update()
    {
       
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Jump");
       MovementAnimation(horizontal , vertical);
       MoveCharacter(horizontal , vertical);

       
        if (Input.GetKeyDown(KeyCode.RightAlt))
        {
            animator.SetTrigger("Walk/Run Toggle");
        }
        
        
       
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {

            animator.SetBool("Crouch", true);
            //Debug.Log("Crouching");
            bc.size = new Vector2 (0.5245454f, 1.269899f );
            bc.offset = new Vector2 (-0.04246405f,0.5954856f);
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {

            animator.SetBool("Crouch", false);
            // Debug.Log("NotCrouching");
             bc.size = new Vector2 (0.4168615f,2.017859f  );
            bc.offset = new Vector2 (0.01137787f,0.9694713f);
        }

       
    }

     private void MovementAnimation(float horizontal , float vertical)

     {
        animator.SetFloat("Speed", Mathf.Abs(horizontal));

        Vector3 scale = transform.localScale;
        if (horizontal < 0)
        {
            scale.x = -1f * Mathf.Abs(scale.x);
        }
        else if (horizontal > 0)
        {
            scale.x = Mathf.Abs(scale.x);
        }
       transform.localScale = scale;
       
       
       if (vertical > 0)
       {
         animator.SetBool("Jump", true);
       }

       else 
       {
           animator.SetBool("Jump", false);
       }
      
          
     }

      
      private void MoveCharacter(float horizontal, float vertical)
     {
        // Player Movement Horizontal
       Vector3 position = transform.position;
       position.x += horizontal * speed * Time.deltaTime;
       transform.position = position;
       // Player Movement Vertical
         if (vertical > 0)
        {
         rb2d.AddForce(new Vector2(0f , jump),ForceMode2D.Impulse);
        
        }
        

       
      
      }

      
     
    
}
