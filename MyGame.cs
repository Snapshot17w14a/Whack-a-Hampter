using GXPEngine;
using GXPEngine.SceneManager;

class MyGame : Game {

	public MyGame() : base(960, 640, false, false, 1280, 720,true) 
	{
		Initialize();
		SceneManager.LoadScene(GameData.SceneToLoad);
	}

	public static void Main (string[] args)
	{
		new MyGame().Start();
	}

	private void Initialize()
	{
		SceneManager.AddScene(GameData.SceneToLoad, SceneManager.CreateScene(GameData.SceneToLoad));
	}
}