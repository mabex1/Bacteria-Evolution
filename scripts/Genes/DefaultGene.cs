using Godot;
using System;

namespace Evolution.Genes;
public partial class DefaultGene : Gene
{
    public DefaultGene()
    {
        color = Colors.Gray;
        Name = "Default";
    }
}    
