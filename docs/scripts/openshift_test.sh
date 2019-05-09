set -o verbose

OPENSHIFT_URL="http://jwt-auth-gh-oc-test.7e14.starter-us-west-2.openshiftapps.com"

TOKEN=$( \
  curl -s \
  -X POST ${OPENSHIFT_URL}/users/authenticate \
  -H "Content-Type: application/json" \
  -H "Accept: application/json" \
  -d '{"Username": "produccion-api-username","Password": "produccion-api-test-Lj1KzZii28"}' | \
  jq -r '.token' \
)

curl -s -X GET ${OPENSHIFT_URL}/info \
  -H "Accept: application/json" \
  -H "Authorization: Bearer ${TOKEN}" | \
  jq .