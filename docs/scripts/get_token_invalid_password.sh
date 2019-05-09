set -o verbose

curl -s \
  -X POST http://localhost:4000/users/authenticate \
  -H "Content-Type: application/json" \
  -H "Accept: application/json" \
  -d '{"Username": "produccion-api-username","Password": "!!!produccion-api-test-Lj1KzZii28"}' | \
  jq .