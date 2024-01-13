using Scripts.Data;

namespace Scripts
{
    public interface IAttack
    {
        public void Attack();
    }
    public interface ITakeDamage
    { 
        public void TakeDamage(FloatData damage);
    }

    public interface IDie
    {
        public void Die();
    }
}