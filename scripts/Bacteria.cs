using Evolution.Genes;
using Godot;
using System;
using System.Runtime.InteropServices;

public partial class Bacteria : RigidBody2D
{
	[Export] public float Energy = 200f;
	[Export] public Gene CurrentGene;
	bool _isDividing = false;
	private Sprite2D _sprite;
	private Area2D _visionArea;
	private Timer _movementTimer;
	private Timer _energyTimer;

	private Vector2 _currentDirection;
	private Food _targetFood;
	private Bacteria _targetEnemy;
	private Bacteria _targetVictim;

	private float _speed = 100f;
	private float _metabolismCost = 0.8f;
    private Area2D _collectArea;

    public override void _Ready()
	{
		_sprite = GetNode<Sprite2D>("Sprite2D");
		_visionArea = GetNode<Area2D>("VisionArea");

		if(CurrentGene == null)
		{
			SetGene(new DefaultGene());
		}
		else
		{
			SetGene(CurrentGene);
		}

		GravityScale = 0f;
		LinearDamp = 0.8f;
        LockRotation = true;

        _visionArea.AreaEntered += OnVisionAreaEntered;
        _visionArea.AreaExited += OnVisionAreaExited;

        _visionArea.BodyEntered += OnVisionBodyEntered;
        _visionArea.BodyExited += OnVisionBodyExited;

        BodyEntered += OnBodyEntered;

        _energyTimer = new Timer();
		_energyTimer.WaitTime = 0.5f;
		_energyTimer.Timeout += () => Energy -= 1 * _metabolismCost;
		AddChild(_energyTimer);
		_energyTimer.Start();

		_movementTimer = new Timer();
		_movementTimer.WaitTime = 0.2f;
		_movementTimer.Timeout += UpdateMovement;
		AddChild(_movementTimer);
        _movementTimer.Start();

        _collectArea = GetNode<Area2D>("CollectArea");
        _collectArea.AreaEntered += OnCollectAreaEntered;

    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Energy <= 0)
		{
			QueueFree();
			return;
		}


	}

    private void OnCollectAreaEntered(Area2D area)
    {
        if (area is Food food)
        {
            AddEnergy(food.EnergyValue);
            food.QueueFree();
        }
    }

    private void UpdateMovement()
	{
		if (CurrentGene is VampireGene)
		{
			if(_targetVictim != null && IsInstanceValid(_targetVictim))
			{
				MoveTo(_targetVictim.Position);
			}
			else
			{
				RandomMove();
			}
		}
		else if (CurrentGene is HunterGene)
		{
			if(_targetFood != null && IsInstanceValid(_targetFood))
			{
				MoveTo(_targetFood.Position);
			}
			else if (_targetEnemy != null && IsInstanceValid(_targetEnemy))
			{
				MoveTo(_targetEnemy.Position);
			}
			else
			{
				RandomMove();
			}
		}
		else
		{
            if (_targetFood != null && IsInstanceValid(_targetFood))
            {
                MoveTo(_targetFood.Position);
            }
            else
            {
                RandomMove();
            }
        }
	}

	private void MoveTo(Vector2 target)
	{
		Vector2 direction = (target - Position).Normalized();
		_currentDirection = direction;
		LinearVelocity = direction * (_speed * CurrentGene.SpeedBonus);

		if (_currentDirection.Length() > 0)
		{
            float angle = _currentDirection.Angle() + Mathf.Pi / 2;
            _sprite.Rotation = angle;

        }
    }

	private void RandomMove()
	{
		if (GD.Randf() < 0.1f)
		{
			_currentDirection = new Vector2((float)GD.RandRange(-1, 1), (float)GD.RandRange(-1, 1)).Normalized();
		}
		LinearVelocity = _currentDirection * (_speed * CurrentGene.SpeedBonus);

        if (_currentDirection.Length() > 0)
        {
            float angle = _currentDirection.Angle() + Mathf.Pi / 2;
            _sprite.Rotation = angle;

        }
    }

	public void AddEnergy(float amount)
	{
		Energy += amount;

		//if (Energy > 200) Energy = 200;

		if (Energy >= CurrentGene.ReproductionCost)
		{
			Divide();
		}
	}

	public void Divide()
	{
        if (Energy < CurrentGene.ReproductionCost) return;

        if (_isDividing) return;
		_isDividing = true;

		Energy -= CurrentGene.ReproductionCost;

		Gene childGene;
		float mutationChance = 0.1f + CurrentGene.MutationChanceBonus;

		if(GD.Randf() < mutationChance)
		{
			childGene = GeneFactory.CreateRandom();
		}
		else
		{
			childGene = CurrentGene;
		}

		var bacteriaScene = GD.Load<PackedScene>("res://scenes/bacteria.tscn");

		Bacteria child = (Bacteria)bacteriaScene.Instantiate();

		child.SetGene(childGene);
        child.Energy = this.Energy / 2;

        Vector2 offset = new Vector2((float)GD.RandRange(-20, 20), (float)GD.RandRange(-20, 20));

		child.Position = this.Position + offset;

        GetParent().CallDeferred("add_child", child);

        var sound = GetNode<AudioStreamPlayer2D>("DivideSound");
		if(sound != null)
		{
			sound.Play();
		}

		_isDividing = false;
	}

	public void SetGene(Gene gene)
	{
		CurrentGene = gene;

		Sprite2D sprite = GetNode<Sprite2D>("Sprite2D");
		sprite.Modulate = CurrentGene.color;
		ApplyGeneStats();
	}

	private void ApplyGeneStats()
	{
		_speed = 100 * CurrentGene.SpeedBonus;
		_metabolismCost = CurrentGene.MetabolismCost;
	}

    private void OnVisionBodyEntered(Node2D body)
    {
        if (body is Bacteria other)
        {

            if (CurrentGene is HunterGene)
                _targetEnemy = other;
            else if (CurrentGene is VampireGene)
                _targetVictim = other;
            else if (CurrentGene is PeaceGene && other.CurrentGene is HunterGene)
                RunAwayFrom(other);
        }
    }
    private void OnVisionAreaEntered(Area2D area)
    {
        if (area is Food food && !(CurrentGene is VampireGene))
        {
            _targetFood = food;
        }
    }

    private void OnVisionAreaExited(Node2D body)
	{
		if (body == _targetFood)
		{
			_targetFood = null;
		}
	}

    private void OnVisionBodyExited(Node2D body)
    {
        if (body == _targetEnemy)
        {
            _targetEnemy = null;
        }
        if (body == _targetVictim)
        {
            _targetVictim = null;
        }
    }

    private void RunAwayFrom(Bacteria predator)
	{
		Vector2 direction = (Position - predator.Position).Normalized();
		LinearVelocity = direction * (_speed * CurrentGene.SpeedBonus * 1.2f);
	}

	private void OnBodyEntered(Node body)
	{
		if (body is Food food && !(CurrentGene is VampireGene))
		{
			AddEnergy(food.EnergyValue);
			food.QueueFree();
		}
		if (body is Bacteria other)
		{
			if (CurrentGene is HunterGene)
			{
				Attack(other);
			}
			else if(CurrentGene is VampireGene)
			{
				StealEnergy(other);
			}
		}
	}

	private void Attack(Bacteria target)
	{
		float damage = CurrentGene.AttackDamage;
		this.Energy = target.Energy / 2;
		target.TakeDamage(damage);
	}

	private void StealEnergy(Bacteria target)
	{
		float stolen = target.Energy * this.CurrentGene.VampireSteal;
		target.Energy -= stolen;
		Energy += stolen;

		if (target.Energy <= 0)
		{
			target.QueueFree();
		}
		if (Energy > 200) Energy = 200;
	}

	public void TakeDamage(float damage)
	{
		float defense = CurrentGene.DefenseBonus;
		float finalDamage = damage * (1f - defense);
		Energy -= finalDamage;

		if (Energy <= 0)
		{
			QueueFree();
		}
	}
}
