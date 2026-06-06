using Godot;
using System;

namespace Evolution.Genes;
public partial class SciencetistGene : Gene
{
    public SciencetistGene()
    {
        Name = "Sciencetist";
        color = Colors.LightBlue;
    }

    public override float MutationChanceBonus => 15f;
}
