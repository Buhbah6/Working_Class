using Godot;
using System;
using System.Collections.Generic;

public partial class Register : Node2D
{
	[Export] private Label costLabel;
	[Export] private Label chargeLabel;
	[Export] private Panel costSection;
	[Export] private Panel chargeSection;
	[Export] private Panel buttonSection;
	[Export] public AudioStreamPlayer sfx;
	[Export] public Quest quest;

	[Export] private Node2D[] npcs = new Node2D[0];

	private int currentNpcIndex = 0;
	private string enteredValue = "";
	private float currentCost = 0f;

	public override void _Ready()
	{
		GetNode<AudioStreamPlayer>("/root/BgMusic").Stop();
		// Hide all NPCs except the first one
		for (int i = 0; i < npcs.Length; i++){
			npcs[i].Visible = (i == 0);
			quest.AddTask(new TaskItem());
		}

		// Connect all keypad buttons
		foreach (Node child in buttonSection.GetChildren())
		{
			if (child is Button button)
				button.Pressed += () => OnButtonPressed(button.Name);
		}

		StartRound();
	}

	private void StartRound()
	{
		// Zoom in / animate NPC
		AnimateNpc(npcs[currentNpcIndex]);

		// Generate random cost between 0 and 99.99
		var rng = new Random();
		currentCost = (float)(rng.NextDouble() * 99.99);
		costLabel.Text = $"${currentCost:F2}";

		// Reset entered value
		enteredValue = "";
		chargeLabel.Text = "$0.00";
	}

	private void OnButtonPressed(string buttonName)
	{
		sfx.Play();
		switch (buttonName)
		{
			case "ButDel":
				if (enteredValue.Length > 0)
					enteredValue = enteredValue[..^1];
				break;

			case "ButEnter":
				CheckTotal();
				return;

			default:
				if (buttonName.StartsWith("But"))
				{
					string digit = buttonName.Replace("But", "");
					if (float.TryParse(digit, out _))
						enteredValue += digit;
				}
				break;
		}

		// Format as a currency number
		if (float.TryParse(enteredValue, out float num))
			chargeLabel.Text = $"${num / 100f:F2}";
		else
			chargeLabel.Text = "$0.00";
	}

	private void CheckTotal()
	{
		float entered = 0f;
		float.TryParse(chargeLabel.Text.Trim('$'), out entered);

		if (Mathf.Abs(entered - currentCost) < 0.01f)
		{
			GD.Print("Correct!");
			quest.Tasks[quest.CompletedTasks].Complete();
			npcs[currentNpcIndex].Visible = false;

			currentNpcIndex++;
			if (currentNpcIndex < npcs.Length)
				StartRound();
			else
				GameOver();
		}
		else
		{
			GD.Print("Wrong amount!");
		}
	}

	private void GameOver()
	{
		costLabel.Text = "All customers served!";
		chargeLabel.Text = "";
	}

	private async void AnimateNpc(Node2D npc)
	{
		npc.Visible = true;

		var tween = GetTree().CreateTween();
		Vector2 startScale = new Vector2(0.8f, 0.8f);
		npc.Scale = startScale;

		tween.TweenProperty(npc, "scale", new Vector2(1f, 1f), 0.8f)
			.SetTrans(Tween.TransitionType.Sine)
			.SetEase(Tween.EaseType.Out);

		await ToSignal(tween, Tween.SignalName.Finished);
	}
}
