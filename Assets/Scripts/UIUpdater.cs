using UnityEngine;
using TMPro;

public class UIUpdater : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI lifeText;
    [SerializeField] private TextMeshProUGUI enemyText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateEnemyText(GameManager.INSTANCE.Enemies);
        GameManager.INSTANCE.EnemyNumberChanged += UpdateEnemyText;
        lifeText.text = "100";
        GameManager.INSTANCE.PlayerHealth.OnHealthChanged += UpdateLifeText;
    }

    private void UpdateLifeText(int life)
    {
        lifeText.text = life.ToString();
    }

    private void UpdateEnemyText(int enemies)
    {
        enemyText.text = enemies.ToString();
    }
}
