using UnityEngine;
using System.Collections;

public class Food : MonoBehaviour
{
    public enum FoodType { Normal, Big }

    [Header("Chọn loại mồi")]
    public FoodType foodType;

    [Header("Khung Bản Đồ (Nhập số giống trong Rắn)")]
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public float step = 0.5f; // Khớp với bước đi 0.25 của rắn

    [Header("Kéo các Component vào đây")]
    public Snake snakeScript;
    public HighScoreBoard scoreBoardScript;

    private void Start()
    {
        if (foodType == FoodType.Normal)
        {
            RandomizePosition();
        }
        else
        {
            gameObject.SetActive(false); // Ẩn mồi lớn lúc mới vào
        }
    }

    private void RandomizePosition()
    {
        // Ép vị trí mồi phải chia hết cho 0.25, không chia hết cho 0.5 để khớp với miệng con rắn
        float x = Mathf.Round(Random.Range(minX, maxX) / step) * step + 0.25f;
        float y = Mathf.Round(Random.Range(minY, maxY) / step) * step + 0.25f;
        transform.position = new Vector3(x, y, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Nhận diện rắn chạm vào mà không cần quan tâm Tag
        if (other.GetComponent<Snake>() != null || other.CompareTag("Player"))
        {

            if (foodType == FoodType.Normal)
            {
                if (scoreBoardScript != null) scoreBoardScript.AddScore(1); // Mồi thường +1 điểm
                RandomizePosition();
            }
        }
    }
}
