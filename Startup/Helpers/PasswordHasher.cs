using System.Security.Cryptography;

namespace Startup.Helpers;

public static class PasswordHasher
{
    private const int SALT_SIZE = 16;
    private const int KEY_SIZE = 32;
    private const int ITERATIONS = 10000;
    private static readonly HashAlgorithmName _hashAlgorithmName = HashAlgorithmName.SHA256;
    private const char SALT_DELIMETER = ';';

    public static string Hash(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(SALT_SIZE);
        var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, ITERATIONS, _hashAlgorithmName, KEY_SIZE);
        return string.Join(SALT_DELIMETER, Convert.ToBase64String(salt), Convert.ToBase64String(hash));
    }

    public static bool Validate(string passwordHash, string password)
    {
        var passwordElements = passwordHash.Split(SALT_DELIMETER);
        var salt = Convert.FromBase64String(passwordElements[0]);
        var hash = Convert.FromBase64String(passwordElements[1]);
        var hashInput = Rfc2898DeriveBytes.Pbkdf2(password, salt, ITERATIONS, _hashAlgorithmName, KEY_SIZE);
        return CryptographicOperations.FixedTimeEquals(hash, hashInput);
    }
}