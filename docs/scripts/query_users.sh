set -o verbose

TOKEN=$( \
  curl -s \
  -X POST http://localhost:4000/users/authenticate \
  -H "Content-Type: application/json" \
  -H "Accept: application/json" \
  -d '{"Username": "produccion-api-username","Password": "produccion-api-test-Lj1KzZii28"}' | \
  jq -r '.token' \
)

curl -s -X GET http://localhost:4000/users \
  -H "Accept: application/json" \
  -H "Authorization: Bearer ${TOKEN}" | \
  jq .