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

    public override float MetabolismCost => 0.7f;
    public override float DefenseBonus => 30f;
}
