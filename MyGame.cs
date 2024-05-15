using GXPEngine.SceneManager;
using GXPEngine.Scenes;
using GXPEngine;

class MyGame : Game {

	public MyGame() : base(1920 / 2, 1280 / 2, false, false, 1280, 720, true) 
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
		SceneManager.AddScene("Level1", SceneManager.CreateScene(typeof(TiledTestScene), "Maps/Level1.tmx"));
		SceneManager.AddScene("Level2", SceneManager.CreateScene(typeof(TiledTestScene), "Maps/Level1.tmx"));
		SceneManager.AddScene("Level3", SceneManager.CreateScene(typeof(TiledTestScene), "Maps/Level1.tmx"));
	}
}