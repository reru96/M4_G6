using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class LifeController : MonoBehaviour
{
    [SerializeField] private int maxHp = 10;
    [SerializeField] private int currentHp = 10;
    [SerializeField] private bool fullHpOnAwake = true;

    public int GetMax() => maxHp;
    public int GetHp() => currentHp;

    [SerializeField] private UnityEvent<int, int> onLifeChanged;
    [SerializeField] private UnityEvent onDeath;

    private void Awake()
    {
        if (fullHpOnAwake)
        {
            SetHp(maxHp); 
        }
    }

    public void SetHp(int hp)
    {
        int oldHp = currentHp;
        currentHp = Mathf.Clamp(hp, 0, maxHp); 

        Debug.Log($"HP aggiornati: {currentHp}/{maxHp}");
        onLifeChanged?.Invoke(currentHp, maxHp); 

        if (oldHp > 0 && currentHp == 0)
        {
            Debug.Log($"Il personaggio {gameObject.name} è deceduto");
            onDeath?.Invoke();
        }
    }

    public void AddHp(int amount)
    {
        SetHp(currentHp + amount);
    }
}
