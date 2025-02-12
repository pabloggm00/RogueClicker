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

    // Funci�n para aumentar llaves
    public void AumentarLlaves(int cantidad)
    {
        keys += cantidad;
    }

    // Funci�n para aumentar vitalidad
    public void AumentarVitalidad(int cantidad)
    {
        vitalidad += cantidad;
    }

    // Funci�n para disminuir llaves
    public void DisminuirLlaves(int cantidad)
    {
        keys -= cantidad;
    }

    // Funci�n para disminuir vitalidad
    public void DisminuirVitalidad(int cantidad)
    {
        vitalidad -= cantidad;
    }

    // Funci�n para aumentar el da�o de click
    public void AumentarDmg(int cantidad)
    {
        dmgClick += cantidad;
    }

    // Funci�n para aumentar el da�o de autoclick
    public void AumentarAutoclickDmg(int cantidad)
    {
        dmgAutoclick += cantidad;
    }

    // Funci�n para aumentar el intervalo de autoclicks
    public void AumentarIntervalAutoclick(float cantidad)
    {
        intervalAutoclick += cantidad;
    }

    // Funci�n para disminuir el intervalo de autoclicks
    public void DisminuirIntervalAutoclick(float cantidad)
    {
        intervalAutoclick -= cantidad;
    }

    // Funci�n para aumentar el da�o cr�tico
    public void AumentarCriticalDmg(float cantidad)
    {
        criticalDmg += cantidad;
    }

    // Funci�n para aumentar la probabilidad de cr�tico
    public void AumentarCriticalProb(float cantidad)
    {
        criticalProb += cantidad;
    }

    #endregion

    // Funci�n para calcular el da�o total con probabilidad de cr�tico
    public CalculateDamagePlayer CalcularDanoTotal()
    {

        CalculateDamagePlayer damagePlayer = new CalculateDamagePlayer();

        // Generar un valor aleatorio entre 0 y 1
        float probabilidad = UnityEngine.Random.Range(0f, 1f);

        // Si la probabilidad es menor que la probabilidad cr�tica, el da�o es multiplicado por el da�o cr�tico
        if (probabilidad < criticalProb)
        {
            damagePlayer.dmg = Mathf.FloorToInt(dmgClick * criticalDmg); // Aplica el da�o cr�tico
            damagePlayer.isCritical = true;
        }
        else
        {
            damagePlayer.dmg = dmgClick; // Si no hay cr�tico, solo se devuelve el da�o normal
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
