using GXPEngine.SceneManagement;
using GXPEngine.Scenes;
using GXPEngine;

class MyGame : Game {

	private Sprite _mouse;

	public MyGame() : base(960, 640, false, false, 1280, 720,true) 
	{
		GameData.Initialize();
		AddChild(_mouse = new Sprite("mouse.png"));
		_mouse.SetOrigin(_mouse.width / 2, _mouse.height / 2);
		ShowMouse(GameData.ShowMouse);
		SceneManager.LoadScene(GameData.SceneToLoad);
		GameData.SoundHandler.PlaySound("BGM", 2);
    }

	public static void Main (string[] args)
	{
		new MyGame().Start();
    }

	private void Update()
	{
		SetChildIndex(_mouse, GetChildCount() - 1);
		_mouse.SetXY(Input.mouseX, Input.mouseY);
	}
}