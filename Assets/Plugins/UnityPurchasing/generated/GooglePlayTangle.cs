#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("4YLgRIjAQkppARBOxjb+QmfacmFjuJjmAMIZoHWw9oxvjb+QrBi9c7rHYWYn2mPkePrwqya37pd3BXDFFPAzt+vjZ1H0lMLCHVRu1RKFl6/PTEJNfc9MR0/PTExNwxJ2Qcgn633PTG99QEtEZ8sFy7pATExMSE1OiMGyl8l5tqQlxShtrY57GxIQeoHl0hiUAaUsxZhxbf4K4ehFUWlRI4vtZ979Jg4xdRjEMV8svvG1Fu8F6zDr9K8HLpQvD4/zqkQyJXnEMDUL6iv8pLh1TFTChpyRZ/x6OVqh5BrEnDm0/yT5Isy/Jjrhd9SzXFbbrKLePbY37yHGlHOUFwKoV6XItCwXtdl53F2NqWPoDQWyhb/77PxR5ruz+1kJ32L/yk9OTE1M");
        private static int[] order = new int[] { 4,3,9,13,13,13,9,12,8,12,13,13,13,13,14 };
        private static int key = 77;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
