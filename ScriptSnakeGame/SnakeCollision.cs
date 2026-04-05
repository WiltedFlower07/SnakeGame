using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeCollision : MonoBehaviour
{
    [SerializeField] private GameObject LoseScene;

    void OnTriggerEnter2D(Collider2D collision)
    {

        // Xử lý va chạm với thân mình
        if ( collision.CompareTag("Body"))
        {
            Destroy(collision.gameObject);
            LoseScene.SetActive(true);
        }
    }
}
