using UnityEngine;
using static UnityEngine.ParticleSystem;

public class BackgroundParticles : MonoBehaviour
{
    public static BackgroundParticles instance;

    public ParticleSystem particles;
    public float multiplierSpeed;

    private ParticleSystem.MainModule mainModule;
    private ParticleSystem.EmissionModule emissionModule;
    private ParticleSystem.ShapeModule shapeModule;

    private Camera mainCamera;

    private void Awake()
    {
        instance = this;
        mainCamera = Camera.main;
    }

    private void Start()
    {
        mainModule = particles.main;
        emissionModule = particles.emission;
        shapeModule = particles.shape;
        UpdateParticleSettings(1); // Inicializamos en la primera sala
        AdjustShapeToScreen();
    }

    private void AdjustShapeToScreen()
    {
        if (mainCamera == null) return;

        float screenWidth = mainCamera.orthographicSize * 2f * mainCamera.aspect;
        float screenHeight = mainCamera.orthographicSize * 2f;

        // Ajustar ancho del Shape
        shapeModule.scale = new Vector3(screenWidth, shapeModule.scale.y, shapeModule.scale.z);

        // Ajustar posición 1 unidad por encima de la parte superior de la pantalla
        Vector3 newPosition = mainCamera.transform.position + new Vector3(0, screenHeight / 2f + 0.75f, 0);
        transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
    }

    public void UpdateParticleSettings(int currentRoom)
    {
        float progress = Mathf.Clamp01(currentRoom / 500f); // Normaliza entre 0 y 1

        // Obtener color basado en la progresión
        Color startColor = GetRoomColor(currentRoom);
        mainModule.startColor = startColor;

        // Ajustar cantidad de partículas para mayor impacto visual
        emissionModule.rateOverTime = Mathf.Lerp(10, 25, progress);

        mainModule.simulationSpeed = Mathf.Lerp(0.2f, 1.8f, progress);

        // Ajustar velocidad de partículas
       
    }

    private Color GetRoomColor(int room)
    {
        if (room <= 50) return Color.white;  // Blanco puro
        if (room <= 100) return new Color(1f, 0.98f, 0.8f);  // Amarillo Suave
        if (room <= 150) return new Color(1f, 0.84f, 0f);  // Dorado
        if (room <= 200) return new Color(1f, 0.65f, 0f);  // Naranja
        if (room <= 250) return new Color(1f, 0.55f, 0f);  // Naranja Oscuro
        if (room <= 300) return new Color(1f, 0.27f, 0f);  // Rojo Fuego
        if (room <= 350) return new Color(0.86f, 0.08f, 0.24f);  // Carmesí
        if (room <= 400) return new Color(0.7f, 0.13f, 0.13f);  // Rojo Intenso
        if (room <= 450) return new Color(0.55f, 0f, 0f);  // Rojo Oscuro
        if (room > 450) return new Color(0.3f, 0f, 0f);  // Rojo Sangre
        return Color.black;  // Negro total
    }
}
