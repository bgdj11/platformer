using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim; // Referenca na Animator komponentu
    private player_movement player_movement; // Referenca na skriptu za kretanje igraca
    [SerializeField] private float attackCooldown; // Vreme izmedju napada
    [SerializeField] private Transform firePoint; // Pozicija gde ce se ispaljivati projektili
    [SerializeField] private GameObject[] fireBalls; // Niz projektila

    private float cooldownTimer = Mathf.Infinity; // Trenutno vreme do sledeceg napada

    private void Awake()
    {
        anim = GetComponent<Animator>(); // Inicijalizacija reference na Animator komponentu
        player_movement = GetComponent<player_movement>(); // Inicijalizacija reference na skriptu za kretanje igraca
    }

    private void Update()
    {
        // Provera da li je pritisnuto dugme za napad, da li je proslo dovoljno vreme od poslednjeg napada i da li igrac moze da napada
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && player_movement.canAttack())
        {
            Attack(); // Napad
        }

        cooldownTimer += Time.deltaTime; // Povecavanje vremena do sledeceg napada
    }

    private void Attack()
    {
        anim.SetTrigger("attack"); // Pokretanje animacije napada
        cooldownTimer = 0; // Resetovanje vremena do sledeceg napada

        // Postavljanje pozicije ispaljivanja projektila i odredjivanje smera projektila
        fireBalls[FindFireball()].transform.position = firePoint.position;
        fireBalls[FindFireball()].GetComponent<projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindFireball()
    {
        // Pronalazenje neaktivnog projektila iz niza
        for (int i = 0; i < fireBalls.Length; i++)
        {
            if (!fireBalls[i].activeInHierarchy) return i; // Ako je projektil neaktivan, vrati njegov indeks
        }

        return 0; // Vrati prvi projektil ako su svi aktivni
    }
}
