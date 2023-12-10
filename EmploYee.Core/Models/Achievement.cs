namespace EmploYee.Core.Models;

public class Achievement(string title, string image, double currentValue) : BaseEntity
{
    public string Title { get; private set; } = title;
    public string Image { get; private set; } = image;
    public double CurrentValue { get; private set; } = currentValue;

    public void Update(string title, string image, double currentValue)
    {
        Title = title;
        Image = image;
        CurrentValue = currentValue;
    }
}