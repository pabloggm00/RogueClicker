using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyController : MonoBehaviour
{
    public int baseHP;
    public int currentHP;

    public Animator anim;

    public void Init(int _baseHP)
    {
        baseHP = _baseHP;
        currentHP = baseHP;
        GameplayController.instance.currentEnemy = this;
    }

    public void TakeDamage(int dmg, bool isCritical, bool isAutoclick)
    {
        currentHP -= dmg;
        anim.SetTrigger("Hurt");
        DamagePopUpGenerator.instance.CreateText(dmg.ToString(), isCritical, isAutoclick);

        if ((float)currentHP / baseHP <= 0.04f)
        {
            Debug.Log("Debe morir");
            Die();
        }
    }

    public void Die()
    {
        currentHP = 0;
        GameplayController.instance.EnemyDeath();
    }
}
