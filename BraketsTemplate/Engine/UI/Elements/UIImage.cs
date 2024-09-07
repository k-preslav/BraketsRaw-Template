namespace BraketsEngine;

public class UIImage : UIElement
{
    public UIImage(string imageName="ui/ui_default") : base(text: "", textureName: imageName)
    {

    }

    public async void SetImage(string name)
    {
        base.textureName = name;
        base.texture = await TextureManager.GetTexture(textureName);
    }
}