using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private int direction = 1;
    private float timer = 0f;

    [SerializeField] private float moveDelay = 0.75f;
    [SerializeField] private float step = 0.5f;

    [SerializeField] private Transform bodyPrefab;
    [SerializeField] private HighScoreBoard scoreBoard;
    [SerializeField] private float minX, maxX, minY, maxY;
    private List<Transform> body = new List<Transform>();
    private List<Vector3> positions = new List<Vector3>();
    private Vector3 lastTailPosition;

    void Start()
    {
        positions.Add(transform.position);
    }

    // Cập nhật mỗi frame
    void Update()
    {
        HandleInput();

        timer += Time.deltaTime;

        if (timer >= moveDelay)
        {
            Move();
            timer = 0f;
        }
    }

    // Nhập thay đổi hướng của nhân vật
    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W) && direction != 2) direction = 0;
        if (Input.GetKeyDown(KeyCode.S) && direction != 0) direction = 2;
        if (Input.GetKeyDown(KeyCode.A) && direction != 1) direction = 3;
        if (Input.GetKeyDown(KeyCode.D) && direction != 3) direction = 1;
    }

    // Di chuyển nhân vật
    void Move()
    {
        if (positions.Count > 0)
            lastTailPosition = positions[positions.Count - 1];

        Vector3 newPos = transform.position;

        switch (direction)
        {
            case 0: newPos.y += step; break;
            case 1: newPos.x += step; break;
            case 2: newPos.y -= step; break;
            case 3: newPos.x -= step; break;
        }

        positions.Insert(0, newPos);
        transform.position = newPos;
        if (newPos.x < minX || newPos.x > maxX || newPos.y < minY || newPos.y > maxY)
        {
            Die();
        }

        for (int i = 0; i < body.Count; i++)
        {
            body[i].position = positions[i + 1];
        }

        if (positions.Count > body.Count + 1)
        {
            positions.RemoveAt(positions.Count - 1);
        }
    }

    void Die()
    {
        Debug.Log("Game Over");
        Time.timeScale = 0f;
    }
    // Sau khi ăn tăng độ dài của nhân vật
    public void Grow()
    {
        Transform newPart = Instantiate(bodyPrefab);
        newPart.position = lastTailPosition;
        body.Add(newPart);
    }

    // Xử lý va chạm với thức ăn
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Food"))
        {
            Grow();
            scoreBoard.AddScore(1); // ⭐ thêm dòng này
            Destroy(collision.gameObject);
        }
}
}
