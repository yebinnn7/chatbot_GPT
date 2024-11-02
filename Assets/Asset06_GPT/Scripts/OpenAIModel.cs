using System;

namespace LeastSquares
{
    /// <summary>
    /// Enumerates the available text models.
    /// </summary>
    public enum TextModel
    {
        DaVinci,
        Ada,
        Babbage,
        Curie,
    }

    /// <summary>
    /// Enumerates the available code models.
    /// </summary>
    public enum CodeModel
    {
        DaVinci
    }

    /// <summary>
    /// Enumerates the available chat models.
    /// </summary>
    public enum ChatModel
    {
        ChatGPT
    }
    
    /// <summary>
    /// Enumerates the available audio models.
    /// </summary>
    public enum AudioModel
    {
        Whisper
    }

    /// <summary>
    /// Represents a model used for AI processing.
    /// </summary>
    public class Model
    {
        /// <summary>
        /// The name of the model.
        /// </summary>
        public string ModelName { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Model"/> class.
        /// </summary>
        /// <param name="model">The name of the model.</param>
        public Model(string model)
        {
            ModelName = model;
        }

        /// <summary>
        /// Creates a new <see cref="Model"/> instance from the specified <see cref="TextModel"/>.
        /// </summary>
        /// <param name="model">The <see cref="TextModel"/> to create the model from.</param>
        /// <returns>A new <see cref="Model"/> instance.</returns>
        public static Model FromTextModel(TextModel model)
        {
            return model switch
            {
                TextModel.DaVinci => new Model("davinci"),
                TextModel.Ada => new Model("ada"),
                TextModel.Babbage => new Model("babbage"),
                TextModel.Curie => new Model("curie"),
                _ => throw new ArgumentOutOfRangeException(nameof(model), model, null)
            };
        }
        
        /// <summary>
        /// Creates a new <see cref="Model"/> instance from the specified <see cref="CodeModel"/>.
        /// </summary>
        /// <param name="model">The <see cref="CodeModel"/> to create the model from.</param>
        /// <returns>A new <see cref="Model"/> instance.</returns>
        public static Model FromCodeModel(CodeModel model)
        {
            return model switch
            {
                CodeModel.DaVinci => new Model("code-davinci-003"),
                _ => throw new ArgumentOutOfRangeException(nameof(model), model, null)
            };
        }

        /// <summary>
        /// Creates a new <see cref="Model"/> instance from the specified <see cref="ChatModel"/>.
        /// </summary>
        /// <param name="model">The <see cref="ChatModel"/> to create the model from.</param>
        /// <returns>A new <see cref="Model"/> instance.</returns>
        public static Model FromChatModel(ChatModel model)
        {
            return model switch
            {
                ChatModel.ChatGPT => new Model("gpt-3.5-turbo"),
                _ => throw new ArgumentOutOfRangeException(nameof(model), model, null)
            };
        }
        
        /// <summary>
        /// Creates a new <see cref="Model"/> instance from the specified <see cref="AudioModel"/>.
        /// </summary>
        /// <param name="model">The <see cref="AudioModel"/> to create the model from.</param>
        /// <returns>A new <see cref="Model"/> instance.</returns>
        public static Model FromAudioModel(AudioModel model)
        {
            return model switch
            {
                AudioModel.Whisper => new Model("whisper-1"),
                _ => throw new ArgumentOutOfRangeException(nameof(model), model, null)
            };
        }
    }
}