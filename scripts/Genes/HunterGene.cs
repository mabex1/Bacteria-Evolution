using Godot;
using System;

namespace Evolution.Genes;
public partial class HunterGene : Gene
{
    public HunterGene()
    {
        Name = "Hunter";
        color = Colors.Red;
    }

    public override float AttackDamage => 100f;
}
