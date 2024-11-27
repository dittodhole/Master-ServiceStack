..\..\Resources\curl.exe -X POST http://localhost:61163/tickets -d @ticket.json --header "Content-Type: application/json" -o ticket.json
..\..\Resources\curl.exe -X GET http://localhost:61163/tickets --header "Content-Type: application/json"
..\..\Resources\curl.exe -X PUT http://localhost:61163/tickets/1 -d @ticket.json --header "Content-Type: application/json" -o ticket.json
..\..\Resources\curl.exe -X GET http://localhost:61163/tickets --header "Content-Type: application/json"
..\..\Resources\curl.exe -X POST http://localhost:61163/tickets/1/comments -d @comment.json --header "Content-Type: application/json" -o comment.json
..\..\Resources\curl.exe -X GET http://localhost:61163/tickets/1/comments --header "Content-Type: application/json"
..\..\Resources\curl.exe -X PUT http://localhost:61163/tickets/1/comments/1 -d @comment.json --header "Content-Type: application/json" -o comment.json
..\..\Resources\curl.exe -X GET http://localhost:61163/tickets/1/comments --header "Content-Type: application/json"