using UnityEngine;
using UnityEngine.Events;

public class TriggerEventMediator : MonoBehaviour
{
    public UnityEvent<Collider2D> eventOnTriggerEnter2D;
    public UnityEvent<Collider2D> eventOnTriggerStay2D;
    public UnityEvent<Collider2D> eventOnTriggerExit2D;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        eventOnTriggerEnter2D?.Invoke(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        eventOnTriggerStay2D?.Invoke(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        eventOnTriggerExit2D?.Invoke(other);
    }
}
