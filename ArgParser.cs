namespace CSharpUtils
{
    public static class ArgParser
    {
        public static Map<string, string> Parse(string[] args)
        {
            Map<string, string> options = new HashMap<string, string>();

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i][0] == '-' || args[i][0] == '/') //This start with - or /
                {
                    args[i] = args[i].Substring(1);
                    if (i + 1 >= args.Length || args[i + 1][0] == '-' || args[i + 1][0] == '/') //Next start with - (or last arg)
                    {
                        options.Put(args[i], "null");
                    }
                    else
                    {
                        options.Put(args[i], args[i + 1]);
                        i++;
                    }
                }
            }

            return options;
        }
    }
}
