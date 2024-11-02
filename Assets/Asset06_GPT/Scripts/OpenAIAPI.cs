using System;
using System.Threading.Tasks;
using UnityEngine;
using System.Net.Http;
using System.Net.Http.Headers;

namespace LeastSquares
{
    
    /// <summary>
    /// A class that handles requests to the OpenAI API.
    /// </summary>
    public class OpenAIAPI 
    {
        private string _apiKey;
        
        /// <summary>
        /// Constructor for the OpenAIAPI class.
        /// </summary>
        /// <param name="apiKey">The API key for the OpenAI API.</param>
        public OpenAIAPI(string apiKey)
        {
            _apiKey = apiKey;
        }

        /// <summary>
        /// Sends a request for text completion to the OpenAI API.
        /// </summary>
        /// <param name="prompt">The text prompt to be completed.</param>
        /// <param name="model">The model to use for text completion.</param>
        /// <param name="maxTokens">The maximum number of tokens to generate in the completion.</param>
        /// <param name="temperature">The sampling temperature to use for generating the completion.</param>
        /// <returns>The completed text as a string.</returns>
        public async Task<string> RequestTextCompletion(string prompt, Model model, int maxTokens, double temperature)
        {
            var result = await DoRequest<TextCompletionResponse>("https://api.openai.com/v1/completions", HttpMethod.Post, JsonUtility.ToJson(new TextCompletionRequest
            {
                model = model.ModelName,
                prompt = prompt,
                max_tokens = maxTokens,
                temperature = temperature
            }));
            return result.Text;
        }
        
        /// <summary>
        /// Sends a request for chat completion to the OpenAI API.
        /// </summary>
        /// <param name="messages">The messages to use as context for the chat completion.</param>
        /// <param name="model">The model to use for chat completion.</param>
        /// <param name="temperature">The sampling temperature to use for generating the completion.</param>
        /// <returns>The completed chat message as a string.</returns>
        public async Task<string> RequestChatCompletion(ChatCompletionMessage[] messages, Model model, double temperature)
        {
            //@@ max_tokens (optional) - helloapps
            var result = await DoRequest<ChatCompletionResponse>("https://api.openai.com/v1/chat/completions", HttpMethod.Post, JsonUtility.ToJson(new ChatCompletionRequest
            {
                model = model.ModelName,
                messages = messages,
                temperature = temperature,
                max_tokens = 500
            }));

            return result.Text;
        }

        /// <summary>
        /// Sends a request to the OpenAI API.
        /// </summary>
        /// <typeparam name="T">The type of response expected from the API.</typeparam>
        /// <param name="url">The URL to send the request to.</param>
        /// <param name="method">The HTTP method to use for the request.</param>
        /// <param name="payload">The payload to send with the request.</param>
        /// <returns>The response from the API as an object of type T.</returns>
        private async Task<T> DoRequest<T>(string url, HttpMethod method, string payload) where T : class
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.SendAsync(new HttpRequestMessage
            {
                RequestUri = new Uri(url),
                Method = method,
                Content = new StringContent(payload, System.Text.Encoding.UTF8, "application/json")
            });
                
            if(!response.IsSuccessStatusCode)
                Debug.LogError(await response.Content.ReadAsStringAsync());
                
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonUtility.FromJson<T>(responseBody);
        }
    }
}