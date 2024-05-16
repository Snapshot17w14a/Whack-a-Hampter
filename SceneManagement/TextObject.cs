using System.Drawing;

namespace GXPEngine.SceneManagement
{
    internal class TextObject : EasyDraw
    {
        public new string Text { get; set; }
        private readonly Color _mainTextColor;

        public TextObject(string text, CenterMode horizontal, CenterMode vertical, int width, int height, Font font, Color col) : base(width, height)
        {
            Text = text;
            _mainTextColor = col;
            TextFont(font);
            SetOrigin(width / 2, height / 2);
            TextAlign(horizontal, vertical);
        }

        public void Draw()
        {
            Clear(Color.Transparent);
            Fill(Color.Black);
            Text(Text, width / 2 + 2, height / 2 + 2);
            Fill(_mainTextColor);
            Text(Text, width / 2, height / 2);
        }
    }
}
