using System.Collections;
using UnityEngine;

public class WhipSpawner : WeaponSpawner
{
    protected override IEnumerator StartAttack()
    {
        while (true)
        {
            UpdateAttackPower();
            UpdateAttackSpeed();

            // 1. Sinh ra cây roi hướng chính
            SpawnWeapon(Direction.Self);

            // PHÁT TIẾNG ROI LẦN 1: Khi cây roi đầu tiên xuất hiện
            if (AudioManager.GetInstance() != null)
            {
                AudioManager.GetInstance().PlaySFX(AudioManager.GetInstance().whipSFX);
            }

            yield return new WaitForSeconds(0.1f);

            // 2. Nếu vũ khí đạt Cấp 2 trở lên, vung thêm một cây roi hướng ngược lại
            if (GetLevel() >= 2)
            {
                SpawnWeapon(Direction.Opposite);

                // PHÁT TIẾNG ROI LẦN 2: Cho cây roi hướng ngược lại
                if (AudioManager.GetInstance() != null)
                {
                    AudioManager.GetInstance().PlaySFX(AudioManager.GetInstance().whipSFX);
                }
            }

            yield return new WaitForSeconds(GetAttackSpeed());
        }
    }

    public override void LevelUp()
    {
        switch (GetLevel())
        {
            case 3:
                IncreaseAttackPower(5);
                break;
            case 4:
                IncreaseAttackPower(5);
                IncreaseAdditionalScale(10f);
                break;
            case 5:
                IncreaseAttackPower(5);
                DecreaseAttackSpeed(10f);
                break;
            case 6:
                IncreaseAttackPower(5);
                IncreaseAdditionalScale(10f);
                break;
            case 7:
                IncreaseAttackPower(10);
                DecreaseAttackSpeed(10f);
                break;
        }
    }
}