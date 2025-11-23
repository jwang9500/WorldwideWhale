using Godot;
using System;

public partial class Antivirus : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;

	public override void _Ready()
	{

	}
	
	public override void _PhysicsProcess(double delta)
	{
		// no gravity please
	}
}
