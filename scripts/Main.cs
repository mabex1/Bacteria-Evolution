using Godot;
using System;
using Evolution.Genes;

public partial class Main : Node2D
{
	[Export] public PackedScene BacteriaScene;
	[Export] public PackedScene FoodScene;

	[Export] public float FoodSpawnDelay = 2f;
	[Export] public Vector2 SpawnBounds = new Vector2(1910, 1070);

	private Timer _foodTimer;
	private Timer _statsTimer;
	private int _bacteriaCount = 0;
	private int _foodCount = 0;
    private float _spawnTimer = 0f;

    public override void _Ready()
	{
		if (BacteriaScene == null)
		{
			GD.PrintErr("BacteriaScene not set.");
			return;
		}

        if (FoodScene == null)
        {
            GD.PrintErr("FoodScene not set.");
			return;
        }

		SpawnInitBacteria();

		//_foodTimer = new Timer();
		//_foodTimer.WaitTime = 1.0f;
		//_foodTimer.Timeout += UpdateStats;
		//AddChild(_foodTimer);
		//_foodTimer.Start();

		//_statsTimer = new Timer();
		//_statsTimer.WaitTime = 1.0f;
		//_statsTimer.Timeout += UpdateStats;
		//AddChild(_statsTimer);
		//_statsTimer.Start();
    }

	private void SpawnInitBacteria()
	{
		Bacteria bacteria = (Bacteria)BacteriaScene.Instantiate();
        Vector2 screenSize = GetViewport().GetVisibleRect().Size;
        bacteria.Position = screenSize / 2;
		bacteria.SetGene(new DefaultGene());
        AddChild(bacteria);
	}

	private void SpawnFood()
	{

		try
		{
            Food food = (Food)FoodScene.Instantiate();

            float randomX = (float)GD.RandRange(0, SpawnBounds.X);
            float randomY = (float)GD.RandRange(0, SpawnBounds.Y);
            food.Position = new Vector2(randomX, randomY);
            AddChild(food);
        }
		catch(Exception ex)
		{
			GD.PrintErr(ex.Message);
		}

    }

	private void SpawnFoodAtPosition(Vector2 position)
	{
		Food food = (Food)FoodScene.Instantiate();
		food.Position = position;
		AddChild(food);
	}

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
		if (@event.IsActionPressed("ui_restart"))
		{
			RestartSimulation();
		}

		if (@event is InputEventMouseButton mouseEvent && mouseEvent.ButtonIndex == MouseButton.Left && mouseEvent.Pressed)
		{
			SpawnFoodAtPosition(mouseEvent.Position);
		}

		if(@event.IsActionPressed("ui_cancel") || (@event is InputEventKey keyEvent && keyEvent.Pressed && keyEvent.Keycode == Key.Escape))
		{
			GetTree().Quit();
		}
    }

	public void RestartSimulation()
	{
		foreach(Node child in GetChildren())
		{
			if(child is Bacteria || child is Food)
			{
				child.QueueFree();
			}
		}

		SpawnInitBacteria();
        Engine.TimeScale = 1;
        _foodTimer.Start();

		GD.Print("simulation was restarted");
	}

	private void UpdateStats()
	{
		_bacteriaCount = 0;
		foreach (Node child in GetChildren())
		{
			if(child is Bacteria)
			{
				_bacteriaCount++;
			}
		}

		_foodCount = 0;
		foreach(Node child in GetChildren())
		{
			if (child is Food)
			{
				_foodCount++;
			}
		}

		GD.Print($"[Stats] Bacterias: {_bacteriaCount} | Food: {_foodCount}");
	}
	
	public override void _Process(double delta)
	{
		_spawnTimer += (float)delta;

		if(_spawnTimer >= FoodSpawnDelay)
		{
			_spawnTimer = 0;
			SpawnFood();
		}
    }

    public string GetPopulationStats()
    {
        int total = 0;
        int defaultCount = 0, speedCount = 0, hunterCount = 0, hungerCount = 0;
        int peaceCount = 0, scientistCount = 0, armorCount = 0, vampireCount = 0;

        foreach (Node child in GetChildren())
        {
            if (child is Bacteria bacteria)
            {
                total++;
                string geneName = bacteria.CurrentGene.GetType().Name;
                switch (geneName)
                {
                    case "DefaultGene": defaultCount++; break;
                    case "SpeedGene": speedCount++; break;
                    case "HunterGene": hunterCount++; break;
                    case "HungerGene": hungerCount++; break;
                    case "PeaceGene": peaceCount++; break;
                    case "SciencetistGene": scientistCount++; break;
                    case "ArmorGene": armorCount++; break;
                    case "VampireGene": vampireCount++; break;
                }
            }
        }

        return $"[color=yellow]Всего: {total}[/color]\n" +
               $"gray: {defaultCount}\n" +
               $"blue: {speedCount}\n" +
               $"red: {hunterCount}\n" +
               $"green: {hungerCount}\n" +
               $"white: {peaceCount}\n" +
               $"lightblue: {scientistCount}\n" +
               $"orange: {armorCount}\n" +
               $"purple: {vampireCount}";
    }
}
