using System.Text;

namespace PhoneKeyPad;

public static class OldPhoneTyper
{
    //This type is chosen to get a character in constant time
    //An alternative - 2-dimensional array - would require a cast char => int, and it's less readable
    private static readonly Dictionary<char, char[]> Dictionary = new()
    {
        {'2', ['a', 'b', 'c'] },
        {'3', ['d', 'e', 'f'] },
        {'4', ['g', 'h', 'i'] },
        {'5', ['j', 'k', 'l'] },
        {'6', ['m', 'n', 'o'] },
        {'7', ['p', 'q', 'r', 's'] },
        {'8', ['t', 'u', 'v'] },
        {'9', ['w', 'x', 'y', 'z'] }
    };
    
    private static char? _lastChar;
    private static int _lastCharCounter;
    
    public static string OldPhonePad(string input)
    {
        var result = new StringBuilder();

        for (var i = 0; i < input.Length; i++)
        {
            switch (input[i])
            {
                case '*':
                    if (!_lastChar.HasValue && result.Length > 0)//handles "34 *", when the char in process has been added to result
                        result.Length--;//removes the last char in constant time
                    Reset();//handles "34*", when the char is in process and is not yet added to result
                    break;
                case '#' when i < input.Length - 1://this symbol is allowed only at the last position
                    throw new ArgumentException("Input contains # before the end of the input string.");
                case '#':
                {
                    AppendLastChar(result);
                    Reset();//this is a static class, so it needs to be fresh for the next usage
                    return result.ToString();
                }
                case ' ':
                case '1':
                {
                    AppendLastChar(result);
                    Reset();
                    break;
                }
                default:
                {
                    ProcessChar(input[i], result);
                    break;
                }
            }
        }
        
        throw new ArgumentException("Input does not contain # in the end.");
    }

    private static void ProcessChar(char newChar, StringBuilder stringBuilder)
    {
        if(!Dictionary.ContainsKey(newChar))
            throw new ArgumentException("Input contains an invalid symbol.");

        if (!_lastChar.HasValue)
        {
            _lastChar = newChar;
            _lastCharCounter = 1;
            return;
        }

        if (newChar != _lastChar.Value)
        {
            AppendLastChar(stringBuilder);
            _lastChar = newChar;
            _lastCharCounter = 1;
        }
        else
            _lastCharCounter++;
    }
    
    private static void AppendLastChar(StringBuilder stringBuilder)
    {
        if (_lastChar.HasValue)
            stringBuilder.Append(GetChar());
    }

    private static char GetChar()
    {
        //the counter needs to be % by the length of an array to be able to cycle through the letters
        //handles "33333#"
        return Dictionary[_lastChar!.Value][(_lastCharCounter - 1) % Dictionary[_lastChar.Value].Length];
    }

    private static void Reset()
    {
        _lastChar = null;
        _lastCharCounter = 0;
    }
}