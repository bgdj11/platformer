using UnityEngine;

public class camera_controller : MonoBehaviour
{
    [SerializeField] private float speed; // Brzina kretanja kamere.

    // Trenutna X pozicija kamere
    private float currentPosX = -1.13f;

    private Vector3 velocity = Vector3.zero; // Vektor brzine kamere.
    private void Update()
    {
        // Glatko pomeranje kamere prema novoj X poziciji.
        transform.position = Vector3.SmoothDamp(
            transform.position, // Trenutna pozicija kamere.
            new Vector3(currentPosX, transform.position.y, transform.position.z), // Nova pozicija
            ref velocity, 
            speed 
        );
    }

    // Metoda koja postavlja novu poziciju kamere
    public void MoveToNewRoom(Transform _newRoom)
    {
        currentPosX = _newRoom.position.x + 1.1f;
    }
}
