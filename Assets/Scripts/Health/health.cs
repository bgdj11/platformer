using System.Collections;
using UnityEngine;

public class health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    private void Awake()
    {
        currentHealth = startingHealth; 
        anim = GetComponent<Animator>(); // Uzima referencu na animator igraca
        dead = false;

        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage)
    {
        // Umanjuje helte ukoliko je to moguce
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth); 
        
        if(currentHealth > 0)
        {
            // Igrac je povredjen
            anim.SetTrigger("hurt");

            StartCoroutine(Invunerabillity()); 
        }
        else
        {
            // Igrac je mrtav
            if (!dead)
            {
                anim.SetTrigger("die");
                GetComponent<player_movement>().enabled = false; // Onemogucava igracu da se pomera kada je mrtav
                dead = true;
            }
        }  
    }

    public void AddHealth(float _val)
    {
        // Povecava helte ukoliko je moguce
        currentHealth = Mathf.Clamp(currentHealth + _val, 0, startingHealth);
    }

    private IEnumerator Invunerabillity()
    {   

        Physics2D.IgnoreLayerCollision(8, 9, true);  // Cini igraca neranjivim za vreme iFramesDuration

        // Omogucava da igrac "treperi" numberOfFlashes puta dok je neranjiv
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);

            yield return new WaitForSeconds(iFramesDuration / numberOfFlashes); // Omogucava period cekanja pre izvrsavanja sledece nardbe

            spriteRend.color = Color.white;

            yield return new WaitForSeconds(iFramesDuration / numberOfFlashes);
        }

        Physics2D.IgnoreLayerCollision(8, 9, false); // Igrac ponovo postaje ranjiv

    }

}
