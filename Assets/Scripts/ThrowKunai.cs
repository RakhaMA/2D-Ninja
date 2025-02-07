using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;

public class ThrowKunai : MonoBehaviour
{
    public GameObject kunaiPrefab;
    public Transform kunaiSpawnPoint;
    public float kunaiSpeed = 10f;
    public float kunaiLifeTime = 2f;
    private Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }
    
    public void Throw()
    {
        GameObject kunai = Instantiate(kunaiPrefab, kunaiSpawnPoint.position, kunaiSpawnPoint.rotation);
        kunai.GetComponent<Rigidbody2D>().velocity = kunaiSpawnPoint.right * kunaiSpeed;
        // adjust the rotation of the kunai based on the player's direction
        if (player.isFacingRight)
        {
            kunai.transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        else
        {
            kunai.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        Destroy(kunai, kunaiLifeTime);
    }
}
