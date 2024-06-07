using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _maxHp;

    public int Hp { get; private set; }

    private void Start()
    {
        Hp = _maxHp;
    }

    public void AddDamage(int damage)
    {
        Hp -= damage;
        if (Hp <= 0)
        {
            Hp = 0;
        }
    }
}