# Set working directory for Prig.
$env:URASANDESU_PRIG_CURRENT_DIRECTORY = (Resolve-Path .\ProfilersChainTest\bin\Debug).Path

# Set the target process name that Prig should attach.
$env:URASANDESU_PRIG_TARGET_PROCESS_NAME = "nunit-agent"

# Run tests with OpenCover.
& (Resolve-Path .\packages\OpenCover.*\OpenCover.Console.exe).Path -target:runtests.bat -filter:+[ProfilersChain]* -register:user

# Format the result by ReportGenerator.
& (Resolve-Path .\packages\ReportGenerator.*\tools\ReportGenerator.exe).Path -reports:results.xml -targetdir:coverage
