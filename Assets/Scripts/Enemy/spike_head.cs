using UnityEngine;

public class spike_head : enemy_damage
{
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;
    private float checkTimer;
    private Vector3 destination;
    private bool attacking;

    private Vector3[] directions = new Vector3[4];

    private void OnEnable()
    {
        Stop();
    }

    private void Update()
    {
        if (attacking)  // Ako napada pomeri se ka destinaciji
        {
            transform.Translate(destination * Time.deltaTime * speed);
        }
        else
        {
            checkTimer += Time.deltaTime;
            if (checkTimer > checkDelay)
                CheckForPlayer();
        }      
    }

    private void CheckForPlayer()
    {
        CalculateDirections();

        // Proverava da li u nekom od pravaca moze da vidi igraca
        for (int i = 0; i < directions.Length; i++)
        {
            Debug.DrawRay(transform.position, directions[i], Color.red); // Iscrtava linije iz objekta (GIZMOS)

            // playerLayer dodat da bi proveravao samo kolizije sa igracem
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);

            if(hit.collider != null && !attacking)
            {
                attacking = true;
                destination = directions[i];
                checkTimer = 0;
            }
        }
    }

    private void CalculateDirections()
    {
        directions[0] = transform.right * range; // Desno
        directions[1] = -transform.right * range; // Levo
        directions[2] = transform.up * range; // Gore
        directions[3] = -transform.up * range; // Dole
    }

    private void Stop()
    {
        destination = transform.position;
        attacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        Stop(); // Ukuliko udari u nesto zaustavlja se
    }
}
