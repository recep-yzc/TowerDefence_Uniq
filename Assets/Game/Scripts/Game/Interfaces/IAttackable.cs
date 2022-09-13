namespace Game.Interface
{
    public interface IAttackable
    {
        public void Hit(IDamageable damageable, float damage);
    }
}