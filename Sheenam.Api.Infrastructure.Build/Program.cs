using ADotNet.Clients;
using ADotNet.Models.Pipelines.GithubPipelines;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks.SetupDotNetTaskV1s;

// Define the pipeline
var githubPipeline = new GithubPipeline
{
    Name = "Sheenam Build Pipeline",

    OnEvents = new Events
    {
        PullRequest = new PullRequestEvent
        {
            Branches = new[] { "main" }
        },
        Push = new PushEvent
        {
            Branches = new[] { "main" }
        }
    },

    Jobs = new Dictionary<string, Job>
    {
        ["build"] = new Job
        {
            RunsOn = BuildMachines.Windows2022,

            Steps = new List<GithubTask>
            {
                new CheckoutTaskV2
                {
                    Name = "Checkout Code"
                },

                new SetupDotNetTaskV1
                {
                    Name = "Setting Up .NET",
                    TargetDotNetVersion = new TargetDotNetVersion
                    {
                        DotNetVersion = "6.0.300"
                    }
                },

                new RestoreTask
                {
                    Name = "Restore NuGet Packages"
                },

                new DotNetBuildTask
                {
                    Name = "Build Project"
                },

                new TestTask
                {
                    Name = "Run Tests"
                }
            }
        }
    }
};


var client = new ADotNetClient();

client.SerializeAndWriteToFile(
    adoPipeline: githubPipeline,
    path: "../../../../.github/workflows/dornet.yml");
