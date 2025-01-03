using UnityEngine;

public class Coin : MonoBehaviour
{
    public AudioClip coinPickupSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(20 * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null && playerController.IsDoubleCoinsActive())
            {
                PlayerManager.numberOfCoins += 2;
                Score.score += 10;
            }
            else
            {
                PlayerManager.numberOfCoins++;
                Score.score += 5;
            }
            audioSource.PlayOneShot(coinPickupSound); // Play coin pickup sound
            Debug.Log(PlayerManager.numberOfCoins);

            Destroy(gameObject);
        }
    }
}