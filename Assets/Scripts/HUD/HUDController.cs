using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public TMP_Text currentRoomText;
    public TMP_Text keysText;

    [Header("HP")]
    public GameObject hpObject;
    public Image enemyHP;
    private GameplayController gameplayController;

    private void Start()
    {
        gameplayController = GameplayController.instance;
    }

    private void OnEnable()
    {
        RoomGenerator.OnEnemyDeath += HideHP;
        EnemyRoom.OnInitEnemy += ShowHP;
    }

    private void OnDisable()
    {
        RoomGenerator.OnEnemyDeath -= HideHP;
        EnemyRoom.OnInitEnemy -= ShowHP;
    }

    // Update is called once per frame
    void Update()
    {
        currentRoomText.text = gameplayController.currentRoom.ToString();

        keysText.text = gameplayController.GetKeys().ToString();

        if (gameplayController.currentEnemy != null && hpObject.activeSelf)
        {
            enemyHP.fillAmount = (float)gameplayController.currentEnemy.currentHP / gameplayController.currentEnemy.baseHP;
        }

    }

    public void HideHP()
    {
        hpObject.SetActive(false);
    }

    public void ShowHP()
    {
        hpObject.SetActive(true);
    }
}
