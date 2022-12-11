using System;
using UnityEngine;

public abstract class BaseUnit : MonoBehaviour
{
    public int   level;
    public float maxLife;
    public float currentLife;
    public float moveSpeed;
    public float expGrantedOnDeath;

    public event EventHandler<EventArgs> Died;
    public void TakeDmg(float dmg)
    {
        currentLife -= dmg;
        
        if (currentLife <= 0)
        {
            OnDied();
            Destroy(this.gameObject);
        }
    }

    private void OnDied()
    {
        Died?.Invoke(this, EventArgs.Empty);
    }
}