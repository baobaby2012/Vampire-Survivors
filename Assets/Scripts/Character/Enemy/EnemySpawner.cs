using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] TextMeshProUGUI killCountText;
    static EnemySpawner instance;
    List<GameObject> enemyList = new List<GameObject>(500);
    const float maxX = 10;
    const float maxY = 16;

  
    float spawnDelay = 0.25f;
    int stage;
    int killCount;

    private EnemySpawner() { }

    enum Direction
    {
        North,
        South,
        West,
        East
    }

    void Awake()
    {
        Initialize();
    }

    void Start()
    {
        StartCoroutine(SpawnEnemy());      
        StartCoroutine(listChecker());     
        StartCoroutine(StageTimer());      
    }

    void Initialize()
    {
        instance = this;
        stage = 1;
        killCount = 0;
    }

   // Chỉnh thời gian spawn quái
    IEnumerator StageTimer()
    {
        while (stage < 5)
        {
            yield return new WaitForSeconds(20f);
            IncreaseStage();
        }
    }

    IEnumerator SpawnEnemy()
    {
        GameObject newEnemy;
        int spawnAmountPerTick = 2; // Mỗi 0.25s đẻ ra 2 con quái

        while (true)
        {
            for (int i = 0; i < spawnAmountPerTick; i++)
            {
                // THAY ĐỔI CỐT LÕI TẠI ĐÂY:
                // Chọn ngẫu nhiên một Stage từ 1 đến Stage hiện tại để lấy quái.
                // Ví dụ: Nếu đang ở Stage 3, biến này sẽ ra ngẫu nhiên số 1, 2, hoặc 3.
                // Do đó game sẽ đẻ TRỘN LẪN cả quái cũ và quái mới vào nhau!
                int randomStagePool = Random.Range(1, stage + 1);

                switch (randomStagePool)
                {
                    default:
                    case 1:
                        newEnemy = ObjectPooling.GetObject(CharacterData.CharacterType.FlyingEye);
                        break;
                    case 2:
                        newEnemy = ObjectPooling.GetObject(CharacterData.CharacterType.Goblin);
                        break;
                    case 3:
                        newEnemy = ObjectPooling.GetObject(CharacterData.CharacterType.Mushroom);
                        break;
                    case 4:
                    case 5:
                        newEnemy = ObjectPooling.GetObject(CharacterData.CharacterType.Skeleton);
                        break;
                }

                if (newEnemy != null)
                {
                    newEnemy.transform.position = RandomPosition();
                    newEnemy.SetActive(true);
                    enemyList.Add(newEnemy);
                }

                // Nếu game đã đạt đến Stage 5 (Độ khó cao nhất), sinh thêm quái ngẫu nhiên liên tục
                if (stage == 5)
                {
                    newEnemy = ObjectPooling.GetObject(CharacterData.CharacterType.FlyingEye);
                    if (newEnemy != null)
                    {
                        newEnemy.transform.position = RandomPosition();
                        newEnemy.SetActive(true);
                        enemyList.Add(newEnemy);
                    }
                }
            }

            // Chờ 0.25 giây rồi tiếp tục vòng lặp đẻ quái kế tiếp
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    Vector3 RandomPosition()
    {
        Vector3 pos = new Vector3();
        Direction direction = (Direction)Random.Range(0, 4);

        switch (direction)
        {
            case Direction.North:
                pos.x = Random.Range(player.transform.position.x - maxX, player.transform.position.x + maxX);
                pos.y = player.transform.position.y + 10f;
                break;
            case Direction.South:
                pos.x = Random.Range(player.transform.position.x - maxX, player.transform.position.x + maxX);
                pos.y = player.transform.position.y - 10f;
                break;
            case Direction.West:
                pos.x = player.transform.position.x - 16f;
                pos.y = Random.Range(player.transform.position.y - maxY, player.transform.position.y + maxY);
                break;
            case Direction.East:
                pos.x = player.transform.position.x + 15f;
                pos.y = Random.Range(player.transform.position.y - maxY, player.transform.position.y + maxY);
                break;
        }

        return pos;
    }

    public Vector2 GetNearestEnemyPosition()
    {
        if (enemyList.Count == 0) return player.position;

        float[] min = { 0, int.MaxValue };

        for (int i = 0; i < enemyList.Count; i++)
        {
            if (enemyList[i] == null) continue;

            float distance = (enemyList[i].transform.position - Player.GetInstance().GetPosition()).sqrMagnitude;
            if (min[1] > distance)
            {
                min[0] = i;
                min[1] = distance;
            }
        }

        return enemyList[(int)min[0]].transform.position;
    }

    public Vector2 GetRandomEnemyPosition()
    {
        if (enemyList.Count == 0) return player.position;
        int random = Random.Range(0, enemyList.Count);
        return enemyList[random].transform.position;
    }

    IEnumerator listChecker()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);

            for (int i = enemyList.Count - 1; i >= 0; i--)
            {
                if (enemyList[i] == null || !enemyList[i].activeSelf)
                {
                    enemyList.RemoveAt(i);
                }
            }
        }
    }

    public void IncreaseStage()
    {
        ++stage;
        Debug.Log("Đã qua thêm 20 giây! Quái mới đã được mở khóa vào hàng ngũ. Stage: " + stage);

        // Đẩy tốc độ đẻ quái dồn dập hơn khi mở khóa giai đoạn mới
        if (stage == 3 || stage == 4)
        {
            spawnDelay *= 0.8f;
        }
    }

    public void IncreaseKillCount()
    {
        ++killCount;
        killCountText.text = killCount.ToString();
    }

    public static EnemySpawner GetInstance()
    {
        return instance;
    }

    public int GetListCount()
    {
        return enemyList.Count;
    }
}