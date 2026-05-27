using UnityEngine;
using TMPro;

public class GlitchBlinkEffect : MonoBehaviour
{
    private TextMeshProUGUI textComponent;

    [Header("--- CÀI ĐẶT CHỚP TẮT (BLINK) ---")]
    [Tooltip("Thời gian chữ sáng/tắt. Số càng nhỏ chớp giật càng nhanh")]
    public float blinkSpeed = 0.15f;
    private float blinkTimer;
    private bool isVisible = true;

    [Header("--- ĐỔI MÀU LIÊN TỤC (RGB RAINBOW) ---")]
    [Tooltip("Tốc độ chuyển đổi màu sắc cầu vồng")]
    public float colorSpeed = 2.5f;

    void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    void OnEnable()
    {
        blinkTimer = 0f;
        isVisible = true;
    }

    void Update()
    {
        if (textComponent == null) return;

        // 1. Tạo màu cầu vồng chạy liên tục theo thời gian
        float hue = (Time.time * colorSpeed) % 1f;
        Color rainbowColor = Color.HSVToRGB(hue, 1f, 1f); // Hệ màu HSV cho ra màu RGB cực tươi

        // 2. Xử lý bộ đếm thời gian chớp tắt
        blinkTimer += Time.deltaTime;
        if (blinkTimer >= blinkSpeed)
        {
            isVisible = !isVisible; // Đảo trạng thái ẩn <-> hiện
            blinkTimer = 0f;        // Reset lại thời gian đếm
        }

        // 3. ÉP ALPHA VÀO MÀU (Bí kíp để TextMeshPro bắt buộc phải chớp tắt)
        if (isVisible)
        {
            rainbowColor.a = 1f; // Hiện chữ 100%
        }
        else
        {
            rainbowColor.a = 0f; // Biến mất hoàn toàn không tì vết
        }

        // Kích hoạt màu và hiệu ứng lên chữ
        textComponent.color = rainbowColor;
    }
}