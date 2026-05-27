using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    // Tạo biến tĩnh lưu trữ Singleton
    private static DifficultyManager instance;

    // Hàm GetInstance() mà các Script khác đang tìm kiếm
    public static DifficultyManager GetInstance()
    {
        return instance;
    }

    [Header("--- THỜI GIAN TRẬN ĐẤU ---")]
    private float timeElapsed;

    [Header("--- TỶ LỆ TĂNG TIẾN (MỖI PHÚT) ---")]
    public float hpIncreasePerMinute = 0.4f;      // Máu quái tăng 40% mỗi phút
    public float speedIncreasePerMinute = 0.12f;  // Tốc độ quái tăng 12% mỗi phút
    public float spawnSpeedUpPerMinute = 0.15f;   // Tốc độ spawn tăng 15% mỗi phút
    public float expRequiredIncrease = 0.3f;      // Yêu cầu EXP thăng cấp tăng thêm 30% mỗi phút

    void Awake()
    {
        // Khởi tạo Singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;
    }

    // Các hàm trả về hệ số nhân dựa trên số phút trôi qua
    public float GetHPMultiplier() => 1f + (timeElapsed / 60f) * hpIncreasePerMinute;
    public float GetSpeedMultiplier() => 1f + (timeElapsed / 60f) * speedIncreasePerMinute;
    public float GetSpawnIntervalMultiplier() => Mathf.Max(0.2f, 1f - (timeElapsed / 60f) * spawnSpeedUpPerMinute);
    public float GetXPMultiplier() => 1f + (timeElapsed / 60f) * expRequiredIncrease;
}