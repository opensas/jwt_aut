set -o verbose

curl -I -s -X GET http://localhost:4000/info \
  -H "Accept: application/json" \
  -H "Authorization: Bearer XXXX"
