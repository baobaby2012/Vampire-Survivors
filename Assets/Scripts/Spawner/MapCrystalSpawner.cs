using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCrystalSpawner : MonoBehaviour
{
    [Header("Cài đặt sinh Pha lê")]
    [Tooltip("Thời gian giữa 2 lần sinh pha lê (giây)")]
    [SerializeField] float spawnInterval = 1f;

    [Header("Giới hạn bản đồ")]
    [Tooltip("Tọa độ X tối đa (trái/phải)")]
    [SerializeField] float mapWidth = 20f;
    [Tooltip("Tọa độ Y tối đa (trên/dưới)")]
    [SerializeField] float mapHeight = 20f;

    void Start()
    {
        // Bắt đầu vòng lặp rải pha lê liên tục
        StartCoroutine(SpawnCrystalRoutine());
    }

    IEnumerator SpawnCrystalRoutine()
    {
        while (true) // Lặp vô hạn khi game đang chạy
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnRandomCrystal();
        }
    }

    void SpawnRandomCrystal()
    {
        // 1. Tính toán một vị trí ngẫu nhiên trên bản đồ
        float randomX = Random.Range(-mapWidth, mapWidth);
        float randomY = Random.Range(-mapHeight, mapHeight);
        Vector2 randomPosition = new Vector2(randomX, randomY);

        // 2. Lấy pha lê từ Object Pool (Mặc định lấy pha lê xanh, bạn có thể Random cả màu nếu muốn)
        GameObject crystal = ObjectPooling.GetObject(CrystalData.CrystalType.blue);

        // 3. Đặt pha lê ra vị trí ngẫu nhiên và bật nó lên
        crystal.transform.position = randomPosition;
        crystal.SetActive(true);
    }
}