using Godot;

public partial class Player : CharacterBody2D
{
	[Signal]
	public delegate void HitEventHandler();

	private void onBodyEntered(Node2D body)
	{
		EmitSignal(SignalName.Hit);
		// Must be deferred as we can't change physics properties on a physics callback.
		GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
	}

	[Export]
	public int Speed { get; set; } = 400; // How fast the player will move (pixels/sec).

	public Vector2 ScreenSize; // Size of the game window.

	public override void _Ready()
	{
		ScreenSize = GetViewportRect().Size;
		//SetProcess(true);
		//SetPhysicsProcess(true);
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Vector2.Zero;
		// GD.Print("Physics running");

		if (Input.IsActionPressed("moveRight")) velocity.X += 1;
		if (Input.IsActionPressed("moveLeft")) velocity.X -= 1;
		if (Input.IsActionPressed("moveDown")) velocity.Y += 1;
		if (Input.IsActionPressed("moveUp")) velocity.Y -= 1;

		Vector2 direction = velocity;

		var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

		if (direction.Length() > 0)
		{
			direction = direction.Normalized();
			Velocity = direction * Speed;
			animatedSprite2D.Play();
		}
		else
		{
			Velocity = Vector2.Zero;
			animatedSprite2D.Stop();
		}

		Velocity = direction * Speed;
		MoveAndSlide();

		// Flip sprite
		if (Velocity.X != 0)
		{
			animatedSprite2D.Animation = "walk";
			animatedSprite2D.FlipV = false;
			animatedSprite2D.FlipH = Velocity.X < 0;
		}
	}
	public void Start(Vector2 position)
	{
		Position = position;
		Show();
		GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
	}
}
