..\..\Resources\curl.exe -X GET http://localhost:5555/authenticate/basic --user johndoe:password --header "Content-Type: application/json" -c cookies.txt
..\..\Resources\curl.exe -X GET http://localhost:5555/hello/John --header "Content-Type: application/json" -b cookies.txt
..\..\Resources\curl.exe -X GET http://localhost:5555/requestlogs --header "Content-Type: application/json" -b cookies.txt