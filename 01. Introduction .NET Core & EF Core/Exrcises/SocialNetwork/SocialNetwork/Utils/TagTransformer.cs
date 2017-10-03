namespace SocialNetwork.Models
{
    public static class TagTransformer
    {
        public static string Transformer(string input)
        {
            if (input.IndexOf(" ") > -1)
            {
                input = input.Replace(" ", string.Empty);
            }

            if (input[0] != '#') input = input.Insert(0, "#");

            if (input.Length > 20) input = input.Remove(20);

            return input;
        }
    }
}
