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

    public override float AttackDamage => 50f;
    public override float SpeedBonus => 1.3f;
    public override float MetabolismCost => 1.2f;
    public override float ReproductionCost => 130;
}
