using UnityEngine;

public class health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    private void Awake()
    {
        currentHealth = startingHealth; 
        anim = GetComponent<Animator>(); // Uzima referencu na animator igraca
        dead = false;
    }

    public void TakeDamage(float _damage)
    {
        // Umanjuje helte ukoliko je to moguce
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth); 
        
        if(currentHealth > 0)
        {
            // Igrac je povredjen
            anim.SetTrigger("hurt");
            
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

}
