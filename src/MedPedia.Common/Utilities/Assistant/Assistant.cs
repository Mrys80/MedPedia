namespace MedPedia.Common.Utilities.Assistant;

public class Assistant
{
    public static string GenerateRandomString(int length)
    {
        var random = new Random();
        const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
    
    public static string EnglishNumbers(string input)
    {
        var lettersDictionary = new Dictionary<char, char>
        {
            ['۰'] = '0',
            ['۱'] = '1',
            ['۲'] = '2',
            ['۳'] = '3',
            ['۴'] = '4',
            ['۵'] = '5',
            ['۶'] = '6',
            ['۷'] = '7',
            ['۸'] = '8',
            ['۹'] = '9'
        };

        return lettersDictionary.Aggregate(input, (current, item)
            => current.Replace(item.Key, lettersDictionary[item.Key]));
    }
}