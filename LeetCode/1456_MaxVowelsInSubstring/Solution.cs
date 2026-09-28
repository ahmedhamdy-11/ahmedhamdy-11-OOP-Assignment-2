public class Solution
{
    public int MaxVowels(string s, int k)
    {
        int vowelCount = 0;

        for (int i = 0; i < k; i++)
        {
            if (IsVowel(s[i]))
            {
                vowelCount++;
            }
        }

        int maxVowels = vowelCount;

        for (int i = k; i < s.Length; i++)
        {
            if (IsVowel(s[i]))
            {
                vowelCount++;
            }

            if (IsVowel(s[i - k]))
            {
                vowelCount--;
            }

            if (vowelCount > maxVowels)
            {
                maxVowels = vowelCount;
            }
        }

        return maxVowels;
    }

    private static bool IsVowel(char c)
    {
        return c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u';
    }
}
