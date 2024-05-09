using GXPEngine;
using GXPEngine.Scenes;
using GXPEngine.SceneManager;

class MyGame : Game {

	public MyGame() : base(800, 600, false, false, pPixelArt: true) 
	{
		Initialize();
		SceneManager.LoadScene("Test");
	}

	public static void Main (string[] args)
	{
		new MyGame().Start();
	}

	private void Initialize()
	{
		SceneManager.AddScene("GameScene", new GameScene());
		SceneManager.AddScene("Test", new TiledTestScene());
	}
}