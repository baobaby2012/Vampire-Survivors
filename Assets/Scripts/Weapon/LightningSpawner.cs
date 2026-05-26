using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningSpawner : WeaponSpawner
{
   
    protected override IEnumerator StartAttack()
    {
        EnemySpawner enemySpawner = EnemySpawner.GetInstance();

        while (true)
        {
            UpdateAttackPower();
            UpdateAttackSpeed();

            // --- Tia sét 1 (Cơ bản) ---
            if (enemySpawner.GetListCount() > 0)
            {
                SpawnWeapon(Direction.Right);
                PlayLightningSound();
            }

            yield return new WaitForSeconds(0.1f);

            // --- Tia sét 2 ---
            if (enemySpawner.GetListCount() > 0)
            {
                SpawnWeapon(Direction.Right);
                PlayLightningSound();
            }

            yield return new WaitForSeconds(0.1f);

            // --- Tia sét 3 (Yêu cầu Level >= 2) ---
            if (GetLevel() >= 2 && enemySpawner.GetListCount() > 0)
            {
                SpawnWeapon(Direction.Right);
                PlayLightningSound();
            }

            yield return new WaitForSeconds(0.1f);

            // --- Tia sét 4 (Yêu cầu Level >= 3) ---
            if (GetLevel() >= 3 && enemySpawner.GetListCount() > 0)
            {
                SpawnWeapon(Direction.Right);
                PlayLightningSound();
            }

            yield return new WaitForSeconds(0.1f);

            // --- Tia sét 5 (Yêu cầu Level >= 5) ---
            if (GetLevel() >= 5 && enemySpawner.GetListCount() > 0)
            {
                SpawnWeapon(Direction.Right);
                PlayLightningSound();
            }

            yield return new WaitForSeconds(0.1f);

            // --- Tia sét 6 (Yêu cầu Level >= 7) ---
            if (GetLevel() >= 7 && enemySpawner.GetListCount() > 0)
            {
                SpawnWeapon(Direction.Right);
                PlayLightningSound();
            }

            // Đợi hồi chiêu vũ khí trước khi kích hoạt đợt bão sét tiếp theo
            yield return new WaitForSeconds(GetAttackSpeed());
        }
    }

  
    private void PlayLightningSound()
    {
        if (AudioManager.GetInstance() != null)
        {
            AudioManager.GetInstance().PlaySFX(AudioManager.GetInstance().lightningSFX);
        }
    }
}