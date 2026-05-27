using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whip : Weapon
{
    List<GameObject> hitEnemies = new List<GameObject>();

    [Header("--- CẤU HÌNH HÚT MÁU ---")]
    [Tooltip("Tỉ lệ hút máu: 0.1 = 10% sát thương, 0.2 = 20% sát thương gây ra...")]
    [Range(0f, 1f)]
    [SerializeField] private float lifestealRatio = 0.1f; // Mặc định hút 10% sát thương, bạn có thể chỉnh thông số này ở Inspector

    void OnEnable()
    {
        hitEnemies.Clear();
        StartCoroutine(StartDestroy());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            GameObject enemyObj = collision.gameObject;

            if (!hitEnemies.Contains(enemyObj))
            {
                hitEnemies.Add(enemyObj);

                // 1. Tính toán sát thương ngẫu nhiên dựa trên attackPower hiện tại
                int damage = RandomDamage(attackPower);
                enemyObj.GetComponent<Enemy>().ReduceHealthPoint(damage);

                // 2. TÍNH TOÁN LẠI TỶ LỆ HÚT MÁU ĐỘNG:
                // Lấy lượng sát thương thực tế nhân với tỉ lệ phần trăm
                int healAmount = Mathf.RoundToInt(damage * lifestealRatio);

                // Chỉ hồi máu khi lượng máu hút được lớn hơn 0
                if (healAmount > 0 && Player.GetInstance() != null)
                {
                    Player.GetInstance().Heal(healAmount);
                    // Bật dòng dưới này nếu bạn muốn kiểm tra số máu hồi được trong tab Console của Unity
                    // Debug.Log($"[Whip] Gây {damage} dame -> Hút được {healAmount} HP ({lifestealRatio * 100}%)");
                }
            }
        }
    }
}