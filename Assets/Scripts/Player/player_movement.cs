using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField] private float speed = 8; // Brzina kretanja igraca
    [SerializeField] private float jumpPower = 20; // Snaga skoka igraca
    private Animator anim;
    private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask groundLayer; // LayerMask za detekciju zemlje
    [SerializeField] private LayerMask wallLayer; // LayerMask za detekciju zida
    private float wallJumpCooldown; // Cooldown za wall jump
    private float horizontalInput; // Ulaz za horizontalno kretanje

    private void Awake()
    {
        // Inicijalizacija referenci na komponente
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal"); // Uzimanje ulaza za horizontalno kretanje

        // Okretanje igraca u zavisnosti od pritiska na levo/desno dugme
        if(horizontalInput > 0.01f) 
        {
            transform.localScale = Vector3.one; 
        }
        else if (horizontalInput < - 0.01f)  
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // Postavljanje animator parametara
        anim.SetBool("run", horizontalInput != 0); 
        anim.SetBool("grounded", isGrounded()); 

        // Wall jumping
        if(wallJumpCooldown > 0.2f)
        {
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y); // Postavljanje brzine kretanja igraca

            if(onWall() && !isGrounded())
            {
                body.gravityScale = 0; // Zaustavljanje gravitacije ako je igrac na zidu
                body.velocity = Vector2.zero; // Postavljanje brzine na nulu
            }
            else
            {
                body.gravityScale = 3; // Postavljanje gravitacije na normalnu vrednost
            }

            if (Input.GetKey(KeyCode.Space))
            {
                jump(); // Pokretanje skoka 
            }

        }
        else
        {
            wallJumpCooldown += Time.deltaTime; // Dodavanje vremena na cooldown za wall jump
        }

    }

    private void jump()
    {
        if(isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower); // Postavljanje brzine za skok
            // body.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);


            anim.SetTrigger("jump"); // Pokretanje animacije skoka
        }
        else if(onWall() && !isGrounded())
        {
            if (horizontalInput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0); // Wall jump sa zida ako se ne pritiska levo ili desno
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z); // Okretanje igraca
            }
            else
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6); // Wall jump sa zida ako se pritisne levo/desno
            }
            wallJumpCooldown = 0; // Resetovanje cooldowna za wall jump
            
        }
       
    }
    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);

        return raycastHit.collider != null; // Provera da li je igrac na zemlji
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);

        return raycastHit.collider != null; // Provera da li je igrac na zidu
    }

    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded() && !onWall();
    }

}
