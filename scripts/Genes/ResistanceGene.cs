using Godot;
using System;

namespace Evolution.Genes;

public partial class ResistanceGene : Gene
{

    public ResistanceGene()
    {
        Name = "Resistance";
        color = Colors.Green;
    }

    public override float MetabolismCost => 0.5f;
    public override float SpeedBonus => 0.85f;
    public override float ReproductionCost => 150f;
}
