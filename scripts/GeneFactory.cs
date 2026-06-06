using Godot;
using System;
using Evolution.Genes;
public partial class GeneFactory
{
	private static Random _random = new Random();
	public static Gene CreateRandom()
	{
        int roll = _random.Next(0, 100);

        if (roll < 18) return new DefaultGene();      // 20%
        if (roll < 35) return new SpeedGene();        // 15%
        if (roll < 50) return new HunterGene();       // 15%
        if (roll < 62) return new ResistanceGene();   // 12%
        if (roll < 73) return new PeaceGene();        // 11%
        if (roll < 83) return new SciencetistGene();  // 10%
        if (roll < 91) return new ArmorGene();        // 8%

        if (roll < 8) return new VampireGene(); //idk
        return new VampireGene();
    }

    public static Gene Mutate (Gene original)
    {
        double MutationChance = 0.05 + original.MutationChanceBonus;

        if(_random.NextDouble() < MutationChance)
        {
            return CreateRandom();
        }
        return original;
    }
}
