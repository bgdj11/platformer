using System.Runtime.CompilerServices;
using UnityEngine;

public class camera_controller : MonoBehaviour
{
    // kamera v1 (room-camera)

    [SerializeField] private float speed; // Brzina kretanja kamere.
    // Trenutna X pozicija kamere
    private float currentPosX = -1.13f;
    private Vector3 velocity = Vector3.zero; // Vektor brzine kamere.

    // kamera v2 (player-camera)

    [SerializeField] private Transform player;
    [SerializeField] private float frontDistance;  // Udaljenost za centriranje kamere ispred igraca
    [SerializeField] private float cameraSpeed; // Brzina pomeranja ka novoj poziciji kamere
    private float lookAhead;
    
    private void Update()
    {
        /*
        // Glatko pomeranje kamere prema novoj X poziciji - sobi
        transform.position = Vector3.SmoothDamp(
            transform.position, // Trenutna pozicija kamere.
            new Vector3(currentPosX, transform.position.y, transform.position.z), // Nova pozicija
            ref velocity, 
            speed 
        );
        */

        // Pomeranje kamere naspram igraca
        transform.position = new Vector3(player.position.x + lookAhead, transform.position.y, transform.position.z);
        lookAhead = Mathf.Lerp(lookAhead, (frontDistance * player.localScale.x), Time.deltaTime * cameraSpeed);

    }

    // Metoda koja postavlja novu poziciju kamere
    public void MoveToNewRoom(Transform _newRoom)
    {
        currentPosX = _newRoom.position.x + 1.1f;
    }
}
