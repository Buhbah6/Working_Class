using Godot;
using System;
using System.Collections.Generic;

public partial class Quest : Node
{
	[Signal]
	public delegate void CompletedQuestEventHandler(Quest quest);
	
	private TextureRect questBanner;
	private Label questLabel;
	private TextureRect taskImage;
	private Label managerLabel;
	
	
	private Godot.Collections.Array<String> completionText = [
		"Wow! Good Job!",
		"Tasks Completed!",
		"Amazing!!!",
		"hands meat..."
	];
	
	private Godot.Collections.Array<String> managerText = [
		"Keep working...",
		"mmHuhm huhmm...",
		"I am not happy here",
		"This is not how the working",
		"Im watchings",
		"Hands meats...",
		"Fuck you...",
		"Bithc!",
		"#Fuck My Life.",
		"I hate you...",
		"You're mom! <3"
	];
	
	[Export]
	public Godot.Collections.Array<TaskItem> Tasks { get; set; }
	
	public int TaskCount { get; set; }
	public int CompletedTasks { get; set; }
	
	public override void _Ready()
	{
		foreach(TaskItem task in Tasks){
			task.CompletedTask += OnTaskCompleted;
			TaskCount ++;
		}
		CompletedTasks = 0;
	
		questBanner = GetNode<TextureRect>("%QuestBanner");
		questBanner.Visible = false;
		
		taskImage = GetNode<TextureRect>("%ThumbsUp");
		taskImage.Visible = false;
		
		managerLabel = GetNode<Label>("%ManagerQuote");
		
		questLabel = GetNode<Label>("%Text");
		questLabel.Text = completionText.PickRandom();
	}
		
	private void OnTaskCompleted(){
		GD.Print("Task Completed");
		CompletedTasks++;
		managerLabel.Text = managerText.PickRandom();
		taskImage.Visible = true;
		if(CompletedTasks >= TaskCount){
			GD.Print("Quest Complete");
			questBanner.Visible = true;
			taskImage.Visible = false;
			EmitSignal(SignalName.CompletedQuest, this);
		}
	}	
}
