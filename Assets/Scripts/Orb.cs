using UnityEngine;

/// <summary>
/// Event handler for Orbs
/// </summary>
public class Orb : MonoBehaviour
{
    /// <summary>
    /// Destroy offscreen orbs
    /// </summary>    
    void OnBecameInvisible()
    {
        Destroy(gameObject); 
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }

}
