using Godot;
using System;

namespace Evolution.Genes;

public partial class SpeedGene : Gene
{
    public SpeedGene()
    {
        Name = "Speedy";
        color = Colors.Blue;
    }

    public override float SpeedBonus => 1.2f;
    public override float MetabolismCost => 1.5f;
}
