using GXPEngine;

class MyGame : Game {

	public MyGame() : base(800, 600, false, false) 
	{
		
	}

	public static void Main (string[] args)
	{
		new MyGame().Start();
	}
}