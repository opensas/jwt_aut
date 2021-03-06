# Proyecto de ejemplo de autenticación con jwt

## Objetivo

El presente proyecto tiene por objetivo servir como referencia para autenticarse con una API rest utilizando JWT. A su vez se utilizará para efectuar pruebas de conectividad.

## Instalación

- Instalar netcore 2.2.203 ([descargar aquí](https://dotnet.microsoft.com/download/dotnet-core/2.2))

- Descargar el repositorio

```shell
$ git clone git@gitlab.com:sgte-it/trabajo/fiscalizacion/jwt_auth.git
```

- Descargar las dependencias y ejecutar el proyecto

```shell
$ cd jwt_auth/src

$ dotnet restore
  Restore completed in 764.58 ms for /home/sas/devel/apps/sgte-it/trabajo/fiscalizacion/tmp/jwt_auth/src/src.csproj.

$ dotnet run --urls=http://localhost:4000/
: Microsoft.AspNetCore.DataProtection.KeyManagement.XmlKeyManager[0]
      User profile is available. Using '/xxxxxxx/.aspnet/DataProtection-Keys' as key repository; keys will not be encrypted at rest.
Hosting environment: Development
Content root path: /xxxxxxxxxx/jwt_auth/src
Now listening on: http://localhost:4000
Application started. Press Ctrl+C to shut down.
```

## Instalación desde docker

- Descargar el repositorio

```shell
$ git clone git@gitlab.com:sgte-it/trabajo/fiscalizacion/jwt_auth.git
```

- Crear la imagen y ejecutar un container

```shell
$ cd jwt_auth/src

$ docker build -t jwt_auth-image .
Sending build context to Docker daemon  2.445MB
Step 1/10 : FROM mcr.microsoft.com/dotnet/core/sdk:2.2-alpine AS build
 ---> 857924d04240
[...]
Successfully built 5ef516833260
Successfully tagged jwt_auth-image:latest

$ docker run -it --rm -p 4000:80 jwt_auth-image jwt_auth-container
warn: Microsoft.AspNetCore.DataProtection.KeyManagement.XmlKeyManager[35]
      No XML encryptor configured. Key {8ce6a9e1-ada4-43e0-8ac3-9cff47f024f6} may be persisted to storage in unencrypted form.
Hosting environment: Production
Content root path: /app
Now listening on: http://0.0.0.0:4000
Application started. Press Ctrl+C to shut down.
```

## Ejemplos de uso

### Obtención de un JWT (Json Web Token)

Usuario: "produccion-api-username"

Password = "produccion-api-test-Lj1KzZii28"

Para obtener un JWT hay que efectuar un POST al endpoint /apiRoot/users/authenticate, de la siguiente manera

```shell
$ curl -X POST http://localhost:4000/users/authenticate \
  -H "Content-Type: application/json" \
  -H "Accept: application/json" \
  -d '{"Username": "produccion-api-username","Password": "produccion-api-test-Lj1KzZii28"}'

{
  "id":2,
  "firstName":"Produccion",
  "lastName":"API",
  "username":"produccion-api-username",
  "password":null,"token":"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjIiLCJuYmYiOjE1NTY3ODkwNzEsImV4cCI6MTU1NzM5Mzg3MSwiaWF0IjoxNTU2Nzg5MDcxfQ.2vk-C83XRgI0CwNPXR1_zjsOTzk3OCVt2j_esdGxNqw"
}
```

Sintaxis de curl para windows

```shell
curl -X POST http://localhost:4000/users/authenticate ^
  -H "Content-Type: application/json" ^
  -H "Accept: application/json" ^
  -d "{\"Username\": \"produccion-api-username\",\"Password\": \"produccion-api-test-Lj1KzZii28\"}"

{
  "id":2,
  "firstName":"Produccion",
  "lastName":"API",
  "username":"produccion-api-username",
  "password":null,"token":"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjIiLCJuYmYiOjE1NTY3ODkwNzEsImV4cCI6MTU1NzM5Mzg3MSwiaWF0IjoxNTU2Nzg5MDcxfQ.2vk-C83XRgI0CwNPXR1_zjsOTzk3OCVt2j_esdGxNqw"
}
```

### Acceder a un endpoint protegido utilizando el JWT

Utilizando el token obtenido en el ejemplo anterior, pasarlo en un header de tipo Authorization

```shell
$ curl -X GET http://localhost:4000/info \
>   -H "Accept: application/json" \
>   -H "Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjIiLCJuYmYiOjE1NTY3ODkwNzEsImV4cCI6MTU1NzM5Mzg3MSwiaWF0IjoxNTU2Nzg5MDcxfQ.2vk-C83XRgI0CwNPXR1_zjsOTzk3OCVt2j_esdGxNqw"

{
  "http":{
    "is_https":false,
    "method":"GET",
    "path":"/info",
    "path_base":"",
    "protocol":"HTTP/1.1",
    "scheme":"http",
    },
  },
  "ip":{
    "remote_ip":"127.0.0.1",
    "remote_port":54846,
    "local_ip":"127.0.0.1",
    "local_port":4000
  },
  "platform":{
    "c#":"v4.0.30319",
    "runtime":".NETCoreApp,Version=v2.2",
    "netcore":"2.2.4",
    "framework":".NET Core 2.2.4 (Framework 4.6.27521.02)",
    "framework_version":"4.6.27521.02"
  },
  "os":{
    "description":"Linux 4.15.0-1036-oem #41-Ubuntu SMP Sun Apr 7 05:24:01 UTC 2019",
    "architecture":"X64",
    "process_architecture":"X64",
    "server":""
  }
}
```

Sintaxis de curl para windows

```shell
curl -X GET http://localhost:4000/info ^
  -H "Accept: application/json" ^
  -H "Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjIiLCJuYmYiOjE1NTY3ODkwNzEsImV4cCI6MTU1NzM5Mzg3MSwiaWF0IjoxNTU2Nzg5MDcxfQ.2vk-C83XRgI0CwNPXR1_zjsOTzk3OCVt2j_esdGxNqw"
```

Consultar scripts en `docs/scripts` y otros ejemplos de uso en `docs/examples.rest`

### Ejemplo completo

Obtenemos un token y lo guardamos en la variable de entorno TOKEN

Nota: usamos [jq](https://stedolan.github.io/jq/download/) para parsear y formatear el json y extraer el token

```shell
$ TOKEN=$( \
  curl -s \
  -X POST http://localhost:4000/users/authenticate \
  -H "Content-Type: application/json" \
  -H "Accept: application/json" \
  -d '{"Username": "produccion-api-username","Password": "produccion-api-test-Lj1KzZii28"}' | \
  jq -r '.token' \
)

$ echo $TOKEN
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjIiLCJuYmYiOjE1NTczNzg2NTgsImV4cCI6MTU1Nzk4MzQ1OCwiaWF0IjoxNTU3Mzc4NjU4fQ.i_AqaSJL41mw5aeWqneXrv0UgsvxQVKTb1WdwvZIDEc
```

```shell
$ curl -s -X GET http://localhost:4000/users \
  -H "Accept: application/json" \
  -H "Authorization: Bearer ${TOKEN}" | \
  jq .

[
  {
    "id": 1,
    "firstName": "Test",
    "lastName": "User",
    "username": "test",
    "password": null,
    "token": null
  },
  {
    "id": 2,
    "firstName": "Produccion",
    "lastName": "API",
    "username": "produccion-api-username",
    "password": null,
    "token": null
  }
]
```

### Probando el servicio desde openshift

A fin de realizar las pruebas, desplegamos un web service de prueba en Openshift en `
http://jwt-auth-jwt-auth.apps.us-east-2.online-starter.openshift.com`. (La instancia fue creada el 11 de junio, debido a la política de openshift la instancia expirará el 10/08/2019). La misma puede ser utilizada de la siguiente manera:

```shell
OPENSHIFT_URL="http://jwt-auth-jwt-auth.apps.us-east-2.online-starter.openshift.com"

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

{
  "http": {
    "host": {
      "value": "jwt-auth-jwt-auth.apps.us-east-2.online-starter.openshift.com",
      "hasValue": true,
      "host": "jwt-auth-jwt-auth.apps.us-east-2.online-starter.openshift.com",
      "port": null
    },
    "cookies": [],
[...]
  "ip": {
    "remote_ip": "::ffff:10.130.12.13",
    "remote_port": 57356,
    "local_ip": "::ffff:10.128.5.25",
    "local_port": 8080
  },
  "platform": {
    "c#": "v4.0.30319",
    "runtime": ".NETCoreApp,Version=v2.2",
    "netcore": "2.2.5",
    "framework": ".NET Core 2.2.5 (Framework 4.6.27617.05)",
    "framework_version": "4.6.27617.05"
  },
  "os": {
    "description": "Linux 4.18.0-80.1.2.el8_0.x86_64 #1 SMP Sun Apr 28 09:21:22 UTC 2019",
    "architecture": "X64",
    "process_architecture": "X64",
    "server": "jwt-auth-1-n8b8h"
  }
}
```