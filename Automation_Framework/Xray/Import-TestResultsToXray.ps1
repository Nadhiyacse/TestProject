param(
      [Parameter(Mandatory=$true)]
      [string]$XrayClientSecret, 

      [Parameter(Mandatory=$true)]
      [string]$XmlResultFile
)

$baseXrayUri = '#{BaseXrayUri}'
$fileDirectory = '#{XrayFileDirectory}'
$jsonInfoFile = '#{XrayJsonInfoFile}'
$clientId = '#{XrayClientId}'

$fileEncoding = [System.Text.Encoding]::GetEncoding("iso-8859-1")
$boundary = [System.Guid]::NewGuid().ToString()

# Enforce TLS 1.2
[System.Net.ServicePointManager]::SecurityProtocol = [System.Net.SecurityProtocolType]::Tls12

# Get Authorization Token
$body = "{ `"client_id`": `"$clientId`",`"client_secret`": `"$XrayClientSecret`" } "
$xrayToken = Invoke-RestMethod "$baseXrayUri/authenticate" -Method 'POST' -ContentType 'application/json' -Body $body

# Compose the Body
# Equivalent to '-F info=@jsonInfoFile -F results=@XmlResultFile' in cURL
$xmlResultFileBin = [IO.File]::ReadAllBytes("$fileDirectory\$XmlResultFile")
$xmlFileEnc = $fileEncoding.GetString($xmlResultFileBin)

$jsonInfoFileBin = [IO.File]::ReadAllBytes("$fileDirectory\$jsonInfoFile")
$jsonFileEnc = $fileEncoding.GetString($jsonInfoFileBin)

$boundary = [System.Guid]::NewGuid().ToString()
$body = (
    "--$boundary",
    "Content-Disposition: form-data; name=`"info`"; filename=`"$jsonInfoFile`"`r`n",
    $jsonFileEnc,
    "--$boundary",
    "Content-Disposition: form-data; name=`"results`"; filename=`"$XmlResultFile`"`r`n",
    $xmlFileEnc,
    "--$boundary--`r`n"
    ) -join "`r`n"

# Import test results to Xray
$headers = @{ "Authorization" = "Bearer $xrayToken" }
$response = Invoke-RestMethod "$baseXrayUri/import/execution/nunit/multipart" -Method 'POST' -ContentType "multipart/form-data; boundary=$boundary" -Headers $headers -Body $body
$response | ConvertTo-Json