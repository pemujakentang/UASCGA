using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerUpType { SpeedBoost, Invincibility, HighJump, DoubleCoins }
    public PowerUpType powerUpType;
    public float duration = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.ActivatePowerUp(powerUpType, duration);
                Destroy(gameObject);
            }
        }
    }
}