using UnityEngine;

public interface ITool
{
    public Sprite GetImage();
    public void Main();

    public void UpMain();

    public void Secondary();
    public void UpSecondary();

    public void Passive();

    public void OnEquip();
    public void OnUnequip();
}
