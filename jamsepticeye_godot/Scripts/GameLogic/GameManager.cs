using Godot;
using System;
using System.Threading.Tasks;

public partial class GameManager : Node
{
	[Export] public Quest quest;
	[Export] public String nextScene;
	
	public async Task WaitBeforeRedirect(String redirectLink)
	{
		// Wait for 1 second
		await ToSignal(GetTree().CreateTimer(1.0f), SceneTreeTimer.SignalName.Timeout);
		GD.Print("Loading New Scene");
		GetTree().ChangeSceneToFile(redirectLink);
	}
	
	public override void _Ready(){
		quest.CompletedQuest += OnQuestCompleted;
		quest.FailQuest += OnQuestFail;
		
	}
	
	public void OnQuestCompleted(){
		GD.Print("Quest Completed");
		WaitBeforeRedirect(nextScene);
	}
	
	public void OnQuestFail(){
		GD.Print("Quest Failed");
		WaitBeforeRedirect("res://Scenes/credits.tscn");
	}
}
