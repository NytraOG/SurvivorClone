using UnityEngine;

public abstract class BaseUnit : MonoBehaviour
{
    public int   level;
    public float maxLife;
    public float currentLife;
    public float moveSpeed;
    public float expGrantedOnDeath;
}