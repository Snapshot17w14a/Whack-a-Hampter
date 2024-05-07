using GXPEngine;
using GXPEngine.Scenes;
using GXPEngine.SceneManager;

class MyGame : Game {

	public MyGame() : base(800, 600, false, false) 
	{
		Initialize();
		SceneManager.LoadScene("GameScene");
	}

	public static void Main (string[] args)
	{
		new MyGame().Start();
	}

	private void Initialize()
	{
		SceneManager.AddScene("GameScene", new GameScene());
	}
}