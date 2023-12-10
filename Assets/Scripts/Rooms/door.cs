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
                nextRoom.GetComponent<Room>().ActivateRoom(true); // Aktivira sobu u koju igrac ulazi
                previousRoom.GetComponent<Room>().ActivateRoom(false); // Deaktivira sobu iz koje igrac izlazi
            }
            else
            {
                cam.MoveToNewRoom(previousRoom);
                previousRoom.GetComponent<Room>().ActivateRoom(true); // Aktivira sobu u koju igrac ulazi
                nextRoom.GetComponent<Room>().ActivateRoom(false); // Deaktivira sobu iz koje igrac izlazi
            }
        }
    }
}
