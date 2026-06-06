using Godot;
using System;

namespace Evolution.Genes;
public partial class Gene : Node
{
	//base gene
	public Color color { get; protected set; }
	public string Name { get; protected set; }

	public virtual float SpeedBonus => 1.0f;
    public virtual float MetabolismCost => 0.8f;
    public virtual float AttackDamage => 0f;
    public virtual float DefenseBonus => 0f;
    public virtual float VampireSteal => 0f;
    public virtual float ReproductionCost => 100f;
    public virtual float MutationChanceBonus => 0f;

}
