using System.Threading.Tasks;

namespace LeastSquares
{
    /// <summary>
    /// A static class that manages access to the OpenAI API.
    /// </summary>
    public static class OpenAIAccessManager
    {
        private static string _apiKey;
        private static Model _imageModel;
        private static OpenAIAPI _api;

        /// <summary>
        /// The maximum number of tokens to use for text completion.
        /// </summary>
        public static int MaxTokens { get; set; } = 500;

        /// <summary>
        /// The temperature to use for text completion.
        /// </summary>
        public static double Temperature { get; set; } = 0.1;

        /// <summary>
        /// The text model to use for text completion.
        /// </summary>
        public static TextModel TextModel { get; set; }

        /// <summary>
        /// The code model to use for text completion.
        /// </summary>
        public static CodeModel CodeModel { get; set; }

        /// <summary>
        /// The chat model to use for chat completion.
        /// </summary>
        public static ChatModel ChatModel { get; set; }

        /// <summary>
        /// Calculates the number of tokens to use for a given prompt.
        /// </summary>
        /// <param name="prompt">The prompt to calculate the number of tokens for.</param>
        /// <returns>The number of tokens to use for the prompt.</returns>
        private static int CalculateTokensForPrompt(string prompt)
        {
            return (int)((prompt.Length / 4f) * 1.2f);
        }

        /// <summary>
        /// Requests text completion for a given prompt.
        /// </summary>
        /// <param name="prompt">The prompt to request text completion for.</param>
        /// <returns>The completed text.</returns>
        public static async Task<string> RequestStringCompletion(string prompt)
        {
            var actualTokens = MaxTokens - CalculateTokensForPrompt(prompt);
            return await _api.RequestTextCompletion(prompt, Model.FromTextModel(TextModel), actualTokens, Temperature);
        }

        /// <summary>
        /// Requests chat completion for a given set of messages.
        /// </summary>
        /// <param name="messages">The messages to request chat completion for.</param>
        /// <returns>The completed chat message.</returns>
        public static async Task<string> RequestChatCompletion(ChatCompletionMessage[] messages)
        {
            Model chat_mode = new Model("gpt-3.5-turbo");            

            return await _api.RequestChatCompletion(messages, chat_mode, Temperature);
        }

        /// <summary>
        /// Sets the API key to use for accessing the OpenAI API.
        /// </summary>
        /// <param name="ApiKey">The API key to use.</param>
        public static void SetAPIKey(string ApiKey)
        {
            _apiKey = ApiKey;
            _api = new OpenAIAPI(_apiKey);
        }
    }
}