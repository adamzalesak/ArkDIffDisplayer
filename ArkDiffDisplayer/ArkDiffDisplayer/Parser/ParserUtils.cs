namespace ArkDiffDisplayer.Parser
{
    public class ParserUtils
    {
        public static string LoadFieldFromLine(string line, ParserOptions option)
        {
            // All lines are of the same structure. They have 8 fields that need to be filled.
            // Sample line: 03/17/2023,ARKK,"ZOOM VIDEO COMMUNICATIONS-A",ZM,98980L101,"8,769,511","$619,039,781.49",7.95%
            var lineWithoutCommas = line.Split(',');

            switch (option)
            {
                case ParserOptions.Date:
                    return lineWithoutCommas[0];
                case ParserOptions.Name:
                    return lineWithoutCommas[2].Replace("\"","");
                case ParserOptions.Weight:
                    return lineWithoutCommas[lineWithoutCommas.Length - 1];
                case ParserOptions.Shares:
                    var lineWithoutStringSign = line.Split('\"');
                    return lineWithoutStringSign[3];
                default:
                    throw new ArgumentException();
            }
        }
    }
}