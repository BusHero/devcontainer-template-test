using Nuke.Common;
using Nuke.Common.IO;

sealed partial class Build
{
	[Parameter("Feature to build")] private readonly string Feature;
	private AbsolutePath Features => RootDirectory / "features";

	private Target TestFeature => _ => _
		.Requires(() => Feature)
		.DependsOn(InstallDevcontainer)
		.Executes(() =>
		{
			return Devcontainer($"features test --features {Feature} --project-folder {Features} -i alpine");
		});

	private Target PublishFeatures => _ => _
		.DependsOn(InstallDevcontainer)
		.Executes(() =>
		{
			return Devcontainer($"features publish {Features / "src"} --namespace {GitHubNamespace}/features");
		});
}
