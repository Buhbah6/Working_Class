using Godot;
using System;
using System.Collections.Generic;

public partial class Quest : Node
{
	[Signal]
	public delegate void CompletedQuestEventHandler(Quest quest);
	
	[Signal]
	public delegate void FailQuestEventHandler(Quest quest);
	
	private TextureRect questBanner;
	private Label questLabel;
	private TextureRect taskImage;
	private Label managerLabel;
	private TextureProgressBar timer;
	
	private bool questComplete = false;
	
	private TextureRect you;
	
	[Export]
	public float TimeToComplete;
	private float initTime;
	
	[Export]
	public Godot.Collections.Array<Texture2D> managerTexture;
	
	[Export]
	public Godot.Collections.Array<String> completionText = [
		"Wow! Good Job!",
		"Tasks Completed!",
		"Amazing!!!",
		"hands meat...",
		"The work is done",
		"Lol you shit..."
	];
	
	[Export]
	public Godot.Collections.Array<String> managerText = [
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
	
	public void AddTask(TaskItem task){
		Tasks.Add(task);
		task.CompletedTask += OnTaskCompleted;
		TaskCount ++;
	}
	
	public override void _Ready()
	{
		foreach(TaskItem task in Tasks){
			task.CompletedTask += OnTaskCompleted;
			TaskCount ++;
		}
		CompletedTasks = 0;
		initTime = TimeToComplete;
		
		questBanner = GetNode<TextureRect>("%QuestBanner");
		questBanner.Visible = false;
		
		taskImage = GetNode<TextureRect>("%ThumbsUp");
		you = GetNode<TextureRect>("%you");
		
		taskImage.Visible = false;
		
		managerLabel = GetNode<Label>("%ManagerQuote");
		
		timer = GetNode<TextureProgressBar>("%QuestTimer");
		timer.MaxValue = initTime;
		
		questLabel = GetNode<Label>("%Text");
		questLabel.Text = completionText.PickRandom();
	}
		
	public override void _Process(double delta){
		base._Process(delta);
		if(!questComplete){
			TimeToComplete -= (float)delta;
			timer.Value = initTime - TimeToComplete;
			if (TimeToComplete <= 0 ) {
				QuestFailed();
			}
		}
	}
		
	private void QuestFailed(){
		GD.Print("Quest Complete");
		questBanner.Visible = true;
		taskImage.Visible = false;
		questLabel.Text = "BAD JOB";
		you.Texture = GD.Load<Texture2D>("res://Assets/Materials/youbad.png");
		EmitSignal(SignalName.FailQuest, this);
	}
		
	private void OnTaskCompleted(){
		GD.Print("Task Completed");
		CompletedTasks++;
		managerLabel.Text = managerText.PickRandom();
		taskImage.Texture = managerTexture.PickRandom();
		taskImage.Visible = true;
		if(CompletedTasks >= TaskCount){
			GD.Print("Quest Complete");
			questBanner.Visible = true;
			taskImage.Visible = false;
			questComplete = true;
			you.Texture = GD.Load<Texture2D>("res://Assets/Materials/you2.png");
			EmitSignal(SignalName.CompletedQuest, this);
		}
	}	
}
