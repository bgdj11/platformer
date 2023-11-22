using UnityEngine;

public class projectile : MonoBehaviour
{
    [SerializeField] private float speed; // Brzina projektila

    private bool hit; // Da li je projektil udario u nesto
    private float direction; // Smer kretanja projektila
    private float lifeTime; // Vreme trajanja projektila

    private BoxCollider2D boxCollider; // Referenca na BoxCollider2D komponentu
    private Animator anim; // Referenca na Animator komponentu

    private void Awake()
    {
        // Inicijalizacija referenci na komponente
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (hit) return; // Ako je projektil vec udario, prekini azuriranje

        float movementSpeed = speed * Time.deltaTime * direction; // Racunanje brzine kretanja projektila
        transform.Translate(movementSpeed, 0, 0); // Pomeranje projektila po x osi

        lifeTime += Time.deltaTime; // Dodavanje proteklog vremena od poslednjeg azuriranja

        if (lifeTime > 5)
        {
            gameObject.SetActive(false); // Deaktivacija projektila nakon odredjenog vremena
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true; // Postavljanje flaga da je projektil udario
        boxCollider.enabled = false; // Iskljucivanje collider-a projektila
        anim.SetTrigger("explode"); // Pokretanje animacije eksplozije
    }

    public void SetDirection(float _direction)
    {
        lifeTime = 0; // Resetovanje vremena trajanja projektila
        direction = _direction; // Postavljanje smera kretanja projektila
        gameObject.SetActive(true); // Aktiviranje projektila
        hit = false; // Postavljanje flaga da projektil nije udario
        boxCollider.enabled = true; // Omogucavanje collider-a projektila

        float localScaleX = transform.localScale.x; // Uzimanje vrednosti skale po X osi

        if (Mathf.Sign(localScaleX) != direction) // Provera da li je smer skale jednak smeru kretanja
        {
            localScaleX = -localScaleX; // Promena smera skale po X osi
        }

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z); // Postavljanje nove skale projektila
    }

    private void Deactivate()
    {
        gameObject.SetActive(false); // Metoda za deaktivaciju projektila
    }
}
