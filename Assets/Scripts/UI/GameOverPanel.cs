using UnityEngine;
using TMPro;

public class GameOverPanel : MonoBehaviour
{
    [Header("--- THÀNH PHẦN HIỂN THỊ (BẢNG DEAD) ---")]
    [SerializeField] private TextMeshProUGUI finalKillsText;
    [SerializeField] private TextMeshProUGUI finalTimeText;
    [SerializeField] private TextMeshProUGUI finalLevelText; // THÊM MỚI: Khung chữ hiện Level kết thúc

    [Header("--- THÀNH PHẦN HUD GỐC ---")]
    [Tooltip("Kéo Object 'Time' ở ngoài Canvas vào đây")]
    [SerializeField] private TextMeshProUGUI hudTimerText;
    [Tooltip("Kéo Object Text hiển thị chữ LV ở góc trên bên phải vào đây")]
    [SerializeField] private TextMeshProUGUI hudLevelText;   // THÊM MỚI: Ô chứa Text Level trên HUD

    void OnEnable()
    {
        // 1. Tự động lấy số lượng Kill từ EnemySpawner sang
        if (EnemySpawner.GetInstance() != null)
        {
            finalKillsText.text = "KILLS: " + EnemySpawner.GetInstance().GetKillCount();
        }
        else
        {
            finalKillsText.text = "KILLS: 0";
        }

        // 2. Lấy trực tiếp chuỗi thời gian đang chạy đưa vào bảng kết quả
        if (hudTimerText != null)
        {
            finalTimeText.text = "TIME: " + hudTimerText.text;
        }
        else
        {
            finalTimeText.text = "TIME: --:--";
        }

        // 3. THÊM MỚI: Lấy chữ số Level đạt được từ HUD đưa vào bảng kết quả
        if (hudLevelText != null)
        {
            finalLevelText.text = "LEVEL: " + hudLevelText.text; // Kết quả sẽ hiển thị dạng: LEVEL: LV 5
        }
        else
        {
            finalLevelText.text = "LEVEL: --";
        }
    }
}