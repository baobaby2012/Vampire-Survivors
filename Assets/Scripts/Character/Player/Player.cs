using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{
    [SerializeField] Slider hpSlider;
    [SerializeField] ParticleSystem bleeding;
    [SerializeField] GameObject GameOverWindow;
    static Player instance;
    float attackSpeed;
    float expAdditional;
    int luck;
    bool isColliding; // Biến này bây giờ sẽ quản lý trạng thái bất tử tạm thời

    private Player() { }

    void Awake()
    {
        Initialize();
    }

    protected override void Initialize()
    {
        base.Initialize();
        GameOverWindow.SetActive(false);
        instance = this;
        attackSpeed = 100f;
        expAdditional = 100f;
        luck = 0;
        hpSlider.maxValue = GetHealthPoint();
        hpSlider.value = GetHealthPoint();
        isColliding = false;

        GetFirstWeapon();
    }

    public static Player GetInstance()
    {
        return instance;
    }

    public float GetAttackSpeed()
    {
        return attackSpeed;
    }

    public float GetExpAdditional()
    {
        return expAdditional;
    }

    public int GetLuck()
    {
        return luck;
    }

    public void DecreaseAttackSpeed(float value)
    {
        attackSpeed -= value;
    }

    public void IncreaseExpAdditional(float value)
    {
        expAdditional += value;
    }

    public void IncreaseLuck(int value)
    {
        luck += value;
    }

    public void Heal(int healAmount)
    {
        if (PlayerMove.GetInstance().isDead) return;

        RecoverHealthPoint(healAmount);
        hpSlider.value = GetHealthPoint();
    }

    public override void Die()
    {
        PlayerMove.GetInstance().isDead = true;
        StartCoroutine(DieAnimation());
    }

    protected override IEnumerator DieAnimation()
    {
        GetAnimator().SetBool("Death", true);

        yield return new WaitForSeconds(1f);

        GameOverWindow.SetActive(true);
        Time.timeScale = 0f;
    }

    void GetFirstWeapon()
    {
        // Chỉnh sửa lại GetComponent để lấy chính xác từ GameObject hiện tại thay vì Parent
        switch (GetComponent<Player>().GetCharacterType())
        {
            case CharacterData.CharacterType.Knight:
                Inventory.GetInstance().AddWeapon(WeaponData.WeaponType.Whip);
                break;
            case CharacterData.CharacterType.Bandit:
                Inventory.GetInstance().AddWeapon(WeaponData.WeaponType.Axe);
                break;
        }
    }

    public override void ReduceHealthPoint(int damage)
    {
        if (PlayerMove.GetInstance().isDead || isColliding) return;

        base.ReduceHealthPoint(damage);
        hpSlider.value = GetHealthPoint();
        bleeding.Play();

        isColliding = true;

    

        if (hitCoroutine == null)
            hitCoroutine = StartCoroutine(UnderAttack());
    }

    protected override IEnumerator UnderAttack()
    {
        // Chớp nháy đỏ để báo hiệu dính đòn
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        spriteRenderer.color = Color.white;

        // Đợi thêm một khoảng ngắn (Thời gian bất tử bảo vệ Player, ví dụ 0.2 giây)
        yield return new WaitForSeconds(0.2f);

        // Hết thời gian bất tử, cho phép nhận sát thương trở lại
        isColliding = false;

        hitCoroutine = null;
    }
}