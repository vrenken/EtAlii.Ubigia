cd "C:\Git\Github\Vrenken\EtAlii.Ubigia"

#dotnet build `
#    .\Source\EtAlii.Ubigia.sln `
#    --configuration:'Release-Ubuntu' 

dotnet publish `
    .\Source\EtAlii.Ubigia.sln `
    --configuration:'Release-Ubuntu' 
#    --no-build `
#    --self-contained `
#    --runtime linux-x64

docker build `
    --tag ubigia/storage:preview `
    --file ./Source/Infrastructure/Hosting/Hosts/EtAlii.Ubigia.Infrastructure.Hosting.DockerHost/Dockerfile `
    .

dotnet dev-certs https -ep c:\temp\ubigia\system\certificate.pfx -p MY_SILLY_PASSWORD
dotnet dev-certs https --trust

docker stop ubigia_test_storage | Out-Null -ErrorAction SilentlyContinue
docker rm ubigia_test_storage | Out-Null -ErrorAction SilentlyContinue
docker create `
    --name ubigia_test_storage `
    --env SERILOG_SERVER_URL='http://seq.avalon:5341' `
    --env UBIGIA_CERTIFICATE_FILE='/ubigia/system/certificate.pfx' `
    --env UBIGIA_CERTIFICATE_PASSWORD='MY_SILLY_PASSWORD' `
    --volume c:\temp\ubigia\data:/ubigia/data `
    --volume c:\temp\ubigia\system:/ubigia/system `
    --publish 64000:64000 `
    --publish 64001:64001 `
    --publish 64002:64002 `
    ubigia/storage:preview 
docker start ubigia_test_storage

# /ubigia/data
# /ubigia/system
