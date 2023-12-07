using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow_trap : MonoBehaviour
{
    [SerializeField] float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] arrows;
    private float cooldownTimer;

    private void Attack()
    {
        cooldownTimer = 0;

        arrows[FindArrow()].transform.position = firePoint.position;  // Postavlja strelu na poziciju firePoint-a

        arrows[FindArrow()].GetComponent<enemy_projectile>().ActivateProjectile(); // Trazi neaktivnu strelu i zatim je aktivira
    }

    private int FindArrow()
    {
        // Vraca slobodnu strelu
        for (int i = 0; i < arrows.Length; i++)
        {
            if (!arrows[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (cooldownTimer > attackCooldown)
            Attack();
    }
}
