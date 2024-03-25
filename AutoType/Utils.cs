using System.Collections.Generic;

namespace AutoType
{
    internal static class Utils
    {
        internal static IEnumerable<string> GetKeyStrokes(this string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                string result;
                switch (c)
                {
                    case '+':
                    case '^':
                    case '%':
                    case '~':
                    case '(':
                    case ')':
                    case '{':
                    case '}':
                        result = $"{{{c}}}";
                        break;

                    case '\t':
                        result = "{TAB}";
                        break;

                    case '\n':
                        result = "{ENTER}";
                        break;

                    case '\r':
                        int nextCharIndex = i + 1;
                        if (nextCharIndex != text.Length && text[nextCharIndex] == '\n')
                        {
                            // \r\n counts as 1 keystroke, so skip the next char.
                            i++;
                        }

                        result = "{ENTER}";
                        break;

                    default:
                        result = c.ToString();
                        break;
                }

                yield return result;
            }
        }
    }
}