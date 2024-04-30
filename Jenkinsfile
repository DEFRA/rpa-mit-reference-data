@Library('defra-library@v-9') _

def validateClosure = {
  stage('nuget.config Transform') {
    writeFile file: './RPA.MIT.ReferenceData.Api/nuget.config', text: """<?xml version='1.0' encoding='utf-8'?>
        <configuration>
        <packageSources>
            <clear />
            <add key='nuget' value='https://api.nuget.org/v3/index.json' />
            <add key='DEFRA' value='${MIT_PACKAGE_FEED_URL}' />
        </packageSources>
        <packageSourceCredentials>
            <DEFRA>
            <add key='Username' value='${MIT_PACKAGE_FEED_USERNAME}' />
            <add key='ClearTextPassword' value='${MIT_PACKAGE_FEED_PAT}' />
            </DEFRA>
        </packageSourceCredentials>
        </configuration>""", encoding: "UTF-8"

    writeFile file: './RPA.MIT.ReferenceData.Api.Test/nuget.config', text: """<?xml version='1.0' encoding='utf-8'?>
        <configuration>
        <packageSources>
            <clear />
            <add key='nuget' value='https://api.nuget.org/v3/index.json' />
            <add key='DEFRA' value='${MIT_PACKAGE_FEED_URL}' />
        </packageSources>
        <packageSourceCredentials>
            <DEFRA>
            <add key='Username' value='${MIT_PACKAGE_FEED_USERNAME}' />
            <add key='ClearTextPassword' value='${MIT_PACKAGE_FEED_PAT}' />
            </DEFRA>
        </packageSourceCredentials>
        </configuration>""", encoding: "UTF-8"

    writeFile file: './.env', text: """
        PACKAGE_FEED_URL=${MIT_PACKAGE_FEED_URL}
        PACKAGE_FEED_USERNAME=${MIT_PACKAGE_FEED_USERNAME}
        PACKAGE_FEED_PAT=${MIT_PACKAGE_FEED_PAT}
    """
  }
}

buildDotNetCore project: 'RPA.MIT.ReferenceData.Api', validateClosure: validateClosure