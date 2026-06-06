using Godot;
using System;

namespace Evolution.Genes;
public partial class VampireGene : Gene
{
	public VampireGene()
	{
		Name = "Vampire";
		color = Colors.Purple;
	}

    public override float VampireSteal => 0.40f;
    public override float SpeedBonus => 1.5f;
    public override float MetabolismCost => 0.8f;
	public override float ReproductionCost => 90f;
}
