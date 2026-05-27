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
        int baseSpawnAmountPerTick = 2; // Khởi đầu mỗi nhịp đẻ ra 2 con quái

        while (true)
        {
            // MỚI THÊM 1: Càng lên Stage cao, số lượng quái xuất hiện trong CÙNG 1 LÚC càng nhiều
            int currentSpawnAmount = baseSpawnAmountPerTick + (stage - 1);

            for (int i = 0; i < currentSpawnAmount; i++)
            {
                // Chọn ngẫu nhiên một Stage từ 1 đến Stage hiện tại để lấy quái.
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

            // MỚI THÊM 2: Lấy thời gian chờ gốc (spawnDelay) nhân với hệ số giảm dần từ DifficultyManager
            // Điều này ép thời gian sinh quái bị bóp ngắn lại liên tục khi trận đấu kéo dài
            float actualDelay = spawnDelay;
            if (DifficultyManager.GetInstance() != null)
            {
                actualDelay *= DifficultyManager.GetInstance().GetSpawnIntervalMultiplier();
            }

            // Chờ theo thời gian đã được bóp ngắn rồi tiếp tục vòng lặp
            yield return new WaitForSeconds(actualDelay);
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
   
    public int GetKillCount()
    {
        return killCount;
    }
}