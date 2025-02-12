using TMPro;
using UnityEngine;

public class DamagePopUpGenerator : MonoBehaviour
{
    public static DamagePopUpGenerator instance;

    public float timeToDestroy = 1f;

    [Header("Zona de Spawn")]
    public Vector2 spawnAreaMin; // Punto mínimo de la zona
    public Vector2 spawnAreaMax; // Punto máximo de la zona

    [Header("Tipo de daño")]
    public GameObject prefabTextNormalDamage;
    public GameObject prefabTextCriticalDamage;
    public GameObject prefabTextAutoClickDamage;

    private void Awake()
    {
        instance = this;
    }

    public void CreateText(string number, bool isCritical, bool isAutoClick)
    {
        float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        float randomY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
        Vector2 spawnPosition = new Vector2(randomX, randomY);

        GameObject prefabText;

        if (isAutoClick)
        {
            prefabText = prefabTextAutoClickDamage;
        }
        else
        {
            prefabText = isCritical ? prefabTextCriticalDamage : prefabTextNormalDamage;
        }

        

        GameObject textObject = Instantiate(prefabText, spawnPosition, Quaternion.identity, transform);
        TMP_Text text = textObject.GetComponent<TMP_Text>();
        text.text = number;
        Destroy(textObject, timeToDestroy);



    }

    public void DestroyAllNumber()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        // Calculamos el centro y el tamaño de la caja
        Vector2 center = (spawnAreaMin + spawnAreaMax) / 2f;
        Vector2 size = spawnAreaMax - spawnAreaMin;

        // Establecemos el color del gizmo
        Gizmos.color = Color.green;
        // Dibujamos un cubo con wireframe que representa la zona
        Gizmos.DrawWireCube(center, size);
    }
}
