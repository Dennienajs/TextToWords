namespace Words.Api;

public static class ApiEndpoints
{
    private const string ApiBase = "api";

    public static class Text
    {
        private const string Base = ApiBase + "/text/";

        public const string Post = Base;
        public const string DeleteAllWords = Base;
    }
}
