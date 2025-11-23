using Godot;
using System;

public partial class Main : Node
{
	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("pause"))
			TogglePause();
	}

	private void TogglePause()
	{
		bool paused = !GetTree().Paused;
		GetTree().Paused = paused;

		GetNode<CanvasLayer>("PauseMenu").Visible = paused;
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var resumeButton = GetNode<Button>("PauseMenu/Menu/Container/Resume");
		resumeButton.Pressed += OnResumePressed;

		GetNode<CanvasLayer>("PauseMenu").Hide();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnResumePressed()
	{
		GetTree().Paused = false;
		GetNode<CanvasLayer>("PauseMenu").Hide();
	}
}
