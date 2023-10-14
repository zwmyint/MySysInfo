using Spectre.Console;
using Spectre.Console.Json;
using System.Text.Json.Serialization;
using System.Text.Json;

// Do not use @ for the string since it will cause \n become \\n after serialize to JSON
var prompt = "using System;\n\nnamespace HelloWorld\n{\n    class Program\n    {\n        //Ask the user for their name and say 'Hello'\n\n";
// Second example
//var prompt = "using System;\n\nnamespace Fibonacci\n{\n    class Program\n    {\n        //Write a fibonacci method and call it from main\n\n";

// Replace your API key here
var apiKey = "YOUR_OPENAI_API_KEY";

var url = "https://api.openai.com/v1/completions";
var client = new HttpClient();
client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

var requestBody = new CompletionRequestBody
{
    model = OpenAIModel.CodeCushman1, // or you can try CodeDavinci2 
    prompt = prompt, // The code and comment you send over to generate code to you 
    max_tokens = 1024, // Max 1024 char for input + output.
    temperature = 0,
    best_of = 1,
    echo = true,
    frequency_penalty = 0,
    //logprobs = 0,
    presence_penalty = 0,
    top_p = 1
};
var json = JsonSerializer.Serialize(requestBody);
Console.WriteLine("Request Body:");
AnsiConsole.Write(new JsonText(json));
Console.WriteLine();

var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
var response = await client.PostAsync(url, content);
var responseJson = await response.Content.ReadAsStringAsync();
var res = JsonSerializer.Deserialize<CodexResult>(responseJson);
if (res?.id != null)
{
    Console.WriteLine("Response Body:");
    AnsiConsole.Write(new JsonText(responseJson));
    Console.WriteLine();
    //if (res?.choices?[0].text != null)
    //{

    //}
    var result = res?.choices?[0].text?.Replace("\\\\n", Environment.NewLine).Replace("\\n", Environment.NewLine).Replace("\\t", Environment.NewLine).Replace("\\\"", "\"");
    Console.WriteLine("Code:");
    Console.WriteLine(result);
}
else
{
    Console.WriteLine("Error:");
    AnsiConsole.Write(new JsonText(responseJson));
    Console.WriteLine();
}


// Code below mainly helps us easier to call and get the result from OpenAI API with the API description from OpenAI documentation, so we don't need always go to check the doc.
// You can call the OpenAI API without the code below if you prefer pure Json string only.
public class CodexResult
{
    public string? id { get; set; }
    [JsonPropertyName("object")]
    public string? objectResult { get; set; }

    public int created { get; set; }
    public string? model { get; set; }
    public List<Choice>? choices { get; set; }
    public Usage? usage { get; set; }
}

public class Choice
{
    public string? text { get; set; }
    public int index { get; set; }
    public LogProbs? logprobs { get; set; }
    public string? finishReason { get; set; }
}

public class LogProbs
{
    public List<string>? tokens { get; set; }
    public List<double?>? token_logprobs { get; set; }
    public List<object>? top_logprobs { get; set; }
    public List<long>? text_offset { get; set; }
}

public class Usage
{
    public int prompt_token { get; set; }
    public int completion_tokens { get; set; }
    public int total_tokens { get; set; }
}


/// <summary>
/// Creates a completion for the provided prompt and parameters
/// POST https://api.openai.com/v1/completions
/// https://platform.openai.com/docs/api-reference/completions/create
/// </summary>
public class CompletionRequestBody
{
    /// <summary>
    /// ID of the model to use. You can use the List models API to see all of your available models, or see our Model overview for descriptions of them.
    /// Required
    /// </summary>
    public string? model { get; set; }
    /// <summary>
    /// The prompt(s) to generate completions for, encoded as a string, array of strings, array of tokens, or array of token arrays.
    /// Note that<|endoftext|> is the document separator that the model sees during training, so if a prompt is not specified the model will generate as if from the beginning of a new document.
    /// Optional Defaults to <|endoftext|>
    /// </summary>
    public string prompt { get; set; } = "<|endoftext|>";
    /// <summary>
    /// The suffix that comes after a completion of inserted text.
    /// Optional Defaults to null
    /// </summary>
    public string? suffix { get; set; } = null;
    /// <summary>
    /// The maximum number of tokens to generate in the completion.
    /// The token count of your prompt plus max_tokens cannot exceed the model's context length. Most models have a context length of 2048 tokens (except for the newest models, which support 4096).
    /// Optional Defaults to 16
    /// </summary>
    public int max_tokens { get; set; } = 16;
    /// <summary>
    /// What sampling temperature to use, between 0 and 2. Higher values like 0.8 will make the output more random, while lower values like 0.2 will make it more focused and deterministic.
    /// We generally recommend altering this or top_p but not both.
    /// Optional Defaults to 1
    /// </summary>
    public double temperature { get; set; } = 1;
    /// <summary>
    /// An alternative to sampling with temperature, called nucleus sampling, where the model considers the results of the tokens with top_p probability mass. So 0.1 means only the tokens comprising the top 10% probability mass are considered.
    /// We generally recommend altering this or temperature but not both.
    /// Optional Defaults to 1
    /// </summary>
    public double top_p { get; set; } = 1;
    /// <summary>
    /// How many completions to generate for each prompt.
    /// Note: Because this parameter generates many completions, it can quickly consume your token quota.Use carefully and ensure that you have reasonable settings for max_tokens and stop.
    /// Optional Defaults to 1
    /// </summary>
    public int n { get; set; } = 1;
    /// <summary>
    /// Whether to stream back partial progress. If set, tokens will be sent as data-only server-sent events as they become available, with the stream terminated by a data: [DONE] message.
    /// Optional Defaults to false
    /// </summary>
    public bool stream { get; set; }
    /// <summary>
    /// Include the log probabilities on the logprobs most likely tokens, as well the chosen tokens. For example, if logprobs is 5, the API will return a list of the 5 most likely tokens. The API will always return the logprob of the sampled token, so there may be up to logprobs+1 elements in the response.
    /// The maximum value for logprobs is 5. If you need more than this, please contact us through our Help center and describe your use case.
    /// Optional Defaults to null
    /// </summary>
    public int? logprobs { get; set; } = null;
    /// <summary>
    /// Echo back the prompt in addition to the completion
    /// Optional Defaults to false
    /// </summary>
    public bool echo { get; set; }
    /// <summary>
    /// Up to 4 sequences where the API will stop generating further tokens. The returned text will not contain the stop sequence.
    /// Optional Defaults to null
    /// </summary>
    public string? stop { get; set; } = null;
    /// <summary>
    /// Number between -2.0 and 2.0. Positive values penalize new tokens based on whether they appear in the text so far, increasing the model's likelihood to talk about new topics.
    /// Optional Defaults to 0
    /// </summary>
    public double presence_penalty { get; set; }
    /// <summary>
    /// Number between -2.0 and 2.0. Positive values penalize new tokens based on their existing frequency in the text so far, decreasing the model's likelihood to repeat the same line verbatim.
    /// Optional Defaults to 0
    /// </summary>
    public double frequency_penalty { get; set; }
    /// <summary>
    /// Generates best_of completions server-side and returns the "best" (the one with the highest log probability per token). Results cannot be streamed.
    /// When used with n, best_of controls the number of candidate completions and n specifies how many to return – best_of must be greater than n.
    /// Note: Because this parameter generates many completions, it can quickly consume your token quota.Use carefully and ensure that you have reasonable settings for max_tokens and stop.
    /// Optional Defaults to 1
    /// </summary>
    public int best_of { get; set; } = 1;
    /// <summary>
    /// Modify the likelihood of specified tokens appearing in the completion.
    /// Accepts a json object that maps tokens(specified by their token ID in the GPT tokenizer) to an associated bias value from -100 to 100. You can use this tokenizer tool (which works for both GPT-2 and GPT-3) to convert text to token IDs. Mathematically, the bias is added to the logits generated by the model prior to sampling. The exact effect will vary per model, but values between -1 and 1 should decrease or increase likelihood of selection; values like -100 or 100 should result in a ban or exclusive selection of the relevant token.
    /// As an example, you can pass { "50256": -100} to prevent the <|endoftext|> token from being generated.
    /// Optional Defaults to null
    /// </summary>
    public Dictionary<string, int> logit_bias { get; set; } = new Dictionary<string, int>();
    /// <summary>
    /// A unique identifier representing your end-user, which can help OpenAI to monitor and detect abuse.
    /// Optional
    /// </summary>
    public string user { get; set; } = "";
}

public static class OpenAIModel
{
    /// <summary>
    /// Most capable Codex model. Particularly good at translating natural language to code. In addition to completing code, also supports inserting completions within code.
    /// Max 8,001 tokens
    /// </summary>
    public const string CodeDavinci2 = "code-davinci-002";
    /// <summary>
    /// Almost as capable as Davinci Codex, but slightly faster. This speed advantage may make it preferable for real-time applications.
    /// Up to 2,048 tokens
    /// </summary>
    public const string CodeCushman1 = "code-cushman-001";
    /// <summary>
    /// Can do any language task with better quality, longer output, and consistent instruction-following than the curie, babbage, or ada models. Also supports inserting completions within text.
    /// Max 4,097 tokens
    /// </summary>
    public const string TextDavinci3 = "text-davinci-003";
    /// <summary>
    /// Similar capabilities to text-davinci-003 but trained with supervised fine-tuning instead of reinforcement learning  
    /// Max 4,097 tokens
    /// </summary>
    public const string TextDavinci2 = "text-davinci-002";
    /// <summary>
    /// Very capable, faster and lower cost than Davinci.
    /// Max 2,049 tokens
    /// </summary>
    public const string TextCurie1 = "text-curie-001";
    /// <summary>
    /// Capable of straightforward tasks, very fast, and lower cost.
    /// Max 2,049 tokens
    /// </summary>
    public const string TextBabbage1 = "text-babbage-001";
    /// <summary>
    /// Capable of very simple tasks, usually the fastest model in the GPT-3 series, and lowest cost.
    /// Max 2,049 tokens
    /// </summary>
    public const string TextAda1 = "text-ada-001";
}









