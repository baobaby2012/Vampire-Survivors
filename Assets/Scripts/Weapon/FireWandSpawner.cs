using System.Collections;
using UnityEngine;

public class FireWandSpawner : WeaponSpawner
{
    int effectNum = 3;           
    const float spreadAngle = 15f; 
    const float speed = 200f;      
    const float delay = 0.07f;    

    protected override IEnumerator StartAttack()
    {
        EnemySpawner enemySpawner = EnemySpawner.GetInstance();

        while (true)
        {
            UpdateAttackPower();
            UpdateAttackSpeed();

         
            if (enemySpawner.GetListCount() > 0)
            {
                Vector2 destination = (enemySpawner.GetRandomEnemyPosition() -
                                      (Vector2)transform.position).normalized;
                float newSpreadAngle = 0f;

                for (int i = 0; i < effectNum; ++i)
                {
                    if (i % 2 == 1) newSpreadAngle += spreadAngle;

                    SpawnWeapon(newSpreadAngle, destination);
                    if (AudioManager.GetInstance() != null)
                        AudioManager.GetInstance().PlaySFX(AudioManager.GetInstance().fireballSFX);

                    yield return new WaitForSeconds(delay); 

                    newSpreadAngle *= -1; 
                }
            }
            yield return new WaitForSeconds(GetAttackSpeed());
        }
    }

    void SpawnWeapon(float spreadAngle, Vector2 destination)
    {
        // 1. Lấy thực thể đạn từ Object Pooling
        GameObject weapon = ObjectPooling.GetObject(GetWeaponType());

        // 2. Định vị tọa độ xuất hiện cho viên đạn
        weapon.transform.position = GetWeaponData().GetBasePosition();
        if (GetWeaponData().GetParent().Equals(WeaponData.Parent.Self))
            weapon.transform.position += Player.GetInstance().GetPosition();

        // 3. KHÔI PHỤC: Truyền dữ liệu vào đạn (SỬA LỖI NULL LINE 48 Ở ĐÂY)
        weapon.GetComponent<Weapon>().SetParameters(
            GetWeaponData(), GetAttackPower(), GetInactiveDelay(), Direction.Self
        );

        // 4. KHÔI PHỤC: Cập nhật kích thước (Scale) theo cấp độ gậy phép
        float scaleRatio = GetAdditionalScale() / 100f;
        weapon.transform.localScale = new Vector2(
            GetWeaponData().GetBaseScale().x * scaleRatio,
            GetWeaponData().GetBaseScale().y * scaleRatio
        );

        // 5. Thuật toán lượng giác xoay ma trận hướng đạn 2D
        if (spreadAngle != 0f)
        {
            float rad = spreadAngle / 180f * Mathf.PI; // Đổi sang Radian
            float rx = destination.x * Mathf.Cos(rad) - destination.y * Mathf.Sin(rad);
            float ry = destination.x * Mathf.Sin(rad) + destination.y * Mathf.Cos(rad);
            destination = new Vector2(rx, ry);
        }

        Vector2 destVector = destination.normalized;
        float angle = (destVector.y < 0) ? -Vector2.Angle(destVector, new Vector2(1, 0))
                                         : Vector2.Angle(destVector, new Vector2(1, 0));
        weapon.transform.rotation = Quaternion.Euler(0f, 0f, angle - 8.5f);
        weapon.SetActive(true); // Kích hoạt đạn

        // 6. Nạp lực vật lý đẩy đạn bay đi
        Rigidbody2D rb = weapon.GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero; // Reset vận tốc cũ trong Pool
        rb.AddForce(destVector * speed, ForceMode2D.Force);
    }
}