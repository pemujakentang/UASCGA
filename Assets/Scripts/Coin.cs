using UnityEngine;

public class Coin : MonoBehaviour
{
    void Start()
    {

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
            }
            else
            {
                PlayerManager.numberOfCoins++;
            }
            Debug.Log(PlayerManager.numberOfCoins);
            Destroy(gameObject);
        }
    }
}