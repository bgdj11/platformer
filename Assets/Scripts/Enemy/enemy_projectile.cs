using UnityEngine;

public class enemy_projectile : enemy_damage // Nasledjuje klasu enemy_damage kako bi svaki put pri dodiru sa igracom nanosio damage
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float lifetime;

    public void ActivateProjectile()
    {
        lifetime = 0;
        gameObject.SetActive(true); // Aktiviramo projektil
    }

    private void Update()
    {
        float movementSpeed = speed * Time.deltaTime; // Brzina nezavisna od frame rate-a
        transform.Translate(movementSpeed * Mathf.Sign(gameObject.transform.localScale.x), 0, 0);  // Pomeramo projektil po x osi

        lifetime += Time.deltaTime;

        // Nakon odredjenog vremena deaktiviramo objekat
        if(lifetime > resetTime )
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision); // Poziva OnTriggerEnter2D iz enemy_damage

        gameObject.SetActive(false); // Deaktiviramo projektil ako pogodi bilo sta

    }
}
