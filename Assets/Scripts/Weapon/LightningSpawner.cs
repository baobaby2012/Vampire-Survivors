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

            if (enemySpawner.GetListCount() > 0)
            {
                SpawnWeapon(Direction.Right);
                PlayLightningSound();
            }

            yield return new WaitForSeconds(0.1f);

            if (enemySpawner.GetListCount() > 0)
            {
                SpawnWeapon(Direction.Right);
                PlayLightningSound();
            }

            yield return new WaitForSeconds(0.1f);

            if (GetLevel() >= 2 && enemySpawner.GetListCount() > 0)
            {
                SpawnWeapon(Direction.Right);
                PlayLightningSound();
            }

            yield return new WaitForSeconds(0.1f);

            if (GetLevel() >= 3 && enemySpawner.GetListCount() > 0)
            {
                SpawnWeapon(Direction.Right);
                PlayLightningSound();
            }

            yield return new WaitForSeconds(0.1f);

            if (GetLevel() >= 5 && enemySpawner.GetListCount() > 0)
            {
                SpawnWeapon(Direction.Right);
                PlayLightningSound();
            }

            yield return new WaitForSeconds(0.1f);

            if (GetLevel() >= 7 && enemySpawner.GetListCount() > 0)
            {
                SpawnWeapon(Direction.Right);
                PlayLightningSound();
            }

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