#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class AppleTangle
    {
        private static byte[] data = System.Convert.FromBase64String("XUFGW0BdUBg/GT0vKnwtKjokaFmB9VcLHOMM/PAm/0L9iw0KON6IhZ4ylLprDTsD7iY0n2S1d0rhYqk+gopYu256fOiGBmia0dLKWeTPimUPGQ0vKnwtIjo0aFlZRUwJakxbXZgZccVzLRulQZqmNPdMWtZOd0yVl91assf7TSbiUGYd8YsX0FHWQuEHaY/ebmRWIXcZNi8qfDQKLTEZP0tFTAlaXUhHTUhbTQldTFtEWglILCkqqygmKRmrKCMrqygoKc24gCBHTQlKRkdNQF1ARkdaCUZPCVxaTFlFTAlqTFtdQE9ASkhdQEZHCWhc6UoaXt4TLgV/wvMmCCfzk1owZpxsVzZlQnm/aKDtXUsiOaporhqjqCECLygsLC4rKD83QV1dWVoTBgZeplqoSe8yciAGu5vRbWHZSRG3PNxNHAo8YjxwNJq93t+1t+Z5k+hxeVMZqyhfGScvKnw0Jigo1i0tKisocI4sIFU+aX84N136nqIKEm6K/EZZRUwJe0ZGXQlqaBk3PiQZHxkdG1tISl1ASkwJWl1IXUxETEddWgcZJC8gA69hr94kKCgsLCkqqygoKXUaH3MZSxgiGSAvKnwtLzorfHoYOk6mIZ0J3uKFBQlGWZ8WKBmlnmrmIXcZqyg4Lyp8NAktqyghGasoLRkUD04JoxpD3iSr5vfCigbQekNyTUBPQEpIXUBGRwloXF1BRltAXVAYXl4HSFlZRUwHSkZEBkhZWUVMSkgvKnw0Jy0/LT0C+UBuvV8g191CpLy3UyWNbqJy/T8eGuLtJmTnPUD4DcvC+J5Z9iZsyA7j2ERRxM6cPj42uPI3bnnCLMR3UK0Ewh+LfmV8xT8ZPS8qfC0qOiRoWVlFTAl7RkZdLxkmLyp8NDooKNYtLBkqKCjWGTRWaIGx0PjjT7UNQjj5ipLNMgPqNia0FNoCYAEz4dfnnJAn8Hc1/+IUA69hr94kKCgsLCkZSxgiGSAvKnw2rKqsMrAUbh7bgLJppwX9mLk78XtMRUBIR0pMCUZHCV1BQFoJSkxbqT0C+UBuvV8g191CpAdpj95uZFYFCUpMW11AT0BKSF1MCVlGRUBKUKIwoPfQYkXcLoILGSvBMRfReSD6CUZPCV1BTAldQUxHCUhZWUVASkgJamgZqygLGSQvIAOvYa/eJCgoKJwThN0mJym7IpgIPwdd/BUk8ks/CUhHTQlKTFtdQE9ASkhdQEZHCVkuxVQQqqJ6CfoR7ZiWs2YjQtYC1R+wZQRRnsSlsvXaXrLbX/teGWboLS86K3x6GDoZOC8qfC0jOiNoWVnwH1bornzwjrCQG2vS8fxYt1eIe+AwW9x0J/xWdrLbDCqTfKZkdCTYGastkhmrKoqJKisoKysoKxkkLyAcGxgdGRofcz4kGhwZGxkQGxgdGQYZqOovIQIvKCwsLisrGaifM6iaGTgvKnwtIzojaFlZRUwJYEdKBxhg8V+2Gj1MiF694AQrKigpKIqrKFAJSFpaXERMWglISkpMWV1IR0pMXUBPQEpIXUwJS1AJSEdQCVlIW11FTAlgR0oHGA8ZDS8qfC0iOjRoWasoKS8gA69hr95KTSwoGajbGQMveYOj/PPN1fkgLh6ZXFwI");
        private static int[] order = new int[] { 5,54,59,24,10,26,50,31,42,14,43,43,57,25,35,55,20,28,52,50,46,47,42,47,33,56,43,36,30,52,53,55,57,52,53,42,54,49,38,57,44,46,46,56,47,45,51,48,59,58,50,56,55,58,56,56,59,57,59,59,60 };
        private static int key = 41;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
