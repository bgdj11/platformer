using UnityEditor;
using UnityEngine;

public class door : MonoBehaviour
{
    [SerializeField] private Transform previousRoom; // Referenca na prethodnu sobu
    [SerializeField] private Transform nextRoom; // Referenca na sledecu sobu
    [SerializeField] private camera_controller cam; // Referenca na kontroler kamere.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") 
        {
            // Proverava poziciju igraca u odnosu na poziciju vrata kako bi odredio u koju sobu da pomeri kameru.
            if (collision.transform.position.x < transform.position.x)
            {
                cam.MoveToNewRoom(nextRoom);
            }
            else
            {
                cam.MoveToNewRoom(previousRoom);
            }
        }
    }
}
