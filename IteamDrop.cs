using UnityEngine;

public class IteamDrop : MonoBehaviour
{
    public GameObject itemPrefab;
    private float dropChance = 0.8f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            if (Random.value < dropChance)
            {
                Instantiate(itemPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}
