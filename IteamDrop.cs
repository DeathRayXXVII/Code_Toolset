using UnityEngine;

public class IteamDrop : MonoBehaviour
{
    public GameObject itemPrefab;
    public float dropChance = 0.5f;

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
