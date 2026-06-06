using Godot;
using System;

public partial class Food : Area2D
{
	[Export] public int EnergyValue = 40;
	private Sprite2D _sprite;

	public override void _Ready()
	{
		_sprite = GetNode<Sprite2D>("Sprite2D");
		BodyEntered += OnBodyEntered;
	}
	
	private void OnBodyEntered(Node2D body)
	{
		if (body is Bacteria bacteria)
		{
			bacteria.AddEnergy(EnergyValue);
			QueueFree();
		}
	}
}
