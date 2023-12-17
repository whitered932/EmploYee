namespace EmploYee.Core.Models;

public class Session : BaseEntity
{
    private Session()
    {
    }

    public Session(DateTime expiresAtUtc, long userId)
    {
        ExpiresAtUtc = expiresAtUtc;
        UserId = userId;
    }

    public void UpdateExpiredAt(DateTime dateTimeUtc)
    {
        ExpiresAtUtc = dateTimeUtc;
    }

    public bool IsExpired(DateTime utcNow)
    {
        return utcNow > ExpiresAtUtc;
    }

    public long UserId { get; private set; }
    public DateTime ExpiresAtUtc { get; private set; }
}