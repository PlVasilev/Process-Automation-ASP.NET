$count = 0
do {
    $count++
    Write-Output "[$env:STAGE_NAME] Starting container [Attempt: $count]"
    
    $testStart = Invoke-WebRequest -Uri http://35.226.39.6:80 -UseBasicParsing
    
    if ($testStart.statuscode -eq '200') {
        $started = $true
    } else {
        Start-Sleep -Seconds 10
    }
    
} until ($started -or ($count -eq 3))

if (!$started) {
    exit 1
}

$count = 0
do {
    $count++
    Write-Output "[$env:STAGE_NAME] Starting container [Attempt: $count]"
    
    $testStart = Invoke-WebRequest -Uri http://34.123.241.8:5003/index.html -UseBasicParsing
    
    if ($testStart.statuscode -eq '200') {
        $started = $true
    } else {
        Start-Sleep -Seconds 10
    }
    
} until ($started -or ($count -eq 3))

if (!$started) {
    exit 1
}

$count = 0
do {
    $count++
    Write-Output "[$env:STAGE_NAME] Starting container [Attempt: $count]"
    
    $testStart = Invoke-WebRequest -Uri http://35.193.222.65:5009/index.html -UseBasicParsing
    
    if ($testStart.statuscode -eq '200') {
        $started = $true
    } else {
        Start-Sleep -Seconds 10
    }
    
} until ($started -or ($count -eq 3))

if (!$started) {
    exit 1
}

$count = 0
do {
    $count++
    Write-Output "[$env:STAGE_NAME] Starting container [Attempt: $count]"
    
    $testStart = Invoke-WebRequest -Uri http://34.67.159.110:5007/index.html -UseBasicParsing
    
    if ($testStart.statuscode -eq '200') {
        $started = $true
    } else {
        Start-Sleep -Seconds 10
    }
    
} until ($started -or ($count -eq 3))

if (!$started) {
    exit 1
}
