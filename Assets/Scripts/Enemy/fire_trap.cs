using System.Collections;
using UnityEngine;

public class fire_trap : MonoBehaviour
{
    [Header("FireTrap timers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
    [SerializeField] private int damage;


    private Animator anim;
    private SpriteRenderer spriteRend;

    private bool triggered; // Da li je zamka aktivirana
    private bool active; // Pokazuje da li je zamka aktivna i da li povredjuje igraca

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            if (!triggered)
            {
                StartCoroutine(ActivateFireTrap());
            }
        if (active)
            collision.GetComponent<health>().TakeDamage(damage);
    }

    private IEnumerator ActivateFireTrap()
    {
        // Omogucava delay dok zamka ne krene da nanosi damage
        triggered = true;
        spriteRend.color = Color.red; // Zamka pocrveni kada je aktivirana

        yield return new WaitForSeconds(activationDelay);
        active = true;
        spriteRend.color = Color.white; // Dok je zamka aktivna ne treba da bude vise crvena

        anim.SetBool("activated", true); // Postavljamo parametar activaed na true i omogucavamo prelaz iz idle u activated animaciju

        // Zamka nanosi damage igracu za vreme activeTime
        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;

        anim.SetBool("activated", false); // Vraca animaciju i idle
    }
}
