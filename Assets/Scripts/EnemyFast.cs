using UnityEngine;

public class EnemyFast : Enemy
{
    public new void Start()
    {
        base.Start();
        this.aggro = this.aggro * 2f;
    }

    protected override void Move()
    {
        base.Move();
    }
}
