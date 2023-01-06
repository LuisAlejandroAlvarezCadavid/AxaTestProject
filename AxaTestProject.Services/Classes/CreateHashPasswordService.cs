using AxaTestProject.Repositories.DataEntities;
using AxaTestProject.Repositories.Interfaces;
using AxaTestProject.Shared.Models.Login;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;

namespace AxaTestProject.Services.Classes
{
    /*public class CreateHashPassword : ICreateHashPassword
    {
        public async Task<string> GetHashPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);
            string hashed = await Task.Run(() => Convert.ToBase64String(KeyDerivation.Pbkdf2(password: password!, salt: salt, prf: KeyDerivationPrf.HMACSHA256, iterationCount: 100000,
                                                    numBytesRequested: 256 / 8)));

            return hashed;
        }
    }*/


    public class CreateHashPasswordService<TUser> : IPasswordHasher<TUser> where TUser : class
    {

        private readonly RandomNumberGenerator _rng;    

        public CreateHashPasswordService(ILoginRepository loginRepository)
        {
            _rng = RandomNumberGenerator.Create();            
        }

        public string HashPassword(TUser user, string password)
        {
            return GetHashPassword(password, _rng);
        }

        private string GetHashPassword(string password, RandomNumberGenerator rng)
        {
            const KeyDerivationPrf Pbkdf2Prf = KeyDerivationPrf.HMACSHA1;
            const int Pbkdf2IterCount = 1000;
            const int Pbkdf2SubkeyLength = 256 / 8;
            const int SaltSize = 128 / 8;

            byte[] salt = new byte[SaltSize];
            _rng.GetBytes(salt);
            byte[] subkey = KeyDerivation.Pbkdf2(password, salt, Pbkdf2Prf, Pbkdf2IterCount, Pbkdf2SubkeyLength);

            var outputBytes = new byte[1 + SaltSize + Pbkdf2SubkeyLength];
            outputBytes[0] = 0x00; // format marker
            Buffer.BlockCopy(salt, 0, outputBytes, 1, SaltSize);
            Buffer.BlockCopy(subkey, 0, outputBytes, 1 + SaltSize, Pbkdf2SubkeyLength);
            return Convert.ToBase64String(outputBytes);
        }        

        public PasswordVerificationResult VerifyHashedPassword(TUser user, string hashedPassword, string providedPassword)
        {
            byte[] decodedHashedPassword = Convert.FromBase64String(hashedPassword);

            const KeyDerivationPrf Pbkdf2Prf = KeyDerivationPrf.HMACSHA1;
            const int Pbkdf2IterCount = 1000;
            const int Pbkdf2SubkeyLength = 256 / 8;
            const int SaltSize = 128 / 8;


            if (decodedHashedPassword.Length != 1 + SaltSize + Pbkdf2SubkeyLength)
            {
                return PasswordVerificationResult.Failed;
            }

            byte[] salt = new byte[SaltSize];
            Buffer.BlockCopy(decodedHashedPassword, 1, salt, 0, salt.Length);

            byte[] expectedSubkey = new byte[Pbkdf2SubkeyLength];
            Buffer.BlockCopy(decodedHashedPassword, 1 + salt.Length, expectedSubkey, 0, expectedSubkey.Length);

            // Hash the incoming password and verify it
            byte[] actualSubkey = KeyDerivation.Pbkdf2(providedPassword, salt, Pbkdf2Prf, Pbkdf2IterCount, Pbkdf2SubkeyLength);

            if (CryptographicOperations.FixedTimeEquals(actualSubkey, expectedSubkey))
                return PasswordVerificationResult.Success;
            else
                return PasswordVerificationResult.Failed;

        }
        
    }
}
