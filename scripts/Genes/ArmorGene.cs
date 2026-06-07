using Godot;
using System;

namespace Evolution.Genes;
public partial class ArmorGene : Gene
{
	public ArmorGene()
	{
		Name = "Tank";
		color = Colors.Orange;
	}

    public override float MetabolismCost => 0.9f;
    public override float DefenseBonus => 0.4f;
    public override float ReproductionCost => 120f;
    public override float SpeedBonus => 0.7f;
}
