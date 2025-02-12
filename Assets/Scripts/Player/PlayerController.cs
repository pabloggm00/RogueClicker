using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UIElements;


public class PlayerController : MonoBehaviour
{

    [Header("Damage")]
    public int dmgClick;
    public int dmgAutoclick;
    public float intervalAutoclick;
    [Range(0,1)]
    public float criticalProb;
    public float criticalDmg;

    [Header("HUD")]
    public int keys;
    public int vitalidad;

    [Header("Control")]
    public bool isAutoclick;
    public static event Action<int,bool,bool> OnPlayerAutoClick;

    private void OnEnable()
    {
        InputManager.playerControls.Player.Attack.performed += OnAttackInput;
        InputManager.playerControls.Player.Attack.canceled += OnAttackInput;
    }

    private void OnDisable()
    {
        InputManager.playerControls.Player.Attack.performed -= OnAttackInput;
        InputManager.playerControls.Player.Attack.canceled -= OnAttackInput;
    }

    public void OnAttackInput(InputAction.CallbackContext context)
    {

        /*if (context.performed) return;

        //Vector2 position = context.action.;
        Debug.Log("Estoy tocando");
        Vector2 ray = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
        RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero);

        if (hit.transform != null)
        {
            if (hit.transform.TryGetComponent<EnemyController>(out EnemyController enemyController))
            {
                enemyController.TakeDamage(CalcularDanoTotal());

            }

        }*/
    }


    private void Update()
    {
        if (Input.touchCount > 0)
        {
            UnityEngine.Touch touch = Input.GetTouch(0);

            if (touch.phase == UnityEngine.TouchPhase.Began)
            {
               
                Vector2 ray = Camera.main.ScreenToWorldPoint(touch.position);
                RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero);

                if (hit.transform != null)
                {
                    if (hit.transform.TryGetComponent<EnemyController>(out EnemyController enemyController))
                    {
                        CalculateDamagePlayer damagePlayer = CalcularDanoTotal();
                        enemyController.TakeDamage(damagePlayer.dmg, damagePlayer.isCritical, false);

                    }

                }
            }
        }

        
    }


    public void StartAutoClick()
    {
        InvokeRepeating("AutoClick", 1f, intervalAutoclick);
    }

    public void StopAutoclick()
    {
        CancelInvoke("AutoClick"); 
    }

    void AutoClick()
    {
        if (!isAutoclick) return;

        int dmg = dmgAutoclick;

        if (dmg == 0)
            return;

        OnPlayerAutoClick?.Invoke(dmg, false, true); //no es critico y si es autoclick
    }


    #region Modificadores

    // Función para aumentar llaves
    public void AumentarLlaves(int cantidad)
    {
        keys += cantidad;
    }

    // Función para aumentar vitalidad
    public void AumentarVitalidad(int cantidad)
    {
        vitalidad += cantidad;
    }

    // Función para disminuir llaves
    public void DisminuirLlaves(int cantidad)
    {
        keys -= cantidad;
    }

    // Función para disminuir vitalidad
    public void DisminuirVitalidad(int cantidad)
    {
        vitalidad -= cantidad;
    }

    // Función para aumentar el daño de click
    public void AumentarDmg(int cantidad)
    {
        dmgClick += cantidad;
    }

    // Función para aumentar el daño de autoclick
    public void AumentarAutoclickDmg(int cantidad)
    {
        dmgAutoclick += cantidad;
    }

    // Función para aumentar el intervalo de autoclicks
    public void AumentarIntervalAutoclick(float cantidad)
    {
        intervalAutoclick += cantidad;
    }

    // Función para disminuir el intervalo de autoclicks
    public void DisminuirIntervalAutoclick(float cantidad)
    {
        intervalAutoclick -= cantidad;
    }

    // Función para aumentar el daño crítico
    public void AumentarCriticalDmg(float cantidad)
    {
        criticalDmg += cantidad;
    }

    // Función para aumentar la probabilidad de crítico
    public void AumentarCriticalProb(float cantidad)
    {
        criticalProb += cantidad;
    }

    #endregion

    // Función para calcular el daño total con probabilidad de crítico
    public CalculateDamagePlayer CalcularDanoTotal()
    {

        CalculateDamagePlayer damagePlayer = new CalculateDamagePlayer();

        // Generar un valor aleatorio entre 0 y 1
        float probabilidad = UnityEngine.Random.Range(0f, 1f);

        // Si la probabilidad es menor que la probabilidad crítica, el daño es multiplicado por el daño crítico
        if (probabilidad < criticalProb)
        {
            damagePlayer.dmg = Mathf.FloorToInt(dmgClick * criticalDmg); // Aplica el daño crítico
            damagePlayer.isCritical = true;
        }
        else
        {
            damagePlayer.dmg = dmgClick; // Si no hay crítico, solo se devuelve el daño normal
            damagePlayer.isCritical = false;
        }

        return damagePlayer;
    }

    public class CalculateDamagePlayer
    {
        public int dmg;
        public bool isCritical;
    }
}
