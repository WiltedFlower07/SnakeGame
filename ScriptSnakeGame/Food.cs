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
    public float step = 0.5f; // Khớp với bước đi 0.5 của rắn

    [Header("Kéo các Component vào đây")]
    public Snake snakeScript;
    public HighScoreBoard scoreBoardScript;

    [Header("Cài đặt Mồi Thường")]
    public Food bigFoodScript;
    public int itemsToSpawnBigFood = 5;
    private int foodEatenCount = 0;

    [Header("Cài đặt Mồi Lớn")]
    public float activeTime = 5f;
    private Coroutine timerCoroutine;

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

    public void SpawnBigFood()
    {
        if (gameObject.activeInHierarchy) return;

        RandomizePosition();
        gameObject.SetActive(true);

        // Hủy đếm ngược cũ (nếu có) và bắt đầu đếm lại từ đầu
        if (timerCoroutine != null) StopCoroutine(timerCoroutine);
        timerCoroutine = StartCoroutine(CountdownToDisappear());
    }

    private IEnumerator CountdownToDisappear()
    {
        yield return new WaitForSeconds(activeTime);
        gameObject.SetActive(false); // Hết giờ tự ẩn đi
    }

    private void RandomizePosition()
    {
        // Ép vị trí mồi phải chia hết cho 0.5 để khớp với miệng con rắn
        float x = Mathf.Round(Random.Range(minX, maxX) / step) * step;
        float y = Mathf.Round(Random.Range(minY, maxY) / step) * step;
        transform.position = new Vector3(x, y, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Nhận diện rắn chạm vào mà không cần quan tâm Tag
        if (other.GetComponent<Snake>() != null || other.CompareTag("Player"))
        {

            if (snakeScript != null) snakeScript.Grow(); // Báo rắn dài ra

            if (foodType == FoodType.Normal)
            {
                if (scoreBoardScript != null) scoreBoardScript.AddScore(1); // Mồi thường +1 điểm
                RandomizePosition();
                foodEatenCount++;

                // Đủ 5 cục thì gọi mồi lớn
                if (foodEatenCount % itemsToSpawnBigFood == 0 && bigFoodScript != null)
                {
                    bigFoodScript.SpawnBigFood();
                }
            }
            else
            {
                if (scoreBoardScript != null) scoreBoardScript.AddScore(5); // Mồi lớn +5 điểm
                gameObject.SetActive(false); // Ăn xong ẩn đi ngay
            }
        }
    }
}