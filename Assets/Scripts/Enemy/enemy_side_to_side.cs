using UnityEngine;

public class enemy_side_to_side : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float movementDistance;
    [SerializeField] private float speed;
    private bool movingLeft;
    private float leftEdge;
    private float rightEdge;

    private void Awake()
    {
        // Postavlja levo i desno ogranicenje
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
    }

    private void Update()
    {   
        // Pomera objekat levo i desno u odredjenim granicama

        if (movingLeft)
        {
            if (transform.position.x > leftEdge)  // Proverava da li je dosao do leve ivice
            {   
                // Ako nije dosao do leve ivice pomera se i dalje na levo
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
                movingLeft = false; // Prestaje da se pomera na levo
        }
        else
        {
            if (transform.position.x < rightEdge)  // Proverava da li je dosao do desne ivice
            {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
                movingLeft = true;  // Pocinje da se pomera na levo
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            // Ukoliko je dotaknut igrac poziva metodu TakeDamage health skripte igraca
            collision.GetComponent<health>().TakeDamage(damage);
        }
    }
}
