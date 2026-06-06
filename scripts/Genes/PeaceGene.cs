using Godot;
using System;

namespace Evolution.Genes;
public partial class PeaceGene : Gene
{
    public PeaceGene()
    {
        Name = "Peaceful";
        color = Colors.WhiteSmoke;
    }

    public override float ReproductionCost => 150f;
}
