using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static public GameManager INSTANCE;
    private int enemies = 0;
    private Health playerHealth;

    public int Enemies { get => enemies; set => enemies = value; }
    public Health PlayerHealth { get => playerHealth; set => playerHealth = value; }
    public void Awake()
    {
        if(INSTANCE == null)
        {
            INSTANCE = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        enemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    public void EnemyKilled()
    {
        enemies--;
        EnemyNumberChanged?.Invoke(enemies);
        if(enemies <= 0)
        {
            Victory();
        }
    }

    public System.Action<int> EnemyNumberChanged;

    public void Victory()
    {
        SceneManager.LoadScene("Victory");
    }
}
